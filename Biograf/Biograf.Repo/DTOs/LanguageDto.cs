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
    public class LanguageDto
    {
        public int Id { get; set; }
        public string Name {  get; set; }     
        public int? MovieId { get; set; }
        //[JsonIgnore]
        //public Movie? Movie { get; set; }
    }
}
