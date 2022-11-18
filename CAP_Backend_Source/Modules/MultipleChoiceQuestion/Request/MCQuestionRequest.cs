namespace CAP_Backend_Source.Modules.MultipleChoiceQuestion.Request
{
    public class MCQuestionRequest
    {
        public class CreateMCQuestionRequest
        {
            public int TestsId { get; set; }

            public string McquestionTitle { get; set; } = null!;

            public string Content1 { get; set; } = null!;

            public string? Content2 { get; set; }

            public string? Content3 { get; set; }

            public string Content4 { get; set; } = null!;

            public string Answer { get; set; } = null!;
        }

        public class UpdateMCQuestionRequest
        {
            public string McquestionTitle { get; set; } = null!;

            public string Content1 { get; set; } = null!;

            public string? Content2 { get; set; }

            public string? Content3 { get; set; }

            public string Content4 { get; set; } = null!;

            public string Answer { get; set; } = null!;
        }
    }
}
