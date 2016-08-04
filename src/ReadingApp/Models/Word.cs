using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ReadingApp.Models
{

    public class Word
    {
        public int Id { get; set; }

        public string Stem { get; set; }

        public IList<WordComment> WordComments { get; set; }
    }
}
