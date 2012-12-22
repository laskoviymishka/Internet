using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public interface IRepository<T>
    {
        T GetByID(int ID);
        List<T> GetItems();
        List<T> GetItemsWithParams(int param);
        List<T> GetItemsWithParams(string param);

        void CreateItem(T item);
        void DeleteItem(T item);
        void UpdateItem(T item, T newItem);
    }
}