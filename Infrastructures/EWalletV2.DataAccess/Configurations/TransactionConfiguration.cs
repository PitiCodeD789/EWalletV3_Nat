using EWalletV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.DataAccess.Configurations
{
    class TransactionConfiguration : BaseConfiguration<TransactionEntity>
    {
        public override void Configure(EntityTypeBuilder<TransactionEntity> e)
        {
            e.ToTable("Transactions");

            e.Property(p => p.CustomerId)
                .HasColumnName("customer_id");

            e.Property(p => p.OtherId)
                .HasColumnName("other_id");

            e.Property(p => p.Amount)
                .HasColumnName("Amount");
        }
    }
}
