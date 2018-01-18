using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class People
    {
        public int Key { get; set; }
        public bool Sex { get; set; }
        public string Human { get; set; }
        public string Name { get; set; }
        public DateTime Born { get; set; }
        public string Pserie { get; set; }
        public string Pnumber { get; set; }
        public DateTime? Pexpire { get; set; }
        public DateTime? Pgiven { get; set; }
        public string PgivenOrg { get; set; }
        public int? NationalityKey { get; set; }
        public int BornPlaceKey { get; set; }
        //public bool VisaNeeded { get; set; }
        public int Visa { get; set; }
        public string AgeForXML()
        {
            return Human == "MR" || Human == "MRS" ? "ADL" : Human;
        }
        public string SexForXML()
        {
            return Sex ? "MALE" : "FEMALE";
        }
    }
}