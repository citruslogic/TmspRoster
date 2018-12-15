using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TmspRoster.Startup))]
namespace TmspRoster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
