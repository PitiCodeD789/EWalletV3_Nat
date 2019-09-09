﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.Interfaces;
using EWalletV2.Domain.DtoModels.Auth;
using AutoMapper;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService,
                              IUserService userService,
                              IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        //CheckEmail
        [HttpGet("CheckEmail/{email}")]
        public IActionResult CheckEmail([EmailAddress]string email)
        {
            bool isExist = _userService.ExistingEmail(email);

            string refNumer = _authService.SaveOtp(email);
            if (string.IsNullOrEmpty(refNumer))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error" });
            }

            //TODO: Con not send otp without otp, maybe move to service
            Task.Run(() => SendOtp(email));

            CheckEmailViewModel viewModel = new CheckEmailViewModel
            {
                IsExist = isExist,
                RefNumber = refNumer
            };

            return Ok(viewModel);
        }

        private void SendOtp(string email)
        {
            //TODO: send otp via email
            //throw new NotImplementedException();
        }

        //CheckOtp
        [HttpPost("CheckOtp")]
        public IActionResult CheckOtp([FromBody]CheckOtpCommand command)
        {
            string email = command.Email;
            string otp = command.Otp;
            string refNumber = command.RefNumber;

            bool isValidateOtp = _authService.ValidateOtp(email, otp, refNumber);

            CheckOtpViewModel viewModel = new CheckOtpViewModel
            {
                IsValidateOtp = isValidateOtp
            };

            return Ok(viewModel);
        }

        //Register
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterCommand command)
        {
            string email = command.Email;

            RegisterDtoCommand registerDtoCommand = _mapper.Map<RegisterDtoCommand>(command);

            // TODO: create model dto for register user
            string accountNumber = _authService.Register(registerDtoCommand);

            if (string.IsNullOrEmpty(accountNumber))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });
            }

            RegisterViewModel viewModel = new RegisterViewModel
            {
                RefreshToken = _authService.GetRefreshToken(email),
                Token = GetToken(),
                Account = accountNumber
            };

            return Ok(viewModel);

        }

        private string GetToken()
        {
            return "";
        }

        //Login
        [HttpPost("Login")]
        public IActionResult LoginWithUsernameAndPassword([FromBody] LoginUserAndPassCommand command)
        {
            string username = command.Username;
            string password = command.Password;

            LoginUserAndPassDto loginUserAndPassDto = _authService.LoginWithUsernameAndPassword(username, password);

            if (loginUserAndPassDto == null)
                return NotFound();

            LoginUserAndPassViewModel model = _mapper.Map<LoginUserAndPassViewModel>(loginUserAndPassDto);

            model.Token = GetToken();
            model.RefreshToken = _authService.GetRefreshToken(username);

            return Ok(model);
        }

        //Logout
        [HttpGet("Logout")]
        public IActionResult Logout([EmailAddress]string email)
        {
            bool isLogout = _authService.Logout(email);

            if (!isLogout)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });
            }

            return NoContent();
        }

    }
}