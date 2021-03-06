﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PostFeed.Domain;
using PostFeed.Infrastructure;
using PostFeed.Services;
using PostFeed.Views.ViewModels;

namespace PostFeed.Controllers
{
    public class PostsController : ApiController
    {
        private PostServices postServices = new PostServices();
        
        // GET: api/Posts
        [HttpGet]
        [Route("api/Posts")]
        public IQueryable<Post> GetPosts()
        {
            return postServices.GetAll();
        }

        // GET: api/Posts/Recent/id
        [ResponseType(typeof(IEnumerable<Post>))]
        [Route("api/Posts/Recent/{id}")]
        public IEnumerable<Post> GetRecentPosts(int id = 10)
        {
            return postServices.GetRecent(id, new TimeSpan(1, 0, 0, 0));
        }

        // GET: api/Posts/5
        [HttpGet]
        [Route("api/Posts/{id}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = postServices.Get(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        [HttpPut]
        [Route("api/Posts/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            postServices.Update(post);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Posts
        [HttpPost]
        [Route("api/Posts")]
        [ResponseType(typeof(PostViewModel))]
        public IHttpActionResult PostPost([FromBody] PostViewModel post)
        {
            post.TimePosted = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            int postid = postServices.Add(new Post(post));
            post.Id = postid;
            return CreatedAtRoute(
                "DefaultApi", 
                new {
                    controller = "Posts",
                    id = postid
                }, 
                post);
        }

        // DELETE: api/Posts/5
        [HttpDelete]
        [Route("api/Posts/{id}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = postServices.Get(id);
            if (post == null)
            {
                return NotFound();
            }

            postServices.Delete(post);

            return Ok(post);
        }
    }
}