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

            e.Property(p => p.ReceiveId)
                .HasColumnName("receiver_id");

            e.Property(p => p.PayerId)
                .HasColumnName("payer_id");

            e.Property(p => p.Amount)
                .HasColumnName("Amount");
        }
    }
}
