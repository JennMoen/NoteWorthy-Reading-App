using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Infrastructure;
using ReadingApp.Services.Models;
using ReadingApp.Models;


namespace ReadingApp.Services
{

    public class ResourceService
    {

        private ResourceRepository _rRepo;
        private UserRepository _uRepo;

        public ResourceService(ResourceRepository rr, UserRepository ur)
        {

            _rRepo = rr;
            _uRepo = ur;

        }


        public IList<ResourceDTO> GetResourcesForUser(string user)     //creates Model with Resource properties
        {

            return (from r in _rRepo.GetResourcesForUser(user)
                    select new ResourceDTO()
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Author = r.Author,
                        Link = r.Link,
                        ImageUrl = r.ImageUrl,
                        DateCreated = r.DateCreated,
                        LastUpdated = r.LastUpdated
                    }).ToList();

        }

        public void AddResource(ResourceDTO resource, string currentUser)
        {

            Resource dbResource = new Resource()
            {
                Id = resource.Id,
                Title = resource.Title,
                Author = resource.Author,
                Link = resource.Link,
                ImageUrl = resource.ImageUrl,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now,
                UserId = _uRepo.GetUser(currentUser).First().Id


            };

            _rRepo.Add(dbResource);



        }

        public void DeleteResource(ResourceDTO resource, string currentUser) {

            Resource dbResource = new Resource()
            {
                Id = resource.Id,
                Title = resource.Title,
                Author = resource.Author,
                Link = resource.Link,
                ImageUrl = resource.ImageUrl,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now,
                UserId = _uRepo.GetUser(currentUser).First().Id


            };

            _rRepo.Delete(dbResource, resource.Id);

        }


        public ResourceDTO FindById(int id, string user)
        {

            return (from r in _rRepo.FindById(id, user)
                    select new ResourceDTO
                    {
                        Id = r.Id,
                        Author = r.Author,
                        Title = r.Title,
                        DateCreated = r.DateCreated,
                        LastUpdated = r.LastUpdated,
                        ImageUrl = r.ImageUrl,
                        Link = r.Link,
                        Comments = (from c in r.Comments
                                    select new CommentDTO
                                    {   Id=c.Id,
                                        Location = c.Location,
                                        Text = c.Text,
                                        ResourceId = r.Id

                                    }).ToList()

                    }).FirstOrDefault();








        }


    }

}





