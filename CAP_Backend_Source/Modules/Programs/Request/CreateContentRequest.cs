using System.ComponentModel.DataAnnotations;

namespace CAP_Backend_Source.Modules.Programs.Request
{
    public class CreateContentRequest
    {
        [Required]
        public int ProgramId { get; set; }

        public int Chapter { get; set; }

        public string ContentType { get; set; } = null!;

        public string? ContentTitle { get; set; }
        public string? ContentDescription { get; set; }

        [Required]
        public string Content { get; set; } = null!;
    }
}
