using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iveonik.Stemmers;

namespace ReadingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("stemmer")]
        public List<string> StemmerTest()
        {
            var es = new EnglishStemmer();

            return (from s in new string[] { "computing", "computer", "compute", "computation" }
                    select es.Stem(s)).ToList();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}