﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.Interfaces;
using EWalletV2.Domain.DtoModels.Auth;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using EWalletV2.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using EWalletV2.Api.ViewModels;
using static EWalletV2.Api.ViewModels.EW_Enumerations;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public AuthController(IAuthService authService,
                              IUserService userService,
                              IMapper mapper,
                              IConfiguration configuration
                              )
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        //CheckEmail
        [AllowAnonymous]
        [HttpGet("CheckEmail/{email}")]
        public IActionResult CheckEmail([EmailAddress]string email)
        {
            bool isExist = _userService.ExistingEmail(email);
            string refNumer = _authService.SaveOtp(email.ToLower());
            if (string.IsNullOrEmpty(refNumer))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error" });
            }

            CheckEmailViewModel viewModel = new CheckEmailViewModel
            {
                IsExist = isExist,
                RefNumber = refNumer
            };

            if (isExist)
            {
                var account = _userService.GetAccountDetailByEmail(email);
                int roleNumber = Convert.ToInt32(account.AccountNumber.Remove(2));
                viewModel.Role = (EW_UserTypeEnum)roleNumber;
            }

            return Ok(viewModel);
        }

        //CheckOtp
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterCommand command)
        {
            string email = command.Email.ToLower();
            command.Email = command.Email.ToLower();

            RegisterDtoCommand registerDtoCommand = _mapper.Map<RegisterDtoCommand>(command);

            // TODO: create model dto for register user
            string accountNumber = _authService.Register(registerDtoCommand);

            if (string.IsNullOrEmpty(accountNumber))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });
            }

            GetToken getToken = new GetToken(_configuration);

            RegisterViewModel viewModel = new RegisterViewModel
            {
                RefreshToken = _authService.GetRefreshToken(email),
                Token = getToken.Token,
                Account = accountNumber
            };

            return Ok(viewModel);

        }

        //GetTokenByRefreshToken
        [AllowAnonymous]
        [HttpPost("GetTokenByRefreshToken")]
        public IActionResult GetTokenByRefreshToken([FromBody] GetTokenByRefreshTokenCommand command)
        {
            string refeshToken = command.RefreshToken;
            string email = command.Email?.ToLower();

            bool isCorrectRefreshToken = _authService.CheckRefreshToken(email, refeshToken);

            if (isCorrectRefreshToken)
            {
                GetToken getToken = new GetToken(_configuration);
                var token = getToken.Token;

                GetTokenByRefreshTokenViewModel viewModel = new GetTokenByRefreshTokenViewModel()
                {
                    Token = token
                };

                return Ok(viewModel);
            }
            
            return BadRequest();
        }

        //Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult LoginWithUsernameAndPassword([FromBody] LoginUserAndPassCommand command)
        {
            string username = command.Username;
            string password = command.Password;

            LoginUserAndPassDto loginUserAndPassDto = _authService.LoginWithUsernameAndPassword(username, password);

            if (loginUserAndPassDto == null)
                return BadRequest();
            GetToken getToken = new GetToken(_configuration);
            LoginUserAndPassViewModel model = _mapper.Map<LoginUserAndPassViewModel>(loginUserAndPassDto);

            model.Token = getToken.Token;
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


        //Login
        [AllowAnonymous]
        [HttpPost("LoginByCustomer")]
        public IActionResult LoginByCustomer([FromBody]LoginByCustomerCommand command)
        {

            string email = command.Email;
            string pin = command.Pin;

            bool changePinResult= _authService.ChangePin(email, pin);
            if (!changePinResult)
            {
                return null;
            }

            LoginByCustomerDto loginByCustomerDto = _authService.LoginByCustomer(email, pin);

            if (loginByCustomerDto == null)
                return NotFound();
            GetToken getToken = new GetToken(_configuration);
            LoginByCustomerViewModel model = _mapper.Map<LoginByCustomerViewModel>(loginByCustomerDto);

            model.Token = getToken.Token;
            model.RefreshToken = _authService.GetRefreshToken(email);

            return Ok(model);
        }
    }
}