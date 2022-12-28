using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Request;
using CAP_Backend_Source.Modules.Account.Services;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Service;
using CAP_Backend_Source.Services.User.Request;
using Infrastructure.Exceptions.HttpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP_Backend_UnitTest.Services
{
    public class Positions
    {
        private PositionService PositionService = new PositionService(new MyDbContext(), new FileStorageService());

        [Fact]
        public async Task CreateSuccess()
        {
            var acc = await PositionService.CreateAsync(new BaseActionPosition
            {
                Name = "name",
            });

            Assert.NotNull(acc);
        }

        [Fact]
        public async Task CreateFail()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => PositionService.CreateAsync(new BaseActionPosition
            {
                Name = null,
            }));
        }

        public async Task UpdateSuccess()
        {
            var acc = await PositionService.UpdateAsync(2, new BaseActionPosition
            {
                Name = "NAME",
            });

            Assert.NotNull(acc);
        }

        [Fact]
        public async Task UpdateFail()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => PositionService.UpdateAsync(2000000, new BaseActionPosition { Name = null }));
        }

        [Fact]
        public async Task GetSuccess()
        {
            await PositionService.DeleteAsync(2);
        }
    }
}
