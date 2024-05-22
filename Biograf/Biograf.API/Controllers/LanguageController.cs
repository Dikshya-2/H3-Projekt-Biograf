using Azure.Core;
using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Biograf.Repo.Repositories.GenericRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biograf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguage _languageRepo;

        public LanguageController(ILanguage language)
        {
            _languageRepo = language;
        }
        [HttpGet]
        public async Task<List<LanguageDto>> Get()
        {
            var result = await _languageRepo.Get();

            List<LanguageDto> response = new();
            foreach (var item in result)
            {
                LanguageDto lang = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    MovieId = item.MovieId,
                };
                response.Add(lang);
            }
            return response;
        }
        //[HttpGet]
        //public async Task<List<Language>> Get()
        //{
        //    return await _languageRepo.Get();
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _languageRepo.Get(id));
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(LanguageDto language)
        //{
        //   // return Ok(await _languageRepo.Create(language));

        //    var newLanguage = await _languageRepo.Create(language);

        //    // set the id property of the language dto
        //    language.Id = newLanguage.Id;
        //    language.Name= newLanguage.Name;
        //    language.MovieId= newLanguage.MovieId;

        //    return Ok(language);
        //}


        [HttpPost]
        public async Task<IActionResult> Create(LanguageRequest language)
        {
            var newLanguage = new Language
            {
                Name = language.Name,
                MovieId = language.MovieId,
            };
            var createdLanguage = await _languageRepo.Create(newLanguage);
            var response = new LanguageDto
            {
                Id = newLanguage.Id,
                Name = newLanguage.Name,
                MovieId = newLanguage.MovieId,
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _languageRepo.Delete(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LanguageDto language)
        {
            //return Ok
            var updatedLanguage = (await _languageRepo.Update(id, language));
            return Ok(updatedLanguage);
        }
    }
}
