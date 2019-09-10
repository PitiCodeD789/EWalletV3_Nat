using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Domain.DtoModels.Transaction;
using EWalletV2.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EWalletV2.Api.ViewModels;

namespace EWalletV2.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public TransactionController(IUserService userService, ITransactionService transactionService, IMapper mapper)
        {
            _userService = userService;
            _transactionService = transactionService;
            _mapper = mapper;
        }
        //GetTransaction30Days
        [HttpGet("GetListTransaction/{email}")]
        public IActionResult GetTransaction30Days(string email)
        {
            List<TransactionDetailDto> transactionDetailDtos;
            bool isExist = _userService.ExistingEmail(email);
            if (isExist)
            {
                List<TransactionDetailViewModel> transactionList = new List<TransactionDetailViewModel>();
                transactionDetailDtos = _transactionService.GetTransaction30Days(email);

                transactionList = _mapper.Map<List<TransactionDetailViewModel>>(transactionDetailDtos);

                return Ok(transactionList);
            }
            else
            {
                return NotFound();
            }
        }
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

            if(payment == null)
            {
                return BadRequest();
            }

            PaymentViewModel result = new PaymentViewModel()
            {
                Reference = payment.Reference,
                CreateDatetime = payment.CreateDatetime
            };

            return Ok(result);

        }

        //Topup
 
        [HttpPost("Topup")]
        public IActionResult Topup(TopupCommand command)
        {
            bool isExist = _userService.ExistingEmail(command.Email);
            if (!isExist)
            {
                return BadRequest();
            }

            TopupDto topupDto = _transactionService.Topup(command.Email, command.ReferenceNumber);
            TopupViewModel topupModel = _mapper.Map<TopupViewModel>(topupDto);

            if (topupModel == null || !topupModel.IsSuccess)
                return BadRequest();

            return Ok(topupModel);
        }

        //GetDetailTransaction
        [HttpPost("DetailTransaction")]
        public IActionResult DetailTransaction([FromBody]TransactionCommand command)
        {
            bool isExist = _userService.ExistingEmail(command.Email);
            if (isExist)
            {
                TransactionDto transactionDto = _transactionService.GetDetailTransaction(command.Email, command.TransactionId);

                TransactionViewModel model = _mapper.Map<TransactionViewModel>(transactionDto);

                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }
        //GenerateTopup
     
        [HttpPost("GenerateTopup")]
        public IActionResult GenerateTopup(GenerateTopupCommand command)
        {
            if (int.Parse(command.Account.Substring(0, 2)) != (int)EW_Enumerations.EW_UserTypeEnum.Admin)
            {
                return BadRequest();
            }

            bool isExist = _userService.ExistAccountNo(command.Account);
            if (!isExist)
            {
                return BadRequest();
            }

            string referenceNumber = _transactionService.GenerateTopUp(command.Account, command.Amount);
            if (referenceNumber == null)
                return BadRequest();

            GenerateTopupViewModel viewModel = new GenerateTopupViewModel()
            {
                ReferenceNumber = referenceNumber
            };

            return Ok(viewModel);
        }

    }
}
