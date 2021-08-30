using ProductManagementSample.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Abstract
{
    public interface IEntityRepository<T>
        where T : class, IEntity, new()
    {
        List<T> GetAll(string filter = null);
        T Get(string filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
