using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
