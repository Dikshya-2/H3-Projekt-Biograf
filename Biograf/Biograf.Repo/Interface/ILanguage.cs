using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface ILanguage
    {
        Task<Language> Create(Language language);
       Task< List<Language>> Get();
        Task<Language> Get(int id);
       Task<Language> Delete(int id);
        Task<Language> Update(int id, LanguageDto language); 
    }
}
