using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface IMovie
    {
        Task<MovieResponse> Create(MovieDto movieDto);
        Task<List<Movie>> Get();
        Task<Movie> Get(int id);
        Task<List<Movie>> GetMovieByCategoryId(int catetoryId);
        Task<List<MovieDetail>> GetDetail(int id);
        Task<Movie> Delete(int id);
        //Task<Movie> Update(int id, MovieDto movie);
        Task<MovieResponse> Update(int id, MovieDto movieDto);
        Task<Movie> Get(string name); 
        Task<List<Movie>> Search(string query);  



    }
}
