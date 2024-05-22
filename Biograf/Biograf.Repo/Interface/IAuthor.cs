using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface IAuthor
    {
        Task<Author> Create (AuthorDto author);

        //Author Create(AuthorDto author);
       Task <List<Author>> Get();
        Task<Author> Get(int id);
        Task<Author> GetAge (int age);
        Task<List<Author>> GetAuthorsByAge(int age);
        Task <Author> Delete(int id);
        Task <Author> Update(int id, AuthorDto author);
    }
}
