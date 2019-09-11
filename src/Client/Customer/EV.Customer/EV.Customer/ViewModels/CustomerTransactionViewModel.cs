using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class CustomerTransactionViewModel
    {
        private readonly ITransactionServices _transactionService;
        public CustomerTransactionViewModel()
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
            else
            {
                //Payment
                Image1 = "AccountOrange";
                FullName1 = FullName;
                AccountNumber1 = AccountNumber;

                Image2 = "Wallet";
                FullName2 = transaction.FirstName + " " + transaction.LastName;
                AccountNumber2 = transaction.Account;
            }
        }

        public async void GetTransactions()
        {
            ResultServiceModel<List<TransactionViewModel>> result = await _transactionService.GetTransaction30Days(Email);
            if(result.IsError != true)
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

        public List<TransactionViewModel> Transactionlist { get; set; }

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
