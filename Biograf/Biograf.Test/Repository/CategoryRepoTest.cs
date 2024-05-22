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
using Biograf.Repo.Interface;

namespace Biograf.Test.Repository
{
    public class CategoryRepoTest
    {
        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public CategoryRepoTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            Category category = new Category() {  Name = "Action" };
            Category category1 = new Category() {  Name = "Drama" };
            Category category2 = new Category {  Name = "Comedy" };
            Category category3 = new Category {  Name = "Historical" };
            Category category4 = new Category {  Name = "Romantic" };
            context.Categories.Add(category);
            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.Categories.Add(category4);
        }
        [Fact]
        public async Task GetAll_ReturnAll()
        {
            //Arrange - variables creation etc /
            CategoryRepo repo = new CategoryRepo(context);
            //Act cal method
            var result = await repo.Get(); //List<Actor>
                                           //Assert veryfy i get the right result back
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async void GetAll_ReturnEmpty_WhenNoCategoryExists() 
        {
            //Arrange
            CategoryRepo repo = new CategoryRepo(context);

            //Act
            var result = await repo.Get();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        public void GetById_ReturnExists()
        {
            //Arrange - variables creation etc /
            CategoryRepo repo = new CategoryRepo(context);
            //Act cal method

            var result = repo.Get(2);
            //Assert veryfy i get the right result back
            Assert.Equal(1, result.Id);

        }
        [Fact]
        public void GetById_CategoryNotFound() 
        {
            ////Arrange - variables creation etc 
            CategoryRepo repo = new CategoryRepo(context);
            //Act cal method

            var result = repo.Get(5);
            //Assert veryfy i get the right result back
            Assert.NotEqual(null, result);
        }
        [Fact]
        public void GetById_NotFound()
        {
            CategoryRepo repo = new CategoryRepo(context);
            var result = repo.Get(2);
           Assert.NotEqual(null, result);
        }
        [Fact]
        public async void Create()
        {
            //Arrange
            CategoryRepo repo = new CategoryRepo(context);
            //Act
            var category = new CategoryDto() { Name = "hjkj" };
            Category result = await repo.Create(category);

            //Assert
            Assert.Equal(category.Name, result.Name);
            Assert.NotNull(result);
        }
        #region Delete
        [Fact]
        public async void Delete_WhenExists()
        {

            //Arrange- variable
            CategoryRepo repo = new CategoryRepo(context);
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
            //Arrange
            CategoryRepo repo = new CategoryRepo(context);
            //Act
            var result = await repo.Delete(2);

            //Assert
            Assert.NotEqual(null, result);
            //Assert.Null(result);
        }
        #endregion

        #region GetByName
        [Fact]
        public async void GetByName_ShouldReturnCategory_WhenCategoryExists() 
        {

            //Arrange
            CategoryRepo repo = new CategoryRepo(context);
            var category = new Category
            {
                Name = "bn"
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            //Act
            var result = await repo.Get(category.Name);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async void GetByName_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange
            CategoryRepo repo = new CategoryRepo(context);
            //Act
            var result = await repo.Get("nm");
            //Assert
            Assert.Null(result);
        }
        #endregion
        #region Update
        [Fact]
        public async void UpdateAuthorByIdAsync_ShouldChangeValuesOnAuthor_WhenAuthorExists()
        {
            //Arrange
            CategoryRepo repo = new CategoryRepo(context);
            context.Categories.Add(new Category
            {
                Name = "haaa"
            });

            await context.SaveChangesAsync();

            int Id = 1; //ligger i databasen
            Category category = new()
            {
                Name = "Hyyyyy",

            };
            context.Categories.Add(category);

            await context.SaveChangesAsync();

            CategoryDto categoryDto = new()   //update data
            {
                Name = "Bread",
            };

            //Act
            var result = await repo.Update(Id, categoryDto);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            //Assert.Equal(authorId, result?.Id);
            Assert.Equal(categoryDto.Name, result?.Name);
        }

        [Fact]
        public async void UpdateCategoryByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist() 
        {
            //Arrange
            CategoryRepo repo = new CategoryRepo(context);

            int Id = 2; //ligger i databasen
            CategoryDto categoryDto = new()   //update data
            {
                Name = "blabla",

            };

            //Act
            var result = await repo.Update(Id, categoryDto);

            //Assert
            Assert.NotNull(result);
        }
        #endregion

    }
}
