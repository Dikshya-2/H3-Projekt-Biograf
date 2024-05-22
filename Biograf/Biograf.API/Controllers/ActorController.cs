using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Biograf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActor _repo;

        public ActorController(IActor actorRepo)
        {
            _repo = actorRepo;
        }

        [HttpGet]
        public List<Actor> Get()
        {
            var actor = _repo.Get();
            return actor;
            // eller
            //Actor actor = new Actor();
            //actor.Name = "Ryan";
            //actor.Age = 8;

            //return _repo.Get(actor);
        }

        [HttpPost]
        public ActorDto Create(ActorRequest request)
        {
            //map DTO to Domain Model
            var obj = new Actor
            {
                Name = request.Name,
                Age = request.Age,
            };
            // Save the new created Actor to the repository

            var createdActor = _repo.Create(obj);

            // Map Domain model to DTO
            var response = new ActorDto
            {
                Id = 1,
                Name = createdActor.Name,
                Age = createdActor.Age
            };
            return (response);
        }

        [HttpGet("{id}")]
        public Actor Get(int id)
        {
            return _repo.Get(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ActorDto actorDto)
        {
            try
            {
                // Call the Update method from your repository
                var updatedActor = _repo.Update(id, actorDto);

                // Check if the actor was not found
                if (updatedActor == null)
                {
                    return NotFound("Actor not found");
                }

                // Return the updated actor
                return Ok(updatedActor);
            }
            catch (Exception ex)
            {
                // Return a generic problem response if an exception occurs
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public Actor Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
