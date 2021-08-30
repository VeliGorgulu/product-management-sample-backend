using ProductManagementSample.Entities.Concrete;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        ProductDetailDto GetProductDetails(string filter);
        List<ProductDetailDto> GetAllProductDetails(string filter = null);
    }
}
