using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class ResultRepository: IRepository<Result>
    {
        private List<Result> _results;
        private TestEntities _testEntity;
        public ResultRepository()
        {
            _testEntity = EntityContextContainer.getEntity();
            _results = _testEntity.Results.ToList<Result>();
        }
        public Result GetByID(int ID)
        {
            return _results.FirstOrDefault<Result>(r => r.ID == ID);
        }

        public List<Result> GetItems()
        {
            
            return _results;
        }

        public List<Result> GetItemsWithParams(int param)
        {
            throw new NotImplementedException();
        }

        public List<Result> GetItemsWithParams(string param)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Result item)
        {
            _testEntity.Results.AddObject(item);
            _testEntity.SaveChanges();
        }

        public void DeleteItem(Result item)
        {
            
           
                while (item.AnswersResults.Count > 0)
                {
                    (new AnswerResultRepository()).DeleteItem(item.AnswersResults.First());
                }
                _testEntity.Results.DeleteObject(this.GetByID(item.ID));
                _testEntity.SaveChanges();
          
           /* вот так оно не падает, но и не удаляет млин
            * приходится дважды кликать*/

            

        }

        public void UpdateItem(Result item, Result newItem)
        {
            throw new NotImplementedException();
        }
    }
}