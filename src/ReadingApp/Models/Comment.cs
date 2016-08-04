using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingApp.Models
{

    public class Comment
    {   
        public int Id { get; set; }

        public int ResourceId { get; set; }
        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; }

        public string Text { get; set; }

        public string Location { get; set; }

        public IList<WordComment> WordComments { get; set; }
    }
}
