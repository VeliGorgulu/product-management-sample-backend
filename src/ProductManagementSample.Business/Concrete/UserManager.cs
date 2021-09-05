using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.BusinessAspects.Autofac;
using ProductManagementSample.Business.ValidationRules.FluentValidation;
using ProductManagementSample.Core.Aspects.Autofac.Caching;
using ProductManagementSample.Core.Aspects.Autofac.Logging;
using ProductManagementSample.Core.Aspects.Autofac.Validation;
using ProductManagementSample.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ProductManagementSample.Core.Entities.Concrete;
using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Concrete
{
    
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        [SecuredOperation("admin,user.admin")]
        public IResult Add(User user)
        {
            _userDal.Add(user);

            return new SuccessResult();
        }


        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        [SecuredOperation("admin,user.admin")]
        public IResult Update(User user)
        {
            _userDal.Update(user);

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperation("admin,user.admin")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);

            return new SuccessResult();
        }

        [CacheAspect]
        [SecuredOperation("admin,user.admin")]
        public IDataResult<List<User>> GetAll()
        {
            var data = _userDal.GetAll();

            return new SuccessDataResult<List<User>>(data);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var data = _userDal.Get($"Email = '{email}'");

            return new SuccessDataResult<User>(data);
        }

        public IDataResult<User> GetById(int id)
        {
            var data = _userDal.Get($"Id = {id}");

            return new SuccessDataResult<User>(data);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var data = _userDal.GetClaims(user);

            return new SuccessDataResult<List<OperationClaim>>(data);
        }

        
    }
}
