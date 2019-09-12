using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EV.Customer.Droid;
using EV.Customer.Interfaces;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(QrCodeScanner))]

namespace EV.Customer.Droid
{
    public class QrCodeScanner : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var scanner = new MobileBarcodeScanner();

            var scanResult = await scanner.Scan();
            return scanResult.Text;
        }
    }
}
