using System.ComponentModel.DataAnnotations;

namespace CAP_Backend_Source.Modules.Programs.Request
{
    public class CreateProgramRequest
    {

        [Required]
        public int FacultyId { get; set; }
        [Required]
        public int AccountIdCreator { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string ProgramName { get; set; } = null!;
        public IFormFile? Image { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsPublish { get; set; }
        [Required]
        public int? Coin { get; set; }
    }
}
