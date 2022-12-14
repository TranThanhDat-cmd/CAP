using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Services;
using CAP_Backend_Source.Modules.Category.Services;
using CAP_Backend_Source.Services.User.Request;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CAP_Backend_Source.Modules.Category.Request.CategoryRequest;

namespace CAP_Backend_UnitTest.Services
{
    public class ManageCategory
    {
        private MyDbContext _myDbContext = new MyDbContext();
        private CategoryResposity categoryResposity = new CategoryResposity(new MyDbContext());
        public static int idCategory = 0;

        #region Create Category
        [Fact]
        public async Task CreateCategory_Success()
        {
            var response = await categoryResposity.CreateCategory(new CreateCategoryRequest()
            {
                Name = "Unit Test of Create Category"
            });
            idCategory = int.Parse(response.CategoryId.ToString());
            Assert.Equal("Unit Test of Create Category", response.CategoryName);
        }

        [Fact]
        public async Task CreateCategory_Fail_NameIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => categoryResposity.CreateCategory(new CreateCategoryRequest()
            {
                Name = ""
            }));
        }
        #endregion

        #region Update Category
        [Fact]
        public async Task UpdateCategory_Success()
        {
            var _category = await _myDbContext.Categories.FirstAsync();

            var response = await categoryResposity.UpdateCategory(_category.CategoryId, new EditCategoryRequest()
            {
                Name = "Unit Test of Edit Category"
            });

            Assert.Equal("Unit Test of Edit Category", response.CategoryName);
        }

        [Fact]
        public async Task UpdateCategory_Fail_IdDoesNotExist()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => categoryResposity.UpdateCategory(0, new EditCategoryRequest()
            {
                Name = "Unit Test of Edit Category"
            }));
        }

        [Fact]
        public async Task UpdateCategory_Fail_NameIsNull()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => categoryResposity.UpdateCategory(idCategory, new EditCategoryRequest()
            {
                Name = ""
            }));
        }

        #endregion

        #region Delete Category
        [Fact]
        public async Task DeleteCategory_Success()
        {
            var _category = await _myDbContext.Categories.FirstAsync();
            var response = await categoryResposity.DeleteCategory(_category.CategoryId);

            Assert.Equal("Successful Delete", response);
        }

        [Fact]
        public async Task DeleteCategory_Fail_IdDoesNotExist()
        {
            await Assert.ThrowsAsync<BadRequestException>(() => categoryResposity.DeleteCategory(0));
        }
        #endregion

        #region Get All Category
        [Fact]
        public async Task GetAllCategory_Success()
        {
            var response = await categoryResposity.GetAllCategory();
            var listCategory = _myDbContext.Categories.ToList();
            Assert.Equal(response.Count(), listCategory.Count());
        }
        #endregion
    }
}
