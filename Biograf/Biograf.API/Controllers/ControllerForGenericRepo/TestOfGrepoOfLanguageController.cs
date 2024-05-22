using Azure;
using Biograf.Repo.DTOs;
using Biograf.Repo.Interface.GenericInterface;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biograf.API.Controllers.GenericController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestOfGrepoOfLanguageController : ControllerBase
    {
        private readonly IGeneric<Language> _genericRepo;

        public TestOfGrepoOfLanguageController(IGeneric<Language> genericRepo)
        {
           _genericRepo = genericRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Create(LanguageRequest language)
        {
            var newLanguage = new Language
            {
                Name = language.Name,
                MovieId = language.MovieId,
            };
            var createdLanguage = _genericRepo.Create(newLanguage);
            var response = new LanguageDto
            {
                Id = newLanguage.Id,
                Name = newLanguage.Name,
                MovieId = newLanguage.MovieId,
            };
            return Ok(response);
        }

    
      

    }
}
