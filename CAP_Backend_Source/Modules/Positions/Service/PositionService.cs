using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.Programs.Service
{
    public interface IPositionService
    {
        Task<List<Position>> GetAsync();
        

    }

    public class PositionService : IPositionService
    {
        private MyDbContext _myDbContext;
        private readonly IFileStorageService _fileStorageService;

        public PositionService(MyDbContext myDbContext, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _myDbContext = myDbContext;
        }

        async Task<List<Position>> IPositionService.GetAsync()
        {
            return await _myDbContext.Positions.ToListAsync();
        }
    }

}
