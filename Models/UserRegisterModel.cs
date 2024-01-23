using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserRegisterModel
    {

        [Required]       
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [Required]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        public bool IsChecked { get; set; }

    }
}
