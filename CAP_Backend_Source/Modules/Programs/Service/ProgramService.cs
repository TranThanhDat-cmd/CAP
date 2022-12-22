using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CAP_Backend_Source.Modules.Programs.Service
{
    public interface IProgramService
    {
        Task<List<Models.Program>> GetAsync(int? userId = default);
        Task<Models.Program> CreateAsync(int userId, CreateProgramRequest request);
        Task<Models.Program> UpdateAsync(int id, CreateProgramRequest request);
        Task<Models.Program?> UpdateStatus(int id, UpdateStatusRequest request);
        Task Like(int userId, int programId, bool isLike);
        Task DeleteAsync(int id);
        Task<Models.Program?> GetDetailAsync(int id, int? userId = default);
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
                Lecturers = request.Lecturers,
                Time = request.Time,
                AccountIdCreator = userId,
                FacultyId = request.FacultyId,
                CategoryId = request.CategoryId,
                ProgramName = request.ProgramName,
                Image = request.Image == null ? null : _fileStorageService.SaveFile(request.Image),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsPublish = false,
                Coin = request.Coin,
                AcademicYearId = request.AcademicYearId,
                Semester = request.Semester,
                Descriptions = request.Descriptions,
                RegistrationEndDate = request.RegistrationEndDate,
                RegistrationStartDate = request.RegistrationStartDate,
                Status = request.Status,
                ProgramPositions = request.PositionIds!.Split(",").Select(x => new ProgramPosition()
                {
                    CreatedAt = DateTime.Now,
                    PositionId = int.Parse(x),
                }).ToList(),
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


            Models.Program? program = await _myDbContext.Programs.Where(x => x.ProgramId == id)
                .Include(x => x.ProgramPositions).FirstOrDefaultAsync();

            if (program == null)
            {
                throw new BadRequestException("ProgramId Not Found");

            }

            if (request.Image != null)
            {
                _fileStorageService.DeleteFile(program.Image!);
                program.Image = _fileStorageService.SaveFile(request.Image!);
            }

            program.FacultyId = request.FacultyId;
            program.Lecturers = request.Lecturers;
            program.Time = request.Time;
            program.CategoryId = request.CategoryId;
            program.ProgramName = request.ProgramName;

            program.StartDate = request.StartDate;
            program.EndDate = request.EndDate;
            program.IsPublish = false;
            program.Coin = request.Coin;
            program.AcademicYearId = request.AcademicYearId;
            program.Semester = request.Semester;
            program.Descriptions = request.Descriptions;
            program.RegistrationEndDate = request.RegistrationEndDate;
            program.RegistrationStartDate = request.RegistrationStartDate;
            program.Status = request.Status;
            program.ProgramPositions = request.PositionIds!.Split(",").Select(x => new ProgramPosition()
            {
                CreatedAt = DateTime.Now,
                PositionId = int.Parse(x),
            }).ToList();

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
            _fileStorageService.DeleteFile(program.Image!);
            _myDbContext.Programs.Remove(program);
            await _myDbContext.SaveChangesAsync();

        }

        public async Task<Models.Program?> GetDetailAsync(int id, int? userId = default)
        {
            var program = await _myDbContext.Programs
                .Include(x => x.Category)
                .Include(x => x.Faculty)
                .Include(x => x.AcademicYear)
                .Include(x => x.AccountPrograms)
                .Include(X => X.ProgramPositions).ThenInclude(X => X.Position)
                .FirstOrDefaultAsync(x => x.ProgramId == id);
            if (program == null)
            {
                return null;
            }
            program.TotalLike = program.AccountPrograms?.Count;
            program.AccountPrograms = null;

            program.AcademicYear.Programs = null;
            program.Category.Programs = null;
            program.Faculty.Programs = null;
            program.IsLike = userId != default && _myDbContext.AccountPrograms.Any(x => x.AccountId == userId && x.ProgramId == program.ProgramId);
            program.ProgramPositions = program.ProgramPositions.Select(x =>
            {
                x.Position.ProgramPositions = null;
                return x;
            }).ToList();

            return program;

        }

        public async Task<List<ContentProgram>> GetContentsAsync(int id)
        {
            return await _myDbContext.ContentPrograms.Where(x => x.ProgramId == id).ToListAsync();
        }

        public async Task<ContentProgram?> GetContentAsync(int id)
        {
            return await _myDbContext.ContentPrograms.Where(x => x.ContentId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Models.Program>> GetAsync(int? userId = default)
        {
            return (await _myDbContext.Programs
                .Include(x => x.Category)
                .Include(x => x.Faculty)
                .Include(x => x.AcademicYear)
                .Include(x => x.AccountPrograms)
                .Include(X => X.ProgramPositions).ThenInclude(x => x.Position)
                .ToListAsync()).ConvertAll(x =>
                {
                    x.IsLike = userId != default && _myDbContext.AccountPrograms.Any(y => y.AccountId == userId && y.ProgramId == x.ProgramId);
                    x.TotalLike = x.AccountPrograms?.Count;
                    x.AccountPrograms = null;
                    x.AcademicYear.Programs = null;
                    x.Category.Programs = null;
                    x.Faculty.Programs = null;
                    x.ProgramPositions = x.ProgramPositions.Select(x =>
                    {
                        x.Position.ProgramPositions = null;
                        return x;
                    }).ToList();
                    return x;
                });

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
                ProgramId = request.ProgramId,
                ContentTitle = request.ContentTitle,
                ContentDescription = request.ContentDescription,
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
            content.ContentTitle = request.ContentTitle;
            content.ContentDescription = request.ContentDescription;
            await _myDbContext.SaveChangesAsync();
            return content;
        }

        public async Task<Models.Program?> UpdateStatus(int id, UpdateStatusRequest request)
        {
            Models.Program? program = await _myDbContext.Programs.Where(x => x.ProgramId == id)
                .FirstOrDefaultAsync();

            if (program == null)
            {
                throw new BadRequestException("ProgramId Not Found");

            }

            program.Status = request.Status;
            await _myDbContext.SaveChangesAsync();
            return program;
        }

        public async Task Like(int userId, int programId, bool isLike)
        {
            Models.Program? program = await _myDbContext.Programs.Where(x => x.ProgramId == programId)
                .FirstOrDefaultAsync();

            if (program == null)
            {
                throw new BadRequestException("ProgramId Not Found");

            }

            var accountProgram = _myDbContext.AccountPrograms.Where(x => x.AccountId == userId && programId == x.ProgramId).FirstOrDefault();
            if (isLike)
            {
                if (accountProgram == null)
                {
                    await _myDbContext.AccountPrograms.AddAsync(new AccountProgram()
                    {
                        AccountId = userId,
                        ProgramId = programId,
                        CreatedAt = DateTime.Now,
                    });
                }
            }
            else
            {
                if (accountProgram != null)
                {
                    _myDbContext.AccountPrograms.Remove(accountProgram);
                }
            }
            await _myDbContext.SaveChangesAsync();
        }
    }

}
