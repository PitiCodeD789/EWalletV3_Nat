﻿using EWalletV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.DataAccess.Configurations
{
    public class TokenConfiguration : BaseConfiguration<TokenEntity>
    {
        public override void Configure(EntityTypeBuilder<TokenEntity> e)
        {

        }
    }
}
