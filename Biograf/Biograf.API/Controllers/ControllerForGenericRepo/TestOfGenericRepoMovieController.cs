using Azure.Core;
using Biograf.Repo.DTOs;
using Biograf.Repo.Interface.GenericInterface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories.GenericRepo;
using Humanizer;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biograf.API.Controllers.GenericController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestOfGenericRepoMovieController : ControllerBase
    {
        private readonly IGeneric<Movie> _genericRepo;
        public TestOfGenericRepoMovieController(IGeneric<Movie> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            //take all movies from the repository 
            var movies = _genericRepo.GetAll();
            
            return Ok(movies);

        }


        [HttpGet("{id}")]
        public ActionResult<Movie> GetById(int id)
        {
            var genre = _genericRepo.GetById(id);
            if (genre == null)
            {
                return NotFound(); //404 not found
            }
            return Ok(genre); // 200 ok reposne 
        }
        [HttpPost("MoviePhoto")]
        public ActionResult Create(Movie movie)
        {
            Movie newMovie = new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                Duration = movie.Duration,
                ReleasedDate = movie.ReleasedDate,
                Photos = movie.Photos.Select(m=> new Photo
                {
                    Id = m.Id,
                    Image = m.Image,
                }).ToList(),
                //Languages = movie.Languages.Select(m => new Language
                //{
                //    Id = m.Id,
                //    Name = m.Name,
                //}).ToList(),
                Categories = movie.Categories.Select(m => new Category
                {
                    Id = m.Id,
                    Name = m.Name,
                }).ToList(),
            };
            var createdActor = _genericRepo.CreateMoviePhoto(newMovie);
            return Ok(createdActor);
            
        }
        [HttpPost]
        public ActionResult Post([FromBody] Movie movie)
        {
            //Check if the model state is valid (based on data annotations )
            if (ModelState.IsValid)
            {
                //Create the new movie in the repository 
                _genericRepo.Create(movie);
                //Return the created movie as an HTTP 200 ok response 
                return Ok(movie);
            }
            // 400 bad request response 
            return BadRequest(ModelState);
        }

    }

}
