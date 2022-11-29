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
        Task<Models.Program> CreateAsync(int userId, CreateProgramRequest request);
        Task<Models.Program> UpdateAsync(int id, CreateProgramRequest request);
        Task DeleteAsync(int id);
        Task<Models.Program?> GetDetailAsync(int id);
        Task<List<ContentProgram>> GetContentsAsync(int id);
        Task<ContentProgram?> GetContentAsync(int id);
        Task<ContentProgram> CreateContentAsync(CreateContentRequest request);
        Task<ContentProgram> UpdateContentAsync(int id, CreateContentRequest request);
        Task DeleteContentAsync(int id);

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

        public async Task<Models.Program> CreateAsync(int userId, CreateProgramRequest request)
        {
            if (request.FacultyId != null && !_myDbContext.Faculties.Any(x => x.FacultyId == request.FacultyId))
            {
                throw new BadRequestException("FacultyId Not Found");

            }

            if (request.CategoryId != null && !_myDbContext.Categories.Any(x => x.CategoryId == request.CategoryId))
            {
                throw new BadRequestException("CategoryId Not Found");

            }


            Models.Program program = new()
            {
                AccountIdCreator = userId,
                FacultyId = request.FacultyId,
                CategoryId = request.CategoryId,
                ProgramName = request.ProgramName,
                Image = request.Image == null ? null : _fileStorageService.SaveFile(request.Image),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsPublish = request.IsPublish,
                Coin = request.Coin,
                AcademicYearId = request.AcademicYearId,
                Positions = request.Positions,
                Semester = request.Semester,
            };

            await _myDbContext.Programs.AddAsync(program);
            await _myDbContext.SaveChangesAsync();
            return program;

        }

        public async Task<Models.Program> UpdateAsync(int id, CreateProgramRequest request)
        {
            if (request.FacultyId != null && !_myDbContext.Faculties.Any(x => x.FacultyId == request.FacultyId))
            {
                throw new BadRequestException("FacultyId Not Found");

            }

            if (request.CategoryId != null && !_myDbContext.Categories.Any(x => x.CategoryId == request.CategoryId))
            {
                throw new BadRequestException("CategoryId Not Found");

            }


            Models.Program? program = await _myDbContext.Programs.FirstOrDefaultAsync(x => x.ProgramId == id);

            if (program == null)
            {
                throw new BadRequestException("ProgramId Not Found");

            }


            program.FacultyId = request.FacultyId;
            program.CategoryId = request.CategoryId;
            program.ProgramName = request.ProgramName;
            program.Image = request.Image == null ? program.Image : _fileStorageService.SaveFile(request.Image);
            program.StartDate = request.StartDate;
            program.EndDate = request.EndDate;
            program.IsPublish = request.IsPublish;
            program.Coin = request.Coin;
            program.AcademicYearId = request.AcademicYearId;
            program.Positions = request.Positions;
            program.Semester = request.Semester;

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

        public async Task<List<ContentProgram>> GetContentsAsync(int id)
        {
            return await _myDbContext.ContentPrograms.Where(x => x.ProgramId == id).ToListAsync();
        }

        public async Task<ContentProgram?> GetContentAsync(int id)
        {
            return await _myDbContext.ContentPrograms.Where(x => x.ContentId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Models.Program>> GetAsync()
        {
            return await _myDbContext.Programs.ToListAsync();
        }

        public async Task DeleteContentAsync(int id)
        {
            var content = await _myDbContext.ContentPrograms.FirstOrDefaultAsync(x => x.ContentId == id);

            if (content == null)
            {
                throw new BadRequestException("ContentId Not Found");

            }
            _myDbContext.ContentPrograms.Remove(content);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task<ContentProgram> CreateContentAsync(CreateContentRequest request)
        {
            if (!_myDbContext.Programs.Any(x => x.ProgramId == request.ProgramId))
            {
                throw new BadRequestException("ProgramId Not Found");
            }
            var content = new ContentProgram()
            {
                Chapter = request.Chapter,
                Content = request.Content,
                ContentType = request.ContentType,
                ProgramId= request.ProgramId,
            };

            _myDbContext.ContentPrograms.Add(content);
            await _myDbContext.SaveChangesAsync();
            return content;
        }

        public async Task<ContentProgram> UpdateContentAsync(int id, CreateContentRequest request)
        {
            if (!_myDbContext.Programs.Any(x => x.ProgramId == request.ProgramId))
            {
                throw new BadRequestException("ProgramId Not Found");
            }
            var content = await GetContentAsync(id);
            if (content == null)
            {
                throw new BadRequestException("ContentId Not Found");
            }

            content.Chapter = request.Chapter;
            content.Content = request.Content;
            content.ContentType = request.ContentType;
            content.ProgramId = request.ProgramId;
            await _myDbContext.SaveChangesAsync();
            return content;
        }
    }

}
