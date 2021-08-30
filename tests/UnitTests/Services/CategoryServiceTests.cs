
using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.Concrete;
using ProductManagementSample.DataAccess.Concrete.AdoNet;
using ProductManagementSample.Entities.Concrete;
using System;
using Xunit;

namespace UnitTests.Services
{
    public class CategoryServiceTests
    {
        ICategoryService _categoryService;
        public CategoryServiceTests()
        {
            _categoryService = new CategoryManager(new AdoNetCategoryDal());
        }

        [Fact]
        public void GetById()
        {
            var expected = new Category() { Id = 1, CategoryName = "Beverages" };

            var actual = _categoryService.GetById(expected.Id).Data;

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.CategoryName, actual.CategoryName);
        }

        [Fact]
        public void GetByCategoryName()
        {
            var expected = new Category() { Id = 1, CategoryName = "Beverages" };

            var actual = _categoryService.GetByCategoryName(expected.CategoryName).Data;

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.CategoryName, actual.CategoryName);
        }
    }
}
