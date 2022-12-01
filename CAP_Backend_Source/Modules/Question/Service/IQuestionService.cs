using static CAP_Backend_Source.Modules.Question.Request.QuestionRequest;

namespace CAP_Backend_Source.Modules.Question.Service
{
    public interface IQuestionService
    {
        Task<int> CreateQuestion(CreateQuestionRequest request);
        Task<string> CreateQuestionContent(int id, CreateQuestionRequest request);
        Task<string> UpdateQuestion(int id, UpdateQuestionRequest request);
        Task<string> DeleteQuestion(int id);
        Task<string> DeleteQuestionContent(int id);
        Task<List<Models.Question>> GetListQuestionByTestId(int id);
    }
}
