using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MusicWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new MusicDBInitializer());

            MusicWebDB db = new MusicWebDB();
            RoleStore<AccountRole> roleStore = new RoleStore<AccountRole>(db);
            RoleManager<AccountRole> roleManager = new RoleManager<AccountRole>(roleStore);

            if (!roleManager.RoleExists("Administrator"))
            {
                AccountRole newRole = new AccountRole("Administrator", "Administrators can add, edit and delete data.");
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("RegisteredUser"))
            {
                AccountRole newRole = new AccountRole("RegisteredUser", "Registered users can create playlist, upload songs.");
                roleManager.Create(newRole);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
}
}
