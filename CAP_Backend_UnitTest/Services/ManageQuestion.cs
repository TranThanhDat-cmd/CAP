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
            var respone = await _questionResposity.CreateQuestionContent(122, request);
            Assert.IsType<String>(respone);
            Assert.Equal("Successfully added question", respone);
        }
        [Fact]
        public async Task CreateQuestionContent_Fail_QuestionIdIsNull()
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
            var _quesiton = await _myDbContext.Questions.FirstOrDefaultAsync();
            var respone = await _questionResposity.DeleteQuestion(_quesiton.QuestionId);
            Assert.IsType<string>(respone);
            Assert.Equal("Successfully deleted question", respone);
        }
        [Fact]
        public async Task DeleteQuestion_Fail_IdQuestionIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.DeleteQuestion(0));
        }
        #endregion

        #region Delete Question Content
        [Fact]
        public async Task DeleteQuestionContent_Success()
        {
            var respone = await _questionResposity.DeleteQuestionContent(552);
            Assert.IsType<string>(respone);
            Assert.Equal("Successfully deleted question content", respone);
        }
        [Fact]
        public async Task DeleteQuestionContent_Fail_IdQuestionIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.DeleteQuestionContent(0));
        }
        #endregion

        #region Get List Question By Test Id
        [Fact]
        public async Task GetListQuestionByTestId_Success()
        {
            var respone = await _questionResposity.GetListQuestionByTestId(40);
            List<Question> _listQuestions = await _myDbContext.Questions.Where(q => q.TestsId == 40).Include(q => q.QuestionContents).ToListAsync();
            Assert.Equal(_listQuestions.Count(), respone.Count());
        }
        #endregion

        #region Update Question
        [Fact]
        public async Task UpdateQuestion_Success()
        {
            var _listQuestionContents = new List<UpdateQuestionContentRequest>()
            {
                new UpdateQuestionContentRequest() { QuestionContentId = 552, Content = "Unit Test of Update Content A", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 553, Content = "Unit Test of Update Content B", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 554, Content = "Unit Test of Update Content C", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 555, Content = "Unit Test of Update Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Update Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            var respone = await _questionResposity.UpdateQuestion(122, request);
            Assert.IsType<string>(respone);
            Assert.Equal("Successfully edited the question", respone);
        }
        [Fact]
        public async Task UpdateQuestion_Fail_IdQuestionIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<UpdateQuestionContentRequest>()
            {
                new UpdateQuestionContentRequest() { QuestionContentId = 552, Content = "Unit Test of Update Content A", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 553, Content = "Unit Test of Update Content B", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 554, Content = "Unit Test of Update Content C", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 555, Content = "Unit Test of Update Content D", IsAnswer = false }
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
            var _listQuestionContents = new List<UpdateQuestionContentRequest>()
            {
                new UpdateQuestionContentRequest() { QuestionContentId = 552, Content = "Unit Test of Update Content A", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 553, Content = "Unit Test of Update Content B", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 554, Content = "Unit Test of Update Content C", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 555, Content = "Unit Test of Update Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(122, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_QuestionTitleIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<UpdateQuestionContentRequest>()
            {
                new UpdateQuestionContentRequest() { QuestionContentId = 552, Content = "Unit Test of Update Content A", IsAnswer = false } ,
                new UpdateQuestionContentRequest() { QuestionContentId = 553, Content = "Unit Test of Update Content B", IsAnswer = false } ,
                new UpdateQuestionContentRequest() { QuestionContentId = 554, Content = "Unit Test of Update Content C", IsAnswer = false } ,
                new UpdateQuestionContentRequest() { QuestionContentId = 555, Content = "Unit Test of Update Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(122, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_ScoreIsNull()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<UpdateQuestionContentRequest>()
            {
                new UpdateQuestionContentRequest() { QuestionContentId = 552, Content = "Unit Test of Update Content A", IsAnswer = false } ,
                new UpdateQuestionContentRequest() { QuestionContentId = 553, Content = "Unit Test of Update Content B", IsAnswer = false } ,
                new UpdateQuestionContentRequest() { QuestionContentId = 554, Content = "Unit Test of Update Content C", IsAnswer = false } ,
                new UpdateQuestionContentRequest() { QuestionContentId = 555, Content = "Unit Test of Update Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Create Question",
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(122, request));
        }
        [Fact]
        public async Task UpdateQuestion_Fail_ContentIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var _listQuestionContents = new List<UpdateQuestionContentRequest>()
            {
                new UpdateQuestionContentRequest() { QuestionContentId = 552, Content = "", IsAnswer = false },
                new UpdateQuestionContentRequest() { QuestionContentId = 553, Content = "", IsAnswer = false } , 
                new UpdateQuestionContentRequest() { QuestionContentId = 554, Content = "Unit Test of Content C", IsAnswer = false } , 
                new UpdateQuestionContentRequest() { QuestionContentId = 555, Content = "Unit Test of Content D", IsAnswer = false }
            };
            var request = new UpdateQuestionRequest()
            {
                TypeId = 1,//test.id
                QuestionTitle = "Unit Test of Create Question",
                Score = 1,
                questionContents = _listQuestionContents
            };
            await Assert.ThrowsAsync<BadRequestException>(() => _questionResposity.UpdateQuestion(122, request));
        }
        #endregion
    }
}
