using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace STE.API.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

		[Index("UserNameIndex", IsUnique = true)]
		[MaxLength(100)]		
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool IsDeleted { get; set; }

		public virtual ICollection<Article> Article_CreatorUser { get; set; }
		public virtual ICollection<ArticleVersion> ArticleVersion_CreatorUser { get; set; }
	}
}