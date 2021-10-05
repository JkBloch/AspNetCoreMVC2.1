using JkBook.Database;
using JkBook.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _context = null;

        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            
            var lstLanguage =  await _context.Language.Select(x => new LanguageModel()
            {
                Id=x.Id,
                Name = x.Name,
                Description = x.Description,
                GroupId=x.GroupId

            }).ToListAsync();

            return lstLanguage;
        }
    }
}
