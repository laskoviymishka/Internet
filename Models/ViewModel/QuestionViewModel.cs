using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Internet.Models.ViewModels
{
    public class QuestionType 
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class QuestionViewModel 
    {
        List<QuestionType> _type;
        public QuestionViewModel() 
        {
            _type = new List<QuestionType>();
            _type.Add(new QuestionType { ID = 2, Name = "Множественный выбор" });
            _type.Add(new QuestionType { ID = 1, Name = "Один вариант ответа" });
            _type.Add(new QuestionType { ID = 3, Name = "Поле для ввода текста" });
        }

        public Question Question { get; set; }
        public List<QuestionType> Type { get { return _type; } }        
    }
}
