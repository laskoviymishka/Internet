using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class QuestionRepository: IRepository<Question>
    {
        private List<Question> _questions;
        private TestEntities _testEntity;
        public QuestionRepository() 
        {
            _testEntity = EntityContextContainer.getEntity();
            _questions = _testEntity.Questions.ToList<Question>();
            
        }
        public Question GetByID(int ID)
        {
            return _questions.FirstOrDefault(q => q.ID == ID);
        }
        public List<Question> GetQuestions() 
        {
            return _questions;
        }

        public void CreateItem(Question item)
        {
            /*if (_testEntity.Questions.ToList<Question>().Count != 0)
            {
                item.ID = _testEntity.Questions.ToList<Question>().Last().ID;
                item.ID++;
            }
            else 
            {
                item.ID = 1;
            }*/
            _testEntity.Questions.AddObject(item);
            _testEntity.SaveChanges();
        }

        public void DeleteItem(Question item)
        {
           /*
                foreach (Answer ar in item.Answers)
                {
                    (new AnswerRepository()).DeleteItem(ar);
                }
 */
            _testEntity.Questions.DeleteObject(this.GetByID(item.ID));
            _testEntity.SaveChanges();
        }

        public void UpdateItem(Question item, Question newItem)
        {
            Question q = _testEntity.Questions.Where(t => t.ID == item.ID).First();
            q.QuestionBody = newItem.QuestionBody;
            q.TestID = newItem.TestID;
            //q.QuestionType = newItem.QuestionType; //все таки не стоит наверное потом менять тип вопроса
            _testEntity.SaveChanges();
        }


        public List<Question> GetItems()
        {
            return _questions;
        }

        public List<Question> GetItemsWithParams(int param)
        {
            return _testEntity.Questions.Where(q => q.TestID == param).ToList<Question>();
        }

        public List<Question> GetItemsWithParams(string param)
        {
            throw new NotImplementedException();
        }
    }
}