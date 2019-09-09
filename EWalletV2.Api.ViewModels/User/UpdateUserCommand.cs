using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.User
{
    public class UpdateUserCommand
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BitrhDate { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10})$")]
        public string MobileNumber { get; set; }

        [Required]
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
