using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.DoTest.Request;
using CAP_Backend_Source.Modules.DoTest.Services;
using CAP_Backend_Source.Modules.Tests.Service;
using Infrastructure.Exceptions.HttpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP_Backend_UnitTest.Services
{
    public class ManageMyProgram
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private DoTestResposity _doTestResposity = new DoTestResposity(new MyDbContext());

        #region Do Test
        [Fact]
        public async Task SaveAnswer_Success()
        {
            var input = new List<DoTestRequest> { 
                new DoTestRequest() {QuestionContentId = 559, QuestionId =123 },
                new DoTestRequest() {QuestionContentId = 563, QuestionId =124 },
                new DoTestRequest() {QuestionContentId = 564, QuestionId =125 },
                new DoTestRequest() {QuestionContentId = 568, QuestionId =126 },
                new DoTestRequest() {QuestionContentId = 573, QuestionId =127 }
            };
            var respone = await _doTestResposity.SaveAnswer(125,input);

            Assert.Equal("Save Answer Success", respone);
        }

        [Fact]
        public async Task SaveAnswer_Fail_QuestionIsNotFound()
        {
            var input = new List<DoTestRequest> {
                new DoTestRequest() {QuestionContentId = 559, QuestionId =100 },
                new DoTestRequest() {QuestionContentId = 563, QuestionId =124 },
                new DoTestRequest() {QuestionContentId = 564, QuestionId =125 },
                new DoTestRequest() {QuestionContentId = 568, QuestionId =126 },
                new DoTestRequest() {QuestionContentId = 573, QuestionId =127 }
            };

            await Assert.ThrowsAsync<BadRequestException>(() => _doTestResposity.SaveAnswer(125, input));
        }
        #endregion
    }
}
