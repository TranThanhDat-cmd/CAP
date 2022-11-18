using CAP_Backend_Source.Models;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using static CAP_Backend_Source.Modules.Tests.Request.TestRequest;

namespace CAP_Backend_Source.Modules.Tests.Service
{
    public class TestResposity : ITestService
    {
        private MyDbContext _myDbContext;
        public TestResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Test> CreateTest(CreateTestRequest request)
        {
            #region check input
            if (request.ProgramId <= 0)
            {
                throw new BadRequestException("ProgramId cannot be null");
            }

            if(request.TestTitle == null || request.TestTitle == "")
            {
                throw new BadRequestException("TestTitle cannot be left blank");
            }

            if (request.TypeId <= 0)
            {
                throw new BadRequestException("TypeId cannot be null");
            }

            if (request.Chapter <= 0)
            {
                throw new BadRequestException("Chapter cannot be null");
            }
            #endregion

            //action
            var test = new Test() {
                ProgramId = request.ProgramId,
                TestTitle = request.TestTitle,
                TypeId = request.TypeId,
                Time = request.Time,
                Chapter = request.Chapter
            };
            await _myDbContext.Tests.AddAsync(test);
            await _myDbContext.SaveChangesAsync();
            return test;
        }

        public async Task<string> DeleteTest(int id)
        {
            var _test = await _myDbContext.Tests.SingleOrDefaultAsync(t => t.TestId == id);
            if (_test == null)
            {
                throw new BadRequestException("Test not found");
            }
            _myDbContext.Tests.Remove(_test);
            await _myDbContext.SaveChangesAsync();
            return "Successful Delete";
        }

        public async Task<List<Test>> GetTestByProgramId(int id)
        {
            List<Test> listTest = await _myDbContext.Tests.Where(t => t.ProgramId == id).ToListAsync();

            return listTest;
        }

        public async Task<Test> UpdateTest(int id, UpdateTestRequest request)
        {
            #region check input
            if (request.TestTitle == null || request.TestTitle == "")
            {
                throw new BadRequestException("TestTitle cannot be left blank");
            }

            if (request.TypeId <= 0)
            {
                throw new BadRequestException("TypeId cannot be null");
            }

            if (request.Chapter <= 0)
            {
                throw new BadRequestException("Chapter cannot be null");
            }
            #endregion check input

            //action
            var _test = await _myDbContext.Tests.SingleOrDefaultAsync(t => t.TestId == id);
            if (_test == null)
            {
                throw new BadRequestException("Test not found");
            }
            _test.TestTitle = request.TestTitle;
            _test.TypeId = request.TypeId;
            _test.Time = request.Time;
            _test.Chapter = request.Chapter;
            await _myDbContext.SaveChangesAsync();
            return _test;
        }
    }
}
