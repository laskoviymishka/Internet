using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class DifficultyRepository:IRepository<Difficulty>
    {
        private TestEntities _testEntity;
        public DifficultyRepository() 
        {
            _testEntity = new TestEntities();
        }
        public Difficulty GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Difficulty> GetItems()
        {
            return _testEntity.Difficulties.ToList();
        }

        public List<Difficulty> GetItemsWithParams(int param)
        {
            throw new NotImplementedException();
        }

        public List<Difficulty> GetItemsWithParams(string param)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Difficulty item)
        {
            _testEntity.Difficulties.AddObject(item);
            _testEntity.SaveChanges();
        }

        public void DeleteItem(Difficulty item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Difficulty item, Difficulty newItem)
        {
            throw new NotImplementedException();
        }
    }
}