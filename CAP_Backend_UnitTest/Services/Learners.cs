using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Learners.Services;
using CAP_Backend_Source.Modules.Programs.Service;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP_Backend_UnitTest.Services
{
    public class Learners
    {
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
    }
}
