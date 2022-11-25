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
        Task<Models.Program> UpdateAsync(int id, CreateProgramRequest request);
        Task DeleteAsync(int id);
        Task<Models.Program?> GetDetailAsync(int id);
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
            if (!_myDbContext.Faculties.Any(x => x.FacultyId == request.FacultyId))
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
                PositionId = request.PositionId
            };

            await _myDbContext.Programs.AddAsync(program);
            await _myDbContext.SaveChangesAsync();
            return program;

        }

        public async Task<Models.Program> UpdateAsync(int id, CreateProgramRequest request)
        {
            if (!_myDbContext.Faculties.Any(x => x.FacultyId == request.FacultyId))
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

            Models.Program? program = await _myDbContext.Programs.FirstOrDefaultAsync(x => x.ProgramId == id);

            if (program == null)
            {
                throw new BadRequestException("ProgramId Not Found");

            }


            program.AccountIdCreator = request.AccountIdCreator;
            program.FacultyId = request.FacultyId;
            program.CategoryId = request.CategoryId;
            program.ProgramName = request.ProgramName;
            program.Image = request.Image == null ? program.Image : _fileStorageService.SaveFile(request.Image);
            program.StartDate = request.StartDate;
            program.EndDate = request.EndDate;
            program.IsPublish = request.IsPublish;
            program.Coin = request.Coin;
            program.PositionId = request.PositionId;

            await _myDbContext.SaveChangesAsync();
            return program;

        }

        public async Task DeleteAsync(int id)
        {


            Models.Program? program = await _myDbContext.Programs.FirstOrDefaultAsync(x => x.ProgramId == id);

            if (program == null)
            {
                throw new BadRequestException("ProgramId Not Found");

            }
            _myDbContext.Programs.Remove(program);
            await _myDbContext.SaveChangesAsync();

        }

        public async Task<Models.Program?> GetDetailAsync(int id)
        {


            return await _myDbContext.Programs.FirstOrDefaultAsync(x => x.ProgramId == id);



        }

        public async Task<List<Models.Program>> GetAsync()
        {
            return await _myDbContext.Programs.ToListAsync();
        }
    }

}
