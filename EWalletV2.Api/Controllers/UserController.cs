using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EWalletV2.Api.ViewModels.User;
using EWalletV2.Domain.DtoModels.User;
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        //GetUser
        [HttpGet("GetUser/{email}")]
        public IActionResult GetUser([EmailAddress]string email)
        {
            AccountViewModel accountViewModel = _userService.GetAccountDetailByEmail(email);
            if (accountViewModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });
            }
            return Ok(accountViewModel);
        }

        //GetBalance
        [HttpPost("GetBalance")]
        public IActionResult GetBalance([FromBody]EmailCommand command)
        {
            AccountViewModel accountViewModel = _userService.GetAccountDetailByEmail(command.Email);
            if (accountViewModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });
            }
            return Ok(accountViewModel);
        }

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
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserCommand user)
        {

            UpdateUserDtoCommand userDto = _mapper.Map<UpdateUserDtoCommand>(user);

            bool IsUpdated = _userService.UpdateUser(userDto);

            if (!IsUpdated)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
