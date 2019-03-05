using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace STE.API.Models
{
	public class Article
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string ArticleText { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime LastUpdate { get; set; }

		public int CreatorUserId { get; set; }
		[ForeignKey("CreatorUserId")]
		public virtual User CreatorUser { get; set; }

		public virtual ICollection<ArticleVersion> ArticleVersion_MainArticle { get; set; }
	}
}