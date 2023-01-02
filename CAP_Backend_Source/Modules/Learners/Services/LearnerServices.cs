using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Learners.Requests;
using CAP_Backend_Source.Modules.Programs.Service;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.Learners.Services
{
    public interface ILearnerServices
    {
        Task RegisterOrUnRegisterAsync(int userId, RegisterOrUnRegisterRequest request);
        Task ImportAsync(ImportLearnerRequest request);
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
                    AccountIdLearner = userId,
                    RegisterStatus = "UnApproved"
                });
            }
            else if (!request.IsRegister && leaner != null)
            {
                _myDbContext.Learners.Remove(leaner);
            }
            _myDbContext.SaveChanges();
        }

        public async Task ImportAsync(ImportLearnerRequest request)
        {

            Models.Program? program = await _myDbContext.Programs.Where(x => x.ProgramId == request.ProgramId)
                .FirstOrDefaultAsync() ?? throw new BadRequestException("ProgramId Not Found");
            request.Emails!.ForEach(x => x.ToLower());
            var accs = _myDbContext.Accounts.Where(x => request.Emails.Contains(x.Email.ToLower()));

            if (accs.Count() != request.Emails.Count)
            {
                throw new BadRequestException("Email Invalid");
            }
            await _myDbContext.Learners.AddRangeAsync(request.Emails!.Select(x => new Learner()
            {
                ProgramId = request.ProgramId,
                AccountIdLearner = accs.First(y => y.Email.ToLower() == x.ToLower()).AccountId,
                RegisterStatus = "Approved"
            }));
            _myDbContext.SaveChanges();

        }
    }
}
