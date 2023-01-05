namespace CAP_Backend_Source.Modules.Learners.Requests
{
    public class AddLearnerRequest
    {
        public int AccountIdLearner { get; set; }
        public int ProgramId { get; set; }
        public int? AccountIdApprover { get; set; }
    }
}
