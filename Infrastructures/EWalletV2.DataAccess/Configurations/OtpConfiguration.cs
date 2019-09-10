using EWalletV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.DataAccess.Configurations
{
    class OtpConfiguration : BaseConfiguration<OtpEntity>
    {
        public override void Configure(EntityTypeBuilder<OtpEntity> e)
        {
            base.Configure(e);

            e.ToTable("Otps");

            e.Property(p => p.Otp)
                .HasColumnName("otp")
                .HasMaxLength(100);

            e.Property(p => p.Reference)
                .HasColumnName("reference")
                .HasMaxLength(100);

            e.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(100);
        }
    }
}
