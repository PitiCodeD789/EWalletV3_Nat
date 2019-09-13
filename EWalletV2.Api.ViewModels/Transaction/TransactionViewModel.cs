using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
   public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public string TransactionReference { get; set; }
        public string Account { get; set; }
        public string TransactionType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        //  public DateTime CreateDateTime { get; set; }
        private DateTime _createDateTime;

        public DateTime CreateDateTime
        {
            get { return _createDateTime; }
            set {

                _createDateTime = value.ToLocalTime();
            }
        }

    }
}
