using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface IPhotoRepo
    {
        Task<List<Photo>> GetAll();
        Task<Photo?> GetById(int id); 
        Task<Photo> Create(Photo newPhoto);
        Task<Photo> Delete(int id);
        Task<Photo> Update(int id, Photo author);
    }
}
