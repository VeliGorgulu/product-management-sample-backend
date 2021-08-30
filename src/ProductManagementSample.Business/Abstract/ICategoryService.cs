using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Abstract
{
    public interface ICategoryService
    {
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
        IDataResult<Category> GetById(int id);
        IDataResult<Category> GetByCategoryName(string categoryName);
        IDataResult<List<Category>> GetAll();
    }
}
