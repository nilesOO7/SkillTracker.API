using System.Web.Http;
using SkillTracker.Data.EFCore;
using SkillTracker.Data.Models;

namespace SkillTracker.Service
{
    [ExcludeFromCoverage]
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //// Initialize EF Database
            System.Data.Entity.Database.SetInitializer(new SkillTrackerDbInitializer());
        }
    }
}
