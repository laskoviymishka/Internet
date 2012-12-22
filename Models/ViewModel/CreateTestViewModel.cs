using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Internet.Models
{
    public class CreateTestViewModel
    {
        [Required]
        public Test Test { get; set; }
        [Required]
        public List<TestCategory> Categories { get { return (new CategoryRepository()).GetItems(); } }
        [Required]
        public List<Difficulty> Difficulties { get{ return (new DifficultyRepository()).GetItems(); }}
    }
}