using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Text;
using Internet.Extension;
using Internet.Models;

namespace Internet
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           EntityContextContainer.Update();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            if (Server != null)
            {
                //string path = @"D:\учеба\проекты\Visual Studio 2010\Projects\TestSystem\TestSystem\Internet\App_Data\Log.txt";
                //string path = @"d:\DZHosts\LocalUser\laskoviymish\www.LaskoviyMish.somee.com\app_data\log.txt";
                Exception ex = Server.GetLastError();
                /*using (FileStream fs = File.Create(path, 1024))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(ex.Message);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                File.AppendAllText(path, ex.Message+"/n");*/
            }


        }
    }
}