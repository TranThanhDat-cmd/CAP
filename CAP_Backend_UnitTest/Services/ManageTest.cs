using Azure.Core;
using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Category.Services;
using CAP_Backend_Source.Modules.Question.Service;
using CAP_Backend_Source.Modules.Tests.Request;
using CAP_Backend_Source.Modules.Tests.Service;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CAP_Backend_Source.Modules.Category.Request.CategoryRequest;
using static CAP_Backend_Source.Modules.Tests.Request.TestRequest;

namespace CAP_Backend_UnitTest.Services
{
    public class ManageTest
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private TestResposity testResposity = new TestResposity(new MyDbContext());

        #region Create Test
        [Fact]
        public async Task CreateTest_Success()
        {
            var request = new CreateTestRequest()
            {
                ContentId = 1,
                TestTitle = "Unit Test of Create Test",
                Time = 5,
                Chapter = 1,
                IsRandom = false,
            };
            var respose = await testResposity.CreateTest(request);

            Assert.NotNull(respose);
            Assert.Equal(1, respose.ContentId);
            Assert.Equal(request.TestTitle, respose.TestTitle);
            Assert.Equal(5, respose.Time);
            Assert.True(respose.Chapter == 1);
            Assert.False(respose.IsRandom);
        }

        [Fact]
        public async Task CreateTest_Fail_ContentIdDoesNotExist()
        {
            var request = new CreateTestRequest()
            {
                TestTitle = "Unit Test of Create Test",
                Time = 5,
                Chapter = 1,
                IsRandom = false,
            };

            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.CreateTest(request));
        }
        [Fact]
        public async Task CreateTest_Fail_TestTitleIsBlank()
        {
            var request = new CreateTestRequest()
            {
                ContentId = 1,
                Time = 5,
                Chapter = 1,
                IsRandom = false,
            };

            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.CreateTest(request));
        }
        [Fact]
        public async Task CreateTest_Fail_ChapterIsNull()
        {
            var request = new CreateTestRequest()
            {
                ContentId = 1,
                TestTitle = "Unit Test of Create Test",
                Time = 5,
                IsRandom = false,
            };

            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.CreateTest(request));
        }
        #endregion

        #region Delete Test
        [Fact]
        public async Task DeleteTest_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var respose = await testResposity.DeleteTest(_test.TestId);

            Assert.Equal("Successful Delete", respose);
        }
        [Fact]
        public async Task DeleteTest_Fail_IdDoesNotExist()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.DeleteTest(0));
        }
        #endregion

        #region Get Test By Content Id
        [Fact]
        public async Task GetTestByContentId_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var respose = await testResposity.GetTestByContentId(_test.TestId);

            Assert.Equal(_test.TestId, respose.TestId);
            Assert.Equal(_test.ContentId, respose.ContentId);
            Assert.Equal(_test.TestTitle, respose.TestTitle);
            Assert.Equal(_test.Time, respose.Time);
            Assert.Equal(_test.Chapter, respose.Chapter);
            Assert.Equal(_test.IsRandom, respose.IsRandom);
        }
        [Fact]
        public async Task GetTestByContentId_Fail_IdDoesNotExist()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.GetTestByContentId(0));
        }
        #endregion

        #region Update Test
        [Fact]
        public async Task UpdateTest_Success()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var request = new UpdateTestRequest()
            {
                TestTitle = "Unit Test of Create Test",
                Time = 5,
                IsRandom = false,
            };
            var respose = await testResposity.UpdateTest(_test.TestId, request);

            Assert.NotNull(respose);
            Assert.Equal(request.TestTitle, respose.TestTitle);
            Assert.Equal(5, respose.Time);
            Assert.False(respose.IsRandom);
        }
        [Fact]
        public async Task UpdateTest_Fail_IdDoesNotExist()
        {
            var request = new UpdateTestRequest()
            {
                TestTitle = "Unit Test of Create Test",
                Time = 5,
                IsRandom = false,
            };

            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.UpdateTest(0, request));
        }
        [Fact]
        public async Task UpdateTest_Fail_TestTitleIsBlank()
        {
            var _test = await _myDbContext.Tests.FirstOrDefaultAsync();
            var request = new UpdateTestRequest()
            {
                TestTitle = "",
                Time = 5,
                IsRandom = false,
            };
            await Assert.ThrowsAsync<BadRequestException>(() => testResposity.UpdateTest(_test.TestId, request));
        }
        #endregion
    }
}
