using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class Transport
    {
        public string Name { get; set; }
        public int Key { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
        public int ClassKey { get; set; }
        public int FrpPlaceKey { get; set; }
        public int AddInfant { get; set; }
        public int Partner { get; set; }
        public int AdultInfClass { get; set; }
        public int AdultInffrplace { get; set; }
        public int OnlineClass { get; set; }
        public bool External { get; set; }
        public int Count { get; set; }
    }
}