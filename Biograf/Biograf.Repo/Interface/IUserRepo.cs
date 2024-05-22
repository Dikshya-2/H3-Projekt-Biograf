using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface IUserRepo
    {
        Task<User> Create(User user);
        Task<List<User>> Get();
        Task<User> Get(int id);
        Task<User> Delete(int id);
        Task<User> Update(int id, User user);
        Task<User> getByemail(string email);
    }
}
