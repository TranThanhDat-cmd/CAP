using CAP_Backend_Source.Modules.Category.Services;
using CAP_Backend_Source.Modules.Faculty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.Category.Request.CategoryRequest;
using static CAP_Backend_Source.Modules.Faculty.Request.FacultyRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        public FacultiesController(IFacultyService facultyService) 
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _facultyService.GetAllFaculty());
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(CreateFacultyRequest request)
        {
            return Ok(await _facultyService.CreateFaculty(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFaculty(int id, EditFacultyRequest request)
        {
            return Ok(await _facultyService.UpdateFaculty(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            return Ok(await _facultyService.DeleteFaculty(id));
        }
    }
}
