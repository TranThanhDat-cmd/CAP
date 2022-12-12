namespace CAP_Backend_Source.Modules.ReviewProgram.Request
{
    public class ReviewProgramRequest
    {
        public class CreateReviewer
        {
            public int ProgramId { get; set; }
            public int AccountId { get; set; }
        }
    }
}
