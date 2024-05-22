using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biograf.Repo.Models.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Image { get; set; }
        //[JsonIgnore]
        //public Movie? Movie { get; set; }
        [JsonIgnore]
        public int? MovieId { get; set; }


    }
}
