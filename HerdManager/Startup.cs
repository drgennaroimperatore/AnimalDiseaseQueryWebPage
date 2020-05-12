using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HerdManager.Startup))]
namespace HerdManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
