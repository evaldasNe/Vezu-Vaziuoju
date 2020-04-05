using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vezu_Vaziuoju.Startup))]
namespace Vezu_Vaziuoju
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
