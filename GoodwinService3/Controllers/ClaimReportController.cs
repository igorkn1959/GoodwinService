using GoodwinService3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace GoodwinService3.Controllers
{
    public class ClaimReportController : ApiController
    {
        PrClaimContext db = new PrClaimContext();
        // GET api/ClaimReport
        public ReportResult Get(string dateBeg = "19000101", string dateEnd = "", int country = 0, string claimIncs = "")
        {
            System.Globalization.CultureInfo c = new System.Globalization.CultureInfo("ru-ru");
            ReportResult result = new ReportResult();
            DateTime dBeg;
            DateTime dEnd;
            bool trueDate = DateTime.TryParseExact(dateBeg, "yyyyMMdd", c, System.Globalization.DateTimeStyles.None, out dBeg);
            if (trueDate)
            {
                bool trueDateEnd;
                if (dateEnd == "")
                {
                    if (dateBeg != "19000101")
                    {
                        dEnd = dBeg;
                        trueDateEnd = true;
                    }
                    else
                    {
                        dEnd = new DateTime(2079, 06, 06);
                        trueDateEnd = true;
                    }
                }
                else
                {
                    trueDateEnd = DateTime.TryParseExact(dateEnd, "yyyyMMdd", c, System.Globalization.DateTimeStyles.None, out dEnd);
                }
                if (trueDateEnd)
                {
                    try
                    {
                        result.Results = db.GetClaims(dBeg, dEnd, country, claimIncs);
                        result.Status = "OK";

                    }
                    catch (Exception ex)
                    {
                        result.Status = "Error";
                        result.ErrorMessage = ex.Message;
                    }
                }
                else
                {
                    result.Status = "Error";
                    result.ErrorMessage = "Not valid date in dateBeg";
                }
            }
            else
            {
                result.Status = "Error";
                result.ErrorMessage = "Not valid date in dateBeg";
            }
            return result;
        }
    }
}
