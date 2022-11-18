using static CAP_Backend_Source.Modules.Tests.Request.TestRequest;

namespace CAP_Backend_Source.Modules.Tests.Service
{
    public interface ITestService
    {
        Task<List<Models.Test>> GetTestByProgramId(int id);
        Task<Models.Test> CreateTest(CreateTestRequest request);
        Task<Models.Test> UpdateTest(int id, UpdateTestRequest request);
        Task<string> DeleteTest(int id);
    }
}
