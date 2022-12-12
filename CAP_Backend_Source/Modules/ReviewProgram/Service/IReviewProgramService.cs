namespace CAP_Backend_Source.Modules.ReviewProgram.Service
{
    public interface IReviewProgramService
    {
        Task<List<Models.Program>> GetListPrograms();
        Task<List<Models.Program>> GetProgramsByIdReviewer(int id);
    }
}
