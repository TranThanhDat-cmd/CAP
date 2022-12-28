using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Service;
using Infrastructure.Exceptions.HttpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP_Backend_UnitTest.Services
{
    public class AcademicYears
    {
        private AcademicYearService AcademicYearService = new AcademicYearService(new MyDbContext(), new FileStorageService());

        [Fact]
        public async Task CreateSuccess()
        {
            var acc = await AcademicYearService.CreateAsync(new BaseActionAcademicYear
            {
                Year = "Year",
            });

            Assert.NotNull(acc);
        }

        [Fact]
        public async Task CreateFail()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => AcademicYearService.CreateAsync(new BaseActionAcademicYear
            {
                Year = null,
            }));
        }

        public async Task UpdateSuccess()
        {
            var acc = await AcademicYearService.UpdateAsync(2, new BaseActionAcademicYear
            {
                Year = "Year",
            });

            Assert.NotNull(acc);
        }

        [Fact]
        public async Task UpdateFail()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => AcademicYearService.UpdateAsync(2000000, new BaseActionAcademicYear { Year = null }));
        }

        [Fact]
        public async Task GetSuccess()
        {
            await AcademicYearService.DeleteAsync(2);
        }
    }
}
