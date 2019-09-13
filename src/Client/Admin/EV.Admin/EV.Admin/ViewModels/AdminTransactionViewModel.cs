using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
    public class AdminTransactionViewModel : INotifyPropertyChanged
    {
        private readonly ITransactionServices _transactionService;

        public AdminTransactionViewModel()
        {
            //Initial
            _transactionService = new TransactionServices();
            Email = App.Email;
            FullName = App.AdminName;
            AccountNumber = App.Account;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            LastestMonth = DateTime.Now;
            Transactionlist = new List<TransactionViewModel>();
            FirstTransactionList = new List<TransactionViewModel>();
            SecondTransactionList = new List<TransactionViewModel>();
            ThridTransactionList = new List<TransactionViewModel>();


            //Command
            ViewTransactionDetailCommand = new Command<int>(ViewTransactionDetail);
            BackButtonCommand = new Command(BackButton);
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
                PayerImage = "Wallet";
                PayerFullName = transaction.FirstName + " " + transaction.LastName;
                PayerAccountNumber = transaction.Account;

                ReceiverImage = "AccountOrange";
                ReceiverFullName = FullName;
                ReceiverAccountNumber = AccountNumber;
            }
            PopupNavigation.PushAsync(new Views.TransactionsOne(this));
        }

        public async Task GetTransactions()
        {
            var result = await _transactionService.GetTransaction30Days(Email);
            if (result != null && result.IsError != true)
            {
                if (result.Model.Count != 0)
                {
                    Transactionlist = result.Model.Where(x => x.TransactionType == "TopUp").ToList();
                    LastestMonth = Transactionlist.Max(x => x.CreateDateTime);
                    FirstTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == LastestMonth.Month).ToList();
                    SecondTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month2.Month).ToList();
                    ThridTransactionList = Transactionlist.Where(x => x.CreateDateTime.Month == Month3.Month).ToList();
                }
            }
            //If Error popup errorPopupPage
        }


        public ICommand BackButtonCommand { get; private set; }
        private void BackButton(object obj)
        {
            PopupNavigation.PopAsync();
        }

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

        private DateTime lastMonth;

        public DateTime LastestMonth
        {
            get { return lastMonth; }
            set
            {
                lastMonth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Month2));
                OnPropertyChanged(nameof(Month3));
            }
        }
        public DateTime Month2
        {
            get { return LastestMonth.AddMonths(-1); }
            set { Month2 = value; }
        }
        public DateTime Month3
        {
            get { return LastestMonth.AddMonths(-2); }
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

        public string PayerImage { get; set; }
        public string PayerFullName { get; set; }
        public string PayerAccountNumber { get; set; }

        public string ReceiverImage { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverAccountNumber { get; set; }

        public string TransactionName { get; set; }
        public decimal TransactionPaid { get; set; }
        public string TransactionReference { get; set; }

        

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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
