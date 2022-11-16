using static CAP_Backend_Source.Modules.Faculty.Request.FacultyRequest;

namespace CAP_Backend_Source.Modules.Faculty.Services
{
    public interface IFacultyService
    {
        Task<List<Models.Faculty>> GetAllFaculty();
        Task<Models.Faculty> CreateFaculty(CreateFacultyRequest request);
        Task<Models.Faculty> UpdateFaculty(int id, EditFacultyRequest request);
        Task<String> DeleteFaculty(int id);
    }
}
