using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Infrastructure;
using ReadingApp.Services.Models;
using ReadingApp.Models;
using Iveonik.Stemmers;


namespace ReadingApp.Services
{
    [Route("api/[controller]")]
    public class CommentService
    {
        private CommentRepository _commentRepo;
        private EnglishStemmer _stemmer;
        private WordRepository _wRepo;
        private ResourceRepository _rRepo;


        public CommentService(CommentRepository cr, EnglishStemmer es, WordRepository wr, ResourceRepository rr)
        {

            _commentRepo = cr;
            _stemmer = es;
            _wRepo = wr;
            _rRepo = rr;

        }

        public IList<CommentDTO> GetCommentsByResourceId(int id)
        {

            return (from c in _commentRepo.GetCommentsByResourceId(id)

                    select new CommentDTO()
                    {
                        Id = c.Id,
                        Location = c.Location,
                        Text = c.Text,
                        ResourceId = c.ResourceId


                    }).ToList();

        }

        public CommentDTO GetCommentById(int id)
        {
            return (from c in _commentRepo.GetCommentById(id)

                    select new CommentDTO()
                    {
                        Id = c.Id,
                        Location = c.Location,
                        Text = c.Text,
                        ResourceId = c.ResourceId
                    }).FirstOrDefault();


        }

        public IList<CommentDTO> CommentSearch(string searchTerms, string currentUser)
        {

            var words = (from s in searchTerms.Split(' ')
                         select _stemmer.Stem(s)).ToList();

            return (from c in _commentRepo.CommentSearch(words, currentUser)

                    select new CommentDTO()
                    {
                        Location = c.Location,
                        Text = c.Text,
                        ResourceId = c.ResourceId,
                        Resource = c.Resource.Title
                    }).ToList();


        }


        public void DeleteComment(CommentDTO comment, string currentUser)
        {

            Comment dbComment = new Comment()
            {
                Id = comment.Id,
                Location = comment.Location,
                Text = comment.Text,


            };

            _commentRepo.Delete(dbComment);


        }

        public void UpdateComment(CommentDTO comment, string currentUser)
        {

            Comment dbComment = new Comment()
            {
                Id = comment.Id,
                Location = comment.Location,
                Text = comment.Text,
                ResourceId=comment.ResourceId

            };

            _commentRepo.Edit(dbComment);


        }


        public void AddComment(CommentDTO comment, string currentUser)
        {

            Comment dbComment = new Comment()
            {
                Id = comment.Id,
                Location = comment.Location,
                Text = comment.Text,
                ResourceId = _rRepo.FindById(comment.ResourceId, currentUser).First().Id

            };

            _commentRepo.Add(dbComment);

            //takes text from submitted comment and splits it into an array, then runs it through the English stemmer
            var words = (from s in dbComment.Text.Split(' ')
                         select _stemmer.Stem(s)).ToList();

            //runs stemmed words through FindWords method and picks out the stems
            var dbWords = _wRepo.FindWords(words).ToList();


            //
            words.RemoveAll(w => dbWords.Any(dbw => dbw.Stem == w));
            var newWords = (from w in words
                            select new Word()
                            {
                                Stem = w

                            }).ToList();

            _wRepo.AddWords(newWords);

            dbWords.AddRange(newWords);



            _wRepo.AddWordComments((from w in dbWords
                                    select new WordComment()
                                    {
                                        WordId = w.Id,
                                        CommentId = dbComment.Id

                                    }).ToList());

        }








    }
}

