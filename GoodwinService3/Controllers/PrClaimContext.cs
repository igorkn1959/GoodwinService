using GoodwinService3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GoodwinService3.Controllers
{
    public class PrClaimContext
    {
        string connectionString;
        public PrClaimContext()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KompasConnection"].ConnectionString;
        }

        public List<PrClaim> GetClaims(DateTime date, DateTime dateEnd, int country, string claimlist)
        {
            List<PrClaim> result = new List<PrClaim>();
            string partnerList = Properties.Settings.Default.PartnerList;//"2870";
            string statusListValue = Properties.Settings.Default.StatusList;//"1, 2, 4, 5";
            DateTime dateFromValue = date;
            DateTime dateTillValue = dateEnd;
            DateTime dateRateValue = date;
            string hotelIncTableName = "##hotelinc" + Math.Floor((DateTime.Now - DateTime.Today).TotalMilliseconds).ToString();
            string claimstrValue = claimlist;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            SqlCommand createTemp = new SqlCommand();
            SqlCommand runProc = new SqlCommand();
            string resultTableMame = RunProcInit(partnerList, statusListValue, dateFromValue, dateTillValue, dateRateValue, hotelIncTableName, claimstrValue, con, runProc, country);
            createTemp.CommandType = CommandType.Text;

            createTemp.CommandText = string.Format("create table {0} (inc int null)", hotelIncTableName);
            createTemp.Connection = con;


            string getResultCommandText = "SELECT prclm.claim, prclm.amount, prclm.net, currency.alias as currency, prclm.hnet as HotelNet, prclm.fnet as FlightsNet, prclm.inet as InsurancesNet, prclm.tnet AS TransfersNet, prclm.enet as ExcursionsNet, prclm.vnet as VisasNet, prclm.supnet AS SupplementsNet, prclm.snet as ServicesNet, hpartner.name AS HotelPartner, claim.confirmed, claim.confirmeddate, prclm.pcount AS TouristCount, people.name as TouristName, status.lname as ClaimStatus, state.lname AS Country, state.inc as CountryId, claim.datebeg, claim.dateend FROM " + resultTableMame + " prclm inner join  claim on prclm.claim = claim.inc left outer join [order] ord on ord.inc = prclm.[order] left outer join partner hpartner on hpartner.inc = ord.partner left outer join currency on currency.inc = prclm.currency inner join people on prclm.people = people.inc inner join status on claim.status = status.inc inner join tour on tour.inc = claim.tour inner join state on state.inc = tour.state ORDER BY prclm.inc DESC";
            SqlCommand getResult = new SqlCommand(getResultCommandText, con);
            
            con.Open();
            createTemp.ExecuteNonQuery();
            int r = runProc.ExecuteNonQuery();
            var reader = getResult.ExecuteReader();

            FillResult(reader, result);
            con.Close();

            return result;
        }

        private void FillResult(SqlDataReader reader, List<PrClaim> result)
        {
            while (reader.Read())
            {
                Models.PrClaim row = new Models.PrClaim();
                row.Claim = reader.GetInt32(reader.GetOrdinal("claim"));
                row.Amount = reader.GetDouble(reader.GetOrdinal("amount"));
                row.Net = reader.GetDouble(reader.GetOrdinal("net"));
                row.Currency = reader.GetString(reader.GetOrdinal("currency"));
                row.HotelNet = reader.GetDouble(reader.GetOrdinal("HotelNet"));
                row.FlightsNet = reader.GetDouble(reader.GetOrdinal("FlightsNet"));
                row.InsurancesNet = reader.GetDouble(reader.GetOrdinal("InsurancesNet"));
                row.TransfersNet = reader.GetDouble(reader.GetOrdinal("TransfersNet"));
                row.ExcursionsNet = reader.GetDouble(reader.GetOrdinal("ExcursionsNet"));
                row.VisasNet = reader.GetDouble(reader.GetOrdinal("VisasNet"));
                row.SupplementsNet = reader.GetDouble(reader.GetOrdinal("SupplementsNet"));
                row.ServicesNet = reader.GetDouble(reader.GetOrdinal("ServicesNet"));
                row.TouristCount = reader.GetInt32(reader.GetOrdinal("TouristCount"));
                row.TouristName = reader.GetString(reader.GetOrdinal("TouristName"));
                string claimStatus;
                string claimSt = reader.GetString(reader.GetOrdinal("ClaimStatus"));
                if (claimSt == "Canceled")
                {
                    claimStatus = "Canceled";
                }
                else
                {
                    if (reader.GetBoolean(reader.GetOrdinal("Confirmed")))
                    {
                        claimStatus = "Confirmed";
                    }
                    else
                    {
                        if (reader.IsDBNull(reader.GetOrdinal("confirmeddate")))
                        {
                            claimStatus = "Request";
                        }
                        else
                        {
                            claimStatus = "Denied";
                        }
                    }
                }
                row.ClaimStatus = claimStatus;
                row.Country = reader.GetString(reader.GetOrdinal("Country"));
                row.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                if (!reader.IsDBNull(reader.GetOrdinal("HotelPartner")))
                {
                    row.HotelPartner = reader.GetString(reader.GetOrdinal("HotelPartner"));
                }
                string dateformat = "yyyy-MM-dd";
                row.DateBeg = reader.GetDateTime(reader.GetOrdinal("datebeg")).ToString(dateformat);
                row.DateEnd = reader.GetDateTime(reader.GetOrdinal("dateend")).ToString(dateformat);
                //row.Confirmed = reader.GetBoolean(reader.GetOrdinal("Confirmed"));
                result.Add(row);
            }
        }

        private string RunProcInit(string partnerList, string statusListValue, DateTime dateFromValue, DateTime dateTillValue, DateTime dateRateValue, string hotelIncTableName, string claimstrValue, SqlConnection con, SqlCommand runProc, int country)
        {
            runProc.Connection = con;
            runProc.CommandType = CommandType.StoredProcedure;

            DateTime minDate = new DateTime(1900, 01, 01);
            DateTime maxDate = new DateTime(2079, 06, 06);
            int all = -2147483647;

            SqlParameter cdatefrom = new SqlParameter("@CDateFrom", SqlDbType.SmallDateTime);
            SqlParameter cdatetill = new SqlParameter("@CDateTill", SqlDbType.SmallDateTime);
            SqlParameter ConfirmedDateFrom = new SqlParameter("@ConfirmedDateFrom", SqlDbType.SmallDateTime);
            SqlParameter ConfirmedDateTill = new SqlParameter("@ConfirmedDateTill", SqlDbType.SmallDateTime);
            SqlParameter PDateFrom = new SqlParameter("@PDateFrom", SqlDbType.SmallDateTime);
            SqlParameter PDateTill = new SqlParameter("@PDateTill", SqlDbType.SmallDateTime);
            SqlParameter Owner = new SqlParameter("@Owner", SqlDbType.Int);
            SqlParameter HPartner = new SqlParameter("@HPartner", SqlDbType.Int);
            SqlParameter state = new SqlParameter("@state", SqlDbType.Int);
            SqlParameter TourList = new SqlParameter("@TourList", SqlDbType.VarChar);
            SqlParameter PartnerList = new SqlParameter("@PartnerList", SqlDbType.VarChar);
            SqlParameter PGroupList = new SqlParameter("@PGroupList", SqlDbType.VarChar);
            SqlParameter MediatorList = new SqlParameter("@MediatorList", SqlDbType.VarChar);
            SqlParameter Curr = new SqlParameter("@Curr", SqlDbType.Int);
            SqlParameter baseCurr = new SqlParameter("@BaseCurr", SqlDbType.Int);
            SqlParameter rate = new SqlParameter("@Rate", SqlDbType.Int);
            SqlParameter round = new SqlParameter("@Round", SqlDbType.Int);
            SqlParameter statusList = new SqlParameter("@StatusList", SqlDbType.VarChar);
            SqlParameter tourCurrency = new SqlParameter("@TourCurrency", SqlDbType.Bit);

            SqlParameter dateFrom = new SqlParameter("@DateFrom", SqlDbType.SmallDateTime);
            SqlParameter dateTill = new SqlParameter("@DateTill", SqlDbType.SmallDateTime);
            SqlParameter dateRate = new SqlParameter("@DateRate", SqlDbType.SmallDateTime);
            SqlParameter name = new SqlParameter("@Name", SqlDbType.VarChar);
            SqlParameter hotelinc = new SqlParameter("@hotelinc", SqlDbType.VarChar);
            SqlParameter claimstr = new SqlParameter("@claimstr", SqlDbType.VarChar);

            cdatefrom.Value = minDate;
            cdatetill.Value = maxDate;
            ConfirmedDateFrom.Value = minDate;
            ConfirmedDateTill.Value = maxDate;
            PDateFrom.Value = minDate;
            PDateTill.Value = maxDate;
            Owner.Value = all;
            HPartner.Value = all;
            state.Value = country;
            TourList.Value = "";
            PartnerList.Value = partnerList;
            PGroupList.Value = "";
            MediatorList.Value = "";
            Curr.Value = Properties.Settings.Default.Currency;
            baseCurr.Value = Properties.Settings.Default.BaseCurrency;
            rate.Value = 0;
            round.Value = 0;
            statusList.Value = statusListValue;
            tourCurrency.Value = 0;

            string resultTableMame = "##result" + Math.Floor((DateTime.Now - DateTime.Today).TotalMilliseconds).ToString();
            dateFrom.Value = dateFromValue;
            dateTill.Value = dateTillValue;
            dateRate.Value = dateRateValue;
            name.Value = resultTableMame;

            hotelinc.Value = hotelIncTableName;
            claimstr.Value = claimstrValue;

            runProc.CommandText = "up_PrClm";
            runProc.Parameters.Add(cdatefrom);
            runProc.Parameters.Add(cdatetill);
            runProc.Parameters.Add(ConfirmedDateFrom);
            runProc.Parameters.Add(ConfirmedDateTill);
            runProc.Parameters.Add(PDateFrom);
            runProc.Parameters.Add(PDateTill);
            runProc.Parameters.Add(Owner);
            runProc.Parameters.Add(HPartner);
            runProc.Parameters.Add(state);
            runProc.Parameters.Add(TourList);
            runProc.Parameters.Add(PartnerList);
            runProc.Parameters.Add(PGroupList);
            runProc.Parameters.Add(MediatorList);
            runProc.Parameters.Add(Curr);
            runProc.Parameters.Add(baseCurr);
            runProc.Parameters.Add(rate);
            runProc.Parameters.Add(round);
            runProc.Parameters.Add(statusList);
            runProc.Parameters.Add(tourCurrency);
            runProc.Parameters.Add(dateFrom);
            runProc.Parameters.Add(dateTill);
            runProc.Parameters.Add(dateRate);
            runProc.Parameters.Add(name);
            runProc.Parameters.Add(hotelinc);
            runProc.Parameters.Add(claimstr);

            return resultTableMame;
        }
    }
}