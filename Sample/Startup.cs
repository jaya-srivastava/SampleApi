using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iPractice.Startup))]
namespace iPractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
