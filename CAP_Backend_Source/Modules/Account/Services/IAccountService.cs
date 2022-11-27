using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Entities;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Services.User.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CAP_Backend_Source.Modules.Account.Services
{
    public interface IAccountService
    {
        Task<Models.Account> CreateAsync(CreateAccountRequest request);

        Task<Models.Account?> UpdateAsync(int id, UpdateAccountRequest request);

        Task<List<Models.Account>> GetAsync();

        Task<string> LoginAsync(LoginRequest request);
    }

    public class AccountService : IAccountService
    {
        private MyDbContext _myDbContext;

        public AccountService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Models.Account> CreateAsync(CreateAccountRequest request)
        {
            if (!await IsValidRole(request.RoleId))
            {
                throw new BadRequestException("RoleId Not Found");
            }

            var acc = new Models.Account()
            {
                Email = request.Email!,
                RoleId = request.RoleId,
            };

            await _myDbContext.Accounts.AddAsync(acc);
            await _myDbContext.SaveChangesAsync();
            return acc;
        }

        public async Task<List<Models.Account>> GetAsync()
        {
            return await _myDbContext.Accounts.ToListAsync();
        }

        public async Task<Models.Account?> UpdateAsync(int id, UpdateAccountRequest request)
        {
            if (!await IsValidRole(request.RoleId))
            {
                throw new BadRequestException("RoleId Not Found");
            }

            var acc = await _myDbContext.Accounts.Where(x => x.AccountId == id).FirstOrDefaultAsync();

            if (acc == null)
            {
                throw new BadRequestException("AccountId Not Found");
            }

            acc.RoleId = request.RoleId;
            await _myDbContext.SaveChangesAsync();
            return acc;
        }

        public async Task<bool> IsValidRole(int id)
        {
            return await _myDbContext.Roles.AnyAsync(x => x.RoleId == id);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);
            var response = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new BadRequestException("Token Invalid");
            }

            UserInfor? userInfor = await response.Content.ReadFromJsonAsync<UserInfor>();

            var acc = await _myDbContext.Accounts.Where(x => x.Email == userInfor!.Mail)
                .FirstOrDefaultAsync();
            if (acc is null)
            {
                acc = new Models.Account
                {
                    Email = userInfor!.Mail!,
                    FullName = userInfor!.DisplayName,
                    LastLogin = DateTime.Now,
                    RoleId = 1,
                };
                await _myDbContext.Accounts.AddAsync(acc);
            }
            else
            {
                acc.LastLogin = DateTime.Now;
            }

            await _myDbContext.SaveChangesAsync();

            return GenerateJwtToken((await _myDbContext.Accounts.Where(x => x.AccountId == acc.AccountId)
                .Include(x => x.Role)
                .FirstOrDefaultAsync())!);
        }

        private string GenerateJwtToken(Models.Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes("81426C51-951E-4ACC-8541-000F32540381");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",account.AccountId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            if (account.Role != null)
            {
                tokenDescriptor.Subject.AddClaim(new Claim("Roles", account.Role.RoleName!));
            }

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}