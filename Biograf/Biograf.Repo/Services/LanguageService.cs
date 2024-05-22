using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Services
{
    public interface ILanguageService
    {
        Task<List<LanguageResponse>> GetAll();

    }
    public class LanguageService : ILanguageService
    {
        private readonly ILanguage _languageRepo;

        public LanguageService(ILanguage language)
        {
            _languageRepo = language;
        }

        public Task<List<LanguageResponse>> GetAll()
        {
            throw new NotImplementedException();
        }
        //public Task<List<LanguageResponse>> GetAll()
        //{
        //    List<Language> languages = new _languageRepo.GetAll();
        //    if (languages==0)
        //    {
        //        throw new ArgumentNullException();
        //        return languages.Select(Photo => MapPhotoToPhotoResponse(Photo)).ToList();
        //    }
        //}
    }
}

