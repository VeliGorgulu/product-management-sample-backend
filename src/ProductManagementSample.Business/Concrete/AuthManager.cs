using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.Constants;
using ProductManagementSample.Core.Aspects.Autofac.Logging;
using ProductManagementSample.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ProductManagementSample.Core.Entities.Concrete;
using ProductManagementSample.Core.Utilities.Results;
using ProductManagementSample.Core.Utilities.Security.Hashing;
using ProductManagementSample.Core.Utilities.Security.JWT;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<User> Update(UserForUpdateDto userForUpdateDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForUpdateDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Id = userForUpdateDto.Id,
                Email = userForUpdateDto.Email,
                FirstName = userForUpdateDto.FirstName,
                LastName = userForUpdateDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Update(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult IsEmailExists(string email)
        {
            var result = _userService.GetByEmail(email).Data;

            if (result != null)
            {
                return new SuccessResult(Messages.UserAlreadyExists);
            }

            return new ErrorResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult IsIdExists(int id)
        {
            var result = _userService.GetById(id);
        
            if (result != null)
            {
                return new SuccessResult(Messages.UserAlreadyExists);
            }

            return new ErrorResult();
        }
    }
}
