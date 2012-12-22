using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Internet.Models.ViewModels
{
    public class AnswerViewModel
    {
        public Answer Answer { get; set; }
        public Question Question { get; set; }
    }
}
