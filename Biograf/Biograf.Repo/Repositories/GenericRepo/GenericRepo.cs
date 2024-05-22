using Biograf.Repo.Interface.GenericInterface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Repositories.GenericRepo
{
    public class GenericRepo<T>:IGeneric<T> where T : class
    {
        private readonly DatabaseContext _context;

        public GenericRepo(DatabaseContext context)
        {
            _context = context;
        }
        public T Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public T CreateMoviePhoto(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            T data= _context.Set<T>().Find(id);   
            if (data != null)
            {
                _context.Set<T>().Remove(data);
                _context.SaveChanges ();
            }  
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id) as T;
        }

        public List<T> GetAll()
        {
           return _context.Set<T>().ToList();
        }
        public T Update(int id, T item)
        {
            return item;    
        }
    }
}
