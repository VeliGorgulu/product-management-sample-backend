using ProductManagementSample.Core.Entities.Concrete;
using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.Core.Utilities.Security.JWT;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Update(UserForUpdateDto userForUpdateDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult IsEmailExists(string email);
        IResult IsIdExists(int id);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
