using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Data;
using ReadingApp.Models;

namespace ReadingApp.Infrastructure
{

    public class ResourceRepository
    {
        public ApplicationDbContext _db;

        public ResourceRepository(ApplicationDbContext db)
        {

            _db = db;

        }

       

        public IQueryable<Resource> GetResourcesForUser(string user)   //grabs all books for user
        {

            return from r in _db.Resources
                   where r.User.UserName == user
                   select r;

        }


        public IQueryable<Resource> FindById(int id, string user)  //grabs a book for a user by ResourceId
        {
            return from r in _db.Resources
                   where r.Id == id && r.User.UserName == user
                   select r;


        }

        

        public void Add(Resource resource)      //adds a resource to the database
        {
            
            resource.DateCreated = DateTime.Now;

            _db.Resources.Add(resource);
            _db.SaveChanges();

        }

        public void Delete(Resource resource, int id) {

            resource.Id = id;
            
           
            _db.Resources.Remove(resource);
            _db.SaveChanges();

        }

    }
}
