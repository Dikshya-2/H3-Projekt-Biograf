using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface ICategory
    {
        Task<Category> Create(CategoryDto category);
        Task<List<Category>> Get();
        Task<Category?> FindCategoryById(int categoryId);
        Task<Category> Get(int id);
        Task<Category> Get(string Name);
        Task<Category> Delete(int id);
        Task<Category> Update(int id, CategoryDto author);
         
    }
}
