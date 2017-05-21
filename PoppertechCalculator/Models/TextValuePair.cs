using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class TextValuePair<T>
    {
        public string Text { get; set; }
        public T Value { get; set; }
    }
}