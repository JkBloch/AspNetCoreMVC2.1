using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Enums
{
    public enum BookTypeEnum
    {
        [Display(Name ="Programming Type")]
        Programming=1,

        [Display(Name = "Scripting Type")]
        Scripting =2,
        [Display(Name = "Database  Type")]
        Database =3,
        [Display(Name = "Framework  Type")]
        Framwork =4,
        [Display(Name = "User Interface Tools  Type")]
        UITools =5
    }

}
