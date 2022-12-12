namespace CAP_Backend_Source.Models
{
    public class Reviewer
    {
        public int ReviewerId { get; set; }
        public int ProgramId { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;

        public virtual Program Program { get; set; } = null!;
    }
}
