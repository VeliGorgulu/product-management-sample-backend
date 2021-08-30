using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.BusinessAspects.Autofac;
using ProductManagementSample.Business.ValidationRules.FluentValidation;
using ProductManagementSample.Core.Aspects.Autofac.Caching;
using ProductManagementSample.Core.Aspects.Autofac.Validation;
using ProductManagementSample.Core.Utilities.Business;
using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.DataAccess.Abstract;
using ProductManagementSample.Entities.Concrete;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Concrete
{
    [SecuredOperation("admin,product.admin")]
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfProductNameExists(product.ProductName)
            );

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult();
        }

        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfProductNameExists(product.ProductName)
            );

            if (result != null)
            {
                return result;
            }

            _productDal.Update(product);

            return new SuccessResult();
        }
  
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfIdExists(product.Id)
            );

            if (result != null)
            {
                return result;
            }

            _productDal.Delete(product);

            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            var data = _productDal.GetAll();

            return new SuccessDataResult<List<Product>>(data);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            var data = _productDal.GetAll($"CategoryId = {categoryId}");

            return new SuccessDataResult<List<Product>>(data);
        }

        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetAllProductDetails()
        {
            var data = _productDal.GetAllProductDetails();

            return new SuccessDataResult<List<ProductDetailDto>>(data);
        }

        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetAllProductDetailsByCategoryId(int categoryId)
        {
            var data = _productDal.GetAllProductDetails($"p.CategoryId = {categoryId}");

            return new SuccessDataResult<List<ProductDetailDto>>(data);
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int id)
        {
            var data = _productDal.Get($"Id = {id}");

            return new SuccessDataResult<Product>(data);
        }

        public IDataResult<List<Product>> GetByPrice(decimal minPrice, decimal maxPrice)
        {
            var data = _productDal.GetAll($"Price >= {minPrice} and Price <= {maxPrice}");

            return new SuccessDataResult<List<Product>>(data);
        }

        [CacheAspect]
        public IDataResult<Product> GetByProductName(string productName)
        {
            var data = _productDal.Get($"ProductName = '{productName}'");

            return new SuccessDataResult<Product>(data);
        }

        public IDataResult<List<Product>> GetByStock(int minStock, int maxStock)
        {
            var data = _productDal.GetAll($"Stock >= {minStock} and Stock <= {maxStock}");

            return new SuccessDataResult<List<Product>>(data);
        }

        [CacheAspect]
        public IDataResult<ProductDetailDto> GetProductDetailsById(int id)
        {
            var data = _productDal.GetProductDetails($"p.Id = {id}");

            return new SuccessDataResult<ProductDetailDto>(data);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll($"ProductName = '{productName}'").Any();

            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfIdExists(int id)
        {
            var result = _productDal.GetAll($"Id = {id}").Any();

            if (!result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
