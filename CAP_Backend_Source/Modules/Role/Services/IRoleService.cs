namespace CAP_Backend_Source.Modules.Role.Services
{
    public interface IRoleService
    {
        Task<List<Models.Role>> GetAll();
    }
}
