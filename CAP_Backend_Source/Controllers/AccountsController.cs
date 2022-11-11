using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Modules.Account.Services;
using CAP_Backend_Source.Services.User.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
