using static CAP_Backend_Source.Modules.MultipleChoiceQuestion.Request.MCQuestionRequest;

namespace CAP_Backend_Source.Modules.MultipleChoiceQuestion.Service
{
    public interface IMCQuestionService
    {
        Task<List<Models.MultipleChoiceQuestion>> GetQuestionByTestId(int id);
        Task<Models.MultipleChoiceQuestion> GetQuestionById(int id);
        Task<Models.MultipleChoiceQuestion> CreateQuestion(CreateMCQuestionRequest request);
        Task<Models.MultipleChoiceQuestion> UpdateQuestion(int id, UpdateMCQuestionRequest request);
        Task<string> DeleteQuestion(int id);
    }
}
