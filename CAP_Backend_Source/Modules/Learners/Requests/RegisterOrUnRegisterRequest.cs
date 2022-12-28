namespace CAP_Backend_Source.Modules.Learners.Requests
{
    public class RegisterOrUnRegisterRequest
    {
        public int ProgramId { get; set; }
        public bool IsRegister { get; set; }
    }
}
