using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Services;
using Microsoft.AspNetCore.Authorization;
using ReadingApp.Services.Models;


namespace ReadingApp.Controllers
{
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {

        private ResourceService _rService;
        private CommentService _commentService;


        public ResourceController(CommentService cs, ResourceService rs)
        {
            _commentService = cs;
            _rService = rs;

        }

        [HttpGet]
        [Authorize]
        public IList<ResourceDTO> Get()
        {

            return _rService.GetResourcesForUser(User.Identity.Name);
        }


        [HttpPost]
        public IActionResult Add([FromBody] ResourceDTO resource)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            _rService.AddResource(resource, User.Identity.Name);

            return Ok();

        }


        [HttpGet("{id}")]
        public ResourceDTO Get(int id)
        {

            return _rService.FindById(id, User.Identity.Name);

        }


        [HttpPost("{id}/comments")]
        public IActionResult Add(int id, [FromBody]CommentDTO comment)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            comment.ResourceId = id;
            _commentService.AddComment(comment, User.Identity.Name);

            return Ok();

        }



        [HttpDelete("{id}")]

        public IActionResult Delete(ResourceDTO resource, int id)
        {
            resource.Id = id; //just now added this since it was in the comment controller too
            _rService.DeleteResource(resource, User.Identity.Name);
            return Ok();
        }


     



        /*[HttpDelete("{id}/comments/{id}")]
        public IActionResult Delete(CommentDTO comment) { 
      
      
            _commentService.DeleteComment(comment, User.Identity.Name);
            return Ok();



        }*/
    }
}




