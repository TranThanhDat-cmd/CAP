using CAP_Backend_Source.Models;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.MultipleChoiceQuestion.Request.MCQuestionRequest;

namespace CAP_Backend_Source.Modules.MultipleChoiceQuestion.Service
{
    public class MCQuestionResposity : IMCQuestionService
    {
        private MyDbContext _myDbContext;
        public MCQuestionResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Models.MultipleChoiceQuestion> CreateQuestion(CreateMCQuestionRequest request)
        {
            #region Check input
            if(request.McquestionTitle == null || request.McquestionTitle == "")
            {
                throw new BadRequestException("Title cannot be left blank");
            }
            if (request.Content1 == null || request.Content1 == "")
            {
                throw new BadRequestException("Content1 cannot be left blank");
            }
            if (request.Content2 == null || request.Content2 == "")
            {
                throw new BadRequestException("Content2 cannot be left blank");
            }
            if (request.Answer == null || request.Answer == "")
            {
                throw new BadRequestException("Answer cannot be left blank");
            }
            #endregion
            //action
            var question = new Models.MultipleChoiceQuestion
            {
                TestsId = request.TestsId,
                McquestionTitle = request.McquestionTitle,
                Content1 = request.Content1,
                Content2 = request.Content2,
                Content3 = request.Content3,
                Content4 = request.Content4,
                Answer= request.Answer
            };
            await _myDbContext.MultipleChoiceQuestions.AddAsync(question);
            await _myDbContext.SaveChangesAsync();
            return question;
        }

        public async Task<string> DeleteQuestion(int id)
        {
            var _question = await _myDbContext.MultipleChoiceQuestions.SingleOrDefaultAsync(q => q.McquestionId == id);
            if(_question == null ) 
            {
                throw new BadRequestException("Question not found");
            }
            _myDbContext.MultipleChoiceQuestions.Remove(_question);
            await _myDbContext.SaveChangesAsync();
            return "Successful Delete";
        }

        public async Task<Models.MultipleChoiceQuestion> GetQuestionById(int id)
        {
            var _question = await _myDbContext.MultipleChoiceQuestions.SingleOrDefaultAsync(q => q.McquestionId == id);
            if(_question == null)
            {
                throw new BadRequestException("Question not found");
            }
            return _question;
        }

        public async Task<List<Models.MultipleChoiceQuestion>> GetQuestionByTestId(int id)
        {
            List<Models.MultipleChoiceQuestion> _listQuestion = await _myDbContext.MultipleChoiceQuestions.Where(q => q.TestsId == id).ToListAsync();
            return _listQuestion;
        }

        public async Task<Models.MultipleChoiceQuestion> UpdateQuestion(int id, UpdateMCQuestionRequest request)
        {
            #region Check input
            if (request.Content1 == null || request.Content1 == "")
            {
                throw new BadRequestException("Content1 cannot be left blank");
            }
            if (request.Content2 == null || request.Content2 == "")
            {
                throw new BadRequestException("Content2 cannot be left blank");
            }
            if (request.Answer == null || request.Answer == "")
            {
                throw new BadRequestException("Answer cannot be left blank");
            }
            #endregion
            //action
            var _question = await _myDbContext.MultipleChoiceQuestions.SingleOrDefaultAsync(q => q.McquestionId == id);
            if(_question == null)
            {
                throw new BadRequestException("Question not found");
            }
            _question.McquestionTitle = request.McquestionTitle;
            _question.Content1 = request.Content1;
            _question.Content2 = request.Content2;
            _question.Content3 = request.Content3;
            _question.Content4 = request.Content4;
            _question.Answer = request.Answer;

            await _myDbContext.SaveChangesAsync();
            return _question;
        }
    }
}
