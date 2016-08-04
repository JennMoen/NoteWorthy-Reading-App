using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadingApp.Services
{
    [Route("api/[controller]")]
    public class WordService 
    {
        private WordRepository _wordRepo;

        public WordService(WordRepository wr) {

            _wordRepo = wr;

        }








    }
}
