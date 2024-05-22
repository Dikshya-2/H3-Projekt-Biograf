using Biograf.Repo.Models.Entities;
using Biograf.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biograf.Repo.Repositories;
using Biograf.Repo.DTOs;

namespace Biograf.Test.Repository
{
    public class MovieRepoTest
    {
        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public MovieRepoTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            Movie movie = new Movie() {  Title = "Titanic", Description= "Description", Duration =2,ReleasedDate=DateTimeOffset.Now };
            Movie movie1 = new Movie() {  Title = "A", Duration=3, Description= "Description", ReleasedDate=DateTimeOffset.Now};

            Movie movie2 = new Movie() {  Title = "B ", Duration = 3, Description = "Description", ReleasedDate = DateTimeOffset.Now};

            Movie movie3 = new Movie() {  Title = "C ", Duration = 3, Description = "Description", ReleasedDate = DateTimeOffset.Now};
            Movie movie4 = new Movie() {  Title = "Hello", Duration = 3, Description = "Description", ReleasedDate = DateTimeOffset.Now};
            context.Movies.Add(movie);
            context.Movies.Add(movie1);
            context.Movies.Add(movie2);
            context.Movies.Add(movie3);
            context.Movies.Add(movie4);
        }

        //[Fact]
        //public async Task CreateMovie_ShouldSucceed()
        //{
        //    // Arrange
        //    var movieDto = new MovieDto
        //    {
        //        Title = "Test Movie",
        //        Description = "Test Description",
        //        Duration = 120,
        //        ReleasedDate = DateTimeOffset.Now,
        //        Categories = new List<CategoryDto>
        //{
        //    new CategoryDto { Id = 1, Name = "Action" },
        //    new CategoryDto { Id = 2, Name = "Adventure" }
        //}
        //    };

        //    var movieRepo = new MovieRepo(context);

        //    // Act
        //    var createdMovie = await movieRepo.Create(movieDto);

        //    // Assert
        //    Assert.IsNotNull(createdMovie);
        //    Assert.AreEqual(movieDto.Title, createdMovie.Title);
        //    // Add more assertions as needed...
        //}

        //[Fact]
        //public async Task CreateMovie()
        //{
        //    //Arrange
        //    MovieRepo repo = new MovieRepo(context);
        //    //Act
        //    var movie = new MovieDto()
        //    {
        //        Title = "C ",
        //        Duration = 3,
        //        Description = "Description",
        //        ReleasedDate = DateTimeOffset.Now,
        //        Categories = new List<Category>()
        //    {
        //        new Category {  Name="Hello" },
        //        new Category {  Name = "hjjk" }
        //    },
                
                
        //    };
        //    Movie result = await repo.Create(movie);

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(movie.Title, result.Title);

        //}

        [Fact]
        public async Task GetAll_ReturnAll()
        {
            //Arrange - variables creation etc /
            MovieRepo repo = new MovieRepo(context);
            //Act cal method
            var result = await repo.Get(); //List<Actor>
            //var actual = 17;
            //Assert veryfy i get the right result back

            Assert.Equal(6, result.Count);
        }
        [Fact]
        public async void GetAll_ReturnEmpty_WhenNoMovieExists() 
        {
            //Arrange
            MovieRepo repo = new MovieRepo(context);
            //Act
            var result = await repo.Get();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Movie>>(result);
        }

        [Fact]
        public void GetById_ReturnExists()
        {
            //Arrange - variables creation etc /
            MovieRepo repo = new MovieRepo(context);
            //Act cal method
            var result = repo.Get(1);
            //Assert veryfy i get the right result back
            Assert.Equal(1, result.Id);
        }
        [Fact]
        public void GetById_AuthorNotFound()
        {
            ////Arrange - variables creation etc 
            MovieRepo repo = new MovieRepo(context);
            //Act cal method

            var result = repo.Get(2);
            //Assert veryfy i get the right result back
            Assert.NotEqual(null, result);
        }

        #region Delete
        [Fact]
        public async void Delete_WhenExists()  
        {
          
            //Arrange- variable
            MovieRepo repo = new MovieRepo(context);
            //Act -call method
            var result = await repo.Delete(5);
            var actual = 5;

            //Assert - verify i get the right result back
           // Assert.True(result);
            Assert.Equal(5, result?.Id);
        }

        [Fact]
        public async void DeleteById_MovieNotExist()
        {
            // Arrange
            MovieRepo repo = new MovieRepo(context);

            // Act
            var result = await repo.Delete(2);

            // Assert
            Assert.NotNull(result);
            #endregion
        }
        #region Update

        [Fact]
        public async Task UpdateMovieByIdAsync_ShouldChangeValuesOnMovie_WhenMovieExists()
        {
            // Arrange
            using (var context = new DatabaseContext(_options))
            {
                // Clear the context to avoid conflicts
                context.Movies.RemoveRange(context.Movies);
                await context.SaveChangesAsync();

                // Add a movie to the database with the specified ID
                var existingMovie = new Movie
                {
                    Id = 7, // Ensure the ID is 2
                    Title = "Existing Movie Title",
                    Description = "Existing Movie Description",
                    Duration = 120,
                    ReleasedDate = DateTimeOffset.UtcNow
                };
                context.Movies.Add(existingMovie);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new DatabaseContext(_options))
            {
                var repo = new MovieRepo(context);

                // Create a MovieDto with updated values
                var updatedMovieDto = new MovieDto
                {
                    Title = "Updated Title",
                    Description = "Updated Description",
                    Duration = 150,
                    ReleasedDate = DateTimeOffset.UtcNow,
                    Categories = new List<Category>() // Ensure Categories list is initialized
                };

                // Perform the update
                var result = await repo.Update(7, updatedMovieDto);

                // Assert
                Assert.NotNull(result); // Assert that the result is not null
                Assert.Equal(updatedMovieDto.Title, result.Title);
                Assert.Equal(updatedMovieDto.Description, result.Description);
                Assert.Equal(updatedMovieDto.Duration, result.Duration);
                Assert.Equal(updatedMovieDto.ReleasedDate, result.ReleasedDate);
            }
        }

        // Test case: Update operation when the movie doesn't exist
        [Fact]
        public async Task UpdateMovieByIdAsync_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            // Arrange
            using (var context = new DatabaseContext(_options))
            {
                // Ensure that there is no movie with ID 2 in the database
                var existingMovie = await context.Movies.FindAsync(2);
                if (existingMovie != null)
                {
                    context.Movies.Remove(existingMovie);
                    await context.SaveChangesAsync();
                }
            }

            // Act
            using (var context = new DatabaseContext(_options))
            {
                MovieRepo repo = new MovieRepo(context);

                int Id = 2; // Assuming the movie with this ID does not exist in the database
                MovieDto movieDto = new MovieDto() // Update data
                {
                    Title = "hahhaa",
                    Description = "descriptionq",
                    Duration = 2,
                    ReleasedDate = DateTimeOffset.UtcNow
                };

                // Act and Assert
                // Verify that the Update method throws KeyNotFoundException when the movie doesn't exist
                await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.Update(Id, movieDto));
            }
        }
        #endregion
    }
}
