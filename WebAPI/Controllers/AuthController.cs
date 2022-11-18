using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entites.Concrete;
using Core.Utilities.Helpers;
using Core.Utilities.Helpers.Senders.SenderTemplates;
using Core.Utilities.Helpers.Senders.SmsSenders;
using Core.Utilities.Helpers.Senders.SmsSenders.Twilio;
using Core.Utilities.Helpers.Senders.SmtpSender;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IEmailSender _smtpEmailSender;
        private IUserService _userService;

        public AuthController(IAuthService authService, IEmailSender smtpEmailSender, IUserService userService)
        {
            _authService = authService;
            _smtpEmailSender = smtpEmailSender;
            _userService = userService;

        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {

                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }

            var registerResult = _authService.Register(userForRegisterDto);

            if (registerResult.Success)
            {
                //var userVerify = _userVerifyService.Add(userForRegisterDto.Email);
                //_smtpEmailSender.SendEmail(SmtpConstants.fromAddress, userForRegisterDto.Email, SmtpConstants.emailVerifySubject, userVerify.Data.EmailVerifyToken);
                return Ok(registerResult);
            }

            return BadRequest(registerResult);
        }
        
        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("updatepassword")]
        public IActionResult UpdatePassword(UpdatePasswordDto updatePasswordDTO)
        {
            var result = _authService.UpdatePassword(updatePasswordDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result); 
        }
    }
}