using CAP_Backend_Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.TypeTest.Service
{
    public class TypeTestResposity : ITypeTestService
    {
        private MyDbContext _myDbContext;
        public TypeTestResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<List<Models.QuestionType>> GetAll()
        {
            var list = await _myDbContext.QuestionTypes.ToListAsync();
            return list;
        }
    }
}
