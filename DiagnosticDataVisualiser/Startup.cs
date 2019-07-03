using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiagnosticDataVisualiser.Startup))]
namespace DiagnosticDataVisualiser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
