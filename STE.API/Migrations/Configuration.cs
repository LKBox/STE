namespace STE.API.Migrations
{
	using STE.API.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<STE.API.Models.STEDBContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(STE.API.Models.STEDBContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.		

			User usr = new User()
			{
				Name = "admin",
				Surname = "admin",
				UserName = "admin",
				Password = "12345",
				IsDeleted = false
			};

			context.Users.AddOrUpdate(x => x.UserName, usr);
			context.SaveChanges();
		}
	}
}
