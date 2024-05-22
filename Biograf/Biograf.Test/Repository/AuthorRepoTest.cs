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
    public class AuthorRepoTest
    {
        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public AuthorRepoTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            Author author = new Author() {  Name = "hero", Age = 2 };
            Author author1 = new Author() {  Name = "hay", Age = 22 };

            context.Authors.Add(author);
            context.Authors.Add(author1);
            //context.Actors.Add(actor2);    
        }
        #region GetAll
        [Fact]
        public async Task GetAllAuthor_ReturnAll()
        {
            //Arrange - variables creation etc /
            AuthorRepo repo = new AuthorRepo(context);
            //Act cal method
            var result = await repo.Get(); //List<Actor>
            var actual = 2;
            //Assert veryfy i get the right result back
            Assert.Equal(actual, result.Count);
        }
        [Fact]
        public async void GetAll_ReturnEmpty_WhenNoAuthorExists()
        {
            //Arrange
            AuthorRepo repo = new AuthorRepo(context);

            //Act
            var result = await repo.Get(); 


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
        }
        #endregion

        #region getById
        [Fact]
        public async void GetById_ReturnExists() 
        {
            //Arrange - variables creation etc /
            AuthorRepo repo = new AuthorRepo(context);
            //Act cal method

            var result = await repo.Get(1);
            //Assert veryfy i get the right result back
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.IsType<Author>(result);

        }
        [Fact]
        public void GetById_AuthorNotFound() 
        {
            //Arrange - variables creation etc /
            AuthorRepo repo = new AuthorRepo(context);
            //Act cal method

            var result = repo.Get(2);
            //Assert veryfy i get the right result back
            Assert.NotEqual(null, result);
        }
        #endregion

        #region Create
        [Fact]
        public async void Create() 
        {
            //Arrange
            AuthorRepo repo = new AuthorRepo(context);
            //Act
            var author = new AuthorDto() { Name = "hjkj", Age=21 };
            Author result = await repo.Create(author);

            //Assert
            Assert.Equal(author.Name, result.Name);
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
        }

        #endregion


        #region Delete
        [Fact]
        public async void Delete_WhenExists()
        {

            //Arrange- variable
            AuthorRepo repo = new AuthorRepo(context);
            //Act -call method
            var result = await repo.Delete(2);
            var actual = 2;

            //Assert - verify i get the right result back
            // Assert.True(result);
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(2, result?.Id);
        }

        [Fact]
        public async void DeleteById_MovieNotExist()
        {
            //Arrange
            AuthorRepo repo = new AuthorRepo(context);
            //Act
            var result = await repo.Delete(1);

            //Assert
            Assert.NotEqual(null, result);
           // Assert.Null(result);
        }
        #endregion
        #region Update
        [Fact]
        public async void UpdateAuthorByIdAsync_ShouldChangeValuesOnAuthor_WhenAuthorExists() 
        {
            //Arrange
            AuthorRepo repo = new AuthorRepo(context);
            context.Authors.Add(new Author
            {
                Name = "haaa"
            });

            await context.SaveChangesAsync();

            int authorId = 1; //ligger i databasen
            Author author = new()
            {
                Name = "Hyyyyy",
               
            };
            context.Authors.Add(author);

            await context.SaveChangesAsync();

            AuthorDto updateAuthor = new()   //update data
            {
                Name = "Bread",
            };

            //Act
            var result = await repo.Update(authorId, updateAuthor);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            //Assert.Equal(authorId, result?.Id);
            Assert.Equal(updateAuthor.Name, result?.Name);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange
            AuthorRepo repo = new AuthorRepo(context);

            int authorId = 2; //ligger i databasen
            AuthorDto updateAuthor = new()   //update data
            {
                Name = "blabla",
                
            };

            //Act
            var result = await repo.Update(authorId, updateAuthor);

            //Assert
            Assert.NotNull(result);
        }
        #endregion

    }
}
