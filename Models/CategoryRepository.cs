using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class CategoryRepository:IRepository<TestCategory>
    {
        private TestEntities _testEntity;
        public CategoryRepository() 
        {
            _testEntity = new TestEntities();
        }
        public TestCategory GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<TestCategory> GetItems()
        {
            return _testEntity.TestCategories.ToList();
        }

        public List<TestCategory> GetItemsWithParams(int param)
        {
            throw new NotImplementedException();
        }

        public List<TestCategory> GetItemsWithParams(string param)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(TestCategory item)
        {
            _testEntity.TestCategories.AddObject(item);
            _testEntity.SaveChanges();
        }

        public void DeleteItem(TestCategory item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(TestCategory item, TestCategory newItem)
        {
            throw new NotImplementedException();
        }
    }
}