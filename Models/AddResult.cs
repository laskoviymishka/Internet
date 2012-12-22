using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class AddResult
    {
         AnswerResultRepository _rRepo;
         Result _res;


        public AddResult(Result res) 
        {
            _rRepo = new AnswerResultRepository();
            _res = res;

           
            
        }
        public Result Score(List<string> items) 
        {
               
            foreach (string item in items) 
            {
                AnswerResult ansRes = new AnswerResult();
                ansRes.AnswerID = int.Parse(item);
                ansRes.ResultID = _res.ID;
                _rRepo.CreateItem(ansRes);
            }
            return _res;
        }
        public Result TextScore(string answerID,string textAnswer)
        {
                AnswerResult ansRes = new AnswerResult();
                ansRes.AnswerID = int.Parse(answerID);
                ansRes.ResultID = _res.ID;
                ansRes.AnswerText = textAnswer;
                _rRepo.CreateItem(ansRes);
            return _res;
        }
    }
}