using Biograf.Repo.DTOs;
using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface
{
    public interface IActor
    {
        Actor Create(Actor actor);
        List<Actor> Get(); 
        Actor Get(int id);
        Actor Delete(int id);
        Actor Update(int id, ActorDto actor);
    }
}
