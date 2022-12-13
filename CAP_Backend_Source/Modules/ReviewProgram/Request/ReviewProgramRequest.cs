namespace CAP_Backend_Source.Modules.ReviewProgram.Request
{
    public class ReviewProgramRequest
    {
        public class CreateReviewerRequest
        {
            public int ProgramId { get; set; }
            public int AccountId { get; set; }
        }

        public class ApproveProgramRequest
        {
            public int ProgramId { get; set; }
            public int AccountId { get; set; }
            public bool Approved { get; set; }
            public string? Comment { get; set; }
            public DateTime ApprovalDate { get; set; }
        }
    }
}
