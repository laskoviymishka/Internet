using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internet.Models;
using System.Collections.Specialized;
using Internet.Models.ViewModel;
using Internet.Models.ModelService;

namespace Internet.Controllers
{
    public class TestPassingController : Controller
    {
        //
        // GET: /TestPassing/
        IRepository<Test> _repository;
        public TestPassingController() 
        {
            _repository = new TestRepository();
        }
         [Authorize]
        public ActionResult Index()
        {
            return View(_repository.GetItems());
        }
        [Authorize]
        public ActionResult Test(int id)
        {
            return View(_repository.GetByID(id));
        }
        [HttpPost]
        public ActionResult Test(int id, FormCollection item)
        {

            List<string> temp = new List<string>();
            Result res = new Result();
            res.TestID = id;
            res.UserName = User.Identity.Name;
            (new ResultRepository()).CreateItem(res);
            AddResult ar = new AddResult(res);
            foreach (string items in item.Keys)
            {
                if (items.StartsWith("T"))
                {
                    foreach (string value in item.GetValues(items))
                    {
                        ar.TextScore(items.Substring(2), value);
                    }
                }
                else 
                {
                    foreach (string value in item.GetValues(items))
                    {
                        temp.Add(value);
                    }
                }
            }
            ar.Score(temp);
            return RedirectToAction("TestResult/" + res.ID);
        }
        public ActionResult TestResult(int id)
        {
            ViewData["MAX_SCORE"] = ScoreService.MaxScore((new ResultRepository()).GetByID(id).Test);
            return View((new ResultRepository()).GetByID(id));
        }
    }
}
