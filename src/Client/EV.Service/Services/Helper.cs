using EWalletV2.Api.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EV.Service.Services
{
    public static class Helper
    {
        public static string BaseUrl { get; set; } = "http://192.168.1.39:30000/api/";

        public static bool CheckSumTopup(GenerateTopupViewModel viewModel)
        {
            string sumString = "";
            sumString = viewModel.AccountNumber + viewModel.Amount + viewModel.ExpireDate + viewModel.FirstName + viewModel.ReferenceNumber;
            string hashedSum = SHA256Hash(sumString);
            if (hashedSum == viewModel.CheckSum)
            {
                return true;
            }
            return false;
        }

        public static string CheckSumTopupCreate(GenerateTopupViewModel viewModel)
        {
            string sumString = "";
            sumString = viewModel.AccountNumber + viewModel.Amount + viewModel.ExpireDate + viewModel.FirstName + viewModel.ReferenceNumber;
            string hashedSum = SHA256Hash(sumString);
            return hashedSum;
        }
        public static string SHA256Hash(string input)
        {
            SHA256 sha256 = SHA256Managed.Create();
            string secretKey = "BB19FBF21693FB2B084B6DDF0F29C21143812666B9A41AD1555EC2A7B62ADB0D2BAF5A3602674F0D1D247C5393E252C2E61D05EAEA4DEAC886175294DA349765";
            byte[] bytes = Encoding.UTF8.GetBytes(input + secretKey);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
