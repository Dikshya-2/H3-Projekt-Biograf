//using Biograf.Repo.DTOs;
//using Biograf.Repo.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Biograf.Repo.Services
//{
//    public interface IMovieService
//    { Task<List<MovieResponse>> GetAll();
//    Task<MovieResponse> GetById(int id);
//    Task<MovieResponse> Create(MovieRequest newMovie);
//    Task<MovieResponse?> Update(int id, MovieRequest updateMovie); //dto object send ind og dto objekt som kommer ud.
//    Task<MovieResponse?> Delete(int id);
//    Task<MovieResponse> CreatePhoto(int id, PhotoRequest photoRequest);
//    Task<MovieResponse> GetByName(string name);
//    Task<List<MovieResponse>> SearchName(string search);

//    }
//    public class MovieService : IMovieService
//    {
//        private readonly IMovie _movieRepo;
//        private readonly IPhotoRepo _photoRepo;

//        public MovieService(IMovie movieRepo, IPhotoRepo photoRepo)
//        {
//           _movieRepo = movieRepo;
//            _photoRepo = photoRepo;
//        }
//        public Task<MovieResponse> Create(MovieRequest newMovie)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MovieResponse> CreatePhoto(int id, PhotoRequest photoRequest)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MovieResponse?> Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<MovieResponse>> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MovieResponse> GetById(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MovieResponse> GetByName(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<MovieResponse>> SearchName(string search)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MovieResponse?> Update(int id, MovieRequest updateMovie)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
