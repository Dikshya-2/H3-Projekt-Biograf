using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biograf.Repo.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }

        //[JsonIgnore]
        //public List<Movie> Movies { get; set; }
    }
  

   
}
