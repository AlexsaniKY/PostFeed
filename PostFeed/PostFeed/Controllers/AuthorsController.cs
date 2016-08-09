using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PostFeed.Domain;
using PostFeed.Services;

namespace PostFeed.Controllers
{
    public class AuthorsController : ApiController
    {
        private AuthorServices authorServices = new AuthorServices();

        // GET: api/Authors
        public IQueryable<Author> GetAuthors()
        {
            return authorServices.GetAll();
        }

        // GET: api/Authors/5
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
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            authorServices.Add(author);

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
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