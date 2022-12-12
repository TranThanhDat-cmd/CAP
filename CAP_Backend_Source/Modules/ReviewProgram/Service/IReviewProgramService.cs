using static CAP_Backend_Source.Modules.ReviewProgram.Request.ReviewProgramRequest;

namespace CAP_Backend_Source.Modules.ReviewProgram.Service
{
    public interface IReviewProgramService
    {
        Task<List<Models.Program>> GetListPrograms();
        Task<List<Models.Reviewer>> GetProgramsByIdReviewer(int id);
        Task<Models.Reviewer> SetReviewer(CreateReviewer request);
    }
}
