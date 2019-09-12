﻿using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Merchant.ViewModels
{
    public class MerchantTransactionModel : INotifyPropertyChanged
    {
        private readonly ITransactionServices _transactionService;
        public MerchantTransactionModel()
        {
            //Initial
            _transactionService = new TransactionServices();
            FullName = App.FirstName + " " + App.LastName;
            AccountNumber = App.Account;
            Email = App.Email;

            LastestMonth = DateTime.Now;
            Transactionlist = new List<TransactionViewModel>();
            FirstTransactionList = new List<TransactionViewModel>();
            SecondTransactionList = new List<TransactionViewModel>();
            ThridTransactionList = new List<TransactionViewModel>();
            
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
                PayerImage = "AccountOrange";
                PayerFullName = FullName;
                PayerAccountNumber = AccountNumber;

                ReceiverImage = "Wallet";
                ReceiverFullName = transaction.FirstName + " " + transaction.LastName;
                ReceiverAccountNumber = transaction.Account;
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

        public async Task GetTransactions()
        {
            var result = await _transactionService.GetTransaction30Days("nomustang11@gmail.com");
            var a = result.Model;
            if (result.IsError != true)
            {
                Transactionlist = result.Model;
                LastestMonth = Transactionlist.Max(x => x.CreateDateTime);
                FirstTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == LastestMonth.Month).ToList();
                SecondTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month2.Month).ToList();
                ThridTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month3.Month).ToList();
            }
            //If Error popup errorPopupPage
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string PayerImage { get; set; }
        public string PayerFullName { get; set; }
        public string PayerAccountNumber { get; set; }

        public string ReceiverImage { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverAccountNumber { get; set; }

        public string TransactionName { get; set; }
        public decimal TransactionPaid { get; set; }
        public string TransactionReference { get; set; }

        private List<TransactionViewModel> _transactionlist;
        public List<TransactionViewModel> Transactionlist
        {
            get { return _transactionlist; }
            set
            {
                _transactionlist = value;
                OnPropertyChanged();
            }
        }

        private List<TransactionViewModel> _firstTransactionList;
        public List<TransactionViewModel> FirstTransactionList
        {
            get { return _firstTransactionList; }
            set
            {
                _firstTransactionList = value;
                OnPropertyChanged();
            }
        }
        private List<TransactionViewModel> _secondTransactionList;
        public List<TransactionViewModel> SecondTransactionList
        {
            get { return _secondTransactionList; }
            set
            {
                _secondTransactionList = value;
                OnPropertyChanged();
            }
        }
        private List<TransactionViewModel> _thridTransactionList;
        public List<TransactionViewModel> ThridTransactionList
        {
            get { return _thridTransactionList; }
            set
            {
                _thridTransactionList = value;
                OnPropertyChanged();
            }
        }

        private DateTime _lastestMonth;
        public DateTime LastestMonth

        {
            get { return _lastestMonth; }
            set
            {
                _lastestMonth = value;
                OnPropertyChanged();
            }
        }
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
                OnPropertyChanged();
                if (FirstTransactionList.Count != 0) return true;
                return false;
            }
            set
            {
                FirstMonth = value;
            }
        }
        public bool SecondMonth
        {
            get
            {
                OnPropertyChanged();
                if (SecondTransactionList.Count != 0) return true;
                return false;
            }
            set { SecondMonth = value; }
        }
        public bool ThirdMonth
        {
            get
            {
                OnPropertyChanged();
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

