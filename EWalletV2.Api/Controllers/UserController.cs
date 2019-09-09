using System;
using System.Collections.Generic;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userservice;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {

            _userservice = userService;
            _mapper = mapper;

        }

        //GetUser
        //GetBalance
        //GetAccountName
        //UpdateUser
        [Authorize]
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserCommand user)
        {

            UpdateUserDtoCommand userDto = _mapper.Map<UpdateUserDtoCommand>(user);

            bool IsUpdated = _userservice.UpdateUser(userDto);

            if (!IsUpdated)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}