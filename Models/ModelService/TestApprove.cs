using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models.ModelService
{
    public class TestApprove
    {
        public static bool Approve(Test item) 
        {
            if(item.Questions.Count==0)
            {
                return false;                            
            }
            foreach (Question question in item.Questions) 
            {
                bool haveCheckedAnswer=false;
                if (question.Answers.Count == 0)
                {
                    return false;
                }
                else 
                {
                    foreach(Answer answer in question.Answers)
                    {
                        if(answer.IsRight)
                        {
                            haveCheckedAnswer = true;
                        }
                    }
                    if (!haveCheckedAnswer) 
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}