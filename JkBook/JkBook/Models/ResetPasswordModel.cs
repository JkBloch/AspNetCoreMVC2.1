using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required,DataType(DataType.Password),Display(Name ="New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        [Compare("NewPassword",ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public bool IsSuccess { get; set; }
    }
}
