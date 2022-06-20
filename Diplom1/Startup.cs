using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Diplom1.Startup))]
namespace Diplom1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
