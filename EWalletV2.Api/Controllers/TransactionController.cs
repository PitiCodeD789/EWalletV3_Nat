using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Domain.DtoModels.Transaction;
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        public TransactionController(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }
        //GetTransaction30Days
        //Payment
        [HttpPost("payment")]
        public IActionResult Payment([FromBody]PaymentCommand command)
        {
            string email = command.Email;
            string merchantAccNo = command.MerchantAccountNo;
            decimal pay = command.Pay;

            if (Int32.Parse(merchantAccNo.Substring(0, 2)) != (int)EW_Enumerations.EW_UserTypeEnum.Merchant)
            {
                return BadRequest();
            }

            bool isEmail = _userService.ExistingEmail(email);
            bool isMerchant = _userService.ExistAccountNo(merchantAccNo);

            if (!isEmail || !isMerchant)
            {
                return BadRequest();
            }

            PaymentDto payment = _transactionService.Payment(email, merchantAccNo, pay);

            PaymentViewModel result = new PaymentViewModel()
            {
                Reference = payment.Reference,
                CreateDatetime = payment.CreateDatetime
            };

            return Ok(result);

        }

        //Topup
        //GetDetailTransaction
        //GenerateTopup
    }
}