using ReadingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingApp.Services.Models
{
    public class ResourceDTO

    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Link { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdated { get; set; }

        public string ImageUrl { get; set; }

        public IList<CommentDTO> Comments { get; set; }

       
    }
}
