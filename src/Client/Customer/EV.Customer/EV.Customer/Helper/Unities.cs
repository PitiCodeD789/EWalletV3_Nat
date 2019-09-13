using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace EV.Customer.Helper
{
    public class Unities
    {
        public static bool CheckEmailFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            try
            {
                MailAddress m = new MailAddress(email);
                if (Regex.IsMatch(email, "^[\\w\\.@]{0,64}$"))
                {
                    return true;
                }
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool CheckDigitaAndLength(string digit, int length)
        {
            string pattern = @"^\d{" + length + @"}$";
            return Regex.IsMatch(digit, pattern);
        }
        public static string BirthDateSet(string input)
        {
            if (input == null)
            {
                return "";
            }
            if (input.Length < 1)
            {
                return "";
            }

            char[] inputByte = input.ToCharArray();
            string formatDate = "";
            int check = input.Length;

             if (check > 10)
            {
                check = 10;
            }

          
          
            for (int i = 0; i < check; i++)
            {
               
                 if (i == 2 && inputByte[i] != '/')
                {
                    formatDate += "/";
                }
                else if (i == 5 && inputByte[i] != '/')
                {
                    formatDate += "/";
                }

                formatDate += inputByte[i];
            }
            
            return formatDate;



        }

       public static bool ValidateName(string name)
        {
            if (name == null)
            {
                return false;
            }
            if (name.Length < 1)
            {
                return false;
            }
            string strRegex =
                    @"^[a-zA-Zก-๋]{1,50}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool ValidateStringDateFormat(string date)
        {

            if (date == null)
            {
                return false;
            }
            if (date.Length < 1 && date.Length > 10)
            {
                return false;
            }

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime BirthDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);

                DateTime nowDate = DateTime.UtcNow;
                if(nowDate > BirthDate.AddYears(15))
                {
                    return true;
                }else
                {
                    return false;
                }
                
               
            }
            catch
            {
                return false;
            }
           
            

        }

        public static bool ValidateStringMobile(string mobilePhone)
        {

            if (mobilePhone == null)
            {
                return false;
            }
            if (mobilePhone.Length < 1)
            {
                return false;
            }
            string strRegex =
                    @"^[0]([0-9]{9})$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(mobilePhone))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
