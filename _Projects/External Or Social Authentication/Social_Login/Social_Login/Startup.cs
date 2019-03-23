using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Social_Login.Startup))]
namespace Social_Login
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
