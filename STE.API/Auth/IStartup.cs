using Owin;

namespace STE.API.Auth
{
	public interface IStartup
	{
		void Configuration(IAppBuilder app);
	}
}