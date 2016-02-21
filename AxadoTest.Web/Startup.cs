using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AxadoTest.Web.Startup))]
namespace AxadoTest.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
