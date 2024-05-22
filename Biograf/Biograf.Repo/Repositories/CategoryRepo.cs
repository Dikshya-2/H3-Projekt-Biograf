using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Repositories
{
    public class CategoryRepo: ICategory
    {
        private readonly DatabaseContext _context;

        public CategoryRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(CategoryDto category)
        {
            Category newCategory = new Category
            {
                Name = category.Name,
            };
             _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }
        public async Task<Category> Delete(int id)
        {
            var obj = await _context.Categories.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<List<Category>> Get()
        {
            //return await _context.Categories.ToListAsync(); i can also include photos
            return await _context.Categories.Include(c=>c.Movies).ToListAsync();

        }
        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category?> FindCategoryById(int categoryId)
        {
            return await _context.Categories.Include(s => s.Movies).ThenInclude(s => s.Photos).FirstOrDefaultAsync(s => s.Id == categoryId);

        }

        public async Task<Category> Get(string Name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == Name);
        }

        public async Task<Category> Update(int id, CategoryDto category)
        {
            Category update = new Category
            {
                Name = category.Name
            };
            var obj = await _context.Categories.FindAsync(id);
            obj.Name = category.Name;
            await _context.SaveChangesAsync();
            return update;
        }
    }
}
