using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class EntityContextContainer
    {
        private static TestEntities _container;
        public static void Update() { _container = new TestEntities(); }
        public static TestEntities getEntity() 
        {
            return _container;
        }
    }
}