using System.ComponentModel.DataAnnotations;

namespace CAP_Backend_Source.Modules.Programs.Request
{
    public class CreateProgramRequest
    {


        public int? FacultyId { get; set; }

        public int? CategoryId { get; set; }
        [Required]
        public string ProgramName { get; set; } = null!;
        public IFormFile? Image { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public int? Coin { get; set; }
        public DateTime? RegistrationStartDate { get; set; }

        public DateTime? RegistrationEndDate { get; set; }
        [Required]
        public string? PositionIds { get; set; }
        public string? Descriptions { get; set; }
        public string? Lecturers { get; set; }

        public int? AcademicYearId { get; set; }
        public int? Time { get; set; }

        public int? Semester { get; set; }

        public string? Status { get; set; }

    }
}
