using CAP_Backend_Source.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CAP_Backend_Source.Modules.ReviewProgram.Service
{
    public class ReviewProgramResposity : IReviewProgramService
    {
        private readonly MyDbContext _myDbContext;
        public ReviewProgramResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<List<Models.Program>> GetListPrograms()
        {
            List<Models.Program> _listPrograms = await _myDbContext.Programs.Where(p => p.Status == "Chờ duyệt").ToListAsync();

            return _listPrograms;
        }

        public async Task<List<Models.Program>> GetProgramsByIdReviewer(int id)
        {
            List<Models.Program> _listPrograms = new List<Models.Program>();
            List<Models.Program> _listReviewer = await _myDbContext.Programs.Where(p => p.AccountIdCreator == id).ToListAsync();
            foreach (var program in _listReviewer)
            {
                
            }
            throw new NotImplementedException();
        }
    }
}
