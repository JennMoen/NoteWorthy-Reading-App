using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Data;
using ReadingApp.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadingApp.Infrastructure
{

    public class WordRepository
    {
        private ApplicationDbContext _db;

        public WordRepository(ApplicationDbContext db)
        {

            _db = db;

        }


        public IQueryable<Word> FindWords(IList<string> searchWords)//this will be the comment text you pass in when you submit a new comment
        {

            return from w in _db.Words 
                   where searchWords.Any((sw) => sw == w.Stem)
                   select w;

        }

        public void AddWords(IList<Word> words)
        {

            _db.Words.AddRange(words);
            _db.SaveChanges();



        }

        public void AddWordComments(IList<WordComment> wordComments) {

            _db.WordComments.AddRange(wordComments);
            _db.SaveChanges();

        }




    }
}
