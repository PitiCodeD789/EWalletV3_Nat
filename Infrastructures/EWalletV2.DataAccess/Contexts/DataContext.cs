using EWalletV2.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.DataAccess.Contexts
{
  public  class DataContext : DbContext
    {
        public DbSet<TokenEntity> Tokens { get; set; }
    }
}
