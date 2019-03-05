using STE.API.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace STE.API.Controllers
{
	[Authorize]
	public class ArticleVersionsController : ApiController
    {
        private STEDBContext db = new STEDBContext();

		// GET: api/ArticleVersions/GetArticleVersions
		public IQueryable<ArticleVersion> GetArticleVersions()
        {
            return db.ArticleVersions;
        }

		// GET: api/ArticleVersions/GetArticleVersionsByArticleId?articleId=5
		public IQueryable<ArticleVersion> GetArticleVersionsByArticleId(int articleId)
		{
			return db.ArticleVersions.Where(x => x.ArticleId == articleId).OrderByDescending(x => x.VersionDate);
		}

		// GET: api/ArticleVersions/GetArticleVersion/5
		[ResponseType(typeof(ArticleVersion))]
        public IHttpActionResult GetArticleVersion(int id)
        {
            ArticleVersion articleVersion = db.ArticleVersions.Find(id);
            if (articleVersion == null)
            {
                return NotFound();
            }

            return Ok(articleVersion);
        }

		// PUT: api/ArticleVersions/PutArticleVersion/5
		[ResponseType(typeof(void))]
        public IHttpActionResult PutArticleVersion(int id, ArticleVersion articleVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articleVersion.Id)
            {
                return BadRequest();
            }

            db.Entry(articleVersion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleVersionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

		// POST: api/ArticleVersions/PostArticleVersion
		[ResponseType(typeof(ArticleVersion))]
        public IHttpActionResult PostArticleVersion(ArticleVersion articleVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ArticleVersions.Add(articleVersion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = articleVersion.Id }, articleVersion);
        }

		// DELETE: api/ArticleVersions/DeleteArticleVersion/5
		[ResponseType(typeof(ArticleVersion))]
        public IHttpActionResult DeleteArticleVersion(int id)
        {
            ArticleVersion articleVersion = db.ArticleVersions.Find(id);
            if (articleVersion == null)
            {
                return NotFound();
            }

            db.ArticleVersions.Remove(articleVersion);
            db.SaveChanges();

            return Ok(articleVersion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleVersionExists(int id)
        {
            return db.ArticleVersions.Count(e => e.Id == id) > 0;
        }
    }
}