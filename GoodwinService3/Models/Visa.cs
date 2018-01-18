using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class Visa
    {
        public int VisaPrInc { get; set; }
        public int StateInc { get; set; }
        public string StateName { get; set; }
        public string StateLname { get; set; }
        public string VisaName { get; set; }
        public string VisaLName { get; set; }
        public short VisaDays { get; set; }
        public bool Commission { get; set; }
        public bool VisaInPacket { get; set; }
        public int Partner { get; set; }
    }
}