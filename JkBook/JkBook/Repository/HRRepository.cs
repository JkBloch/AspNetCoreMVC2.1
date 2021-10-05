using JkBook.Database;
using JkBook.Database;
using JkBook.Enums;
using JkBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Repository
{
    public class HRRepository : IHRRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration configuration;

        public HRRepository(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }


        #region Employee
     
        public async Task<int> InsertEmployee(EmployeeModel model)
        {
            var newEmployee = new Employee()
            {

                EmployeeCode = model.EmployeeCode,
                EmployeeName = model.EmployeeName,
                Gender = model.Gender.ToString(),
                EmployeeStatus = model.EmployeeStatus.ToString(),
                DateOfBirth = model.DateOfBirth
                
            };

         
            await _context.Employee.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee.Id;
        }
        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel model)
        {
            var existEmployee = await _context.Employee.Where(x => x.Id == model.Id).FirstOrDefaultAsync();


            existEmployee.Id = model.Id;
            existEmployee.EmployeeCode = model.EmployeeCode;
            existEmployee.EmployeeName = model.EmployeeName;
            existEmployee.Gender = model.Gender.ToString();
            existEmployee.EmployeeStatus = model.EmployeeStatus.ToString();
            existEmployee.DateOfBirth = model.DateOfBirth;

            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            return await _context.Employee
                 .Select(employee => new EmployeeModel()
                 {
                     Id = employee.Id,
                     EmployeeCode = employee.EmployeeCode,
                     EmployeeName = employee.EmployeeName,
                     Gender = (employee.Gender==GenderEnum.Male.ToString()? GenderEnum.Male : GenderEnum.Female),
                     EmployeeStatus = (employee.EmployeeStatus== StatusEnum.Active.ToString() ? StatusEnum.Active : StatusEnum.Left),                     
                     DateOfBirth = employee.DateOfBirth
                 }).ToListAsync();

        }
        public async Task<EmployeeModel> GetEmployeeById(int id)
        {

            return await _context.Employee.Where(x => x.Id == id)
                .Select(employee => new EmployeeModel()
                {
                    Id = employee.Id,
                    EmployeeCode = employee.EmployeeCode,
                    EmployeeName = employee.EmployeeName,
                    Gender = (employee.Gender == GenderEnum.Male.ToString() ? GenderEnum.Male : GenderEnum.Female),
                    EmployeeStatus = (employee.EmployeeStatus == StatusEnum.Active.ToString() ? StatusEnum.Active : StatusEnum.Left),

                    DateOfBirth = employee.DateOfBirth

                }).FirstOrDefaultAsync();
          

        }
        public async Task DeleteEmployeeById(int id)
        {
            var existEmployee = await _context.Employee.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Employee.Remove(existEmployee);
            await _context.SaveChangesAsync();

        }
        #endregion

        #region Department

        public async Task<int> InsertDepartment(DepartmentModel model)
        {
            var newDepartment = new Department()
            {

                DepartmentCode = model.DepartmentCode,
                DepartmentName = model.DepartmentName,

            };


            await _context.Department.AddAsync(newDepartment);
            await _context.SaveChangesAsync();
            return newDepartment.Id;
        }
        public async Task<DepartmentModel> UpdateDepartment(DepartmentModel model)
        {
            var existDepartment = await _context.Department.Where(x => x.Id == model.Id).FirstOrDefaultAsync();


            existDepartment.Id = model.Id;
            existDepartment.DepartmentCode = model.DepartmentCode;
            existDepartment.DepartmentName = model.DepartmentName;

            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<DepartmentModel>> GetAllDepartments()
        {
            return await _context.Department
                 .Select(Department => new DepartmentModel()
                 {
                     Id = Department.Id,
                     DepartmentCode = Department.DepartmentCode,
                     DepartmentName = Department.DepartmentName
                                 }).ToListAsync();

        }
        public async Task<DepartmentModel> GetDepartmentById(int id)
        {

            return await _context.Department.Where(x => x.Id == id)
                .Select(Department => new DepartmentModel()
                {
                    Id = Department.Id,
                    DepartmentCode = Department.DepartmentCode,
                    DepartmentName = Department.DepartmentName
                }).FirstOrDefaultAsync();


        }
        public async Task DeleteDepartmentById(int id)
        {
            var existDepartment = await _context.Department.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Department.Remove(existDepartment);
            await _context.SaveChangesAsync();

        }
        #endregion

        #region AddressBook

        public async Task<int> InsertAddressBook(AddressBookModel model)
        {
            var newAddressBook = new AddressBook()
            {

                Name = model.Name,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Pincode = model.Pincode,
                MobileNo = model.MobileNo,

            };


            await _context.AddressBook.AddAsync(newAddressBook);
            await _context.SaveChangesAsync();
            return newAddressBook.Id;
        }
        public async Task<AddressBookModel> UpdateAddressBook(AddressBookModel model)
        {
            var existAddressBook = await _context.AddressBook.Where(x => x.Id == model.Id).FirstOrDefaultAsync();


            existAddressBook.Id = model.Id;
            existAddressBook.Name = model.Name;
            existAddressBook.Address = model.Address;
            existAddressBook.City = model.City;
            existAddressBook.State = model.State;
            existAddressBook.Pincode = model.Pincode;
            existAddressBook.MobileNo = model.MobileNo;


            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<AddressBookModel>> GetAllAddressBooks()
        {
            return await _context.AddressBook
                 .Select(model => new AddressBookModel()
                 {
                     Id = model.Id,
                     Name = model.Name,
                     Address = model.Address,
                     City = model.City,
                     State = model.State,
                     Pincode = model.Pincode,
                     MobileNo = model.MobileNo,
                 }).ToListAsync();

        }
        public async Task<AddressBookModel> GetAddressBookById(int id)
        {

            return await _context.AddressBook.Where(x => x.Id == id)
                .Select(model => new AddressBookModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Pincode = model.Pincode,
                    MobileNo = model.MobileNo,
                }).FirstOrDefaultAsync();


        }
        public async Task DeleteAddressBookById(int id)
        {
            var existAddressBook = await _context.AddressBook.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.AddressBook.Remove(existAddressBook);
            await _context.SaveChangesAsync();

        }
        #endregion
    }
}
