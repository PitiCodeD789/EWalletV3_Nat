using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Domain.DtoModels.Transaction;
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        ITransactionService _transactionService;
        IUserService _userService;
        IMapper _mapper;
        public TransactionController(ITransactionService transactionService, IUserService userService, IMapper mapper)
        {
            _transactionService = transactionService;
            _userService = userService;
            _mapper = mapper;
        }

        //GetTransaction30Days

        //Payment

        //Topup
        [Authorize]
        [HttpPost("Topup")]
        public IActionResult Topup(TopupCommand command)
        {
            bool isExist = _userService.ExistingEmail(command.Email);
            if (!isExist)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "User Not Found" });
            }

            TopupDto topupDto = _transactionService.Topup(command.Email, command.ReferenceNumber);
            TopupModel topupModel = _mapper.Map<TopupModel>(topupDto);

            if (!topupModel.IsSuccess)
                return StatusCode(StatusCodes.Status400BadRequest, topupModel);

            return Ok(topupModel);
        }

        //GetDetailTransaction

        //GenerateTopup
        [Authorize]
        [HttpPost("GenerateTopup")]
        public IActionResult GenerateTopup(TopupCommand command)
        {
            bool isExist = _userService.ExistingEmail(command.Email);
            if (!isExist)
            {
                return NotFound();
            }

            string referenceNumber = _transactionService.GenerateTopUp(command.Email, command.Amount);
            if (referenceNumber == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error" });

            return Ok(new { referenceNumber });
        }

    }
}