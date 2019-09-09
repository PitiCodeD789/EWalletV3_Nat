using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EWalletV2.Api.ViewModels.User;
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //GetUser
        //GetBalance
        [HttpPost]
        public IActionResult GetBalance([EmailAddress]string email)
        {
            AccountViewModel accountViewModel = _userService.GetAccountDetailByEmail(email);
            if (accountViewModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });
            }
            return Ok(accountViewModel);
        }
        //GetAccountName
        //UpdateUser
    }
}