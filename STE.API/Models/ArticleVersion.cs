using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace STE.API.Models
{
	public class ArticleVersion
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string ArticleText { get; set; }
		public DateTime VersionDate { get; set; }

		public int ArticleId { get; set; }
		[ForeignKey("ArticleId")]
		public virtual Article MainArticle { get; set; }

		public int CreatorUserId { get; set; }
		[ForeignKey("CreatorUserId")]
		public virtual User CreatorUser { get; set; }
	}
}