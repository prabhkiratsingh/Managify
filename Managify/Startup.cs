using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Managify.Startup))]
namespace Managify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
