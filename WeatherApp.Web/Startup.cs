using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherApp.Web.Startup))]
namespace WeatherApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
