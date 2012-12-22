using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internet.Models;

namespace Internet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        [ChildActionOnly]
        public ActionResult NavBar()
        {
            NavBarModel model = new NavBarModel();
            model.Categorys = EntityContextContainer.getEntity().TestCategories.ToList();
            model.Difficulties = EntityContextContainer.getEntity().Difficulties.ToList();
            return View(model);
        }
    }
}
