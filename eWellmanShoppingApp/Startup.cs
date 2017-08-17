using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eWellmanShoppingApp.Startup))]
namespace eWellmanShoppingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
