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

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public PinController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        //LoginByPin
        //CheckPin
        [HttpPost("CheckPin")]
        public IActionResult CheckPin([FromBody] CheckPinCommand checkPin)
        {
            CheckPinDto user = _userService.GetUserByEmail(checkPin.Email);
            if (user == null)
            {
                return BadRequest();
            }
            bool isCorrect = _authService.CheckPin(checkPin.Pin, checkPin.Email);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error" });
            }

            return Ok(refNumber);

        }

    }
}
