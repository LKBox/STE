using STE.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STE.API.Services
{
	public class UserService
	{
		public User CheckUser(string username, string password)
		{
			using (STEDBContext db = new STEDBContext())
			{
				return db.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
			}
		}
	}
}