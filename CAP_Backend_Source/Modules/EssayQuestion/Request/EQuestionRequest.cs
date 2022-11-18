namespace CAP_Backend_Source.Modules.EssayQuestion.Request
{
    public class EQuestionRequest
    {
        public class CreateEQuestionRequest
        {
            public int TestsId { get; set; }

            public string EquestionTitle { get; set; } = null!;
        }

        public class UpdateEQuestionRequest
        {
            public string EquestionTitle { get; set; } = null!;
        }
    }
}
