using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        [Required]
        public String DepartmentName { get; set; }
    }
}
