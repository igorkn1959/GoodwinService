using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class Service
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
        public int? HotelKey { get; set; }
        public int? MealKey { get; set; }
        public int? RoomKey { get; set; }
        public int? DepartureTownKey { get; set; }
        public int? ArrivalTownKey { get; set; }
        public int? TransportCompanyKey { get; set; }
        public int? ClassKey { get; set; }
        public int Packet { get; set; }
        public short RouteIndex { get; set; }
        public string Type { get; set; }
        public string Stlname { get; set; }
        public Guid uid { get; set; }
        public int? Partner { get; set; }
        public int ServiceTypeKey { get; set; }
        public int ServiceCategoryKey { get; set; }
        public int Count { get; set; }
        public int AddInfant { get; set; }
    }
}