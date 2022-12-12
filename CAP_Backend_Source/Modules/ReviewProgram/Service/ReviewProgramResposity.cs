using CAP_Backend_Source.Models;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.ReviewProgram.Request.ReviewProgramRequest;

namespace CAP_Backend_Source.Modules.ReviewProgram.Service
{
    public class ReviewProgramResposity : IReviewProgramService
    {
        private readonly MyDbContext _myDbContext;
        public ReviewProgramResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<List<Models.Program>> GetListPrograms()
        {
            List<Models.Program> _listPrograms = await _myDbContext.Programs.Where(p => p.Status == "Chờ duyệt").ToListAsync();

            return _listPrograms;
        }

        public async Task<List<Reviewer>> GetProgramsByIdReviewer(int id)
        {
            List<Reviewer> _listPrograms = await _myDbContext.Reviews.Where(r => r.AccountId == id).Include(r => r.Program).ToListAsync();

            return _listPrograms;
        }

        public async Task<Reviewer> SetReviewer(CreateReviewer request)
        {
            var reviewer = new Reviewer() 
            {
                AccountId = request.AccountId,
                ProgramId = request.ProgramId,
            };

            await _myDbContext.Reviews.AddAsync(reviewer);
            await _myDbContext.SaveChangesAsync();
            return reviewer;
        }
    }
}
