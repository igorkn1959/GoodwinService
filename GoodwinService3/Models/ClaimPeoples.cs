using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class ClaimPeoples
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Human { get; set; }
        public DateTime Born { get; set; }
        bool male;
        public bool Male 
        {
            set
            {
                male = value;
            }
        }
        public string Sex
        {
            get { return male ? "MALE" : "FEMALE"; }
        }
    }
}