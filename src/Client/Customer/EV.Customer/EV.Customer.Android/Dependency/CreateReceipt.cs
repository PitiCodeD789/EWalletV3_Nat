using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using EV.Customer.Droid;
using SkiaSharp;
using Xamarin.Forms;
using EV.Customer.Droid.Dependency;
using EV.Customer.Dependency;

[assembly: Dependency(typeof(CreateReceipt))]
namespace EV.Customer.Droid.Dependency
{
    public class CreateReceipt : ICreateReceipt
    {
        public async Task<bool> SavePhotoAsync(byte[] data, string folder, string filename)
        {
            try
            {
                Java.IO.File picturesDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                Java.IO.File folderDirectory = picturesDirectory;

                if (!string.IsNullOrEmpty(folder))
                {
                    folderDirectory = new Java.IO.File(picturesDirectory, folder);
                    folderDirectory.Mkdirs();
                }

                using (Java.IO.File bitmapFile = new Java.IO.File(folderDirectory, filename))
                {
                    bitmapFile.CreateNewFile();

                    using (FileOutputStream outputStream = new FileOutputStream(bitmapFile))
                    {
                        await outputStream.WriteAsync(data);
                    }

                    // Make sure it shows up in the Photos gallery promptly.
                    MediaScannerConnection.ScanFile(Android.App.Application.Context,
                                                    new string[] { bitmapFile.Path },
                                                    new string[] { "image/png", "image/jpeg" }, null);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
