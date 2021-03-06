﻿using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PostFeed.Domain;
using PostFeed.Services;
using PostFeed.Views.ViewModels;
using System.Collections.Generic;
using System;

namespace PostFeed.Controllers
{
    public class AuthorsController : ApiController
    {
        private AuthorServices authorServices = new AuthorServices();

        // GET: api/Authors
        [HttpGet]
        [Route("api/Authors")]
        [ResponseType(typeof(List<Author>))]
        public List<Author> GetAuthors()
        {
            return authorServices
                .GetAll()
                .ToList();
        }

        // GET: api/Authors/5
        [HttpGet]
        [Route("api/Authors/{id}")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(int id)
        {
            Author author = authorServices.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        [HttpPut]
        [Route("api/Authors/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            authorServices.Update(author);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [HttpPost]
        [Route("api/Authors")]
        [ResponseType(typeof(AuthorViewModel))]
        public IHttpActionResult PostAuthor([FromBody] AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int Id = authorServices.Add(new Author(author));

            return CreatedAtRoute("DefaultApi", new {controller= "Authors", id = Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete]
        [Route("api/Authors/{id}")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = authorServices.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            authorServices.Delete(author);

            return Ok(author);
        }
    }
}