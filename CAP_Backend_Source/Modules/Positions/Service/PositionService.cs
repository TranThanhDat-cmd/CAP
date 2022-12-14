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
        Task<Position> CreateAsync(string name);
        Task<Position?> DetailAsync(int id);
        Task<Position> UpdateAsync(int id, string name);
        Task DeleteAsync(int id);

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

        public async Task<Position> CreateAsync(string name)
        {
            var Position = new Position()
            {
                PositionName = name
            };
            await _myDbContext.Positions.AddAsync(Position);
            await _myDbContext.SaveChangesAsync();
            return Position;
        }

        public async Task<Position?> DetailAsync(int id)
        {
            return await _myDbContext.Positions.Where(x => x.PositionId == id).FirstOrDefaultAsync();
        }

        public async Task<Position> UpdateAsync(int id, string name)
        {
            var Position = await _myDbContext.Positions.Where(x => x.PositionId == id).FirstOrDefaultAsync();
            if (Position == null)
            {
                throw new BadRequestException("Id not found");
            }
            Position.PositionName = name;
            await _myDbContext.SaveChangesAsync();
            return Position;
        }

        public async Task DeleteAsync(int id)
        {
            var Position = await _myDbContext.Positions.Where(x => x.PositionId == id).FirstOrDefaultAsync();
            if (Position == null)
            {
                throw new BadRequestException("Id not found");
            }
            _myDbContext.Positions.Remove(Position);
            await _myDbContext.SaveChangesAsync();
        }
    }

}
