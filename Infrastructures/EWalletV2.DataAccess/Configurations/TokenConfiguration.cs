using EWalletV2.Domain.Entities;
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
            base.Configure(e);

            e.ToTable("Tokens");

            e.Property(p => p.RefreshToken)
                .HasMaxLength(100)
                .HasColumnName("refresh_token");

            e.Property(p => p.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
        }
    }
}
