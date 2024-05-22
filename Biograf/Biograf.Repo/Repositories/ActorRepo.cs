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
    public class ActorRepo : IActor
    {
        private readonly DatabaseContext _context;
        private DbContext context;

        public ActorRepo(DatabaseContext databaseContext )
        {
            _context = databaseContext;
        }

        public ActorRepo(DbContext context)
        {
            this.context = context;
        }

        public Actor Create(Actor actor)
        {
            _context.Actors.Add( actor );
            _context.SaveChanges();
            return actor;
        }
        public List<Actor> Get()
        {
           return _context.Actors.Include(a=>a.Movies).ToList();
        }
        public Actor Get(int id)
        {
            return (_context.Actors.FirstOrDefault(a=>a.Id==id));
        }
        public Actor Update(int id, ActorDto actor)
        { //map DTO to Domain Model
            //Actor obj = new Actor
            //{
            //    Name = actor.Name,
            //    Age = actor.Age,
            //};
            var obj = _context.Actors.Find(id);
            obj.Name = actor.Name;
            obj.Age= actor.Age;          
            _context.SaveChanges();
            return obj;
        }
        public Actor Delete(int id)
        {
            var actor = _context.Actors.Find( id);
            if( actor != null )
            {
                _context.Actors.Remove( actor );
                _context.SaveChangesAsync(); 
            }
            return actor;
        }
    }
}
