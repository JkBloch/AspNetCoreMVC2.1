using JkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            var objDataSource = DataSource();
            return objDataSource.Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBooks(string title,string author)
        {
            var objDataSource = DataSource();
            return objDataSource.Where(x => x.Title == title).Where(x=>x.Author==author).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1,Title="Mvc",Author="JK"},
                new BookModel(){Id=2,Title="Mvc",Author="JK"},
                new BookModel(){Id=3,Title="C#",Author="Javed"},
                new BookModel(){Id=4,Title="Java",Author="Bloch"},
                new BookModel(){Id=5,Title="Php",Author="JKB"},
                new BookModel(){Id=6,Title="MSSQL",Author="Javed"},
                new BookModel(){Id=7,Title="MYSQL",Author="JKB"},

            };
        }


    }
}

