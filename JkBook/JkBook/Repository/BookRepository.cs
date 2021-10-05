using JkBook.Database;
using JkBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration configuration;

        public BookRepository(BookStoreContext context,IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        //public int AddNewBook(BookModel model)
        //{
        //    var newBook = new Books()
        //    {
        //        Title = model.Title,
        //        Author=model.Author,
        //        Description = model.Description,
        //        TotalPages = model.TotalPages.HasValue? model.TotalPages.Value:0,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOn = DateTime.UtcNow
        //    };

        //    _context.Books.Add(newBook);
        //    _context.SaveChanges();
        //    return newBook.Id;
        //}

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                CoverImageUrl =model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl,
                UpdatedOn = DateTime.UtcNow
            };

            newBook.bookGalary = new List<BookGallery>();
            foreach (var file in model.galleryImage)
            {
                newBook.bookGalary.Add(new BookGallery()
                {
                    Name = file.Name,
                    Url = file.Url
                });

            }
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                 .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    LanguageName = book.language.Name,
                    Title = book.Title,
                     CoverImageUrl= book.CoverImageUrl,
                     TotalPages = book.TotalPages,
                     BookPdfUrl=book.BookPdfUrl

                }).ToListAsync();

            //var books = new List<BookModel>();
            //var allBooks = await _context.Books.ToListAsync();
            //if (allBooks?.Any()==true)
            //{
            //    foreach (var book in allBooks)
            //    {
            //            books.Add(new BookModel()
            //            {
            //                Author = book.Author,
            //                Category = book.Category,
            //                Description = book.Description,
            //                Id = book.Id,
            //                LanguageId = book.LanguageId,
            //                LanguageName = book.language.Name,
            //                Title = book.Title,
            //                TotalPages = book.TotalPages

            //            });
            //    }

            //}
            //return books;


        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     LanguageId = book.LanguageId,
                     LanguageName = book.language.Name,
                     Title = book.Title,
                     CoverImageUrl = book.CoverImageUrl,
                     TotalPages = book.TotalPages,
                     BookPdfUrl = book.BookPdfUrl

                 }).Take(count).ToListAsync();

         
        }

        public async Task<BookModel> GetBookById(int id)
        {

            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    LanguageName = book.language.Name,
                    Title = book.Title,
                    CoverImageUrl = book.CoverImageUrl,
                    galleryImage = book.bookGalary.Select(g => new GalleryImageModel()
                    {
                        Id=g.Id,
                        Name=g.Name,
                        Url=g.Url

                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl,
                    TotalPages = book.TotalPages

                }).FirstOrDefaultAsync();

            //var book = await _context.Books.FindAsync(id);
            ////book = await _context.Books.Where(x=>x.Id==id).FirstOrDefaultAsync();
            //if (book!=null)
            //{
            //    var bookDetails = new BookModel()
            //    {
            //        Author = book.Author,
            //        Category = book.Category,
            //        Description = book.Description,
            //        Id = book.Id,
            //        LanguageId = book.LanguageId,
            //        LanguageName = book.language.Name,
            //        Title = book.Title,
            //        TotalPages = book.TotalPages

            //    };
            //    return bookDetails;
            //}
            //return null;

        }
        public List<BookModel> SearchBooks(string title,string author)
        {
            return null;
            //var objDataSource = DataSource();
            //return objDataSource.Where(x => x.Title == title).Where(x=>x.Author==author).ToList();
        }

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){Id=1,Title="Mvc",Author="JK",Description="This is book for Mvc begin",Category="Programming",Language="English",TotalPages=1254},
        //        new BookModel(){Id=2,Title="Mvc",Author="JK",Description="This is book for Advance concept of MVC",Category="Programming",Language="Hindi",TotalPages=3356},
        //        new BookModel(){Id=3,Title="C#",Author="Javed",Description="This is book for C# beginner",Category="Framwork",Language="English",TotalPages=758},
        //        new BookModel(){Id=4,Title="Java",Author="Bloch",Description="This is book for Java Advance",Category="Concept",Language="Gujarati",TotalPages=476},
        //        new BookModel(){Id=5,Title="Php",Author="JKB",Description="This is book for Php beginner to master",Category="Programming",Language="English",TotalPages=985},
        //        new BookModel(){Id=6,Title="MSSQL",Author="Javed",Description="This is book for all advance concept of MS SQL",Category="Programming",Language="English",TotalPages=2547},
        //        new BookModel(){Id=7,Title="MYSQL",Author="JKB",Description="This is book for MY SQL 2014 book",Category="Programming",Language="English",TotalPages=256},

        //    };
        //}

        public string GetAppName()
        {
            return configuration["AppName"];
        }
    }
}

