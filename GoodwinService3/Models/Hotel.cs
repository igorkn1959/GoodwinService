using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class Hotel
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
        public int RoomKey { get; set; }
        public string RoomName { get; set; }
        public int HtplaceKey { get; set; }
        public string HtplaceName { get; set; }
        public int MealKey { get; set; }
        public string MealName { get; set; }
        public int Count { get; set; }
        public int CureKey { get; set; }
        public int AddInfant { get; set; }
        public int RouteIndex { get; set; }
        public int Partner { get; set; }
    }
}