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
    public class AuthorRepo : IAuthor
    {
        private readonly DatabaseContext _context;

        public AuthorRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Author> Create(AuthorDto author)
       // public Author Create(AuthorDto author)

        {
            // Create a new instance of Author
            // Map AuthorDto to Author
            Author newauthor = new Author
            {
                Name = author.Name,
                Age = author.Age
            };

            // Add and save the new author to the context
            _context.Authors.Add(newauthor);
            await _context.SaveChangesAsync();
            // Return the newly created author
            return newauthor;
        }

        public async Task<Author> Delete(int id)
        {
            var obj = await _context.Authors.FirstOrDefaultAsync(a=>a.Id==id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
            //var obj = await Get(id);
            //if (obj != null)
            //{
            //    _context.Authors.Remove(obj);
            //    await _context.SaveChangesAsync();
            //}
            //return obj;
        }

        public async Task<List<Author>> Get()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> Get(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> GetAge(int age)
        {
            return await _context.Authors.Where(a=>a.Age >= age).FirstOrDefaultAsync();

        }

        public async Task<List<Author>> GetAuthorsByAge(int age)
        {
            return await _context.Authors.Where(a => a.Age >= age).ToListAsync();
        }
        public async Task<Author> Update(int id, AuthorDto author)
        {
            Author updateauthor = new Author
            {
                Name = author.Name,
                Age = author.Age
            };
            var obj = await _context.Authors.FindAsync(id);
            obj.Name = author.Name;
            obj.Age = author.Age;
            await _context.SaveChangesAsync();
            return updateauthor;
        }
    }
}
