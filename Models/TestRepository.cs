using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class TestRepository:IRepository<Test>
    {
        TestEntities _testEntity;
        public TestRepository() 
        {
            EntityContextContainer.Update();
            _testEntity = EntityContextContainer.getEntity();
            
        }
        #region Getters
        public Test GetByID(int ID)
        {
            return (Test)_testEntity.Tests.FirstOrDefault<Test>(t => t.ID == ID);
        }


        #endregion


        #region Others
        public void CreateItem(Test item)
        {
            //item.ID = _tests.Tests.ToList().Last<Test>().ID + 1;
            //item.TestCategory = (TestCategory)_tests.TestCategories.Where<TestCategory>(c => c.ID == item.CategoryID).FirstOrDefault<TestCategory>();
           // item.Difficulty = (Difficulty)_tests.Difficulties.Where<Difficulty>(d => d.ID == item.DifficultyID).FirstOrDefault<Difficulty>();
           

            _testEntity.Tests.AddObject(item);
            _testEntity.SaveChanges();
        }
        public void DeleteItem(Test item)
        {


       /* foreach (Question ar in item.Questions)
                {
                    (new QuestionRepository()).DeleteItem(ar);
                }
             
          */
            /*foreach (Result ar in this.GetByID(item.ID).Results)
                {
                    (new ResultRepository()).DeleteItem(ar);
                }
            */
            _testEntity.Tests.DeleteObject(this.GetByID(item.ID));
            _testEntity.SaveChanges();
        }
        public void UpdateItem(Test item,Test newItem) 
        {
            Test test = this.GetByID(item.ID);
            test.TestName = newItem.TestName;
            test.TestDescription = newItem.TestDescription;
            test.CategoryID = newItem.CategoryID;
            test.DifficultyID = newItem.DifficultyID;
            _testEntity.SaveChanges();
        }
        #endregion


        public List<Test> GetItems()
        {
            return _testEntity.Tests.ToList<Test>();
        }


        public List<Test> GetItemsWithParams(int param)
        {
            throw new NotImplementedException();
        }

        public List<Test> GetItemsWithParams(string param)
        {
            throw new NotImplementedException();
        }
    }
}