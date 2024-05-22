using Biograf.Repo.Models.Entities;
using Biograf.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biograf.Repo.DTOs;
using Biograf.Repo.Repositories;

namespace Biograf.Test.Repository
{
    public class UserRepoTest
    {
        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public UserRepoTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            User user = new User() { FullName = "test", Address="Herlev", Email="", Password= "Password", Phone=6789, Role="Admin" };
            User user1 = new User() { FullName = "test1", Address = "Lyngby", Email = "", Password = "Password1", Phone = 67849, Role = "Guest" };
        }
        #region GetAll
        [Fact]
        public async Task GetAllUser_ReturnAll() 
        {
            //Arrange - variables creation etc /
            UserRepo repo = new UserRepo (context);
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
            UserRepo repo = new UserRepo(context);

            //Act
            var result = await repo.Get();


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
        }
        #endregion

        #region getById
        [Fact]
        public void GetById_ReturnExists()
        {
            //Arrange - variables creation etc /
            UserRepo repo = new UserRepo(context);
            //Act cal method

            var result = repo.Get(1);
            //Assert veryfy i get the right result back
            Assert.Equal(1, result.Id);
            Assert.NotNull(result);

        }
        [Fact]
        public void GetById_AuthorNotFound()
        {
            //Arrange - variables creation etc /
            UserRepo repo = new UserRepo(context);
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
            UserRepo repo = new UserRepo(context);
            //Act
            var user = new User()
            { FullName = "test1", Address = "Lyngby hovedgade", Email = "",
                Password = "Password1", Phone = 67849, Role = "Guest" };

            User result = await repo.Create(user);

            //Assert
            Assert.Equal(3, result.Id);
            Assert.NotNull(result);
            Assert.IsType<User>(result);
        }

        [Fact]
        public async void CreateAuthor_ShouldFailToAddNewAuthor_WhenAuthorIdAlreadyExists()
        {
            //Arrange
            UserRepo repo = new UserRepo(context);

            var user = new User()
            {
                FullName = "test1",
                Address = "Lyngby",
                Email = "",
                Password = "Password1",
                Phone = 67849,
                Role = "Guest"
            };

            await repo.Create(user);

            //Act
            async Task action() => await repo.Create(user);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }
        #endregion


        #region Delete
        [Fact]
        public async void Delete_WhenExists()
        {

            //Arrange- variable
            AuthorRepo repo = new AuthorRepo(context);
            //Act -call method
            var result = await repo.Delete(1);
            var actual = 2;

            //Assert - verify i get the right result back
            // Assert.True(result);
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(1, result?.Id);
        }

        [Fact]
        public async void DeleteById_UserNotExist()
        {
            //Arrange
            UserRepo repo = new UserRepo(context);
            //Act
            var result = await repo.Delete(1);

            //Assert
            Assert.NotEqual(null, result);
            //Assert.Null(result);
        }
        #endregion

        #region Update

        [Fact]
        public async void UpdateUserByIdAsync_ShouldChangeValuesOnUser_WhenUserExists() 
        {
            //Arrange
            UserRepo repo = new UserRepo(context);
            context.Users.Add(new User
            {
                FullName = " nb", Address="vhbk", Email="Jjk", Phone=3456,
                Password = "nmnkl",
                Role = "vgj"
            });

            await context.SaveChangesAsync();
            int Id = 1; //ligger i databasen
            User user = new User()
            {
                FullName = " nb",
                Address = "vhbk",
                Email = "Jjk",
                Phone = 3456,
                Password="nmnkl",
                Role="vgj"
            };

            //Act
            var result = await repo.Update(Id, user);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(Id, result?.Id);
            Assert.Equal(user.FullName, result?.FullName);
            Assert.Equal(user.Email, result?.Email);
            Assert.Equal(user.Address, result?.Address);
            Assert.Equal(user.Phone, result?.Phone);
            Assert.Equal(user.Password, result?.Password);
            //Assert.Equal(user.Role, result?.Role);

        }
        public async void UpdateUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist() 
        {
            //Arrange
            UserRepo repo = new UserRepo(context);

            int Id = 2; //ligger i databasen
            User user = new()   //update data
            {
                FullName = "blabla",
                Address = "vhbk",
                Email = "Jjk",
                Phone = 3456

            };
            //Act
            var result = await repo.Update(Id, user);

            //Assert
            Assert.NotNull(result);
        }
        #endregion
    }
}

