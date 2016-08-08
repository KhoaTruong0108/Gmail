using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kt.GmailWeb.WebApp.Startup))]
namespace kt.GmailWeb.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
