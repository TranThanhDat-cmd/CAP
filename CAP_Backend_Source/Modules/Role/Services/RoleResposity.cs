using CAP_Backend_Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.Role.Services
{
    public class RoleResposity : IRoleService
    {
        private MyDbContext _myDbContext;

        public RoleResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<List<Models.Role>> GetAll()
        {
            List<Models.Role> listRoles = await _myDbContext.Roles.ToListAsync();
            return listRoles;
        }
    }
}
