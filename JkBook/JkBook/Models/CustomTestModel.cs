using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class CustomTestModel
    {
        [DataType(DataType.CreditCard)]
        [Display(Name = "Enter Credit Card")]
        public string MyField1 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Enter Currency")]
        public string MyField2 { get; set; }

        //[DataType(DataType.Custom)]
        //[Display(Name = "Enter Custom")]
        //public string MyField3 { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Enter Date")]
        public string MyField4 { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Enter DateTime")]
        public string MyField5 { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Enter Duration")]
        public string MyField6 { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Enter EmailAddress")]
        public string MyField7 { get; set; }

        [DataType(DataType.Html)]
        [Display(Name = "Enter Html")]
        public string MyField8 { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Enter Image Url")]
        public string MyField9 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Enter Multiline Text")]
        public string MyField10 { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        public string MyField11 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Enter Phone Number")]
        public string MyField12 { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Enter Postal Code")]
        public string MyField13 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Enter Text")]
        public string MyField14 { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Enter Time")]
        public string MyField15 { get; set; }


        //[DataType(DataType.Upload)]
        //[Display(Name = "Enter Upload")]
        //public string MyField16 { get; set; }

        //[DataType(DataType.Url)]
        //[Display(Name = "Enter Url")]
        //public string MyField17 { get; set; }


    }
}
