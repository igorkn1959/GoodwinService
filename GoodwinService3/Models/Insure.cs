using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class Insure
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}