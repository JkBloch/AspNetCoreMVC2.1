using System.Collections.Generic;
using System.Threading.Tasks;
using JkBook.Models;

namespace JkBook.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}