using Newtonsoft.Json;
using System.Web.Http;

namespace STE.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services						
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;	
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			config.Formatters.Add(config.Formatters.JsonFormatter);

			// Web API routes
			config.MapHttpAttributeRoutes();					

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { action = "Index", id = RouteParameter.Optional }
			);
		}
	}
}
