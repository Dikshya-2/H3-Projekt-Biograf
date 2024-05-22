using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biograf.Repo.Models.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public int? MovieId { get; set; }   
    }
}
