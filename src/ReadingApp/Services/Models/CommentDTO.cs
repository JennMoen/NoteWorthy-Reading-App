using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingApp.Services.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Location { get; set; }

        public int ResourceId { get; set; }

        public string Resource { get; set; }



    }
}
