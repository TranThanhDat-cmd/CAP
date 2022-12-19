using Azure.Core;
using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Question.Service;
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
            if (request.ContentId <= 0)
            {
                throw new BadRequestException("ProgramId cannot be null");
            }

            if(request.TestTitle == null || request.TestTitle == "")
            {
                throw new BadRequestException("TestTitle cannot be left blank");
            }

            if (request.Chapter <= 0)
            {
                throw new BadRequestException("Chapter cannot be null");
            }

            if (request.IsRandom == null)
            {
                request.IsRandom = false;
            }
            #endregion

            //action
            var test = new Test() {
                ContentId = request.ContentId,
                TestTitle = request.TestTitle,
                Time = request.Time,
                Chapter = request.Chapter,
                IsRandom = request.IsRandom,
            };
            await _myDbContext.Tests.AddAsync(test);
            var _content = await _myDbContext.ContentPrograms.SingleOrDefaultAsync(cp => cp.ContentId == request.ContentId);
            var _program = await _myDbContext.Programs.SingleOrDefaultAsync(p => p.ProgramId == _content.ProgramId);
            if (_program != null)
            {
                _program.Status = "save";
            }
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
            var _questionService = new QuestionResposity(_myDbContext);
            List<Models.Question> listQuestions = await _myDbContext.Questions.Where(q => q.TestsId == id).ToListAsync();
            foreach (var _question in listQuestions)
            {
                await _questionService.DeleteQuestion(_question.QuestionId);
            }
            _myDbContext.Tests.Remove(_test);
            var _content = await _myDbContext.ContentPrograms.SingleOrDefaultAsync(cp => cp.ContentId == _test.ContentId);
            var _program = await _myDbContext.Programs.SingleOrDefaultAsync(p => p.ProgramId == _content.ProgramId);
            if (_program != null)
            {
                _program.Status = "save";
            }
            await _myDbContext.SaveChangesAsync();
            return "Successful Delete";
        }

        public async Task<Test> GetTestByContentId(int id)
        {
            var _test = await _myDbContext.Tests.SingleOrDefaultAsync(t => t.ContentId == id);

            if (_test == null)
            {
                throw new BadRequestException("Test not found");
            }
            return _test;
        }

        public async Task<Test> UpdateTest(int id, UpdateTestRequest request)
        {
            #region check input
            if (request.TestTitle == null || request.TestTitle == "")
            {
                throw new BadRequestException("TestTitle cannot be left blank");
            }

            if(request.IsRandom == null)
            {
                request.IsRandom = false;
            }
            #endregion check input

            //action
            var _test = await _myDbContext.Tests.SingleOrDefaultAsync(t => t.TestId == id);
            if (_test == null)
            {
                throw new BadRequestException("Test not found");
            }
            _test.TestTitle = request.TestTitle;
            _test.Time = request.Time;
            _test.IsRandom= request.IsRandom;
            var _content = await _myDbContext.ContentPrograms.SingleOrDefaultAsync(cp => cp.ContentId == _test.ContentId);
            var _program = await _myDbContext.Programs.SingleOrDefaultAsync(p => p.ProgramId == _content.ProgramId);
            if(_program != null)
            {
                _program.Status = "save";
            }
            await _myDbContext.SaveChangesAsync();
            return _test;
        }
    }
}
