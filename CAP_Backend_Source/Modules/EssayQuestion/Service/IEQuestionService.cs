using static CAP_Backend_Source.Modules.EssayQuestion.Request.EQuestionRequest;

namespace CAP_Backend_Source.Modules.EssayQuestion.Service
{
    public interface IEQuestionService
    {
        Task<List<Models.EssayQuestion>> GetQuestionByTestId(int id);
        Task<Models.EssayQuestion> GetQuestionById(int id);
        Task<Models.EssayQuestion> CreateQuestion(CreateEQuestionRequest request);
        Task<Models.EssayQuestion> UpdateQuestion(int id, UpdateEQuestionRequest request);
        Task<string> DeleteQuestion(int id);
    }
}
