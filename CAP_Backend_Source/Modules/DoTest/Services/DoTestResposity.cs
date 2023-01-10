using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.DoTest.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.DoTest.Services
{
    public class DoTestResposity : IDoTestService
    {
        private MyDbContext _myDbContext;
        public DoTestResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<string> SaveAnswer(int idAccount, List<DoTestRequest> requests)
        {
            double? score = 0;
            var question = await _myDbContext.Questions.FirstOrDefaultAsync(q => q.QuestionId == requests[0].QuestionId);
            if(question == null)
            {
                throw new BadRequestException("Question is not found");
            }
            var _test = await _myDbContext.Tests.SingleOrDefaultAsync(t => t.TestId == question.TestsId);
            if(_test == null)
            {
                throw new BadRequestException("Test is not found");
            }
            foreach(DoTestRequest answer in requests)
            {
                var _question = await _myDbContext.Questions.FirstOrDefaultAsync(q => q.QuestionId == answer.QuestionId);
                var _questionContent = await _myDbContext.QuestionContents.FirstOrDefaultAsync(qc => qc.QuestionContentId == answer.QuestionContentId);
                var _checkAnswer = await _myDbContext.Answers.FirstOrDefaultAsync(a => a.QuestionContentId == answer.QuestionContentId && a.AccountIdRespondent == idAccount);
                if(_checkAnswer != null) 
                {
                    continue;
                }
                
                if(_question.TypeId == 1)
                {
                    var _answer = new Answer()
                    {
                        QuestionId = answer.QuestionId,
                        AccountIdRespondent = idAccount,
                        QuestionContentId = answer.QuestionContentId,
                    };

                    await _myDbContext.Answers.AddAsync(_answer);
                    await _myDbContext.SaveChangesAsync();

                    if (_questionContent != null && _questionContent.IsAnswer == true && _question != null)
                    {
                        score = score + _question.Score;
                    }
                }
                else
                {
                    int count = 0;
                    List<DoTestRequest> listAnswer = requests.Where(r => r.QuestionId == _question.QuestionId).ToList();
                    List<QuestionContent> _check = await _myDbContext.QuestionContents.Where(qc => qc.QuestionId == _question.QuestionId && qc.IsAnswer == true).ToListAsync();
                    foreach(var item in listAnswer)
                    {
                        var _answer = new Answer()
                        {
                            QuestionId = item.QuestionId,
                            AccountIdRespondent = idAccount,
                            QuestionContentId = item.QuestionContentId,
                        };

                        await _myDbContext.Answers.AddAsync(_answer);
                        await _myDbContext.SaveChangesAsync();

                        foreach (var check in _check)
                        {
                            if(item.QuestionContentId == check.QuestionContentId)
                            {
                                count ++;
                                break;
                            }
                        }
                    }

                    score = score + (_question.Score*(count/_check.Count));
                }
                
            }
            await SaveResultTest(_test.TestId, idAccount, score);
            return "Save Answer Success";
        }

        public async Task<ResultTest> SaveResultTest(int idTest, int idAccount, double? score)
        {
            var _resultTest = new ResultTest()
            {
                AccountId = idAccount,
                TestId = idTest,
                Score = score
            };

            await _myDbContext.ResultTests.AddAsync(_resultTest);
            await _myDbContext.SaveChangesAsync();
            return _resultTest;
        }
    }
}
