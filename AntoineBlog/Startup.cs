using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AntoineBlog.Startup))]
namespace AntoineBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
