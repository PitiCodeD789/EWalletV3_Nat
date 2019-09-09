using EWalletV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.DataAccess.Context
{
    public class DataContext
    {
        public DbSet<UserEntity> User { get; set; }
    }
}
