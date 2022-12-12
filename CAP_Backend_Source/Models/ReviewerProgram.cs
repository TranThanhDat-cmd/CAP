namespace CAP_Backend_Source.Models
{
    public class ReviewerProgram
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int AccountId { get; set; }
        public bool Approved { get; set; }
        public string? Comment { get; set; }
        public DateTime ApprovalDate { get; set; }
        public virtual Account Account { get; set; } = null!;

        public virtual Program Program { get; set; } = null!;
    }
}
