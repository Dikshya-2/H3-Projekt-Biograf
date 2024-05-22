using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;

namespace Biograf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor _authorRepo;

        public AuthorController(IAuthor author)
        {
            _authorRepo = author;
        }
        [HttpGet]
        public async Task<List<Author>> Get()
        {
            return await _authorRepo.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _authorRepo.Get(id));
        }

        [HttpGet("Age")]
        public async Task<IActionResult> Age(int age)
        {
            var authors = await _authorRepo.GetAge(age);
            if (authors != null)
            {
                return Ok(authors);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("AgeList")]
        public async Task<IActionResult> ListofAge(int age)
        {
            var authors = await _authorRepo.GetAuthorsByAge(age);
            if (authors != null && authors.Count > 0)
            {
                return Ok(authors);
            }
            else
            {
                return NotFound();
            }
        }
        //[HttpGet("{age}")]
        //public async Task<IActionResult>Age(int age)
        //{
        //    return Ok( await _authorRepo.GetAge(age));
        //}

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto author)

        {
            return Ok( await _authorRepo.Create(author));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // return Ok(await _authorRepo.Delete(id));
            var obj = (await _authorRepo.Delete(id));
            if (obj != null)
            {
                return Ok(obj);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorDto author)
        {
            try
            {
                //return Ok
                var updatedAuthor = (await _authorRepo.Update(id, author));
                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Updating ", ex);

            }
        }
    }
}
