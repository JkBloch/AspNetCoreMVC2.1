using JkBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Database
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string EmployeeStatus { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int DepartmentId { get; set; }

        public Department department { get; set; }
    }
}
