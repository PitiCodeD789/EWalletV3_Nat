using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.DtoModels.Auth;
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
            CheckPinDto user = _userService.GetUserByEmail((string)checkPin.Email);
            if (user == null)
            {
                return BadRequest();
            }
            bool isCorrect = _authService.CheckPin((string)checkPin.Pin, (string)checkPin.Email);
            return isCorrect ? Ok() : (IActionResult)BadRequest();
        }
        //UpdatePin
        //CheckForgotPin
    }
}