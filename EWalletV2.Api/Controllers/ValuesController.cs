using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Domain.DtoModels.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EWalletV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IMapper _mapper;

        public ValuesController(ILogger<ValuesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Get([FromBody]RegisterCommand command)
        {
            RegisterDtoCommand registerDto = _mapper.Map<RegisterDtoCommand>(command);
            return Ok(registerDto);
        }
    }
}