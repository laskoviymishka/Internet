using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internet.Models;
using Internet.Models.ViewModel;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Internet.Models.ModelService;

namespace Internet.Controllers
{
    public class ResultController : Controller
    {
        IRepository<Result> _repo;
        public ResultController() 
        {
            _repo = new ResultRepository();
        }
        //
        // GET: /Result/

        public ActionResult Index()
        {
            List<ResultsViewModel> rvmList = new List<ResultsViewModel>();
            foreach (var item in _repo.GetItems())
            {
                rvmList.Add(new ResultsViewModel { 
                    MaxScore = ScoreService.MaxScore(item.Test),
                    TestName = item.Test.TestName,
                    UserName = item.UserName,
                    Score = ScoreService.ResultScore(item),
                    ResultID = item.ID
                });
            }
            return View(rvmList);
        }
        public ActionResult Json([DataSourceRequest] DataSourceRequest request)
        {
            List<ResultsViewModel> rvmList = new List<ResultsViewModel>();
            foreach (var item in _repo.GetItems())
            {
                rvmList.Add(new ResultsViewModel
                {
                    MaxScore = (new Random()).Next(10),
                    TestName = item.Test.TestName,
                    UserName = item.UserName
                });
            }
            return Json(rvmList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
         [Authorize(Roles = "Professor")]
        public ActionResult Delete(int ID) 
        {
            _repo.DeleteItem(_repo.GetByID(ID));
            return RedirectToAction("Index");
        }
        public ActionResult ResultPartial(int ID) 
        {
            ViewData["MAX_SCORE"] = ScoreService.MaxScore((new ResultRepository()).GetByID(ID).Test);
            if (Request.IsAjaxRequest())
            {
                return PartialView("ResultPartial", _repo.GetByID(ID));
            }
            else 
            {
                return null;
            }
        }
    }
}
