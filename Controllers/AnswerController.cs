using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internet.Models;
using Internet.Models.ViewModels;

namespace Internet.Controllers
{
    public class AnswerController : Controller
    {
        //
        // GET: /Answer/index/5
        IRepository<Answer> _repository;

        public AnswerController() 
        {
            _repository = new AnswerRepository();
        }

        [Authorize(Roles = "Professor")]
        public ActionResult Index(int id)
        {
            ViewData["QUESTION_ID"] = id.ToString();
            IRepository<Question> _tempQuestion = new QuestionRepository();
            ViewData["QUESTION_BODY"] = _tempQuestion.GetByID(id).QuestionBody;
            ViewData["TEST_ID"] = _tempQuestion.GetByID(id).TestID;
            return View(_repository.GetItemsWithParams(id));
        }


        [Authorize(Roles = "Professor")]
        public ActionResult Details(int id)
        {
            return View(_repository.GetByID(id));
        }



        [Authorize(Roles = "Professor")]
        public ActionResult Create(int id)
        {
            AnswerViewModel avm = new AnswerViewModel();
            avm.Question = new QuestionRepository().GetByID(id);
            return View(avm);
        } 


        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult Create(int id, AnswerViewModel item)
        {
            try
            {
                item.Question = (new QuestionRepository()).GetByID(id);
                item.Answer.QuestionID = id;
                if (item.Question.QuestionType==3)//тип вопроса текст
                {
                    item.Answer.IsRight = true;   
                }
                _repository.CreateItem(item.Answer);
                return RedirectToAction("Index/"+id);
            }
            catch
            {
                return View(item);
            }
        }
        


        [Authorize(Roles = "Professor")]
        public ActionResult Edit(int id)
        {
            return View(_repository.GetByID(id));
        }



        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Edit(int id, Answer item)
        {
            if (_repository.GetByID(item.ID).Question.QuestionType == 3)//тип вопроса текст
            {
                item.IsRight = true;
            }
            _repository.UpdateItem(_repository.GetByID(id), item);
            return RedirectToAction("Index/" + _repository.GetByID(id).QuestionID);
        }


        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int id)
        {
            _repository.DeleteItem(_repository.GetByID(id));
            return RedirectToAction("Index/" + _repository.GetByID(id).QuestionID);
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int id, Answer collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
