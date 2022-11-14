using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Modules.Account.Services;
using CAP_Backend_Source.Services.User.Request;
using Infrastructure.Exceptions.HttpExceptions;

namespace CAP_Backend_UnitTest
{
    public class Accounts
    {
        private AccountService accountService = new AccountService(new MyDbContext());

        [Fact]
        public async Task CreateSuccess()
        {
            var acc = await accountService.CreateAsync(new CreateAccountRequest()
            {
                Email = "dattest3333@gmail.com",
                RoleId = 1
            });

            Assert.NotNull(acc);
        }

        [Fact]
        public async Task CreateFail()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => accountService.CreateAsync(new CreateAccountRequest()
            {
                Email = "dattest@gmail.com",
                RoleId = 100000000
            }));
        }

        public async Task UpdateSuccess()
        {
            var acc = await accountService.UpdateAsync(2, new UpdateAccountRequest()
            {
                RoleId = 1
            });

            Assert.NotNull(acc);
        }

        [Fact]
        public async Task UpdateFail()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => accountService.UpdateAsync(2000000, new UpdateAccountRequest()
            {
                RoleId = 1000000
            }));
        }

        [Fact]
        public async Task GetSuccess()
        {
            var acc = await accountService.GetAsync();
            Assert.NotNull(acc);
        }
    }
}