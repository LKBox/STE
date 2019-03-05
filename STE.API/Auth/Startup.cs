﻿using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(STE.API.Auth.Startup))]
namespace STE.API.Auth
{
	public class Startup : IStartup
	{
		public void Configuration(IAppBuilder app)
		{			
			//HttpConfiguration config = new HttpConfiguration();			
			//WebApiConfig.Register(config);
			//app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			//app.UseWebApi(config);
			//ConfigureOAuth(app);

			HttpConfiguration config = new HttpConfiguration();
			ConfigureOAuth(app);
			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);
		}
		private void ConfigureOAuth(IAppBuilder app)
		{
			OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
			{
				TokenEndpointPath = new PathString("/gettoken"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
				AllowInsecureHttp = true,				
				Provider = new Provider()			
			};
			app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}