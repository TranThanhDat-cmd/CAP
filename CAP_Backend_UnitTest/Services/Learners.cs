using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Learners.Services;
using CAP_Backend_Source.Modules.Programs.Service;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAP_Backend_Source.Modules.Learners.Requests;

namespace CAP_Backend_UnitTest.Services
{
    public class Learners
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private LearnerServices learnerServices = new LearnerServices(new MyDbContext(), new FileStorageService());

        [Fact]
        public async Task RegisterOrUnRegisterAsyncSuccess()
        {
            await learnerServices.RegisterOrUnRegisterAsync(1, new CAP_Backend_Source.Modules.Learners.Requests.RegisterOrUnRegisterRequest
            {
                IsRegister = true,
                ProgramId = 17,
            });

        }

        [ExpectedException(typeof(System.Exception))]
        [Fact]
        public async Task RegisterOrUnRegisterAsyncFail()
        {
            await learnerServices.RegisterOrUnRegisterAsync(1, new CAP_Backend_Source.Modules.Learners.Requests.RegisterOrUnRegisterRequest
            {
                IsRegister = true,
                ProgramId = 17,
            });
        }

        [Fact]
        public async Task GetApplicationsSuccess()
        {
            await learnerServices.GetApplications();
        }

        [Fact]
        public async Task GetApplicationSuccess()
        {
            await learnerServices.GetApplication(1);
        }

        [Fact]
        [ExpectedException(typeof(System.Exception))]

        public async Task GetApplicationFail()
        {
            await learnerServices.GetApplication(-1000);
        }

        #region Get List Learners
        [Fact]
        public async Task GetListLearners_Success()
        {
            var respone = await learnerServices.GetListLearners(17);

            List<Learner> _listLearner = await _myDbContext.Learners.Where(l => l.IsRegister == false && l.ProgramId == 17).ToListAsync();

            Xunit.Assert.Equal(_listLearner.Count(), respone.Count());
        }
        #endregion

        #region Add Learner
        [Fact]
        public async Task AddLearner_Success()
        {
            var input = new AddLearnerRequest()
            {
                AccountIdApprover = 61,
                AccountIdLearner = 125,
                ProgramId = 41
            };

            var respone = await learnerServices.AddLearner(input);

            Xunit.Assert.Equal(input.AccountIdApprover, respone.AccountIdApprover);
            Xunit.Assert.Equal(input.AccountIdLearner, respone.AccountIdLearner);
            Xunit.Assert.Equal(input.ProgramId, respone.ProgramId);
        }

        [Fact]
        public async Task AddLearner_Fail_LearnersAlreadyExist()
        {
            var input = new AddLearnerRequest()
            {
                AccountIdApprover = 61,
                AccountIdLearner = 125,
                ProgramId = 41
            };

            await Xunit.Assert.ThrowsAsync<BadRequestException>(() => learnerServices.AddLearner(input));
        }
        #endregion

        #region Update Learner
        [Fact]
        public async Task UpdateLearner_Success()
        {
            var input = new UpdateLearnerRequest
            {
                Status = "Ngừng tham gia",
                Comment = "Unit Test of Update Learner"
            };

            var respone = await learnerServices.UpdateLearner(1,input);

            Xunit.Assert.Equal("Update Success", respone);
        }
        [Fact]
        public async Task UpdateLearner_Fail_LearnersDoNotExist()
        {
            var input = new UpdateLearnerRequest
            {
                Status = "Ngừng tham gia",
                Comment = "Unit Test of Update Learner"
            };
            Xunit.Assert.ThrowsAsync<BadRequestException>(() => learnerServices.UpdateLearner(-1, input));
        }
        #endregion
    }
}
