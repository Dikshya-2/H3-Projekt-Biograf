using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biograf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovie movie;

         private readonly IMovie _movieRepo;

        public MovieController(IMovie movie, IPhotoRepo photoRepo)
        {
            _movieRepo = movie;
        }
        [HttpGet]
        public async Task<List<Movie>> Get()
        {
            return await _movieRepo.Get();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>Get(int id)
        {
            return Ok( await _movieRepo.Get(id));
        }

        [HttpGet("categoryId/{id}")]
        public async Task<IActionResult> GetMovieByCategory(int id)
        {
            return Ok(await _movieRepo.GetMovieByCategoryId(id));
        }

        [HttpGet("moviedetail/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            return Ok(await _movieRepo.GetDetail(id));
        }

        [HttpGet("name")]
        public async Task<IActionResult>GetByName(string name) 
        {
            return Ok( await _movieRepo.Get(name));
        }     

        [HttpGet("search")]
        public async Task<IActionResult>Search(string query)
        {
         
            return Ok( await _movieRepo.Search(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieDto movie)
        {
            return Ok(await _movieRepo.Create(movie));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MovieDto movie)
        {

            return Ok(await _movieRepo.Update(id, movie));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            return Ok(await _movieRepo.Delete(id));
        }
     
    }
}
