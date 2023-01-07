using System.ComponentModel.DataAnnotations;

namespace CAP_Backend_Source.Modules.Learners.Requests
{
    public class RefuseApplicationRequest
    {
        [Required]
        public string? ReasonRefusal { get; set; }

    }
}
