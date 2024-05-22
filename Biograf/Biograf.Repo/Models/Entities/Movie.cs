using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biograf.Repo.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset? ReleasedDate { get; set; }
        //public DateTime ReleasedDate { get; set; }

        [JsonIgnore]
        public List<Actor>? Actors { get; set; }
        [JsonIgnore]
        public List<Photo>? Photos { get; set; }
        [JsonIgnore]
        public List<Language?> Languages { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        [JsonIgnore]
        public int? AuthorId { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }  
    }
}
