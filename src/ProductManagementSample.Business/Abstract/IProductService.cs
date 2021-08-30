using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.Entities.Concrete;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Abstract
{
    public interface IProductService
    {
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
        IDataResult<Product> GetById(int id);
        IDataResult<ProductDetailDto> GetProductDetailsById(int id);
        IDataResult<Product> GetByProductName(string productName);
        IDataResult<List<Product>> GetByPrice(decimal minPrice, decimal maxPrice);
        IDataResult<List<Product>> GetByStock(int minStock, int maxStock);
        
        IDataResult<List<ProductDetailDto>> GetAllProductDetails();
        IDataResult<List<ProductDetailDto>> GetAllProductDetailsByCategoryId(int categoryId);
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);

    }
}
