using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace EV.Customer.Helper
{
    public  class Unities
    {
        public static bool CheckEmailFormat(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    
       
    }
}
