using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class EmployeeViewModel
    {
        public List<EmployeeModel> Employees { get; set; }
        public EmployeeModel SelectedEmployee { get; set; }
        public string DisplayMode { get; set; }
    }
}
