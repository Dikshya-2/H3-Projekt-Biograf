using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.DTOs
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset? ReleasedDate { get; set; }
        public List<CategoryResponse> Categories { get; set; }
    }
}
