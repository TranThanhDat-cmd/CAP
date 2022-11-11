using CAP_Backend_Source.Common;
using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Services.User.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CAP_Backend_Source.Modules.Account.Services
{
    public interface IAccountService
    {
        Task<Models.Account> CreateAsync(CreateAccountRequest request);
        Task<Models.Account?> UpdateAsync(int id, UpdateAccountRequest request);
        Task<List<Models.Account>> GetAsync();
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

            if(acc == null)
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
    }
}
