using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingApp.Models
{

    public class WordComment
    {
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }


        public int WordId { get; set; }
        [ForeignKey("WordId")]
        public Word Word { get; set; }
    }
}
