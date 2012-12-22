using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Internet.Models.ModelService
{
    public class ScoreService 
    {
        public static int MaxScore(Test test) 
        {
            int maxScore = 0;
            foreach (var question in test.Questions) 
            {
                foreach (var answer in question.Answers) 
                {
                    if (answer.IsRight) 
                    {
                        maxScore++;
                    }
                }
            }
            return maxScore;
        }
        public static int ResultScore(Result result) 
        {
            int score = 0;
            foreach (var item in result.AnswersResults) 
            {
                if (item.Answer.Question.QuestionType == 3)
                {
                    if (item.Answer.AnswerBody.ToLower() == item.AnswerText.ToLower()) { score++; }
                }
                else 
                {
                    if (item.Answer.IsRight) { score++; }
                }
            }
            return score;
        }
    }
}
