using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.BusinessAspects.Autofac;
using ProductManagementSample.Business.ValidationRules.FluentValidation;
using ProductManagementSample.Core.Aspects.Autofac.Caching;
using ProductManagementSample.Core.Aspects.Autofac.Logging;
using ProductManagementSample.Core.Aspects.Autofac.Validation;
using ProductManagementSample.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ProductManagementSample.Core.Utilities.Business;
using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.DataAccess.Abstract;
using ProductManagementSample.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Concrete
{
    [SecuredOperation("admin,category.admin")]
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(
                CheckIfCategoryNameExists(category.CategoryName)
            );

            if (result != null)
            {
                return result;
            }
            _categoryDal.Add(category);

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(Category category)
        {
            IResult result = BusinessRules.Run(
                CheckIfCategoryNameExists(category.CategoryName)
            );

            if (result != null)
            {
                return result;
            }

            _categoryDal.Update(category);

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(Category category)
        {
            IResult result = BusinessRules.Run(
                CheckIfIdExists(category.Id)
            );

            if (result != null)
            {
                return result;
            }

            _categoryDal.Delete(category);

            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Category>> GetAll()
        {
            var data = _categoryDal.GetAll();
            
            return new SuccessDataResult<List<Category>>(data);
        }

        public IDataResult<Category> GetByCategoryName(string categoryName)
        {
            var data = _categoryDal.Get($"CategoryName = '{categoryName}'");

            return new SuccessDataResult<Category>(data);
        }

        [CacheAspect]
        public IDataResult<Category> GetById(int id)
        {
            var data = _categoryDal.Get($"Id = {id}");

            return new SuccessDataResult<Category>(data);
        }

        private IResult CheckIfCategoryNameExists(string categoryName)
        {
            var result = _categoryDal.GetAll($"CategoryName = '{categoryName}'").Any();

            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfIdExists(int id)
        {
            var result = _categoryDal.GetAll($"Id = {id}").Any();

            if (!result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
