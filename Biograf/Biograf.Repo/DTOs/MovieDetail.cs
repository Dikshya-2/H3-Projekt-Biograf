using Biograf.Repo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.DTOs
{
    public class MovieDetail
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset? ReleasedDate { get; set; }
        public int? AuthorId { get; set; }
        public int? UserId { get; set; }
        public List<Category> Categories { get; set; }
        public List<Photo>? Photos { get; set; }
        public List<Language>? Languages { get; set; }
    }
}
