using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class AnswerRepository:IRepository<Answer>
    {
        private List<Answer> _answers;
        private TestEntities _testEntity;
        public AnswerRepository()
        {
            _testEntity = EntityContextContainer.getEntity();
            _answers = _testEntity.Answers.ToList<Answer>();
        }
        public Answer GetByID(int ID)
        {
            return _answers.FirstOrDefault<Answer>(a => a.ID == ID);
        }

        public void CreateItem(Answer item)
        {
            _testEntity.Answers.AddObject(item);
            _testEntity.SaveChanges();
        }

        public void DeleteItem(Answer item)
        {
          
           /*     foreach (AnswerResult ar in item.AnswersResults)
                {
                    (new AnswerResultRepository()).DeleteItem(ar);
                }
            */
            _testEntity.Answers.DeleteObject(this.GetByID(item.ID));
            _testEntity.SaveChanges();
           
        }

        public void UpdateItem(Answer item, Answer newItem)
        {
            Answer answer = _testEntity.Answers.Where<Answer>(a => a.ID == item.ID).First<Answer>();
            answer.IsRight = newItem.IsRight;
            answer.AnswerBody = newItem.AnswerBody;

            _testEntity.SaveChanges();
        }


        public List<Answer> GetItems()
        {
            return _answers;
        }


        public List<Answer> GetItemsWithParams(int param)
        {
            return _testEntity.Answers.Where(a => a.QuestionID == param).ToList<Answer>();
        }

        public List<Answer> GetItemsWithParams(string param)
        {
            throw new NotImplementedException();
        }
    }
}