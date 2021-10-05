using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JkBook.Enums;
using JkBook.Helpers;
using Microsoft.AspNetCore.Http;

namespace JkBook.Models
{
    public class AddressBookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the name ")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        [Display(Name = "Mobile No")]
        public int MobileNo { get; set; }        

     

    }
}
