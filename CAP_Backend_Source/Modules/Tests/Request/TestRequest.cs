namespace CAP_Backend_Source.Modules.Tests.Request
{
    public class TestRequest
    {
        public class CreateTestRequest
        {
            public int ProgramId { get; set; }
            public string TestTitle { get; set; } = null!;
            public int TypeId { get; set; }
            public int Time { get; set; }
            public int Chapter { get; set; }
        }

        public class UpdateTestRequest
        {
            public string TestTitle { get; set; } = null!;
            public int TypeId { get; set; }
            public int Time { get; set; }
            public int Chapter { get; set; }
        }
    }
}
