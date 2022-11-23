namespace CAP_Backend_Source.Modules.Tests.Request
{
    public class TestRequest
    {
        public class CreateTestRequest
        {
            public int ContentId { get; set; }

            public string TestTitle { get; set; } = null!;

            public int? Time { get; set; }

            public int Chapter { get; set; }

            public bool? IsRandom { get; set; }
        }

        public class UpdateTestRequest
        {
            public string TestTitle { get; set; } = null!;

            public int? Time { get; set; }

            public int Chapter { get; set; }

            public bool? IsRandom { get; set; }
        }
    }
}
