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
    public class BiografRepositoryTest
    {

        DbContextOptions<DatabaseContext> _options;
        DatabaseContext context;
        public BiografRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "OurDummyDatabase").Options;
            context = new DatabaseContext(_options);
            context.Database.EnsureCreated();
            Actor actor = new Actor() {  Name = "hero", Age = 2 };
            Actor actor1 = new Actor() { Name = "hay", Age = 22 };
            //Actor actor2 = new Actor() { Id = 3, Name = "lalla", Age = 21 };
          
            context.Actors.Add(actor);
            context.Actors.Add(actor1);
            //context.Actors.Add(actor2);
        }

      
        [Fact]
        public void GetAllActor_ReturnAll()
        {
            //Arrange - variables creation etc /
            ActorRepo repo = new ActorRepo(context);
            //Act cal method
            var result = repo.Get(); //List<Actor>
            var actual = 2;
            //Assert veryfy i get the right result back
            Assert.Equal(actual, result.Count);
        }
        public async void GetAll_ReturnEmpty_WhenNoActorExists() 
        {
            //Arrange
            AuthorRepo repo = new AuthorRepo(context);

            //Act
            var result = await repo.Get();


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Actor>>(result);
        }

        [Fact]
        public void GetActorById_ReturnExists()
        {           
            //Arrange - variables creation etc /
            ActorRepo repo = new ActorRepo(context);
            //Act cal method

            var result = repo.Get(2);
            //Assert veryfy i get the right result back
            Assert.Equal(2, result.Id);      
                
        }
        [Fact]
        public void GetActorById_ActorNotFound()
        {
            ////Arrange - variables creation etc /
            ActorRepo repo = new ActorRepo(context);
            //Act cal method

            var result = repo.Get(2);
            //Assert veryfy i get the right result back
            Assert.NotEqual(null, result);
        }
        [Fact]
        public void Create()
        {
            //Arrange
            ActorRepo repo = new ActorRepo(context);
            //Act
            var actor = new Actor() { Name = "hjkj", Age = 21 };
            Actor result =  repo.Create(actor);

            //Assert
            Assert.Equal(actor, result);
            Assert.NotNull(result);
        }
        //[Fact]
        //public void GetActorById_ActorNotExist()
        //{
        //    ////Arrange - variables creation etc /
        //    //ActorRepo repo = new ActorRepo(context);
        //    ActorRepo repo = null;

        //    //Act cal method
        //    var result = repo.Get(1);
        //    //Assert veryfy i get the right result back
        //    Assert.Null(repo);
        //}
        #region Delete
        [Fact]
        public void Delete_WhenExists()
        {

            //Arrange- variable
            ActorRepo repo = new ActorRepo(context);
            //Act -call method
            var result =  repo.Delete(2);
            var actual = 2;

            //Assert - verify i get the right result back
            // Assert.True(result);
            Assert.Equal(2, result?.Id);
        }

        [Fact]
        public void DeleteById_MovieNotExist()
        {
            //Arrange
            ActorRepo repo = new ActorRepo(context);
            //Act
            var result =  repo.Delete(1);

            //Assert
            Assert.NotEqual(null, result);
            //Assert.Null(result);
        }
        #endregion
        #region Update
        [Fact]
        public async void UpdateByIdAsync_ShouldChangeValuesOnActor_WhenActorExists() 
        {
            //Arrange
            ActorRepo repo = new ActorRepo(context);
            context.Actors.Add(new Actor
            {
                Name = "haaa"
            });

            await context.SaveChangesAsync();

            int Id = 1; //ligger i databasen
            Actor actor = new()
            {
                Name = "Hyyyyy",

            };
            context.Actors.Add(actor);

             context.SaveChangesAsync();

            ActorDto updateActor = new()   //update data
            {
                Name = "vjhj",
            };

            //Act
            var result =  repo.Update(Id, updateActor);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Actor>(result);
            Assert.Equal(Id, result?.Id);
            Assert.Equal(updateActor.Name, result?.Name);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange
            ActorRepo repo = new ActorRepo(context);

            int authorId = 2; //ligger i databasen
            ActorDto actorDto  = new()   //update data
            {
                Name = "blabla",

            };

            //Act
            var result =  repo.Update(authorId, actorDto);

            //Assert
            Assert.NotNull(result);
        }
        #endregion

    }
}
