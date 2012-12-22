using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internet.Models
{
    public class BoolSetting
    {
        public BoolSetting(string displayName, bool value)
        {
            DisplayName = displayName;
            Value = value;
        }
        public string DisplayName { get; set; }
        public bool Value { get; set; }
    }
}