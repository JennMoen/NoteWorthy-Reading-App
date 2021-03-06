﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReadingApp.Services;
using ReadingApp.Services.Models;


namespace ReadingApp.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {

        private CommentService _commentService;

        private ResourceService _rService;

        public CommentsController(CommentService cs, ResourceService rs)
        {

            _commentService = cs;

            _rService = rs;

        }

        [HttpGet("search")]
        public IList<CommentDTO> Get([FromQuery] string searchTerms)
        {

            return _commentService.CommentSearch(searchTerms, User.Identity.Name);


        }



        [HttpGet("{id}")]

        public CommentDTO Get(int id)
        {

            return _commentService.GetCommentById(id, User.Identity.Name);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(CommentDTO comment, int id)
        {
            comment.Id = id;
            _commentService.DeleteComment(comment, User.Identity.Name);
            return Ok();

        }


        [HttpPut("{commentId}")]
        //[FromQuery]int commentId
        public IActionResult Update([FromBody] CommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }
            
            _commentService.UpdateComment(comment, User.Identity.Name);

            return Ok();

        }


    }
}
