using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Faculty.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.Faculty.Request.FacultyRequest;

namespace CAP_Backend_Source.Modules.Faculty.Services
{
    public class FacultyResposity :IFacultyService
    {
        private MyDbContext _myDbContext;
        public FacultyResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Models.Faculty> CreateFaculty(CreateFacultyRequest request)
        {
            if(request.Name== null)
            {
                throw new BadRequestException("Name cannot be left blank");
            }
            var faculty = new Models.Faculty()
            {
                FacultyName = request.Name,
            };
            await _myDbContext.Faculties.AddAsync(faculty);
            await _myDbContext.SaveChangesAsync();
            return faculty;
        }

        public async Task<string> DeleteFaculty(int id)
        {
            var _faculty = await _myDbContext.Faculties.SingleOrDefaultAsync(f => f.FacultyId == id);

            if (_faculty == null)
            {
                throw new BadRequestException("Faculty not found");
            }
            _myDbContext.Faculties.Remove(_faculty);
            await _myDbContext.SaveChangesAsync();
            return "Successful Delete";
        }

        public async Task<List<Models.Faculty>> GetAllFaculty()
        {
            List<Models.Faculty> listCategoris = await _myDbContext.Faculties.ToListAsync();
            return listCategoris;
        }

        public async Task<Models.Faculty> UpdateFaculty(int id, EditFacultyRequest request)
        {
            if (request.Name == null || request.Name == "")
            {
                throw new BadRequestException("Name cannot be left blank");
            }
            var _faculty = await _myDbContext.Faculties.SingleOrDefaultAsync(f => f.FacultyId == id);
            if (_faculty == null)
            {
                throw new BadRequestException("Faculty not found");
            }
            _faculty.FacultyName = request.Name;
            await _myDbContext.SaveChangesAsync();

            return _faculty;
        }
    }
}
