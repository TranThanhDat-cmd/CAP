namespace CAP_Backend_Source.Modules.Question.Request
{
    public class QuestionRequest
    {
        public class QuestionContentRequest
        {
            public int? QuestionId { get; set; }

            public string? Content { get; set; }

            public bool? IsAnswer { get; set; }
        }
        public class CreateQuestionRequest
        {
            public int? TestsId { get; set; }

            public int TypeId { get; set; }

            public string QuestionTitle { get; set; } = null!;

            public double? Score { get; set; }
            
            public List<QuestionContentRequest> questionContents { get; set; }
        }
        public class UpdateQuestionRequest
        {
            public int TypeId { get; set; }

            public string QuestionTitle { get; set; } = null!;

            public double? Score { get; set; }

            public List<QuestionContentRequest> questionContents { get; set; }
        }
    }
}
