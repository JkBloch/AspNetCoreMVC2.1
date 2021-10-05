using JkBook.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        [Required]
        public String EmployeeName { get; set; }        
        public GenderEnum Gender { get; set; }
        public StatusEnum EmployeeStatus { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
