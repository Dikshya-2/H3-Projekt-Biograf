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
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepo _photoRepo;

        public PhotoController(IPhotoRepo photoRepo)
        {
            _photoRepo = photoRepo;
        }
        //[HttpGet]
        //public async Task<List<Photo>> Get()
        //{
        //    return await _photoRepo.GetAll();
        //}
        [HttpGet]
        public async Task<List<PhotoDto>> Get()
        {
            var result = await _photoRepo.GetAll();

            List<PhotoDto> response = new();
            foreach (var item in result)
            {
                PhotoDto lang = new()
                {
                    Id = item.Id,
                    Image = item.Image,
                    MovieId = item.MovieId,
                };
                response.Add(lang);
            }
            return response;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _photoRepo.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(PhotoRequest request)
        {
            try
            {
                // Create a new Photo entity from the request
                var photo = new Photo
                {
                    Image = request.Image,
                    MovieId = request.MovieId,
                };

                // Save the new photo to the repository
                await _photoRepo.Create(photo);

                // Create a DTO representation of the created photo
                var response = new PhotoDto
                {
                    Id = photo.Id,
                    Image = photo.Image,
                    MovieId = photo.MovieId,
                };

                // Return a 200 OK response along with the created photo DTO
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs during the creation process, return a problem response
                return Problem(ex.Message);
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(PhotoRequest request)
        //{

        //    var photo = new Photo
        //    {
        //            Image = request.Image,
        //            MovieId = request.MovieId,
        //    };
        //    await _photoRepo.Create(photo);

        //    var response = new PhotoDto
        //    {
        //        Id = photo.Id,
        //        Image = photo.Image,
        //        MovieId = photo.MovieId,
        //    };
        //    return Ok(response);

        //    //return Ok(await _photoRepo.Create(newPhoto));
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = (await _photoRepo.Delete(id));
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
        public async Task<IActionResult> Update(int id, [FromBody] PhotoRequest request)
        {
            try
            {
                // Retrieve the existing photo from the repository
                var existingPhoto = await _photoRepo.GetById(id);

                // If the photo with the given id doesn't exist, return NotFound
                if (existingPhoto == null)
                {
                    return NotFound($"Photo with ID {id} not found.");
                }

                // Update the properties of the existing photo
                existingPhoto.Image = request.Image;
                existingPhoto.MovieId = request.MovieId;

                // Update the existing photo in the repository
                var updatedPhoto = await _photoRepo.Update(id, existingPhoto);

                // Prepare the response DTO
                var response = new PhotoDto
                {
                    Id = updatedPhoto.Id,
                    Image = updatedPhoto.Image,
                    MovieId = updatedPhoto.MovieId
                };

                // Return a successful response with the updated photo data
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while updating the photo.");
            }
        }

    }
}
