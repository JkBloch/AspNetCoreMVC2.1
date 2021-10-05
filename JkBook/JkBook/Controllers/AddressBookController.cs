using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Models;
using JkBook.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JkBook.Controllers
{
    public class AddressBookController : Controller
    {
        private readonly IHRRepository _hrRepository = null;

        public AddressBookController(IHRRepository hrRepository)
        {
            _hrRepository = hrRepository;
        }

        // GET: AddressBook
        public async Task<IActionResult> Index()
        {
            var model = await _hrRepository.GetAllAddressBooks();
            return View(model);

        }

        public async Task<IActionResult> New(int id)
        {
            var model = await _hrRepository.GetAllAddressBooks();
            //model.DisplayMode = "WriteOnly";
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(AddressBookModel addressBookModel)
        {

            var id = await _hrRepository.InsertAddressBook(addressBookModel);
            var model = await _hrRepository.GetAllAddressBooks();
            return View("Index", model);
        }

        [HttpPost]
        public async Task<JsonResult> InsertJson(AddressBookModel addressBookModel)
        {

            var id = await _hrRepository.InsertAddressBook(addressBookModel);
         //   var model = await _hrRepository.GetAllAddressBooks();
            return Json(new { status = "Success" });
        }

        

        [HttpPost]
        public async Task<IActionResult> Select(int id)
        {
            var model = await _hrRepository.GetAllAddressBooks();
            return View("Index", model);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _hrRepository.GetAllAddressBooks();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Update(AddressBookModel addressBookModel)
        {
            var newAddressBookMode = await _hrRepository.UpdateAddressBook(addressBookModel);

            var model = await _hrRepository.GetAllAddressBooks();
            return View("Index", model);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJson(AddressBookModel addressBookModel)
        {
            var newAddressBookMode = await _hrRepository.UpdateAddressBook(addressBookModel);

            //    var model = await _hrRepository.GetAllAddressBooks();
            return Json(new { status = "Success" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _hrRepository.DeleteAddressBookById(id);


            var model = await _hrRepository.GetAllAddressBooks();
            return View("Index", model);
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(AddressBookModel AddressBookModel)
        {
            var model = await _hrRepository.GetAllAddressBooks();
            return View("Index", model);
        }
    }
}