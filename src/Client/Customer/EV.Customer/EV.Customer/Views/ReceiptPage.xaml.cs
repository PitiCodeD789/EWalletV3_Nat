using EV.Customer.Dependency;
using EV.Customer.ViewModels;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptPage : ContentPage
    {
        SKCanvas canvas;
        SKBitmap toBitmap;
        SlipViewModel receipt;

        public ReceiptPage(SlipViewModel slipViewModel)
        {
            InitializeComponent();
            try
            {
                receipt = slipViewModel;
                CreateReceipt();
                BindingContext = new ReceiptPageViewModel();
            }
            catch (Exception e)
            {
                DisplayAlert("Error", e.Message, "Ok");
            }

        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();
            //canvas.DrawBitmap(toBitmap, 0, 0);
            if (toBitmap != null)
            {
                info.Height = info.Width;
                var resizedBitmap = toBitmap.Resize(info, SKBitmapResizeMethod.Mitchell);
                canvas.DrawBitmap(resizedBitmap, info.Width / 2 - resizedBitmap.Width / 2, info.Height / 2 - resizedBitmap.Height / 2);
            }
        }

        async Task<bool> CreateReceipt()
        {
            try
            {
                receipt.CreateDate = receipt.CreateDate.ToLocalTime(); //= DateTime.SpecifyKind(receipt.CreateDate, DateTimeKind.Local);
                ICreateReceipt photoLibrary = DependencyService.Get<ICreateReceipt>();
                string filename = DateTime.Now.ToString("yyyyMMdd-hhmmss");
                string extension = ".png";
                var data = CreateImage();
                bool result = await photoLibrary.SavePhotoAsync(data, "Xamarin", filename + extension);
                sKCanvasView.HeightRequest = 300;
                sKCanvasView.PaintSurface += SKCanvasView_PaintSurface;  // += SKCanvasView_PaintSurface;
                if (!result)
                {
                    await DisplayAlert("Error", "Cannot save new receipt please enable local storage", "Ok");
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private byte[] CreateImage()
        {
            this.GetType().Assembly.GetManifestResourceNames();
            var assembly = typeof(ReceiptPage).GetTypeInfo().Assembly;

            //var webClient = new WebClient();
            //byte[] imageData = webClient.DownloadData("https://kasikornbank.com/SiteCollectionDocuments/about/img/logo/logo.png");
            SKBitmap bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("EV.Customer.Media.enixer_logo.jpg"));
            var canvasHeight = bitmap.Height + 20;
            var canvasWidth = bitmap.Width + 20;
            toBitmap = new SKBitmap(canvasWidth, canvasHeight, bitmap.ColorType, bitmap.AlphaType);

            canvas = new SKCanvas(toBitmap);
            canvas.Clear(SKColors.White);
            //canvas.SetMatrix(SKMatrix.MakeScale(resizeFactor, resizeFactor));
            SKPaint grayscalePaint = new SKPaint()
            {

                ColorFilter = SKColorFilter.CreateBlendMode(new SKColor(0, 0, 0), SKBlendMode.Multiply)

            };
            canvas.DrawBitmap(bitmap, 10, 10);
            canvas.ResetMatrix();

            SKBitmap customer = SKBitmap.Decode(assembly.GetManifestResourceStream("EV.Customer.Media.icon_user_circle_filled.png"));
            SKBitmap wallet = SKBitmap.Decode(assembly.GetManifestResourceStream("EV.Customer.Media.icon_wallet_circle_filled.png"));

            var fontStream = assembly.GetManifestResourceStream("EV.Customer.Media.THSarabunNew.ttf");
            var font = SKTypeface.FromStream(fontStream);

            var boldFontStream = assembly.GetManifestResourceStream("EV.Customer.Media.THSarabunNewBold.ttf");
            var boldFont = SKTypeface.FromStream(boldFontStream);


            var accountName = new SKPaint
            {
                Typeface = boldFont,
                TextSize = 45.0f,
                IsAntialias = true,
                Color = new SKColor(0, 0, 0, 255),
                TextAlign = SKTextAlign.Center
            };
            var accountNumber = new SKPaint
            {
                Typeface = font,
                TextSize = 35.0f,
                IsAntialias = true,
                StrokeWidth = 10.0f,
                Color = new SKColor(156, 156, 156, 255),
                TextAlign = SKTextAlign.Center

            };
            var dateBrush = new SKPaint
            {
                Typeface = font,
                TextSize = 40.0f,
                IsAntialias = true,
                StrokeWidth = 10.0f,
                Color = new SKColor(115, 111, 110, 255),
                TextAlign = SKTextAlign.Center
            };
            var paymentBrush = new SKPaint
            {
                Typeface = font,
                TextSize = 40.0f,
                IsAntialias = true,
                StrokeWidth = 10.0f,
                Color = new SKColor(156, 156, 156, 255),
                TextAlign = SKTextAlign.Center
            };
            var amountBrush = new SKPaint
            {
                Typeface = boldFont,
                TextSize = 100.0f,
                IsAntialias = true,
                StrokeWidth = 10.0f,
                Color = new SKColor(0, 0, 0, 255),
                TextAlign = SKTextAlign.Center
            };

            int scale = 14;
            canvas.DrawText("Customer",
                bitmap.Width / 4, canvasHeight / scale * 6, accountName);
            canvas.DrawText(receipt.OtherName,
                bitmap.Width / 4 * 3, canvasHeight / scale * 6, accountName);
            canvas.DrawText(App.Account,
                bitmap.Width / 4, canvasHeight / scale * 7, accountNumber);
            canvas.DrawText(receipt.OtherAccountNumber,
                bitmap.Width / 4 * 3, canvasHeight / scale * 7, accountNumber);
            canvas.DrawText(((receipt.Type == EW_Enumerations.EW_TypeTransectionEnum.TopUp) ? "Topup" : "Payment") + " (THB)",
                bitmap.Width / 4 * 2, canvasHeight / scale * 8, paymentBrush);

            canvas.DrawText(receipt.Amount.ToString("#,#.00#"),
                bitmap.Width / 4 * 2, canvasHeight / scale * 10, amountBrush);

            canvas.DrawText(receipt.CreateDate.ToString("dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                bitmap.Width / 4 * 2, canvasHeight / scale * 11, dateBrush);
            canvas.DrawText("Ref. " + receipt.Reference,
                bitmap.Width / 4 * 2, canvasHeight / scale * 12, dateBrush);

            canvas.DrawBitmap(customer, (bitmap.Width / 4 * 1) - (customer.Width / 2), canvasHeight / scale * 3);
            canvas.DrawBitmap(wallet, (bitmap.Width / 4 * 3) - (wallet.Width / 2), canvasHeight / scale * 3);

            canvas.Flush();

            var image = SKImage.FromBitmap(toBitmap);
            var data = image.Encode(SKEncodedImageFormat.Png, 90);
            return data.ToArray();
        }


    }
}