using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoccerLeagueManager.Startup))]
namespace SoccerLeagueManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
