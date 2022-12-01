using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.Programs.Service
{
    public interface IAcademicYearService
    {
        Task<List<AcademicYear>> GetAsync();


    }

    public class AcademicYearService : IAcademicYearService
    {
        private MyDbContext _myDbContext;
        private readonly IFileStorageService _fileStorageService;

        public AcademicYearService(MyDbContext myDbContext, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _myDbContext = myDbContext;
        }

        public async Task<List<AcademicYear>> GetAsync()
        {
            return await _myDbContext.AcademicYears.ToListAsync();
        }
    }
}

