using MvcBasic.Models;
using MvcView.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcBasic
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // �C�j�V�����C�U�[��o�^
            Database.SetInitializer<MvcBasicContext>(new MvcBasicInitializer());

            // �C�j�V�����C�U�[��o�^
            Database.SetInitializer<MvcViewContext>(new MvcViewInitializer());
        }
    }
}
