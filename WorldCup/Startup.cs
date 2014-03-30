using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorldCup.Startup))]
namespace WorldCup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
