using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Models;
using JkBook.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JkBook.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController()
        {
            _bookRepository = new BookRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult GetAllBooks()
        {
            var _data= _bookRepository.GetAllBooks();
            return View(_data);
        }

        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return _bookRepository.SearchBooks(bookName,authorName);
         
        }

    }
}