using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GoodwinService3.Models
{
    public class BronClaim
    {
        public string CatClaim;
        public int Currency { get; set; }
        public Guid ClaimGuid;
        //private byte[] catClaimBytes;
        public List<People> Peoples { get; set; }
        private SamoDB Samo;
         public List<string> errors;
        private int townFrom;
        public int TownFrom
        {
            get
            {
                return townFrom;
            }
            set
            {
                townFrom = value;
            }
        }
        public int State { get; set; }
        public int Tour { get; set; }
        public int CatalogKey { get; set; }
        public decimal ComissionPercent { get; set; }
        private int partpass;
        private int partner;
        private int internetUser;
        public int InternetPartner { get; set; }
        public int Owner { get; set; }

        public int PeopleCount
        {
            get;
            set;
        }

        public int Adult
        {
            get;
            set;
        }

        public int Child
        {
            get;
            set;
        }

        ////public int Age1
        //{
        //    get;
        //    set;
        //}

        //public int Age2
        //{
        //    get;
        //    set;
        //}

        //public int Age3
        //{
        //    get;
        //    set;
        //}

        public decimal Price
        {
            get;
            set;
        }

        public int? Claim { get; set; }

        public string ClaimNotes { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
        public int Spog { get; set; }

        public int Nights { get; set; }
        public Packet Packet;

        //private decimal GetDecimal(string p)
        //{
        //    uint num = uint.Parse(p, System.Globalization.NumberStyles.AllowHexSpecifier);
        //    uint c = num / 10000;
        //    uint d = num % 10000;
        //    Decimal result = (decimal)c + ((decimal)d) / 10000;
        //    return result;
        //}


        public BronClaim() 
        {
            Samo = new SamoDB();
            try
            {
                partpass = Samo.GetPartPass();
                partner = Samo.GetPartner(partpass);
                internetUser = Samo.GetInternetUser();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BronClaim (string catClaim) :this()
        {
            this.CatClaim = catClaim;
            //Currency = currency;
            ClaimGuid = Guid.NewGuid();
            Samo.FillPacketInfo(GetCatClaimBytes(), partner);
            Samo.FillCatClaimUnpack(GetCatClaimBytes());
            SamoDB.uf_web_4_cat_claim_unpackRow row = (SamoDB.uf_web_4_cat_claim_unpackRow)Samo.uf_web_4_cat_claim_unpack.Rows[0];
            townFrom = row.townfrom;
            State = row.state;
            Tour = row.tour;
            Currency = row.currency;
            Spog = row.spog;
            CatalogKey = row.inc;
            ComissionPercent = (decimal)Samo.PacketInfo.Rows[0]["Commission"] 
                + (decimal)Samo.PacketInfo.Rows[0]["InternetPartnerCommission"]
                + (decimal)Samo.PacketInfo.Rows[0]["EarlyCommission"]
                + (decimal)Samo.PacketInfo.Rows[0]["MediatorCommission"];
            Nights = row.nights;
            CheckIn = row.checkin;
            CheckOut = row.checkout;
            PeopleCount = row.peoplecount;
            Adult = row.adult;
            Child = row.child;
            //Age1 = row.IsAGE1Null() ? 0 : row.AGE1;
            //Age2 = row.IsAGE2Null() ? 0 : row.AGE2;
            //Age3 = row.IsAGE3Null() ? 0 : row.AGE3;
            Samo.FillTourConfig(Tour, internetUser);
            InternetPartner = int.Parse(Samo.up_WEB_3_tour_config.FirstOrDefault(c => c.What == "INTERNET_PARTNER").Value);
            Owner = int.Parse(Samo.up_WEB_3_tour_config.FirstOrDefault(c => c.What == "FIRMCODE").Value);
            Packet = new Packet();
            Peoples = new List<People>();
            FillPeople();
            
        }


        private byte[] GetCatClaimBytes()
        {
            byte[] result = new byte[CatClaim.Length - 2];
            for (int i = 2; i < CatClaim.Length; i = i + 2)
            {
                result[i / 2 - 1] = Convert.ToByte(CatClaim.Substring(i, 2), 16);
            }
            return result;
        }

        private void FillPeople()
        {
            for (int i = 1; i <= PeopleCount; i++)
            {
                string human = i > Adult ? "CHD" : "MR";
                Peoples.Add(new People() { Human = human, Key = -i });

            }
        }

        public void FillPacket()
        {
            FillHotels();
            FillTransport();
            FillServices();
            FillInsures();
            FillVisas();
        }

        private void FillVisas()
        {
            Samo.FillVisas(GetCatClaimBytes());
            Packet.Visas = new List<Visa>();
            foreach (SamoDB.PacketVisaRow row in Samo.PacketVisa.Where(v => (int)v["VisaPrInc"] != 0))
            {
                Visa visa = new Visa();
                visa.VisaPrInc = (int)row["VisaPrInc"];
                visa.StateInc = (int)row["StateInc"];
                visa.StateName = (string)row["StateName"];
                visa.StateLname = (string)row["StateLname"];
                visa.VisaName = (string)row["VisaName"];
                visa.VisaLName = (string)row["VisaLName"];
                visa.VisaDays = (short)row["VisaDays"];
                visa.Commission = (bool)row["Commission"];
                visa.VisaInPacket = (bool)row["VisaInPacket"];
                visa.Partner = (int)row["Partner"];
                Packet.Visas.Add(visa);
            }
        }

        private void FillInsures()
        {
            
        }

        private void FillHotels()
        {
            Samo.FillHotelPacket(GetCatClaimBytes());
            Packet.Hotels = new List<Hotel>();
            foreach (SamoDB.PacketHotelRow row in Samo.PacketHotel.Rows)
            {
                //SamoDB.HotelRow nr = Samo.Hotel.NewHotelRow();
                Hotel hotel = new Hotel();
                hotel.Key = (int)row["HotelInc"];
                hotel.Name = row["HotelName"].ToString();
                hotel.DateBeg = DateTime.Parse(row["DateBeg"].ToString());
                hotel.DateEnd = DateTime.Parse(row["DateEnd"].ToString());
                hotel.RoomKey = (int)row["RoomInc"];
                hotel.RoomName = row["RoomName"].ToString();
                hotel.MealKey = (int)row["MealInc"];
                hotel.MealName = row["MealName"].ToString();
                hotel.HtplaceKey = (int)row["HtPlaceInc"];
                hotel.HtplaceName = row["HtPlaceName"].ToString();
                hotel.Count = 1;
                hotel.CureKey = 1; //TODO ???
                //hotel.AddInfant = 0;
                hotel.Partner = (int)row["Partner"];
                hotel.RouteIndex = (short)row["RouteIndex"];
                Packet.Hotels.Add(hotel);
            }
        }

        private void FillTransport()
        {
            Samo.FillTransportPacket(GetCatClaimBytes());
            if (Samo.PacketFreight.Count == 0 )
            {
                return;
            }
            foreach (System.Data.DataColumn col in Samo.PacketFreight.Columns)
            {
                Debug.WriteLine(col);
            }
            Packet.Transports = new List<Transport>();
            string dateFormat = "yyyyMMdd";
            System.Globalization.CultureInfo dateCulture = new System.Globalization.CultureInfo("ru-RU");
            foreach (SamoDB.PacketFreightRow row in Samo.PacketFreight)
            {
                Transport transport = new Transport();
                transport.Key = (int)row["Freight"];
                transport.Name = row["Name"].ToString();
                
                transport.DateBeg = DateTime.ParseExact(row["DateBeg"].ToString(),dateFormat,dateCulture);
                transport.DateEnd = DateTime.ParseExact(row["DateEnd"].ToString(), dateFormat, dateCulture); //
                transport.ClassKey = (int)row["Class"];
                transport.FrpPlaceKey = (int)row["FrPlace"];
                //transport.AddInfant = 0;
                transport.Partner = (int)row["Partner"];
                transport.Count = PeopleCount;
                Packet.Transports.Add(transport);
            }
        }

        private void FillServices()
        {
            Samo.FillServicePacket(GetCatClaimBytes());
            if (Samo.PacketService.Count == 0)
            {
                return;
            }
            if (Packet.Servises == null)
            {
                Packet.Servises = new List<Service>();
            }
            string dateFormat = "yyyyMMdd";
            System.Globalization.CultureInfo dateCulture = new System.Globalization.CultureInfo("ru-RU");
            foreach (SamoDB.PacketServiceRow row in Samo.PacketService.Rows)
            {

                Service service = new Service();
                service.Key = (int)row["ServiceInc"];
                service.Name = row["ServiceName"].ToString();
                service.DateBeg = DateTime.ParseExact(row["DateBeg"].ToString(), dateFormat, dateCulture);
                service.DateEnd = DateTime.ParseExact(row["DateEnd"].ToString(), dateFormat, dateCulture);
                if (row["HotelInc"] != DBNull.Value)
                {
                    service.HotelKey = (int)row["HotelInc"];
                }
                if (row["MealInc"] != DBNull.Value)
                {
                    service.MealKey = (int)row["MealInc"];
                }
                if (row["RoomInc"] != DBNull.Value)
                {
                    service.RoomKey = (int)row["RoomInc"];
                }
                
                service.DepartureTownKey = row["SrcTownInc"] == DBNull.Value ? null : (int?)row["SrcTownInc"];
                service.ArrivalTownKey = row["TrgTownInc"] == DBNull.Value ? null : (int?)row["TrgTownInc"];
                if (row["AirlineInc"] != System.DBNull.Value)
                {
                    service.TransportCompanyKey = (int)row["AirlineInc"];
                }
                if (row["ClassInc"] != DBNull.Value)
                {
                    service.ClassKey = (int)row["ClassInc"];
                }
                service.Packet = (int)row["Packet"];
                service.RouteIndex = (short)row["RouteIndex"];
                service.Type = "stOther";
                service.Stlname = row["ServiceTypeLName"].ToString();
                service.Partner = row["Partner"] == DBNull.Value ? null : (int?)row["Partner"];
                service.ServiceTypeKey = (int)row["ServiceTypeInc"];
                service.ServiceCategoryKey = (int)row["ServiceCategoryInc"];
                service.Count = PeopleCount;
                service.uid = Guid.NewGuid();
                Packet.Servises.Add(service);
            }

        }




        internal void CalckBron(bool save)
        {
            string datefotmat = "yyyy-MM-dd";
            XDocument claimDocument = new XDocument();
            XElement root = new XElement("claimDocument");
            claimDocument.Add(root);
            root.Add(new XAttribute("condition", "ccBooked"));
            root.Add(new XAttribute("status", "csNotReaded"));
            root.Add(new XAttribute("payStatus", "psNotPaid"));
            root.Add(new XAttribute("townFromKey", this.TownFrom));
            root.Add(new XAttribute("stateKey", this.State));
            root.Add(new XAttribute("tourKey", Tour));
            root.Add(new XAttribute("catalogKey", CatalogKey));
            root.Add(new XAttribute("comissionPercent", ComissionPercent));
            root.Add(new XAttribute("datebeg", CheckIn.ToString(datefotmat)));
            root.Add(new XAttribute("dateend", CheckOut.ToString(datefotmat)));
            root.Add(new XAttribute("nights", Nights));
            root.Add(new XAttribute("spoKey", Spog));
            root.Add(new XAttribute("mediatorKey", -2147483647));
            bool addInfant = GetAddInfant();
            XElement hotels = new XElement("hotels");
            root.Add(hotels);
            foreach (var hot in Packet.Hotels)
	        {
                XElement hotel = new XElement("hotel");
                hotels.Add(hotel);
                HotelToXElement(datefotmat, hot, hotel, addInfant);
	        }
            XElement transports = new XElement("transports");
            root.Add(transports);
            foreach (var tr in Packet.Transports)
            {
                XElement transport = new XElement("transport");
                TransportToXElement(datefotmat, addInfant, tr, transport);
                transports.Add(transport);
            }
            XElement servises = new XElement("services");
            root.Add(servises);
            foreach (var serv in Packet.Servises)
            {
                XElement servise = new XElement("service");
                servises.Add(servise);
                ServiseToXElement(datefotmat, serv, servise);
            }
            foreach (var visa in Packet.Visas)
            {
                XElement servise = VisaToXElement(datefotmat, visa, addInfant);

                if (servise != null)
                {
                    servises.Add(servise);
                }
                
            }
            XElement peoples = new XElement("peoples");
            root.Add(peoples);
            foreach (var people in Peoples)
            {
                XElement peopleElement = new XElement("people");
                PeopleToXElement(datefotmat, people, peopleElement);
                peoples.Add(peopleElement);
            }
            byte[] cc = GetCatClaimBytes();
            //errors.Clear();
            try
            {
                Samo.BronCalcSave(cc, Tour, Nights, save, true, "", partpass, internetUser, ClaimNotes, Owner , InternetPartner, claimDocument.ToString(), ClaimGuid);
                if (Samo.bron_calc_save.Columns[0].ColumnName == "error")
                {
                    if (errors == null)
                    {
                        errors = new List<string>();
                    }
                    
                    foreach (var errrow in Samo.bron_calc_save)
                    {
                        errors.Add((string)errrow["error"]);
                    }
                }
                else
                {
                    decimal priceStr = (decimal)Samo.bron_calc_save.Rows[0]["PriceStr"];
                    Price = priceStr;
                    if (Samo.bron_calc_save.Rows[0]["Claim"] != DBNull.Value)
                    {
                        Claim = (int?)Samo.bron_calc_save.Rows[0]["Claim"];
                    }
                    
                    
                }
                
                
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private XElement VisaToXElement(string datefotmat, Visa visa, bool addinfant)
        {
            var clients = Peoples.Where(p => p.Visa == visa.VisaPrInc);

            if (clients.Count() > 0)
            {
                XElement servise = new XElement("service");
                servise.Add(new XAttribute("type", "stVisa"));
                servise.Add(new XAttribute("uid", Guid.NewGuid()));
                servise.Add(new XAttribute("key", visa.VisaPrInc));
                servise.Add(new XAttribute("count", clients.Count()));
                servise.Add(new XAttribute("partnerKey", visa.Partner));
                servise.Add(new XAttribute("datebeg", CheckIn.ToString(datefotmat)));
                servise.Add(new XAttribute("dateend", CheckOut.ToString(datefotmat)));
                servise.Add(new XAttribute("addinfant", addinfant ? 1 : 0));
                XElement clientsel = new XElement("clients");
                servise.Add(clientsel);
                foreach (var cl  in clients)
                {
                    XElement client = new XElement("client");
                    client.Add(new XAttribute("peopleKey", cl.Key));
                    client.Add(new XAttribute("common", cl.Key));
                    clientsel.Add(client);
                   
                }
                return servise;
            }
            else
            {
                return null;
            }
        }

        private static void PeopleToXElement(string datefotmat, People people, XElement peopleElement)
        {
            peopleElement.Add(new XAttribute("key", people.Key));
            peopleElement.Add(new XAttribute("age", people.AgeForXML()));
            peopleElement.Add(new XAttribute("human", people.Human));
            peopleElement.Add(new XAttribute("name", people.Name));
            peopleElement.Add(new XAttribute("lname", people.Name));
            peopleElement.Add(new XAttribute("nationalityKey", people.NationalityKey == null ? -2147483647 : people.NationalityKey));
            peopleElement.Add(new XAttribute("sex", people.SexForXML()));
            peopleElement.Add(new XAttribute("born", people.Born.ToString(datefotmat)));
            if (!string.IsNullOrWhiteSpace(people.Pserie))
            {
                peopleElement.Add(new XAttribute("pserie", people.Pserie));
            }
            if (!string.IsNullOrWhiteSpace(people.Pnumber))
            {
                peopleElement.Add(new XAttribute("pnumber", people.Pnumber));
            }
            if (!(people.Pexpire == null))
            {
                peopleElement.Add(new XAttribute("pexpire", ((DateTime)people.Pexpire).ToString(datefotmat)));
            }
            if (!(people.Pgiven == null))
            {
                peopleElement.Add(new XAttribute("pgiven", ((DateTime)people.Pgiven).ToString(datefotmat)));
            }
            
        }

        private void ServiseToXElement(string datefotmat, Service serv, XElement servise)
        {
            servise.Add(new XAttribute("key", serv.Key));
            servise.Add(new XAttribute("uid", Guid.NewGuid()));
            servise.Add(new XAttribute("datebeg", serv.DateBeg.ToString(datefotmat)));
            servise.Add(new XAttribute("dateend", serv.DateEnd.ToString(datefotmat)));
            servise.Add(new XAttribute("type", "stOther"));
            if (serv.HotelKey != null)
            {
                servise.Add(new XAttribute("hotelKey", serv.HotelKey));
            }
            if (serv.MealKey != null)
            {
                servise.Add(new XAttribute("mealKey", serv.MealKey));
            }
            if (serv.RoomKey != null)
            {
                servise.Add(new XAttribute("roomKey", serv.RoomKey));
            }
            
            if (serv.DepartureTownKey != null)
            {
                servise.Add(new XAttribute("departureTownKey",  serv.DepartureTownKey));
            }
            if (serv.ArrivalTownKey != null)
            {
                servise.Add(new XAttribute("arrivalTownKey", serv.ArrivalTownKey));
            }
            
            if (serv.TransportCompanyKey != null)
            {
                servise.Add(new XAttribute("transportCompanyKey", serv.TransportCompanyKey));
            }
            
            if (serv.ClassKey != null)
            {
                servise.Add(new XAttribute("classKey",  serv.ClassKey));
            }
            
            servise.Add(new XAttribute("packet", serv.Packet));
            servise.Add(new XAttribute("routeIndex", serv.RouteIndex));
            servise.Add(new XAttribute("partnerKey", serv.Partner));
            //servise.Add(new XAttribute("headUid", Guid.NewGuid()));
            servise.Add(new XAttribute("count", serv.Count));
            XElement clients = new XElement("clients");
            servise.Add(clients);
            foreach (var p in Peoples)
            {
                XElement client = new XElement("client");
                client.Add(new XAttribute("peopleKey", p.Key));
                client.Add(new XAttribute("common", p.Key));
                clients.Add(client);
            }
        }

        private void TransportToXElement(string datefotmat, bool addInfant, Transport tr, XElement transport)
        {
            transport.Add(new XAttribute("uid", Guid.NewGuid()));
            transport.Add(new XAttribute("key", tr.Key));
            transport.Add(new XAttribute("datebeg", tr.DateBeg.ToString(datefotmat)));
            transport.Add(new XAttribute("dateend", tr.DateEnd.ToString(datefotmat)));
            transport.Add(new XAttribute("classKey", tr.ClassKey));
            transport.Add(new XAttribute("frplaceKey", tr.FrpPlaceKey));
            transport.Add(new XAttribute("count", tr.Count));
            transport.Add(new XAttribute("addinfant", addInfant ? 1 : 0));
            transport.Add(new XAttribute("partnerKey", tr.Partner));
            transport.Add(new XAttribute("external", 0));
            XElement clients = new XElement("clients");
            transport.Add(clients);
            foreach (var p in Peoples)
            {
                XElement client = new XElement("client");
                client.Add(new XAttribute("peopleKey", p.Key));
                client.Add(new XAttribute("common", p.Key));
                clients.Add(client);
            }
        }

        private bool GetAddInfant()
        {
            return Peoples.Where(p => p.Human == "INF").Count() > 0;
        }

        private void HotelToXElement(string datefotmat, Hotel hot, XElement hotel, bool addinfant)
        {
            hotel.Add(new XAttribute("key", hot.Key));
            hotel.Add(new XAttribute("uid", Guid.NewGuid()));
            hotel.Add(new XAttribute("datebeg", hot.DateBeg.ToString(datefotmat)));
            hotel.Add(new XAttribute("dateend", hot.DateEnd.ToString(datefotmat)));
            hotel.Add(new XAttribute("roomKey", hot.RoomKey));
            hotel.Add(new XAttribute("htplaceKey", hot.HtplaceKey));
            hotel.Add(new XAttribute("mealKey", hot.MealKey));
            hotel.Add(new XAttribute("count", hot.Count));
            hotel.Add(new XAttribute("cureKey", hot.CureKey));
            hotel.Add(new XAttribute("addinfant", addinfant ? 1 : 0));
            hotel.Add(new XAttribute("routeIndex", hot.RouteIndex));
            hotel.Add(new XAttribute("partnerKey", hot.Partner));
            XElement clients = new XElement("clients");
            hotel.Add(clients);
            int common = Peoples.First().Key;
            foreach (var p in Peoples)
            {
                XElement client = new XElement("client");
                client.Add(new XAttribute("peopleKey", p.Key));
                client.Add(new XAttribute("common", common));
                clients.Add(client);
            }
        }

        
    }
}