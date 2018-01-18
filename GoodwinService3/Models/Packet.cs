using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class Packet
    {
        public List<Hotel> Hotels { get; set; }
        public List<Transport> Transports { get; set; }
        public List<Service> Servises { get; set; }
        public List<Visa> Visas { get; set; }
        public List<Insure> Insurances { get; set; }
    }
}