using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Merchant.ViewModels
{
    public class MerchantTransactionModel
    {
        private readonly ITransactionServices _transactionService;
        public MerchantTransactionModel()
        {
            //Initial
            _transactionService = new TransactionServices();
            FullName = App.FirstName + " " + App.LastName;
            AccountNumber = App.AccountNumber;
            Email = App.Email;
            MockData();
            //GetTransactions();

            //Command
            ViewTransactionDetailCommand = new Command<int>(ViewTransactionDetail);
            BackButtonCommand = new Command(BackButton);

        }
        public ICommand BackButtonCommand { get; private set; }

        private void BackButton(object obj)
        {
            PopupNavigation.PopAsync();
        }
        public ICommand ViewTransactionDetailCommand { get; set; }
        private void ViewTransactionDetail(int transactionId)
        {
            TransactionViewModel transaction = Transactionlist.Where(x => x.TransactionId == transactionId).FirstOrDefault();
            TransactionName = transaction.TransactionType;
            TransactionPaid = transaction.Balance;
            TransactionReference = transaction.TransactionReference;
            CreateDate = transaction.CreateDateTime;
            if (transaction.TransactionType == "Payment")
            {
                //Payment
                Image1 = "AccountOrange";
                FullName1 = FullName;
                AccountNumber1 = AccountNumber;

                Image2 = "Wallet";
                FullName2 = transaction.FirstName + " " + transaction.LastName;
                AccountNumber2 = transaction.Account;
            }
            PopupNavigation.PushAsync(new Views.TransactionsOne(this));
        }
        private void MockData()
        {
            Transactionlist = new List<TransactionViewModel>()
            {
                new TransactionViewModel(){ TransactionId = 2 , TransactionType = "Payment", TransactionReference = "Payment", FirstName = "test2", LastName = "test2", Account = "Acc222", CreateDateTime = DateTime.MaxValue.AddMonths(-1), Balance = 4444444},
                new TransactionViewModel(){ TransactionId = 3 , TransactionType = "Payment", FirstName = "test3", LastName = "test3", Account = "Acc333", CreateDateTime = DateTime.MaxValue.AddMonths(-2), Balance = 66666},
            };

            LastestMonth = Transactionlist.Max(x => x.CreateDateTime);
            FirstTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == LastestMonth.Month).ToList();
            SecondTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month2.Month).ToList();
            ThridTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month3.Month).ToList();
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
        public List<TransactionViewModel> FirstTransactionList { get; set; }
        public List<TransactionViewModel> SecondTransactionList { get; set; }
        public List<TransactionViewModel> ThridTransactionList { get; set; }
        public DateTime LastestMonth { get; set; }
        public DateTime Month2
        {
            get { return LastestMonth.AddMonths(-1); }
            set { Month2 = value; }
        }
        public DateTime Month3
        {
            get { return Month2.AddMonths(-1); }
            set { Month3 = value; }
        }

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

