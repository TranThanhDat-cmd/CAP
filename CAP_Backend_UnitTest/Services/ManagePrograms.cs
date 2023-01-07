using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Request;
using CAP_Backend_Source.Modules.Programs.Service;
using CAP_Backend_Source.Services.User.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.AspNetCore.Http;

namespace CAP_Backend_UnitTest.Services;

public class ManagePrograms
{
    private ProgramService programService = new ProgramService(new MyDbContext(), new FileStorageService());

    [Fact]
    public async Task CreateSuccess()
    {
        var acc = await programService.CreateAsync(2, new CreateProgramRequest
        {
            FacultyId = 1,
            //AccountIdCreator = 1,
            CategoryId = 1,
            ProgramName = "Dat Tesst",
            Image = null,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            Coin = 10
        });

        Assert.NotNull(acc);
    }

    [Fact]
    public async Task CreateFail()
    {
        await Assert.ThrowsAsync<BadRequestException>(() => programService.CreateAsync(-1, new CreateProgramRequest
        {
            FacultyId = -1,
            //AccountIdCreator = -10000,
            CategoryId = 1,
            ProgramName = "Dat Tesst",
            Image = null,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            Coin = 10
        }));
    }

    [Fact]
    public async Task GetSuccess()
    {
        var acc = await programService.GetAsync();
        Assert.NotNull(acc);
    }
}