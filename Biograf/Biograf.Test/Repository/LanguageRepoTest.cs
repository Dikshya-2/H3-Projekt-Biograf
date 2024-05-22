using Biograf.Repo.DTOs;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Test.Repository
{
    public class LanguageRepoTest
    {
        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public LanguageRepoTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            Language language = new Language() {  Name = "Danish", MovieId = 3 };
            Language language1 = new Language() { Name = "English", MovieId = 4 };
          
            context.Languages.Add(language);
            context.Languages.Add(language1);
        }
        [Fact]
        public async Task GetAll_ReturnAll()
        {
            //Arrange - variables creation etc /
            LanguageRepo repo = new LanguageRepo(context);
            //Act cal method
            var result = await repo.Get(); //List<Actor>
            var actual = 2;
            //Assert veryfy i get the right result back
            Assert.Equal(actual, result.Count);
        }
        [Fact]
        public async void GetAll_ReturnEmpty_WhenNoCategoryExists()
        {
            //Arrange
            LanguageRepo repo = new LanguageRepo(context);

            //Act
            var result = await repo.Get();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Language>>(result);
        }
        [Fact]
        public void GetById_ReturnExists()
        {
            //Arrange - variables creation etc /
            LanguageRepo repo = new LanguageRepo(context);
            //Act cal method

            var result = repo.Get(1);
            //Assert veryfy i get the right result back
            Assert.Equal(1, result.Id);

        }
        [Fact]
        public void GetById_LanguageNotFound()
        {
            ////Arrange - variables creation etc 
            LanguageRepo repo = new LanguageRepo(context);
            //Act cal method

            var result = repo.Get(2);
            //Assert veryfy i get the right result back
            Assert.NotEqual(null, result);
        }
        #region Delete
        [Fact]
        public async void Delete_WhenExists()
        {
            // Arrange
            LanguageRepo repo = new LanguageRepo(context);

            // Act
            var result = await repo.Delete(2);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteById_LanguageNotExist() 
        {
            //Arrange
            LanguageRepo repo = new LanguageRepo(context);
            //Act
            var result = await repo.Delete(1);

            //Assert
            Assert.NotEqual(null, result);
        }
        #endregion
        [Fact]
        public async void CreateLanguage()
        {
            //Arrange
            LanguageRepo repo = new LanguageRepo(context);
            //int expectedId = 2;
            //Act
            var language = new Language() {Id=1024, Name = "hjkj", MovieId=3 };
            var createdLanguage = await repo.Create(language);

            //Assert
            Assert.Equal(createdLanguage, language);

            //Assert.Equal(language.Id, createdLanguage.Id);
            //Assert.Equal(language.Name, createdLanguage.Name);
            //Assert.Equal(language.MovieId, createdLanguage.MovieId);
        }
        #region Update
        [Fact]
        public async void UpdateLanguageByIdAsync_ShouldChangeValuesOnLanguage_WhenLanguageExists() 
        {
            //Arrange
            LanguageRepo repo = new LanguageRepo(context);
            context.Languages.Add(new Language
            {
                Name = "haaa"
            });

            await context.SaveChangesAsync();

            int Id = 1; //ligger i databasen
            Language language  = new()
            {
                Name = "Hyyyyy",

            };
            context.Languages.Add(language);

            await context.SaveChangesAsync();

            LanguageDto languageDto  = new()   //update data
            {
                Name = "nmnm,",
            };

            //Act
            var result = await repo.Update(Id, languageDto);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Language>(result);
            //Assert.Equal(authorId, result?.Id);
            Assert.Equal(languageDto.Name, result?.Name);
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
