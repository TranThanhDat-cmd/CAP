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
        Task<AcademicYear> CreateAsync(string year);
        Task<AcademicYear?> DetailAsync(int id);
        Task<AcademicYear> UpdateAsync(int id, string year);
        Task DeleteAsync(int id);
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

        public async Task<AcademicYear> CreateAsync(string year)
        {
            var academicYear = new AcademicYear()
            {
                Year = year,
            };
            await _myDbContext.AcademicYears.AddAsync(academicYear);
            await _myDbContext.SaveChangesAsync();
            return academicYear;
        }

        public async Task<AcademicYear?> DetailAsync(int id)
        {
            return await _myDbContext.AcademicYears.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AcademicYear> UpdateAsync(int id, string year)
        {
            var academicYear = await _myDbContext.AcademicYears.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (academicYear == null)
            {
                throw new BadRequestException("Id not found");
            }
            academicYear.Year = year;
            await _myDbContext.SaveChangesAsync();
            return academicYear;
        }

        public async Task DeleteAsync(int id)
        {
            var academicYear = await _myDbContext.AcademicYears.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (academicYear == null)
            {
                throw new BadRequestException("Id not found");
            }
            _myDbContext.AcademicYears.Remove(academicYear);
            await _myDbContext.SaveChangesAsync();
        }
    }
}

