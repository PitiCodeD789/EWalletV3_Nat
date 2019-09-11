using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
    public class AdminTransactionViewModel
    {
        private readonly ITransactionServices _transactionService;
        public AdminTransactionViewModel()
        {
            //Initial
            _transactionService = new TransactionServices();
            GetTransactions();

            //Command
            ViewTransactionDetailCommand = new Command<int>(ViewTransactionDetail);
        }

        public ICommand ViewTransactionDetailCommand { get; set; }
        private void ViewTransactionDetail(int transactionId)
        {
            TransactionViewModel transaction = Transactionlist.Where(x => x.TransactionId == transactionId).FirstOrDefault();
            TransactionName = transaction.TransactionType;
            TransactionPaid = transaction.Balance;
            TransactionReference = transaction.TransactionReference;
            CreateDate = transaction.CreateDateTime;
            if (transaction.TransactionType == "TopUp")
            {
                //TopUp
                Image1 = "Wallet";
                FullName1 = transaction.FirstName + " " + transaction.LastName;
                AccountNumber1 = transaction.Account;

                Image2 = "AccountOrange";
                FullName2 = FullName;
                AccountNumber2 = AccountNumber;
            }
        }

        public async void GetTransactions()
        {
            var result = await _transactionService.GetTransaction30Days(Email);
            if (result.IsError != true)
                Transactionlist = result.Model;
            //If Error popup errorPopupPage
        }

        public string Image1 { get; set; }
        public string FullName1 { get; set; }
        public string AccountNumber1 { get; set; }

        public string Image2 { get; set; }
        public string FullName2 { get; set; }
        public string AccountNumber2 { get; set; }

        public string TransactionName { get; set; }
        public decimal TransactionPaid { get; set; }
        public string TransactionReference { get; set; }

        public List<TransactionViewModel> Transactionlist { get; set; }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private decimal _customerBalance;
        public decimal CustomerBalance
        {
            get { return _customerBalance; }
            set { _customerBalance = value; }
        }
    }
}
