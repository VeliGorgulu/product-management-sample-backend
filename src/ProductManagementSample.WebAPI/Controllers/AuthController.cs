using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementSample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var loginResult = _authService.Login(userForLoginDto);
            if (!loginResult.Success)
            {
                return BadRequest(loginResult.Message);
            }

            var result = _authService.CreateAccessToken(loginResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var emailExists = _authService.IsEmailExists(userForRegisterDto.Email);
            if (emailExists.Success)
            {
                return BadRequest(emailExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public ActionResult Update(UserForUpdateDto userForUpdateDto)
        {
            var idExists = _authService.IsIdExists(userForUpdateDto.Id);
            var emailExists = _authService.IsEmailExists(userForUpdateDto.Email);

            if (!idExists.Success)
            {
                return BadRequest(idExists.Message);
            }

            if (!emailExists.Success)
            {
                return BadRequest(emailExists.Message);
            }

            var updateResult = _authService.Update(userForUpdateDto);
            var result = _authService.CreateAccessToken(updateResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
