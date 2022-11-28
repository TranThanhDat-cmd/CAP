using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Question.Service;
using CAP_Backend_Source.Modules.Tests.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static CAP_Backend_Source.Modules.Question.Request.QuestionRequest;

namespace CAP_Backend_UnitTest.Services
{
    public class ManageQuestion
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private QuestionResposity _questionResposity = new QuestionResposity(new MyDbContext());

        #region Create Question
        [Fact]
        public async Task CreateQuestion_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync(); 
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = _test.TestId,
                TypeId = 1,
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            var respone = await _questionResposity.CreateQuestion(request);
            Assert.IsType<int>(respone);
        }
        [Fact]
        public async Task CreateQuestion_Fail_TestsIdIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = true },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TypeId = 1,
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.CreateQuestion(request));
        }
        [Fact]
        public async Task CreateQuestion_Fail_TypeIdIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = true },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = 1,
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.CreateQuestion(request));
        }
        [Fact]
        public async Task CreateQuestion_Fail_QuestionTitleIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = 1,
                TypeId = 1,
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.CreateQuestion(request));
        }
        [Fact]
        public async Task CreateQuestion_Fail_ScoreIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = 1,
                TypeId = 1,
                QuestionTitle = "Unit Test of Create Question",
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.CreateQuestion(request));
        }
        #endregion

        #region Create Question Content
        [Fact]
        public async Task CreateQuestionContent_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = _test.TestId,
                TypeId = 1,
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            var respone = await _questionResposity.CreateQuestionContent(_test.TestId , request);
            Assert.IsType<String>(respone);
            Assert.Equal("Successfully added question", respone);
        }
        [Fact]
        public async Task CreateQuestionContent_Fail_TestsIdIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = 1,
                TypeId = 1,
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.CreateQuestionContent(0, request));
        }
        [Fact]
        public async Task CreateQuestionContent_Fail_ContentIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "", IsAnswer = false }
            };
            var request = new CreateQuestionRequest()
            {
                TestsId = 1,
                TypeId = 1,
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.CreateQuestionContent(1, request));
        }
        #endregion

        #region Delete Question
        [Fact]
        public async Task DeleteQuestion_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var respone = await _questionResposity.DeleteQuestion(1);
            Assert.IsType<string>(respone);
            Assert.Equal("Successfully deleted question", respone);
        }
        [Fact]
        public async Task DeleteQuestion_Fail_IdQuestionIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.DeleteQuestion(0));
        }
        #endregion

        #region Get List Question By Test Id
        [Fact]
        public async Task GetListQuestionByTestId_Success()
        {
            var respone = await _questionResposity.GetListQuestionByTestId(1);
            List<Question> _listQuestions = await _myDbContext.Questions.Where(q => q.TestsId == 1).Include(q => q.QuestionContents).ToListAsync();
            Assert.Equal(_listQuestions.Count(), respone.Count());
        }
        #endregion

        #region Update Question
        [Fact]
        public async Task UpdateQuestion_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            var respone = await _questionResposity.UpdateQuestion(_test.TestId, request);
            Assert.IsType<string>(respone);
            Assert.Equal("Successfully edited the question", respone);
        }
        [Fact]
        public async Task UpdateQuestion_Fail_IdQuestionIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(0, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_TypeIdIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(1, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_QuestionTitleIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(1, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_ScoreIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content A", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content B", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Create Question",
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(1, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_ContentIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<QuestionContentRequest>()
            {
                new QuestionContentRequest() { QuestionId = 1, Content = "", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content C", IsAnswer = false },
                new QuestionContentRequest() { QuestionId = 1, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(0, request));
        }
        #endregion
    }
}
