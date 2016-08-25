using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Data;
using ReadingApp.Models;


namespace ReadingApp.Infrastructure
{

    public class CommentRepository
    {
        private ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db)
        {

            _db = db;

        }

        public IQueryable<Comment> CommentSearch(IList<string> searchTerms, string user)            //find comments that contain search word
        {

            return from c in _db.Comments
                   let wcCount = c.WordComments.Count(wc => searchTerms.Any(w => wc.Word.Stem == w))
                   where wcCount > 0 && c.Resource.User.UserName == user
                   orderby wcCount
                   select c;

        }




        public IQueryable<Comment> GetCommentsByResourceId(int id)      //find comments that match a certain resource id
        {

            return from c in _db.Comments
                   where c.ResourceId == id
                   select c;

        }

        public IQueryable<Comment> GetCommentById(int id, string user)               //find one comment by its id
        {
            return from c in _db.Comments
                   where c.Id == id && c.Resource.User.UserName == user
                   select c;


        }

        public void Add(Comment comment)
        {

            _db.Comments.Add(comment);
            _db.SaveChanges();


        }

        public void Delete(Comment comment, string user)
        {
            _db.Comments.Remove(comment);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }

}
