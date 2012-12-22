using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models.ViewModel
{
    public class ResultsViewModel
    {
        public int ResultID { get; set; }
        public string TestName { get; set; }
        public int MaxScore { get; set; }
        public int Score { get; set; }
        public string UserName { get; set; }
    }
}