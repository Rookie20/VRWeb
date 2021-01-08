using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VRWeb.Startup))]
namespace VRWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
