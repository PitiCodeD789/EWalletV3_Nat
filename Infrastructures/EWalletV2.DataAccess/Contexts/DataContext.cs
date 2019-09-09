using EWalletV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.DataAccess.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
    }
}
