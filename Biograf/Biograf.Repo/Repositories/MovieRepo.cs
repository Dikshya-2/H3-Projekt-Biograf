using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Repositories
{
    public class MovieRepo : IMovie
    {
        private readonly DatabaseContext _context;
        public MovieRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<MovieResponse> Create(MovieDto movieDto)
        {
            // Create the new movie entity from the DTO
            var newMovie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                Duration = movieDto.Duration,
                ReleasedDate = movieDto.ReleasedDate
            };

            // Add the new movie to the context and save changes to generate the ID
            _context.Movies.Add(newMovie);

            // Adding categories
            foreach (var categoryDto in movieDto.Categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryDto.Id);
                if (category != null)
                {
                    newMovie.Categories.Add(category);
                }
            }

            // Save changes again to update the many-to-many relationship
            await _context.SaveChangesAsync();

            // Load the movie with its categories to ensure proper response mapping
            var movieWithCategories = await _context.Movies
                .Include(m => m.Categories)
                .FirstOrDefaultAsync(m => m.Id == newMovie.Id);

            // Map the movie entity to the response DTO
            var response = new MovieResponse
            {
                Id = movieWithCategories.Id,
                Title = movieWithCategories.Title,
                Description = movieWithCategories.Description,
                Duration = movieWithCategories.Duration,
                ReleasedDate = movieWithCategories.ReleasedDate,
                Categories = movieWithCategories.Categories.Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };

            return response;
        }
        public async Task<Movie> Delete(int id)
        {
            // Fetch the movie with its related photos
            var movie = await _context.Movies
                .Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return null; // Movie not found
            }
            //  Delete related photos
            _context.Photos.RemoveRange(movie.Photos);
            _context.Movies.Remove(movie);
            // Save changes to the database
            await _context.SaveChangesAsync();
            return movie;
        }

        //public async Task<Movie>Delete(int id)
        //{
        //    var obj = await _context.Movies.FindAsync(id);
        //    _context.Remove(obj);
        //    await _context.SaveChangesAsync();
        //    return obj;
        //}
        public async Task<List<Movie>> Get()
        {
           //return await _context.Movies.ToListAsync();
            return await _context.Movies.Include(m=>m.Actors).Include(m => m.Languages).Include(m=>m.Categories).Include(m=>m.Photos).ToListAsync();        
        }
        public async Task<Movie>Get(int id)
        {
            //return await _context.Movies.Include(m => m.Photos).Where(m => m.Photos.Any(c => c.Id == id)).FirstOrDefaultAsync(x => x.Id == id);

            return await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Movie>> GetMovieByCategoryId(int categoryId)
        {
            return await _context.Movies.Include(m => m.Categories).Where(m => m.Categories.Any(c => c.Id == categoryId)).ToListAsync(); // Filter movies by categoryId
        
        }
        public async Task<List<MovieDetail>> GetDetail(int id)
        {
            // Fetch the movie details by ID
            var result = await _context.Movies
                .Include(m => m.Languages)
                .Include(m => m.Categories)
                .Include(m => m.Photos)
                .Where(m => m.Id == id)
                .ToListAsync();

            // Map the result to a list of MovieDetail
            List<MovieDetail> response = new();
            foreach (var item in result)
            {
                MovieDetail movie = new()
                {
                    Title = item.Title,
                    Description = item.Description,
                    Duration = item.Duration,
                    ReleasedDate = item.ReleasedDate,
                    AuthorId = item.AuthorId, 
                    UserId = item.UserId,
                    Languages = item.Languages?.ToList() ?? new List<Language>(),
                    Categories = item.Categories?.ToList() ?? new List<Category>(),  
                    Photos = item.Photos?.ToList() ?? new List<Photo>()
                };
                response.Add(movie);
            }
            return response;
        }
        public async Task<Movie> Get(string name) 
        {
            //return await _context.Movies.FirstOrDefaultAsync(m => m.Title == name);
            return await _context.Movies.Include(m => m.Categories).Include(m => m.Photos).Include(m => m.Actors)
                 .FirstOrDefaultAsync(m => m.Title == name);
        }
        public async Task<List<Movie>> Search(string query)
        {

            if (!string.IsNullOrEmpty(query))
            {
                return await _context.Movies.Where(s => s.Title.Contains(query) || s.Description.Contains(query)).Include(s => s.Categories).Include(s => s.Photos).ToListAsync();

            }
            return null;
        }
        public async Task<MovieResponse> Update(int id, MovieDto movieDto)
        {
            // Fetch the existing movie
            var existingMovie = await _context.Movies
                .Include(m => m.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
            {
                // Handle the case when the movie doesn't exist
                throw new KeyNotFoundException("Movie not found.");
            }

            // Update the movie's properties
            existingMovie.Title = movieDto.Title;
            existingMovie.Description = movieDto.Description;
            existingMovie.Duration = movieDto.Duration;
            existingMovie.ReleasedDate = movieDto.ReleasedDate;

            // Handle the categories: clear current categories and add the new ones
            //Removes all existing categories associated with the movie.
            existingMovie.Categories.Clear();
            foreach (var categoryDto in movieDto.Categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryDto.Id);
                if (category != null)
                {
                    existingMovie.Categories.Add(category);
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Map the updated movie entity to the response DTO
            var response = new MovieResponse
            {
                Id = existingMovie.Id,
                Title = existingMovie.Title,
                Description = existingMovie.Description,
                Duration = existingMovie.Duration,
                ReleasedDate = existingMovie.ReleasedDate,
                Categories = existingMovie.Categories.Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };

            return response;
        }

    }
}
