using CAP_Backend_Source.Models;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;
using static CAP_Backend_Source.Modules.Question.Request.QuestionRequest;

namespace CAP_Backend_Source.Modules.Question.Service
{
    public class QuestionResposity : IQuestionService
    {
        private MyDbContext _myDbContext;
        public QuestionResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<int> CreateQuestion(CreateQuestionRequest request)
        {
            #region Check Input
            if(request.TestsId <= 0 || request.TestsId == null)
            {
                throw new BadRequestException("TestsId cannot be null");
            }

            if(request.TypeId <= 0 || request.TypeId == null)
            {
                throw new BadRequestException("TypeId cannot be null");
            }

            if(request.QuestionTitle == null || request.QuestionTitle.Length == 0)
            {
                throw new BadRequestException("QuestionTitle cannot be left blank");
            }

            if(request.Score <= 0 || request.Score == null)
            {
                throw new BadRequestException("Score cannot be null");
            }
            #endregion
            var question = new Models.Question()
            {
                TestsId = request.TestsId,
                TypeId = request.TypeId,
                QuestionTitle = request.QuestionTitle,
                Score = request.Score 
            };
            await _myDbContext.Questions.AddAsync(question);
            await _myDbContext.SaveChangesAsync();
            return question.QuestionId;
        }

        public async Task<string> CreateQuestionContent(int id, CreateQuestionRequest request)
        {
            var _question = await _myDbContext.Questions.SingleOrDefaultAsync(q => q.QuestionId == id);
            #region Check Input
            if (_question == null)
            {
                throw new BadRequestException("Question not found");
            }
            #endregion

            if(request.questionContents != null)
            {
                foreach (var question in request.questionContents)
                {
                    #region Check Input
                    if (question.Content == null || question.Content == "")
                    {
                        throw new BadRequestException("Content cannot be left blank");
                    }

                    if (question.IsAnswer == null)
                    {
                        question.IsAnswer = false;
                    }
                    #endregion
                    var content = new QuestionContent()
                    {
                        QuestionId = id,
                        Content = question.Content,
                        IsAnswer = question.IsAnswer,
                    };
                    await _myDbContext.QuestionContents.AddAsync(content);
                    await _myDbContext.SaveChangesAsync();
                }
            }
            return "Successfully added question";
        }

        public async Task<string> DeleteQuestion(int id)
        {
            var _question = await _myDbContext.Questions.SingleOrDefaultAsync(q => q.QuestionId == id);
            #region Check Input
            if (_question == null)
            {
                throw new BadRequestException("Question not found");
            }
            #endregion
            List<QuestionContent> _listQC = await _myDbContext.QuestionContents.Where(qc => qc.QuestionId == id).ToListAsync();
            _myDbContext.QuestionContents.RemoveRange(_listQC);
            await _myDbContext.SaveChangesAsync();

            _myDbContext.Questions.Remove(_question);
            await _myDbContext.SaveChangesAsync();
            return "Successfully deleted question";
        }

        public async Task<List<Models.Question>> GetListQuestionByTestId(int id)
        {
            List<Models.Question> _listQuestions = await _myDbContext.Questions.Where(q => q.TestsId == id).Include(q => q.QuestionContents).ToListAsync();
            if(_listQuestions == null)
            {
                throw new BadRequestException("TestsId not found");
            }
            return _listQuestions;
        }

        public async Task<string> UpdateQuestion(int id, UpdateQuestionRequest request)
        {
            var _question = await _myDbContext.Questions.SingleOrDefaultAsync(q => q.QuestionId == id);
            #region Check Input
            if (_question == null)
            {
                throw new BadRequestException("Question not found");
            }

            if (request.TypeId <= 0 || request.TypeId == null)
            {
                throw new BadRequestException("TypeId cannot be null");
            }

            if (request.QuestionTitle == null || request.QuestionTitle == "")
            {
                throw new BadRequestException("QuestionTitle cannot be left blank");
            }

            if (request.Score <= 0 || request.Score == null)
            {
                throw new BadRequestException("Score cannot be null");
            }
            #endregion
            _question.TypeId = request.TypeId;
            _question.QuestionTitle = request.QuestionTitle;
            _question.Score = request.Score;
            if (request.questionContents != null)
            {
                List<QuestionContent> listQC = await _myDbContext.QuestionContents.Where(qc => qc.QuestionId == id).OrderBy(qc => qc.QuestionContentId).ToListAsync();
                if(listQC != null && listQC.Count > 0)
                {
                    int i = 0;
                    foreach (var question in request.questionContents)
                    {
                        #region Check Input
                        if (question.Content == null || question.Content.Length == 0)
                        {
                            throw new BadRequestException("QuestionTitle cannot be left blank");
                        }

                        if (question.IsAnswer == null)
                        {
                            question.IsAnswer = false;
                        }
                        #endregion
                        listQC[i].Content = question.Content;
                        listQC[i].IsAnswer = question.IsAnswer;
                        i++;
                    }
                }
                else
                {
                    foreach (var question in request.questionContents)
                    {
                        var content = new QuestionContent()
                        {
                            QuestionId = id,
                            Content = question.Content,
                            IsAnswer = question.IsAnswer,
                        };
                        await _myDbContext.QuestionContents.AddAsync(content);
                    }
                }
            }
            await _myDbContext.SaveChangesAsync();
            return "Successfully edited the question";
        }
    }
}
