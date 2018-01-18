using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class ReportResult
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public List<PrClaim> Results { get; set; }
    }
}