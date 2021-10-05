using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="Registed email address")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }

    }
}
