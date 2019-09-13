using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

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
            if (date.Length < 1)
            {
                return false;
            }
            string strRegex =
                    @"^([0-9]{2}).([0-9]{2}).([0-9]{4})$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(date))
            {
                return true;
            }
            else
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
            else
            {
                mobilePhone = Regex.Replace(mobilePhone, @"[^\d]", "");
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
