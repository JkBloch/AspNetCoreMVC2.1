using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Models;
using JkBook.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JkBook.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IHostingEnvironment _iHostingEnvironment = null;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository,
            IHostingEnvironment iHostingEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _iHostingEnvironment = iHostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataTypeTest()
        {
            return View();
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var _data= await _bookRepository.GetAllBooks();
            return View(_data);
        }
        [Route("book-details/{Id:int:min(1)}", Name = "bookDetails")]
        public async Task<ViewResult> GetBook(int id,string bookname)
        {
            var data= await _bookRepository.GetBookById(id);
            return View(data);
        }
        public ViewResult GetBookDynamic(int id,string companyname,string timeofaction)
        {
            dynamic data = new ExpandoObject();
            data.book = _bookRepository.GetBookById(id);
            data.SBIN = 154845;
            data.Publisher = "abcd";
            data.CompanyName = companyname;
            data.TimeOfAction = timeofaction;

            return View(data);

        }

       [Route("book-details-myR/{Id:int:min(1)}",Name ="bookDetailsRoute")]
        public ViewResult GetBookRouting(int id)
        {
            dynamic data = new ExpandoObject();
            data.book = _bookRepository.GetBookById(id);
            data.SBIN = 154845;
            data.Publisher = "abcd";
            data.CompanyName = "Route company";
            data.TimeOfAction = DateTime.Now.ToString();

            return View(data);

        }

        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return _bookRepository.SearchBooks(bookName,authorName);
         
        }

        [Authorize]
        public async Task<ViewResult> AddNewBook(bool isSuccess=false,int bookId=0)
        {
            //SetLanguageList();
            //ViewBag.LanguageList = new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
            //ViewBag.MultiLanguageList = ViewBag.LanguageList;
            // SetBookTypeList();
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        private async void SetLanguageList()
        {
            var group1 = new SelectListGroup() { Name = "Group 1" };
            var group2 = new SelectListGroup() { Name = "Group 2" };
            var group3 = new SelectListGroup() { Name = "Group 3" };
            var group4 = new SelectListGroup() { Name = "Group 4", Disabled = true };
            var languges = await _languageRepository.GetLanguages();

            ViewBag.LanguageList = languges.Select(x => new SelectListItem()
            {

                Text = x.Name,
                Value = x.Id.ToString(),
                Disabled = (x.Name== "Tamil" || x.Name== "Telugu"),
                Selected = x.Name== "English",
                Group = (x.GroupId == 1 ? group1 : (x.GroupId == 2 ? group2 : (x.GroupId == 3 ? group3 : group4)))

            }).ToList();

       
            ViewBag.MultiLanguageList = languges.Select(x => new SelectListItem()
            {

                Text = x.Name,
                Value = x.Id.ToString()

            }).ToList();

        }
      
        private void SetBookTypeList()
        {
            var booktypeList = new List<string> { "Script","Programming", "Database", "Framework" };
            ViewBag.BookTypeList = new SelectList(booktypeList, "Programming");
        }
        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //        new LanguageModel{Id=1,Text="Hindi",GroupId=1},
        //        new LanguageModel{Id=2,Text="English",GroupId=1},
        //        new LanguageModel{Id=3,Text="Tamil",GroupId=2},
        //        new LanguageModel{Id=4,Text="Gujarati",GroupId=2},
        //        new LanguageModel{Id=5,Text="Urdu",GroupId=3},
        //        new LanguageModel{Id=6,Text="Telugu",GroupId=3},
        //        new LanguageModel{Id=7,Text="Chines",GroupId=4},
        //        new LanguageModel{Id=8,Text="Russian",GroupId=4}

        //    };
        //}
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto!=null)
                {
                    string sBookImagePath = "BooksImage/CoverImage/";
                     bookModel.CoverImageUrl= await UploadImage(sBookImagePath,bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string sBookImagePath = "BooksImage/GalleryImage/";
                    bookModel.galleryImage = new List<GalleryImageModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallaryModel = new GalleryImageModel()
                        {
                            Name = file.Name,
                            Url = await UploadImage(sBookImagePath, file)
                        };
                        bookModel.galleryImage.Add(gallaryModel);


                    }
                    
                }

                if (bookModel.BookPdf != null)
                {
                    string sBookPdfPath = "BooksImage/PdfFile/";
                    bookModel.BookPdfUrl= await UploadImage(sBookPdfPath, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }


            }
            //ViewBag.LanguageList = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            //ViewBag.MultiLanguageList = ViewBag.LanguageList;
            ViewBag.IsSuccess = false;
            //SetLanguageList();
            //SetBookTypeList();
            return View();
        }

        private async Task<string> UploadImage(string folderPath,IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + file.FileName;
            
            string sServerFolder = Path.Combine(_iHostingEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(sServerFolder, FileMode.Create));

            return "/" + folderPath;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddNewBook_Async(BookModel bookModel)
        //{
        //    int id = await _bookRepository.AddNewBook_Async(bookModel);
        //    if (id > 0)
        //    {
        //        return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
        //    }
        //    return View();
        //}

    }
}