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
    public class LanguageRepo : ILanguage
    {
        private readonly DatabaseContext _context;
        public LanguageRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Language> Create(Language language)
        {
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }
        public async Task<Language> Delete(int id)
        {
            var obj = await _context.Languages.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task <List<Language>> Get()
        {
            return await _context.Languages.ToListAsync();
            //return await _context.Languages.Include(l=>l.Movie).ToListAsync();


        }
        public async Task<Language> Get(int id)
        {
            // return await _context.Movies.Include(m=>m.Actors).Include(m => m.Languages).Include(m=>m.Categories).ToListAsync();

            return await _context.Languages.FirstOrDefaultAsync(l => l.Id == id); 
        }
        public async Task<Language> Update(int id, LanguageDto language)
        {
            Language update = new Language
            {
                Name = language.Name,        
            };
            //_context.Entry(language).State = EntityState.Modified; // EFC automatic update
            var obj = await _context.Languages.FirstOrDefaultAsync(l=>l.Id==id);
            obj.Name = language.Name;
            obj.MovieId = language.MovieId;
            await _context.SaveChangesAsync();
            return update;
        }

    }
}
