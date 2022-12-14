using static CAP_Backend_Source.Modules.ReviewProgram.Request.ReviewProgramRequest;

namespace CAP_Backend_Source.Modules.ReviewProgram.Service
{
    public interface IReviewProgramService
    {
        Task<List<Models.Program>> GetListPrograms();
        Task<List<Models.Reviewer>> GetProgramsByIdReviewer(int id);
        Task<Models.Reviewer> SetReviewer(CreateReviewerRequest request);
        Task<Models.ReviewerProgram> ApproveProgram(ApproveProgramRequest request);
        Task<List<Models.ReviewerProgram>> GetApprovedListByIdProgram(int id);
        Task<string> SendReviewer(int programId);
    }
}
