using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Models;
using JkBook.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JkBook.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHRRepository _hrRepository=null;

        public EmployeeController(IHRRepository hrRepository)
        {
            _hrRepository = hrRepository;
        }
        // GET: Employee
        public async Task<IActionResult> Index()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.SelectedEmployee = null;
            return View(model);
            
        }
        
        public async Task<IActionResult> New(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.DisplayMode = "WriteOnly";
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EmployeeModel employeeModel)
        {
           
            var id= await _hrRepository.InsertEmployee(employeeModel);
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.SelectedEmployee= await _hrRepository.GetEmployeeById(id);
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Select(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.SelectedEmployee = await _hrRepository.GetEmployeeById(id);
            model.DisplayMode = "ReadOnly";
            return View("Index", model);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.SelectedEmployee = await _hrRepository.GetEmployeeById(id);
            model.DisplayMode = "ReadWrite";
            return View("Index", model);
          
        }

        [HttpPost ]
        public async Task<IActionResult> Update(EmployeeModel employeeModel)
        {
           var newEmployeeMode=  await _hrRepository.UpdateEmployee(employeeModel);

            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees =  await _hrRepository.GetAllEmployees();
            model.SelectedEmployee = newEmployeeMode;
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

             await _hrRepository.DeleteEmployeeById(id);

            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.SelectedEmployee = null;
            model.DisplayMode = "";
            return View("Index", model);
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(EmployeeModel employeeModel)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employees = await _hrRepository.GetAllEmployees();
            model.SelectedEmployee = await _hrRepository.GetEmployeeById(employeeModel.Id);
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }
        //// GET: Employee/Details/5
        //public IActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Employee/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Employee/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Employee/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Employee/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Employee/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Employee/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //private List<EmployeeModel> GetAllEmployees()
        //{
        //    //return  new List<EmployeeModel>()
        //    //{
        //    //    new EmployeeModel{Id=1,EmployeeCode="EMP101",EmployeeName="ABC"},
        //    //    new EmployeeModel{Id=2,EmployeeCode="EMP102",EmployeeName="name 2"},
        //    //    new EmployeeModel{Id=2,EmployeeCode="EMp104",EmployeeName="name 3"},
        //    //    new EmployeeModel{Id=3,EmployeeCode="EMP105",EmployeeName="Name 4"}

        //    //};
        //}
    }
}