using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.User
{
    public class EmailCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
