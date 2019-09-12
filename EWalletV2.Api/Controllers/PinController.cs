using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.DtoModels.Auth;

using EWalletV2.Api.ViewModels.Pin;

using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EWalletV2.Api.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public PinController(IUserService userService, IAuthService authService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
            _configuration = configuration;
        }

        //LoginByPin
        [AllowAnonymous]
        [HttpPost("LoginByPin")]
        public IActionResult LoginByPin([FromBody]LoginPinCommand command)
        {
            string email = command.Email;
            string pin = command.Pin;

            bool loginPinDto = _authService.CheckPin(pin, email);

            LoginByPinViewModel viewmodel = new LoginByPinViewModel()
            {
                IsLogin = loginPinDto
            };

            return Ok(viewmodel);
        }

        //CheckPin

        [HttpPost("CheckPin")]
        public IActionResult CheckPin([FromBody] CheckPinCommand checkPin)
        {      
            bool isCorrect = _authService.CheckPin((string)checkPin.Pin, (string)checkPin.Email);
            return isCorrect ? Ok() : (IActionResult)BadRequest();
        }
        //UpdatePin
        [HttpPost("updatepin")]
        public IActionResult UpdatePin([FromBody]UpdatePinCommand updatePin)
        {
            bool isUpdatePinSuccess = _authService.UpdatePin(updatePin.Email
                , updatePin.OldPin
                , updatePin.NewPin);
            if (!isUpdatePinSuccess)
            {
                return BadRequest();
            }
            return NoContent();
        }
        //CheckForgotPin
        [AllowAnonymous]
        [HttpPost("CheckForgotPin")]
        public IActionResult CheckForgotPin([FromBody] CheckForgotPinCommand forgotPinCommand)
        {
            bool isUser = _userService.CheckUserByEmailAndBirthday(forgotPinCommand.Email, forgotPinCommand.Birthday);
            if (!isUser)
            {
                return NoContent();
            }
            string refNumber = _authService.SaveOtp(forgotPinCommand.Email);
            if (string.IsNullOrEmpty(refNumber))
            {
                return BadRequest();
            }

            CheckForgotPinViewModel checkForgot = new CheckForgotPinViewModel { RefNumber = refNumber };


            return Ok(checkForgot);
        }

    }
}
