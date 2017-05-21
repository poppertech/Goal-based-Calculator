using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class Response<T>
    {
        public T Model { get; set; }
    }
}