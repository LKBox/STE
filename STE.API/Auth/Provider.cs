using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using STE.API.Models;
using STE.API.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace STE.API.Auth
{
	public class Provider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			await Task.FromResult(context.Validated());
		}
		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			if (!context.OwinContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
				context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
			else
				context.OwinContext.Response.Headers["Access-Control-Allow-Origin"] = "*";

			UserService UserService = new UserService();
			User usr = UserService.CheckUser(context.UserName, context.Password);

			if (usr != null)
			{
				var identity = new ClaimsIdentity(context.Options.AuthenticationType);
				identity.AddClaim(new Claim("userId", usr.Id.ToString()));
				identity.AddClaim(new Claim("name", context.UserName));
				identity.AddClaim(new Claim("role", "admin"));
				AuthenticationTicket ticket = new AuthenticationTicket(identity, null);
				await Task.FromResult(context.Validated(ticket));
			}
			else
			{
				context.SetError("Invalid Request", "Incorrect user info");
			}
		}
	}
}