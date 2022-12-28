using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Learners.Requests;
using CAP_Backend_Source.Modules.Programs.Service;

namespace CAP_Backend_Source.Modules.Learners.Services
{
    public interface ILearnerServices
    {
        Task RegisterOrUnRegisterAsync(int userId, RegisterOrUnRegisterRequest request);
    }

    public class LearnerServices : ILearnerServices
    {
        private MyDbContext _myDbContext;
        private readonly IFileStorageService _fileStorageService;

        public LearnerServices(MyDbContext myDbContext, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _myDbContext = myDbContext;
        }
        public async Task RegisterOrUnRegisterAsync(int userId, RegisterOrUnRegisterRequest request)
        {
            var leaner = _myDbContext.Learners.Where(x => x.AccountIdLearner == userId && x.ProgramId == request.ProgramId).FirstOrDefault();
            if (request.IsRegister && leaner == null)
            {
                await _myDbContext.Learners.AddAsync(new Learner()
                {
                    ProgramId = request.ProgramId,
                    AccountIdLearner = request.ProgramId,
                    RegisterStatus = "UnApproved"
                });
            }
            else if (!request.IsRegister && leaner != null)
            {
                _myDbContext.Learners.Remove(leaner);
            }
            _myDbContext.SaveChanges();
        }
    }
}
