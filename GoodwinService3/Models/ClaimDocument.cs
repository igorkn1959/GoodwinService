using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class ClaimDocument
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string ForPeople { get; set; }
        public string LinkForUnload { get; set; }

    }
}