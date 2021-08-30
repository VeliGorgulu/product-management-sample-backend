using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.Concrete;
using ProductManagementSample.DataAccess.Concrete.AdoNet;
using ProductManagementSample.Entities.Concrete;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class ProductServiceTests
    {
        IProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductManager(new AdoNetProductDal());
        }

        [Fact]
        public void GetById()
        {
            var expected = new Product() { Id = 1, CategoryId = 1, ProductName = "Chai", Price = 18, Stock = 39 };

            var actual = _productService.GetById(expected.Id).Data;

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.CategoryId, actual.CategoryId);
            Assert.Equal(expected.ProductName, actual.ProductName);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Stock, actual.Stock);
        }

        [Fact]
        public void GetProductDetailsById()
        {
            var expected = new ProductDetailDto() { Id = 1, ProductName = "Chai", CategoryName = "Beverages", Price = 18, Stock = 39 };

            var actual = _productService.GetProductDetailsById(expected.Id).Data;

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.CategoryName, actual.CategoryName);
            Assert.Equal(expected.ProductName, actual.ProductName);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Stock, actual.Stock);
        }

        [Fact]
        public void GetByProductName()
        {
            var expected = new Product() { Id = 1, CategoryId = 1, ProductName = "Chai", Price = 18, Stock = 39 };

            var actual = _productService.GetByProductName(expected.ProductName).Data;

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.CategoryId, actual.CategoryId);
            Assert.Equal(expected.ProductName, actual.ProductName);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Stock, actual.Stock);
        }
    }
}
