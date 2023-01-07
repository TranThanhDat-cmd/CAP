using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.DoTest.Request;

namespace CAP_Backend_Source.Modules.DoTest.Services
{
    public interface IDoTestService
    {
        Task<string> SaveAnswer(int idAccount, List<DoTestRequest> requests);
        Task<ResultTest> SaveResultTest(int idTest, int idAccount, double? score);
    }
}
