using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.EssayQuestion.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.EssayQuestion.Request.EQuestionRequest;

namespace CAP_Backend_Source.Modules.EssayQuestion.Service
{
    public class EQuestionResposity : IEQuestionService
    {
        private MyDbContext _myDbContext;
        public EQuestionResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<Models.EssayQuestion> CreateQuestion(CreateEQuestionRequest request)
        {
            #region Check input
            if(request.TestsId <= 0)
            {
                throw new BadRequestException("Test Id not null");
            }

            if(request.EquestionTitle == null || request.EquestionTitle == "")
            {
                throw new BadRequestException("Title cannot left blank");
            }
            #endregion
            //action
            var question = new Models.EssayQuestion
            {
                TestsId = request.TestsId,
                EquestionTitle = request.EquestionTitle
            };
            await _myDbContext.EssayQuestions.AddAsync(question);
            await _myDbContext.SaveChangesAsync();
            return question;
        }

        public async Task<string> DeleteQuestion(int id)
        {
            var _question = await _myDbContext.EssayQuestions.SingleOrDefaultAsync(q => q.EquestionId == id);
            if(_question != null )
            {
                throw new BadRequestException("Question not found");
            }
            _myDbContext.EssayQuestions.Remove(_question);
            await _myDbContext.SaveChangesAsync();
            return "Successful Delete";
        }

        public async Task<Models.EssayQuestion> GetQuestionById(int id)
        {
            var _question = await _myDbContext.EssayQuestions.SingleOrDefaultAsync(q => q.EquestionId == id);
            if(_question == null)
            {
                throw new BadRequestException("Question not found");
            }
            return _question;
        }

        public async Task<List<Models.EssayQuestion>> GetQuestionByTestId(int id)
        {
            List<Models.EssayQuestion> _listQuestion = await _myDbContext.EssayQuestions.Where(q => q.TestsId == id).ToListAsync();
            return _listQuestion;
        }

        public async Task<Models.EssayQuestion> UpdateQuestion(int id, UpdateEQuestionRequest request)
        {
            #region Check input
            if (request.EquestionTitle == null || request.EquestionTitle == "")
            {
                throw new BadRequestException("Title cannot left blank");
            }
            #endregion
            //action
            var _question = await _myDbContext.EssayQuestions.SingleOrDefaultAsync(q => q.EquestionId.Equals(id));
            if (_question == null)
            {
                throw new BadRequestException("Question not found");
            }
            _question.EquestionTitle = request.EquestionTitle;
            await _myDbContext.SaveChangesAsync();
            return _question;
        }
    }
}
