using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internet.Models;

namespace Internet.Controllers
{
    public class TestController : Controller
    {
        IRepository<Test> _repository;
        public TestController() 
        {
            ViewData["APPROVING"] = "true";
            _repository = new TestRepository();
        }
        //
        // GET: /Test/
        
        [Authorize]
        public ActionResult Index()
        {
            TestEntities te = new TestEntities();
            if (User.IsInRole("Professor"))
            {
                ViewData["APPROVING"] = "true";
                return View("List",te.Tests);
            }
            return RedirectToAction("Index","TestPassing");
        }
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int ID)
        {
            _repository.DeleteItem(_repository.GetByID(ID));
            ViewData["APPROVING"] = "false";
            return RedirectToAction("Index", _repository.GetItems());
        }

        [Authorize(Roles = "Professor")]
        public ActionResult Edit(int ID) 
        {
            CreateTestViewModel vm = new CreateTestViewModel();
            vm.Test = _repository.GetByID(ID);
            return View(vm);
        }
        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult Edit(int ID, CreateTestViewModel item)
        {
            try
            {
                _repository.UpdateItem(_repository.GetByID(ID), item.Test);
            }
            catch (Exception)
            {
                return View(item);
            }
           
            return View("List", _repository.GetItems());
        }


        [Authorize(Roles = "Professor")]
        public ActionResult Create()
        {
            return View(new CreateTestViewModel());
        }
        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult Create(CreateTestViewModel item)
        {
            
            item.Test.TestAuthor = User.Identity.Name;

            try
            {
                _repository.CreateItem(item.Test);
            }
            catch (Exception)
            {
                return View(item);
            }
            return RedirectToAction("Index/" + _repository.GetItems().Last().ID, "Question");
        }
        [Authorize(Roles = "Professor")]
        public ActionResult CreateCategory()
        {
           
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult CreateCategory(TestCategory item)
        {
            if (item.CategoryName == null)
            {
                return View(item);
            }
            (new CategoryRepository()).CreateItem(item);
            return View("List", _repository.GetItems());
        }
        [Authorize(Roles = "Professor")]
        public ActionResult CreateDifficulty()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult CreateDifficulty(Difficulty item)
        {
            if (item.DifficultyName == null)
            {
                return View(item);
            }
            (new DifficultyRepository()).CreateItem(item);
            return View("List", _repository.GetItems());
        }
        [Authorize(Roles = "Professor")]
        public ActionResult Approve(int ID)
        {
            Test test = _repository.GetByID(ID);
            test.Approved = Internet.Models.ModelService.TestApprove.Approve(_repository.GetByID(ID));
            _repository.UpdateItem(_repository.GetByID(ID), test);
            if (!test.Approved)
            {
                ViewData["APPROVING"] = test.Approved.ToString();
            }
            else 
            {
                ViewData["APPROVING"] = "true";
            }
            return View("List", _repository.GetItems());
        }
    }
}
