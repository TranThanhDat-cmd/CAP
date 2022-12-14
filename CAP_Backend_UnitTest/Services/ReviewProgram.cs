using Azure.Core;
using Azure;
using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.ReviewProgram.Service;
using CAP_Backend_Source.Modules.Tests.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.ReviewProgram.Request.ReviewProgramRequest;
using Infrastructure.Exceptions.HttpExceptions;

namespace CAP_Backend_UnitTest.Services
{
    public class ReviewProgram
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private ReviewProgramResposity _reviewProgram = new ReviewProgramResposity(new MyDbContext());

        #region Get List Programs
        [Fact]
        public async Task GetListPrograms_Success()
        {
            List<Program> _listprogram = await _myDbContext.Programs.Where(p => p.Status == "Chờ duyệt").ToListAsync();
            var respose = await _reviewProgram.GetListPrograms();

            Assert.Equal(_listprogram.Count(), respose.Count());
        }
        #endregion

        #region Get Programs By Id Reviewer
        [Fact]
        public async Task GetProgramsByIdReviewer_Success()
        {
            var _account = await _myDbContext.Accounts.FirstAsync();
            List<Reviewer> _listPrograms = await _myDbContext.Reviews.Where(r => r.AccountId == _account.AccountId).Include(r => r.Program).ToListAsync();
            var respose = await _reviewProgram.GetProgramsByIdReviewer(_account.AccountId);

            Assert.Equal(_listPrograms.Count(), respose.Count());
        }
        #endregion

        #region Set Reviewer
        [Fact]
        public async Task SetReviewer_Success()
        {
            var request = new CreateReviewerRequest()
            {
                AccountId = 61,
                ProgramId = 17
            };
            var respose = await _reviewProgram.SetReviewer(request);

            Assert.Equal(61, respose.AccountId);
            Assert.Equal(17, respose.ProgramId);
        }
        [Fact]
        public async Task SetReviewer_Fail_ReviewerAlreadyExists()
        {
            var request = new CreateReviewerRequest()
            {
                AccountId = 61,
                ProgramId = 17
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _reviewProgram.SetReviewer(request));
        }
        #endregion

        #region Approve Program
        [Fact]
        public async Task ApproveProgram_Success()
        {
            var request = new ApproveProgramRequest()
            {
                AccountId = 61,
                ProgramId = 17,
                Approved = true,
                ApprovalDate = DateTime.UtcNow,
            };

            var respose = await _reviewProgram.ApproveProgram(request);
            var _product = await _myDbContext.Programs.SingleOrDefaultAsync(p => p.ProgramId == 17);
            Assert.True(respose.Approved);
            Assert.Equal("Đã duyệt", _product.Status);
        }
        [Fact]
        public async Task RefuseProgram_Success()
        {
            var request = new ApproveProgramRequest()
            {
                AccountId = 61,
                ProgramId = 17,
                Approved = false,
                Comment = "Unit Test of Approve Program",
                ApprovalDate = DateTime.UtcNow,
            };

            var _product = await _myDbContext.Programs.SingleOrDefaultAsync(p => p.ProgramId == 17);
            var respose = await _reviewProgram.ApproveProgram(request);
            Assert.False(respose.Approved);
            Assert.Equal("Từ chối", _product.Status);
        }
        [Fact]
        public async Task RefuseProgram_Fail_CommentIsBlank()
        {
            var request = new ApproveProgramRequest()
            {
                AccountId = 61,
                ProgramId = 17,
                Approved = false,
                Comment = "",
                ApprovalDate = DateTime.UtcNow,
            };

            await Assert.ThrowsAsync<BadRequestException>(() => _reviewProgram.ApproveProgram(request));
        }
        #endregion

        #region Get Approved List By Id Program
        [Fact]
        public async Task GetApprovedListByIdProgram_Success()
        {
            var _program = await _myDbContext.Programs.FirstOrDefaultAsync();
            List<ReviewerProgram> _listApproved = await _myDbContext.ReviewsProgram.Where(rp => rp.ProgramId == _program.ProgramId).ToListAsync();

            var respose = await _reviewProgram.GetApprovedListByIdProgram(_program.ProgramId);

            Assert.Equal(_listApproved.Count(), _listApproved.Count());
        }
        #endregion
    }
}
