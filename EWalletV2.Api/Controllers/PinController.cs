using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.DtoModels.Auth;
=======
using EWalletV2.Api.ViewModels.Pin;
>>>>>>> 0d88fc19fc9b37f24785771712b03422dfa5081f
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinController : ControllerBase
    {
<<<<<<< HEAD
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public PinController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
=======
        private readonly IAuthService _authService;
        public PinController(IAuthService authService)
        {
>>>>>>> 0d88fc19fc9b37f24785771712b03422dfa5081f
            _authService = authService;
        }
        //LoginByPin
        //CheckPin
        [HttpPost("CheckPin")]
        public IActionResult CheckPin([FromBody] CheckPinCommand checkPin)
        {
            CheckPinDto user = _userService.GetUserByEmail((string)checkPin.Email);
            if (user == null)
            {
                return BadRequest();
            }
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
    }
}