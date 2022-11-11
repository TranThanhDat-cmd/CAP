using static CAP_Backend_Source.Modules.Category.Request.CategoryRequest;

namespace CAP_Backend_Source.Modules.Category.Services
{
    public interface ICategoryService
    {
        Task<List<Models.Category>> GetAllCategory();
        Task<Models.Category> CreateCategory(CreateCategoryRequest request);
        Task<Models.Category> UpdateCategory(int id, EditCategoryRequest request);
        Task<String> DeleteCategory(int id);
    }
}
