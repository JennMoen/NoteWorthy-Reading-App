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
  
    public class UserRepository

    {
        private ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) {
            _db = db;

        }

        public IQueryable<ApplicationUser> GetUser(string id) {

            return from u in _db.Users
                   where u.UserName==id
                   select u;

        }

    }
}
