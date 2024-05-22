using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biograf.Repo.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset ReleasedDate { get; set; }
        //public Author? Author { get; set; }
        public List<Category> Categories { get; set; } 
       
        //public List<Photo>? photos { get; set; }
        //public List<Language>? languages { get; set; }
    }
}
