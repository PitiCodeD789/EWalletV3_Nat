using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using Rg.Plugins.Popup.Services;
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
            CustomerBalance = App.CustomerBalance;
            FullName = App.FirstName + " " + App.LastName;
            AccountNumber = App.AccountNumber;
            Email = App.Email;
            //GetTransactions();

            //MockUpForTestBinding
            MockData();

            //Command
            ViewTransactionDetailCommand = new Command<int>(ViewTransactionDetail);
            BackButtonCommand = new Command(BackButton);
        }
        public ICommand BackButtonCommand { get; private set; }

        private void BackButton(object obj)
        {
            PopupNavigation.PopAsync();
        }

        private void MockData()
        {
            Transactionlist = new List<TransactionViewModel>()
            {
                new TransactionViewModel(){ TransactionId = 1 , TransactionType = "TopUp", FirstName = "test1", LastName = "test1", Account = "Acc111", CreateDateTime = DateTime.MaxValue, Balance = 2000},
                new TransactionViewModel(){ TransactionId = 4 , TransactionType = "TopUp", FirstName = "test1", LastName = "test1", Account = "Acc111", CreateDateTime = DateTime.MaxValue, Balance = 20200},

                new TransactionViewModel(){ TransactionId = 2 , TransactionType = "Payment", FirstName = "test2", LastName = "test2", Account = "Acc222", CreateDateTime = DateTime.MaxValue.AddMonths(-1), Balance = 4444444},
                new TransactionViewModel(){ TransactionId = 3 , TransactionType = "Payment", FirstName = "test3", LastName = "test3", Account = "Acc333", CreateDateTime = DateTime.MaxValue.AddMonths(-2), Balance = 66666},
            };

            LastestMonth = Transactionlist.Max(x => x.CreateDateTime);
            FirstTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == LastestMonth.Month).ToList();
            SecondTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month2.Month).ToList();
            ThridTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month3.Month).ToList();
        }

        public DateTime LastestMonth { get; set; }
        public DateTime Month2
        {
            get{ return LastestMonth.AddMonths(-1); }
            set { Month2 = value; }
        }
        public DateTime Month3
        {
            get { return Month2.AddMonths(-1); }
            set { Month3 = value; }
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
                PayerImage = "Wallet";
                PayerFullName = transaction.FirstName + " " + transaction.LastName;
                PayerAccountNumber = transaction.Account;

                ReceiverImage = "AccountOrange";
                ReceiverFullName = FullName;
                ReceiverAccountNumber = AccountNumber;
            }
            else
            {
                //Payment
                PayerImage = "AccountOrange";
                PayerFullName = FullName;
                PayerAccountNumber = AccountNumber;

                ReceiverImage = "Wallet";
                ReceiverFullName = transaction.FirstName + " " + transaction.LastName;
                ReceiverAccountNumber = transaction.Account;
            }
            PopupNavigation.PushAsync(new Views.TransactionsOne(this));
        }

        public async void GetTransactions()
        {
            ResultServiceModel<List<TransactionViewModel>> result = await _transactionService.GetTransaction30Days(Email);
            if(result.IsError != true)
            {
                Transactionlist = result.Model;
                LastestMonth = Transactionlist.Max(x => x.CreateDateTime);
                FirstTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == LastestMonth.Month).ToList();
                SecondTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month2.Month).ToList();
                ThridTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month3.Month).ToList();

            }
            //If Error popup errorPopupPage
        }

        public string PayerImage { get; set; }
        public string PayerFullName { get; set; }
        public string PayerAccountNumber { get; set; }

        public string ReceiverImage { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverAccountNumber { get; set; }

        public string TransactionName { get; set; }
        public decimal TransactionPaid { get; set; }

        public bool FirstMonth
        {
            get
            {
                if (FirstTransactionList.Count != 0) return true;
                return false;
            }
            set { FirstMonth = value; }
        }
        public bool SecondMonth
        {
            get
            {
                if (SecondTransactionList.Count != 0) return true;
                return false;
            }
            set { SecondMonth = value; }
        }
        public bool ThirdMonth
        {
            get
            {
                if (ThridTransactionList.Count != 0) return true;
                return false;
            }
            set { ThirdMonth = value; }
        }
        public List<TransactionViewModel> Transactionlist { get; set; }
        public List<TransactionViewModel> FirstTransactionList { get; set; }
        public List<TransactionViewModel> SecondTransactionList { get; set; }
        public List<TransactionViewModel> ThridTransactionList { get; set; }


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
