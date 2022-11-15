using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Category.Request;
using static CAP_Backend_Source.Modules.Category.Request.CategoryRequest;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Exceptions.HttpExceptions;

namespace CAP_Backend_Source.Modules.Category.Services
{
    public class CategoryResposity : ICategoryService
    {
        private MyDbContext _myDbContext;
        public CategoryResposity(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Models.Category> CreateCategory(CreateCategoryRequest request)
        {
            if (request.Name == null || request.Name == "")
            {
                throw new BadRequestException("Name cannot be left blank");
            }
            var category = new Models.Category()
            {
                CategoryName = request.Name
            };

            await _myDbContext.Categories.AddAsync(category);
            await _myDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<String> DeleteCategory(int id)
        {
            var _category = await _myDbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);

            if (_category == null)
            {
                throw new BadRequestException("Category not found");
            }
            _myDbContext.Categories.Remove(_category);
            await _myDbContext.SaveChangesAsync();
            return "Successful Delete";
        }

        public async Task<List<Models.Category>> GetAllCategory()
        {
            List<Models.Category> listCategoris = await _myDbContext.Categories.ToListAsync();
            return listCategoris;
        }

        public async Task<Models.Category> UpdateCategory(int id, EditCategoryRequest request)
        {
            if (request.Name == null || request.Name == "")
            {
                throw new BadRequestException("Name cannot be left blank");
            }
            var _category = await _myDbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            if (_category == null)
            {
                throw new BadRequestException("Category not found");
            }
            _category.CategoryName = request.Name;
            await _myDbContext.SaveChangesAsync();

            return _category;
        }
    }
}
