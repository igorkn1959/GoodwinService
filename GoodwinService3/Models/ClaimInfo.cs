using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodwinService3.Models
{
    public class ClaimInfo
    {
        public int inc { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastEditDate { get; set; }
        public bool Confirmed { get; set; }
        public string StatusFull { get; set; }
        public string Status { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
        public List<ClaimPeoples> Peoples { get; set; }
        public List<Partner> Partners { get; set; } 

        public List<ClaimHotel> Hotels { get; set; }
        public List<ClaimOrder> Freigts { get; set; }
        public List<ClaimOrder> Servises { get; set; }
        public List<ClaimOrder> Insuranses { get; set; }
        public List<ClaimOrder> Visas { get; set; }
        public List<ClaimDocument> ClaimDocuments { get; set; }

        public ClaimInfo(int inc, string uriDoc)
        {
            kompasEntities db = new kompasEntities();
            claim cl = db.claim.FirstOrDefault(c => c.inc == inc);
            if (cl == null)
            {
                throw new Exception(string.Format("Заявка #{0} не найдена", inc));
            }
            else
            {
                this.inc = cl.inc;
                CreateDate = (DateTime)cl.cdate;
                LastEditDate = ((DateTime)cl.edate).ToString("yyyy-MM-dd HH:mm.ss");
                Confirmed = cl.confirmed;
                Status = cl.status1.lname;
                if (cl.status == 3)
                {
                    StatusFull = "Canceled";
                }
                else
                {
                    if (cl.confirmed)
                    {
                        StatusFull = "Confirmed";
                    }
                    else
                    {
                        if (cl.confirmeddate == null)
                        {
                            StatusFull = "Request";
                        }
                        else
                        {
                            StatusFull = "Denied";
                        }
                    }
                }
                DateBeg = (DateTime)cl.datebeg;
                DateEnd = (DateTime)cl.dateend;
                Peoples = new List<ClaimPeoples>();
                foreach (var people in cl.people)
                {
                    Peoples.Add(new ClaimPeoples() { Id = people.inc, Name = people.name, Human = people.human, Born = (DateTime)people.born, Male = people.male });
                }
                var hotels = cl.order.Where(o => o.hotel > 0);
                if (hotels.Count() > 0)
                {
                    Hotels = new List<ClaimHotel>();
                    foreach (var h in hotels)
                    {
                        Hotels.Add(new ClaimHotel() { 
                            Name = h.hotel1.name, 
                            Room = h.room1.name, 
                            Meal = h.meal1.name, 
                            Cost = (decimal)h.cost,
                            CostCurrency = h.currency.alias,
                            Net = (decimal)h.net,
                            NetCurrency = h.currency1.alias,
                            DateBeg = (DateTime)h.datebeg,
                            DateEnd = (DateTime)h.dateend,
                            Count = (short)h.rcount,
                            Partner = (int)h.partner,
                            Peoples =  h.opeople.Select(op => (int)op.people).ToList()
                        });
                    }
                }
                Partners = new List<Partner>();
                foreach (var p in cl.order.Select(o => o.partner2).Distinct())
                {
                    Partners.Add(new Partner()
                    {
                        Id = p.inc,
                        Name = p.name,
                        LName = p.lname,
                        OfficialName = p.officialname,
                        Email = p.email,
                        Email1 = p.email1
                    });
                }
                var freights = cl.order.Where(o => o.freight > 0);
                if (freights.Count() > 0)
                {
                    Freigts = new List<ClaimOrder>();
                    foreach (var fr in freights)
                    {
                        Freigts.Add(new ClaimOrder()
                        {
                            Name = fr.freight1.name,
                            Cost = (decimal)fr.cost,
                            CostCurrency = fr.currency.alias,
                            Net = (decimal)fr.net,
                            NetCurrency = fr.currency1.alias,
                            DateBeg = (DateTime)fr.datebeg,
                            DateEnd = (DateTime)fr.dateend,
                            Count = (short)fr.rcount,
                            Partner = (int)fr.partner,
                            Peoples = fr.opeople.Select(op => (int)op.people).ToList()

                        });
                    }
                }
                var services = cl.order.Where(o => o.service > 0);
                if (services.Count() > 0)
                {
                    Servises = new List<ClaimOrder>();
                    foreach (var srv in services)
                    {
                        Servises.Add(new ClaimOrder()
                        {
                            Name = srv.service1.name,
                            Cost = (decimal)srv.cost,
                            CostCurrency = srv.currency.alias,
                            Net = (decimal)srv.net,
                            NetCurrency = srv.currency1.alias,
                            DateBeg = (DateTime)srv.datebeg,
                            DateEnd = (DateTime)srv.dateend,
                            Count = (short)srv.rcount,
                            Partner = (int)srv.partner,
                            Peoples = srv.opeople.Select(op => (int)op.people).ToList()

                        });
                    }
                }
                var insuranses = cl.order.Where(o => o.insure > 0);
                if (insuranses.Count() > 0)
                {
                    Insuranses = new List<ClaimOrder>();
                    foreach (var ins in insuranses)
                    {
                        Insuranses.Add(new ClaimOrder()
                        {
                            Name = ins.insure1.name,
                            Cost = (decimal)ins.cost,
                            CostCurrency = ins.currency.alias,
                            Net = (decimal)ins.net,
                            NetCurrency = ins.currency1.alias,
                            DateBeg = (DateTime)ins.datebeg,
                            DateEnd = (DateTime)ins.dateend,
                            Count = (short)ins.rcount,
                            Partner = (int)ins.partner,
                            Peoples = ins.opeople.Select(op => (int)op.people).ToList()
                        });
                    }
                }
                var visas = cl.order.Where(o => o.visapr > 0);
                if (visas.Count() > 0)
                {
                    Visas = new List<ClaimOrder>();
                    foreach (var v in visas)
                    {
                        Visas.Add(new ClaimOrder()
                        {
                            Name = v.visapr1.visa1.name,
                            Cost = (decimal)v.cost,
                            CostCurrency = v.currency.alias,
                            Net = (decimal)v.net,
                            NetCurrency = v.currency1.alias,
                            DateBeg = (DateTime)v.datebeg,
                            DateEnd = (DateTime)v.dateend,
                            Count = (short)v.rcount,
                            Partner = (int)v.partner,
                            Peoples = v.opeople.Select(op => (int)op.people).ToList()
                        });
                    }
                }
                
                var claimdocuments = cl.external_document;
                if (claimdocuments.Count > 0)
                {
                    ClaimDocuments = new List<ClaimDocument>();
                    
                    
                    foreach (var doc in claimdocuments)
                    {
                        ClaimDocuments.Add(new ClaimDocument()
                        {
                            DocumentId = doc.inc,
                            DocumentName = doc.name,
                            DocumentType = doc.doctype1.lname,
                            ForPeople = doc.people == null ? "" : cl.people.First(p => p.inc == doc.people).name,
                            LinkForUnload = String.Format("{0}/{1}", uriDoc, doc.inc)
                        });
                    }
                }
            }

        }
    }

    
}