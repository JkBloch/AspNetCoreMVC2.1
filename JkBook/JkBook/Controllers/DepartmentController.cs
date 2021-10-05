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
    public class DepartmentController : Controller
    {
        private readonly IHRRepository _hrRepository = null;

        public DepartmentController(IHRRepository hrRepository)
        {
            _hrRepository = hrRepository;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var model= await _hrRepository.GetAllDepartments();
            return View(model);

        }

        public async Task<IActionResult> New(int id)
        {
            var model = await _hrRepository.GetAllDepartments();
            //model.DisplayMode = "WriteOnly";
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(DepartmentModel departmentModel)
        {

            var id = await _hrRepository.InsertDepartment(departmentModel);
            var model = await _hrRepository.GetAllDepartments();
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Select(int id)
        {
            var model= await _hrRepository.GetAllDepartments();
            return View("Index", model);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            var model= await _hrRepository.GetAllDepartments();
             return View("Index", model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentModel DepartmentModel)
        {
            var newDepartmentMode = await _hrRepository.UpdateDepartment(DepartmentModel);

            var model = await _hrRepository.GetAllDepartments();
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _hrRepository.DeleteDepartmentById(id);

            
            var model= await _hrRepository.GetAllDepartments();
            return View("Index", model);
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(DepartmentModel DepartmentModel)
        {
           var model= await _hrRepository.GetAllDepartments();
            return View("Index", model);
        }


    }
}