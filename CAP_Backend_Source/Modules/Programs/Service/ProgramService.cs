using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.Programs.Service
{
    public interface IProgramService
    {
        Task<List<Models.Program>> GetAsync();
        Task<Models.Program> CreateAsync(CreateProgramRequest request);
    }

    public class ProgramService : IProgramService
    {
        private MyDbContext _myDbContext;
        private readonly IFileStorageService _fileStorageService;

        public ProgramService(MyDbContext myDbContext, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _myDbContext = myDbContext;
        }

        public async Task<Models.Program> CreateAsync(CreateProgramRequest request)
        {
            if (!_myDbContext.Faculties.Any(x=>x.FacultyId ==request.FacultyId))
            {
                throw new BadRequestException("FacultyId Not Found");

            }

            if (!_myDbContext.Categories.Any(x => x.CategoryId == request.CategoryId))
            {
                throw new BadRequestException("CategoryId Not Found");

            }
            
            if (!_myDbContext.Accounts.Any(x => x.AccountId == request.AccountIdCreator))
            {
                throw new BadRequestException("AccountIdCreator Not Found");

            }

            Models.Program program = new()
            {
                AccountIdCreator = request.AccountIdCreator,
                FacultyId = request.FacultyId,
                CategoryId = request.CategoryId,
                ProgramName = request.ProgramName,
                Image = request.Image == null ? null : _fileStorageService.SaveFile(request.Image),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsPublish = request.IsPublish,
                Coin = request.Coin,
            };

            await _myDbContext.Programs.AddAsync(program);
            await _myDbContext.SaveChangesAsync();
            return program;

        }

        public async Task<List<Models.Program>> GetAsync()
        {
            return await _myDbContext.Programs.ToListAsync();
        }
    }

}
