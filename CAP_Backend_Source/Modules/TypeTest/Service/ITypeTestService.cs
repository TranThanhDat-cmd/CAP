namespace CAP_Backend_Source.Modules.TypeTest.Service
{
    public interface ITypeTestService
    {
        Task<List<Models.Type>> GetAll();
    }
}
