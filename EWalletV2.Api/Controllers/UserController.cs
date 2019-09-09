using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWalletV2.Api.ViewModels.Auth;
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
        //GetAccountName ==> dev_pop
        [HttpGet("GetAccount")]
        public IActionResult GetAccount([FromBody]AccountCommand command)
        {
            string accountNumber = command.AccountNumber;
            string accountName = _userService.GetAccountNameByAccountNumber(accountNumber);
            if (accountName == null)
                return NotFound();
            AccountViewModel account = new AccountViewModel()
            {
                AccountName = accountName
            };
            return Ok(account);
        }
        //UpdateUser
    }
}