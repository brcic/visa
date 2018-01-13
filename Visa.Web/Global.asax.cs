using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Visa.Web.Models;

namespace Visa.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            SingleParameter single = SingleParameter.CreateInstance();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
