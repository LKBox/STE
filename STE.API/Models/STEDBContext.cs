using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace STE.API.Models
{
	public class STEDBContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<ArticleVersion> ArticleVersions { get; set; }

		public STEDBContext() : base("STEConnectionString")
		{
			Database.SetInitializer<STEDBContext>(new CreateDatabaseIfNotExists<STEDBContext>());
			this.Configuration.LazyLoadingEnabled = false;
		}

		private void FixEfProviderServicesProblem()
		{
			// The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
			// for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
			// Make sure the provider assembly is available to the running application. 
			// See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
			var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Entity<Article>()
				.HasMany(x => x.ArticleVersion_MainArticle)
				.WithRequired(x => x.MainArticle)
				.HasForeignKey(x => x.ArticleId)
				.WillCascadeOnDelete(true);

		}
	}
}