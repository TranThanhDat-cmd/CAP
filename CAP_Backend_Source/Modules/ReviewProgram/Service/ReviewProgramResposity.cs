using CAP_Backend_Source.Models;
using Infrastructure.Exceptions.HttpExceptions;
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

        public async Task<Reviewer> SetReviewer(CreateReviewerRequest request)
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

        public async Task<ReviewerProgram> ApproveProgram(ApproveProgramRequest request)
        {
            #region Check Input
            if (request.Approved == false && (request.Comment == null || request.Comment.Trim() == "")) 
            {
                throw new BadRequestException("Comment is not blank");
            }
            #endregion
            var information = new ReviewerProgram()
            {
                ProgramId = request.ProgramId,
                AccountId = request.AccountId,
                Approved = request.Approved,
                Comment = request.Comment,
                ApprovalDate = request.ApprovalDate,
            };

            await _myDbContext.ReviewsProgram.AddAsync(information);
            await _myDbContext.SaveChangesAsync();

            return information;
        }

        public async Task<List<ReviewerProgram>> GetApprovedListByIdProgram(int id)
        {
            List<ReviewerProgram> _listApproved = await _myDbContext.ReviewsProgram.Where(rp => rp.ProgramId == id).ToListAsync();

            return _listApproved;
        }
    }
}
