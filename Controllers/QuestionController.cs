using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internet.Models;
using Internet.Models.ViewModels;

namespace Internet.Controllers
{
    public class QuestionController : Controller
    {
        IRepository<Question> _repository;
        //
        // GET: /Question/
        public QuestionController() 
        {
            _repository = new QuestionRepository();
        }

        [Authorize(Roles = "Professor")]
        public ActionResult Index(int id)
        {
            ViewData["ANSWER_ID"] = id.ToString();
            return View(_repository.GetItemsWithParams(id));
        }

        //
        // GET: /Question/Details/5

        [Authorize(Roles = "Professor")]
        public ActionResult Details(int id)
        {
            
            return View(_repository.GetByID(id));
        }

        //
        // GET: /Question/Create

        [Authorize(Roles = "Professor")]
        public ActionResult Create(int id)
        {
            ViewData["TestID"] = id.ToString();
            return View(new QuestionViewModel());
        } 

        //
        // POST: /Question/Create

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Create(int id, QuestionViewModel item)
        {

            ViewData["TestID"] = id.ToString();
           
            item.Question.TestID = id;
            try
            {
                _repository.CreateItem(item.Question);
            }
            catch (Exception) 
            {
                return View(item);
            }
      
            
            return RedirectToAction("Index/" +item.Question.ID, "Answer");
        }
        
        //
        // GET: /Question/Edit/5

        [Authorize(Roles = "Professor")]
        public ActionResult Edit(int id)
        {
            QuestionViewModel qm = new QuestionViewModel();
            qm.Question = _repository.GetByID(id);
            return View(qm);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Edit(int id, QuestionViewModel item)
        {
           
               
                try
                {
                    _repository.UpdateItem(_repository.GetByID(id), item.Question);
                }
                catch (Exception)
                {
                    return View(item);
                }
                return RedirectToAction("Index/" + _repository.GetByID(id).TestID);
        }

        //
        // GET: /Question/Delete/5

        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int id)
        {
            int testID = _repository.GetByID(id).TestID;
            _repository.DeleteItem(_repository.GetByID(id));
            return RedirectToAction("Index/" + testID);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
