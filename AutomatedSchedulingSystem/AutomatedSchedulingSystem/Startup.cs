using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomatedSchedulingSystem.Startup))]
namespace AutomatedSchedulingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
