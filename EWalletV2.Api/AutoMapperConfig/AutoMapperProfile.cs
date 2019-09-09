﻿using AutoMapper;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Api.ViewModels.User;
using EWalletV2.Domain.DtoModels.Auth;
using EWalletV2.Domain.DtoModels.Transaction;
using EWalletV2.Domain.DtoModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWalletV2.Api.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterCommand, RegisterDtoCommand>();
            CreateMap<LoginUserAndPassDto, LoginUserAndPassViewModel>();
            CreateMap<CheckPinDto, LoginPinViewModel>();
            CreateMap<UpdateUserCommand, UpdateUserDtoCommand>();
            CreateMap<TransactionDto, TransactionViewModel>();


        }
    }
}
