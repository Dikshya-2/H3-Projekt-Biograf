using Biograf.Repo.Models.Entities;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Models
{
    public class DatabaseContext : DbContext
    {
      public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //MovieActor
            modelBuilder.Entity<Movie>().HasMany(m => m.Actors).WithMany(a => a.Movies).UsingEntity<Dictionary<string, object>>(
                "MovieActor", j => j.HasOne<Actor>().WithMany().HasForeignKey("ActorId"),
                j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));


            //MovieCategory
            modelBuilder.Entity<Category>().HasMany(m => m.Movies).WithMany(a => a.Categories).UsingEntity<Dictionary<string, object>>(
                "MovieCategory", j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"));
            //Language
            //test

            //modelBuilder.Entity<Language>().HasOne<Movie>().WithMany().HasForeignKey(l => l.MovieId).IsRequired();

            //modelBuilder.Entity<Language>().HasOne<Movie>().WithMany().HasForeignKey(l => l.MovieId).IsRequired();
            //modelBuilder.Entity<Language>().HasOne(l => l.Movie).WithMany(l=>l.Languages).HasForeignKey(m => m.MovieId);

            //Photo
            //modelBuilder.Entity<Photo>()
            //    .HasOne(p => p.Movie)
            //    .WithMany(m => m.Photos);        

            //modelBuilder.Entity<Movie>().HasOne(m => m.AuthorId).WithMany(a => a.Movies).HasForeignKey(m => m.AuthorId);                 // Movie has one Author
            // Author can have many Movies

            //modelBuilder.Entity<Movie>().HasOne(m => m.AuthorId).WithMany(a => a.Movies);




            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Dikshya", Email = "ryan@gmail.com", Phone = 676986, Address = "Lyngby hovedgade", Password = "Passw0rd", Role = "Gues" },
                new User { Id = 2, FullName = "Dikshya", Email = "dik@gmail.com", Phone = 343986, Address = "Lyngby hovedgade", Password = "Passw0rd", Role = "Admin" });


          
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Photos)  // Configure the relationship from Movie to Photo
                .WithOne()               // Movie has many Photo, but Photo has one Movie
                .HasForeignKey(p => p.MovieId);  // Define the foreign key property

            modelBuilder.Entity<Movie>().HasData(
               new Movie{ Id = 1,Title = "Titanic",
                   Description = "Description",
                   Duration = 2,
                   ReleasedDate = DateTimeOffset.Now,
                   AuthorId = 1,
                   UserId = 2
               },
               new Movie { Id = 2,Title = "TitanicA", Description = "Description",
                   Duration = 2,ReleasedDate = DateTimeOffset.Now,
                   AuthorId= 2,
                   UserId = 1,
               },
                new Movie{Id = 3, Title = "TitanicA",Description = "Description",
                    Duration = 2, ReleasedDate = DateTimeOffset.Now,
                    AuthorId = 2,
                    UserId = 2,
                },
                 new Movie{Id = 4, Title = "TitanicA",Description = "Description",
                     Duration = 2,ReleasedDate = DateTimeOffset.Now,
                     AuthorId = 1,
                     UserId = 2,
                 },
                  new Movie{Id = 5,Title = "TitanicA", Description = "Description",
                      Duration = 2, ReleasedDate = DateTimeOffset.Now,
                      AuthorId = 2,
                      UserId = 1,
                  },
                   new Movie
                   {Id = 6, Title = "TitanicA",Description = "Description",
                       Duration = 2, ReleasedDate = DateTimeOffset.Now,
                       AuthorId = 1,
                       UserId = 1,
                   }) ;
            //    modelBuilder.Entity<Photo>()
            //.HasOne(p => p.Movie)
            //.WithMany(m => m.Photos)
            //.HasForeignKey(p => p.MovieId)
            //.OnDelete(DeleteBehavior.Restrict);
            //means that you cannot delete a movie if there are any photos still linked to it.
           // In other words, you must first delete or reassign the photos before you can delete the movie.

            modelBuilder.Entity<Photo>().HasData(
                new Photo { Id = 1, Image = "Billede.jpg" , MovieId= 1},
                new Photo { Id = 2, Image = "BilledeA.jpg", MovieId=2},
                new Photo { Id = 3, Image = "BilledeB.jpg", MovieId = 3 },
                new Photo { Id = 4, Image = "BilledeC.jpg", MovieId= 4 },
                new Photo { Id = 5, Image = "BilledeD.jpg" , MovieId = 5 },
                new Photo { Id = 6, Image = "th.jpg" , MovieId = 6 } );

            modelBuilder.Entity<Actor>().HasData(
               new Actor { Id = 1, Name = "Oscar", Age = 12 },
               new Actor { Id = 2, Name = "Rahul", Age = 33});

           

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Drama" },
                new Category { Id = 3, Name = "Comedy" },
                new Category { Id = 4, Name = "Historical" },
                new Category { Id = 5, Name = "Romantic" });

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Ryan",Age=8 },
                new Author { Id = 2, Name = "Hridaya", Age=1});


            //modelBuilder.Entity<Language>().HasOne(m => m.Movie).WithMany(a => a.Languages).HasForeignKey(a => a.Movie.Id);

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "Danish", MovieId=1},  
                new Language { Id = 2, Name = "English", MovieId=2}); //, MovieId = 2 
        }
    }
}
