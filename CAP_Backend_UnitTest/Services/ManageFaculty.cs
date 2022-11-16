using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Faculty.Services;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.Faculty.Request.FacultyRequest;

namespace CAP_Backend_UnitTest.Services
{
    public class ManageFaculty
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private FacultyResposity facultyResposity = new FacultyResposity(new MyDbContext());
        public static int idFaculty;

        #region Create Faculty
        [Fact]
        public async Task CreateFaculty_Success()
        {
            var response = await facultyResposity.CreateFaculty(new CreateFacultyRequest()
            {
                Name = "Unit Test of Create Faculty"
            });
            idFaculty = response.FacultyId;
            Assert.Equal("Unit Test of Create Faculty", response.FacultyName);
        }

        [Fact]
        public async Task CreateFaculty_Fail_NameIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => facultyResposity.CreateFaculty(new CreateFacultyRequest()
            {
                Name = ""
            }));
        }
        #endregion

        #region Update Faculty
        [Fact]
        public async Task UpdateFaculty_Success()
        {
            var _faculty = await _myDbContext.Faculties.FirstAsync();

            var response = await facultyResposity.UpdateFaculty(_faculty.FacultyId, new EditFacultyRequest()
            {
                Name = "Unit Test of Edit Faculty"
            });

            Assert.Equal("Unit Test of Edit Faculty", response.FacultyName);
        }

        [Fact]
        public async Task UpdateFaculty_Fail_IdDoesNotExist()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => facultyResposity.UpdateFaculty(0, new EditFacultyRequest()
            {
                Name = "Unit Test of Edit Faculty"
            }));
        }

        [Fact]
        public async Task UpdateFaculty_Fail_NameIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => facultyResposity.UpdateFaculty(idFaculty, new EditFacultyRequest()
            {
                Name = ""
            }));
        }
        #endregion

        #region Delete Faculty
        [Fact]
        public async Task DeleteFaculty_Success()
        {
            var _faculty = await _myDbContext.Faculties.FirstAsync();
            var response = await facultyResposity.DeleteFaculty(_faculty.FacultyId);

            Assert.Equal("Successful Delete", response);
        }

        [Fact]
        public async Task DeleteFaculty_Fail_IdDoesNotExist()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => facultyResposity.DeleteFaculty(0));
        }
        #endregion

        #region Get All Faculty
        [Fact]
        public async Task GetAllFaculty_Success()
        {
            var response = await facultyResposity.GetAllFaculty();
            var listFaculty = _myDbContext.Faculties.ToList();
            Assert.Equal(response.Count(), listFaculty.Count());
        }
        #endregion
    }
}
