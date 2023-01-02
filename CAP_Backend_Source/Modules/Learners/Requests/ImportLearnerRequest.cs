using System.ComponentModel.DataAnnotations;

namespace CAP_Backend_Source.Modules.Learners.Requests
{
    public class ImportLearnerRequest
    {
        [Required]
        public int ProgramId { get; set; }
        [Required]

        public List<string>? Emails { get; set; }
    }
}
