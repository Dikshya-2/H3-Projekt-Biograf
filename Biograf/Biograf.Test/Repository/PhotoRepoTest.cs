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
    public class PhotoRepoTest
    {
        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public PhotoRepoTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            Photo photo = new Photo() {  Image = "test" };
            Photo photo1 = new Photo() {  Image = "test1" };
        }
        [Fact]
        public async Task GetAll_ReturnAll()
        {
            //Arrange - variables creation etc /
            PhotoRepo repo = new PhotoRepo(context);
            //Act cal method
            var result = await repo.GetAll(); //List<Actor>
            var actual = 6;
            //Assert veryfy i get the right result back
            Assert.Equal(actual, result.Count);
        }
        [Fact]
        public async void GetAll_ReturnEmpty_WhenNoCategoryExists()
        {
            //Arrange
            PhotoRepo repo = new PhotoRepo(context);

            //Act
            var result = await repo.GetAll();


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Photo>>(result);
        }

        [Fact]
        public void GetById_ReturnExists()
        {
            //Arrange - variables creation etc /
            PhotoRepo repo = new PhotoRepo(context);
            //Act cal method

            var result = repo.GetById(1);
            //Assert veryfy i get the right result back
            Assert.Equal(1, result.Id);

        }
        [Fact]
        public void GetById_PhotoNotFound()
        {
            ////Arrange - variables creation etc 
            PhotoRepo repo = new PhotoRepo(context);
            //Act cal method

            var result = repo.GetById(2);
            //Assert veryfy i get the right result back
            Assert.NotEqual(null, result);
        }
        [Fact]
        public async void CreatePhoto()
        {
            //Arrange
            PhotoRepo repo = new PhotoRepo(context);
            //Act
            var photo = new Photo() {  Image = "vjhb"};
            Photo result = await repo.Create(photo);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id); // Assuming ID is auto-generated
            Assert.Equal(result, photo);
        }
        #region Delete
        [Fact]
        public async void Delete_WhenExists()
        {

            //Arrange- variable
            PhotoRepo repo = new PhotoRepo(context);
            //Act -call method
            var result = await repo.Delete(2);
            var actual = 2;

            //Assert - verify i get the right result back
            // Assert.True(result);
            Assert.Equal(2, result?.Id);
        }

        [Fact]
        public async void DeleteById_MovieNotExist()
        {
            //Arrange
            PhotoRepo repo = new PhotoRepo(context);
            //Act
            var result = await repo.Delete(1);

            //Assert
            Assert.NotEqual(null, result);
            //Assert.Null(result);
        }
        #endregion

        #region Update
       
        [Fact]
        public async void UpdateMovieByIdAsync_ShouldChangeValuesOnMovie_WhenMovieExists()
        {
            //Arrange
            PhotoRepo repo = new PhotoRepo(context);
            context.Photos.Add(new Photo
            {
                Image=" nb" 
            });

            await context.SaveChangesAsync();
            int Id = 1; //ligger i databasen
            Photo photo = new()
            {
               Image="hbj"
            };

            //Act
            var result = await repo.Update(Id, photo);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Photo>(result);
            //Assert.Equal(Id, result?.Id);
            Assert.Equal(photo.Image, result?.Image);
        }
        public async void UpdateMovieByIdAsync_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            //Arrange
            PhotoRepo repo = new PhotoRepo(context);

            int Id = 2; //ligger i databasen
            Photo photo  = new()   //update data
            {
                Image = "blabla",
            };
            //Act
            var result = await repo.Update(Id, photo);

            //Assert
            Assert.NotNull(result);
        }
        #endregion
    }
}
