using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JkBook.Enums;
using JkBook.Helpers;
using Microsoft.AspNetCore.Http;

namespace JkBook.Models
{
    public class BookModel
    {
     
        public int Id { get; set; }
        //[StringLength(100,MinimumLength =5)]
        //[Required(ErrorMessage ="Please enter title of your book")]
        //[MyCustomValidation("C#")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the auther name ")]
        public string Author { get; set; }
        //[StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }
        public string Category { get; set; }

        [Required(ErrorMessage = "Please select language of your book ")]
        public int LanguageId { get; set; }

        public string LanguageName { get; set; }

        //[Required(ErrorMessage = "select languages of your book ")]
        public List<string> MultiLanguage { get; set; }

        //[Required(ErrorMessage = "select Booktype of your book ")]
        public BookTypeEnum BookType { get; set; }
        [Required]
        [Display(Name ="Total pages of book")]
        public int? TotalPages{ get; set; }

        public string CoverImageUrl { get; set; }

        [Display(Name ="Cover Photo Of Book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        [Display(Name = "Gallery image Of Book")]
        [Required]
        public IFormFileCollection GalleryFiles{ get; set; }

        public List<GalleryImageModel> galleryImage { get; set; }


        public string BookPdfUrl { get; set; }

        [Display(Name = "Upload book in pdf format")]
        [Required]
        public IFormFile BookPdf{ get; set; }


    }
}
