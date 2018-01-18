using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class PrClaim
    {
        public int Claim { get; set; }
        public double Amount { get; set; }
        public double Net { get; set; }
        public string Currency { get; set; }
        public double HotelNet { get; set; }
        public double FlightsNet { get; set; }
        public double InsurancesNet { get; set; }
        public double TransfersNet { get; set; }
        public double ExcursionsNet { get; set; }
        public double VisasNet { get; set; }
        public double SupplementsNet { get; set; }
        public double ServicesNet { get; set; }
        public string HotelPartner { get; set; }
        public int TouristCount { get; set; }
        public string TouristName { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string ClaimStatus { get; set; }
        public string DateBeg { get; set; }
        public string DateEnd { get; set; }
        //public bool? Confirmed { get; set; }
        
    }
}