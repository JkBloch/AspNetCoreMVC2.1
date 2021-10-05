using System.Collections.Generic;
using System.Threading.Tasks;
using JkBook.Models;

namespace JkBook.Repository
{
    public interface IHRRepository
    {
        Task DeleteEmployeeById(int id);
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<int> InsertEmployee(EmployeeModel model);
        Task<EmployeeModel> UpdateEmployee(EmployeeModel model);

        Task DeleteDepartmentById(int id);
        Task<List<DepartmentModel>> GetAllDepartments();
        Task<DepartmentModel> GetDepartmentById(int id);
        Task<int> InsertDepartment(DepartmentModel model);
        Task<DepartmentModel> UpdateDepartment(DepartmentModel model);

        Task DeleteAddressBookById(int id);
        Task<List<AddressBookModel>> GetAllAddressBooks();
        Task<AddressBookModel> GetAddressBookById(int id);
        Task<int> InsertAddressBook(AddressBookModel model);
        Task<AddressBookModel> UpdateAddressBook(AddressBookModel model);
    }
}