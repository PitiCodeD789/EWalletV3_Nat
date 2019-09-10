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
            base.Configure(e);

            e.ToTable("Transactions");

            e.Property(p => p.CustomerId)
                .HasColumnName("customer_id");

            e.Property(p => p.OtherId)
                .HasColumnName("other_id");

            e.Property(p => p.Amount)
                .HasColumnName("Amount");

            e.HasOne(x => x.UserCustomerEntity)
                .WithMany(x => x.TransactionCustomerEntities)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserCustomerEntity");

            e.HasOne(x => x.UserOtherEntity)
                .WithMany(x => x.TransactionOtherEntities)
                .HasForeignKey(x => x.OtherId)
                .HasConstraintName("FK_UserOtherEntity");
        }
    }
}
