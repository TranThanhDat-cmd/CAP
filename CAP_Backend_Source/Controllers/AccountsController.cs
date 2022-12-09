using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Modules.Account.Services;
using CAP_Backend_Source.Services.User.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _accountService.GetAsync());
        }

        [HttpGet("Token")]
        public async Task<IActionResult> GetTokenAsync()
        {
            var user = (await _accountService.GetAsync()).Where(x=>x.RoleId==2).First();
            return Ok( _accountService.GenerateJwtToken(user));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            int id = int.Parse(User.FindFirstValue("id").ToString());
            return Ok(await _accountService.GetProfile(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateAccountRequest request)
        {
            return Ok(await _accountService.CreateAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateAccountRequest request)
        {
            return Ok(await _accountService.UpdateAsync(id,request));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            return Ok(new
            {
                Token = await _accountService.LoginAsync(request),
            });
        }
    }
}
