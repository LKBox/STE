using STE.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;


namespace STE.API.Controllers
{
	[Authorize]
	public class ArticlesController : ApiController
	{
		private STEDBContext db = new STEDBContext();

		// GET: api/Articles/GetArticles
		public IQueryable<Article> GetArticles()
		{
			return db.Articles;
		}

		// GET: api/Articles/GetArticle/5
		[ResponseType(typeof(Article))]
		public IHttpActionResult GetArticle(int id)
		{
			Article article = db.Articles.Find(id);
			if (article == null)
			{
				return NotFound();
			}

			return Ok(article);
		}

		// PUT: api/Articles/PutArticle/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutArticle(int id, Article article)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != article.Id)
			{
				return BadRequest();
			}

			try
			{
				var art = db.Articles.Where(x => x.Id == id).FirstOrDefault();
				if (art != null)
				{
					if (art.Title != article.Title || art.ArticleText != article.ArticleText)
					{
						ArticleVersion artVers = new ArticleVersion()
						{
							ArticleId = art.Id,
							Title = art.Title,
							ArticleText = art.ArticleText,
							CreatorUserId = art.CreatorUserId,
							VersionDate = art.LastUpdate
						};

						db.ArticleVersions.Add(artVers);

						art.Title = article.Title;
						art.ArticleText = article.ArticleText;
						art.LastUpdate = DateTime.Now;

						db.SaveChanges();
					}
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ArticleExists(id))
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

		// POST: api/Articles/PostArticle
		[ResponseType(typeof(Article))]
		public IHttpActionResult PostArticle(Article article)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var identity = (ClaimsIdentity)User.Identity;
			if (identity != null)
			{
				IEnumerable<Claim> claims = identity.Claims;
				var UserId = identity.FindFirst("userId").Value;

				article.CreateDate = DateTime.Now;
				article.LastUpdate = DateTime.Now;
				article.CreatorUserId = Convert.ToInt32(UserId);

				db.Articles.Add(article);
				db.SaveChanges();

			}



			return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
		}

		// DELETE: api/Articles/DeleteArticle/5
		[ResponseType(typeof(Article))]
		public IHttpActionResult DeleteArticle(int id)
		{
			Article article = db.Articles
				.Include(x => x.ArticleVersion_MainArticle)
				.Where(x => x.Id == id)
				.FirstOrDefault();

			if (article == null)
			{
				return NotFound();
			}

			db.Articles.Remove(article);
			db.SaveChanges();

			return Ok(article);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ArticleExists(int id)
		{
			return db.Articles.Count(e => e.Id == id) > 0;
		}
	}
}