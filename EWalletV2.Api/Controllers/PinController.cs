using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinController : ControllerBase
    {
        IAuthService _authService;
        public PinController(IAuthService authService)
        {
            _authService = authService;
        }
        //LoginByPin
        //CheckPin
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