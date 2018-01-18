using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
 
    public class ClaimOrder
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
        public string CostCurrency { get; set; }
        public decimal Net { get; set; }
        public string NetCurrency { get; set; }
        public short Count { get; set; }
        public int Partner { get; set; }
        public List<int> Peoples { get; set; }
    }



    public class ClaimHotel : ClaimOrder
    {
        public string Star { get; set; }
        public string Room { get; set; }
        public string Meal { get; set; }
    }



}