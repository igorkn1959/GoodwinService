using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace GoodwinService3.Models
{
    public class CancelClaim
    {
        public int Reason { get; set; }
        public string result { get; set; }
        public string Error { get; set; }
        public List<string> Warnings { get; set; }
        private kompasEntities db;
        private List<LockedResource> lr;
        private SamoDB dataset;

        public CancelClaim()
        {
            lr = new List<LockedResource>();
            Warnings = new List<string>();

        }

        public void CancelWithoutEntity(int claimInc)
        {
            using (dataset = new SamoDB())
            {
                
            }
        }

        public void Cancel(int claiminc)
        {
            //lr = new List<LockedResource>();
            //Warnings = new List<string>();
            using (db = new kompasEntities())
            {
                string cs = ConfigurationManager.ConnectionStrings["kompasTestConnectionString"].ConnectionString;

                db.Database.Connection.ConnectionString = cs;
                db.Database.Connection.Open();
                DateTime st = GetServerDateTime();
                int usercode = 0;
                try
                {
                    usercode = (int)db.users.Where(u => u.alias == Properties.Settings.Default.CancelClaimUser).Select(u => u.code).First();
                    var claimCurrent = db.claim.FirstOrDefault(cl => cl.inc == claiminc);
                    if (claimCurrent == null)
	                {
                        result = "Error";
                        Error = string.Format("Заявка {0} не найдена", claiminc);
                        return;
	                }
                    if (claimCurrent.status == 3)
                    {
                        result = "Error";
                        Error = string.Format("Заявка {0} уже отменена", claiminc);
                        return;
                        //throw new Exception(string.Format("Заявка {0} уже отменена", claiminc));
                    }
                    LockRecord(claiminc, "claim", usercode);

                    //var ord = db.order.Where(o => o.claim == claiminc && o.reserve != null);
                    //foreach (var rord in ord.Where(or => or.hotel > 0 || or.freight > 0).ToList())
                    //{
                    //    var reserve = rord.reserve1;
                    //    string resPartner = reserve.partner3.name;
                    //    short rescount = (short)reserve.rcount;
                    //    reserve.rcount += rord.rcount;
                    //    //rord.reserve = null;
                    //    if (reserve.rcount == 0)
                    //    {
                    //        db.reserve.Remove(reserve);
                    //    }
                    //    db.SaveChanges();
                    //    string logParams = "";
                    //    Int16 logCode = 0;
                    //    if (rord.hotel > 0)
                    //    {
                    //        logCode = 247;
                    //        logParams = "_|" + rord.hotel1.name + "|" + ((DateTime)reserve.datebeg).ToString("M.d.yyyy") + "|" + ((DateTime)reserve.dateend).ToString("M.d.yyyy") + "|" + rord.room1.name + "|" + rord.rcount.ToString() + "|" + claiminc.ToString();
                    //    }
                    //    else
                    //    {
                    //        if (rord.freight > 0)
                    //        {
                    //            logCode = 259;
                    //            logParams = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", rord.freight1.name, ((DateTime)reserve.datebeg).ToString("M.d.yyyy"), rord.class1.name, rord.rcount.ToString(), resPartner, claiminc.ToString());
                    //        }
                    //    }
                    //    WriteLog(st, 57, logCode, logParams);
                    //}
                    //var opeopleWithDoc = claim.opeople.Where(op => op.distributed);
                    
                    //if (opeopleWithDoc.Count() > 0)
                    //{
                    //    foreach (var op in opeopleWithDoc)
                    //    {
                    //        op.distributed = false;
                    //        op.docum = "";
                    //        op.ddate = null;
                    //    }
                    //}
                    //db.SaveChanges();
                    //List<FrClosedName> fclosed = GetFClosedNames(claiminc);
                    StringBuilder claimNotes = new StringBuilder();
                    var pdetails = db.pdetail.Where(pd => pd.claim == claiminc).ToList();
                    foreach (var pd in pdetails)
                    {
                        var paimnt = pd.payment1;
                        LockRecord(paimnt.inc, "payment", usercode);
                        paimnt.fullassign = false;
                        paimnt.pay -= pd.realpay;
                        paimnt.rest += pd.realpay;
                        string pnote = string.Format("Отмена заявки № {0}, сумма оплаты {1} {2} ({3} {4}, курс {5}).", claiminc, pd.realpay, pd.currency1.alias, pd.pay, paimnt.currency1.alias, pd.rate);
                        string claimNote = string.Format("Оплата {0} {1} ({2}, курс {3};).", pd.realpay, pd.adate, pd.rate);
                        claimNotes.AppendLine(claimNote);
                        paimnt.insidenote += pnote;
                    }
                    CancelClaimDocument(claiminc, usercode);
                    claimCurrent.status = 3;
                    claimCurrent.sent = false;
                    claimCurrent.partpayment = false;
                    claimCurrent.edate = st;
                    claimCurrent.adate = null;
                    claimCurrent.privatecomment += claimNotes.ToString(); //TODO инфо об отмененных платежах
                    claimCurrent.reason = Reason;
                    claimCurrent.canceldate = st;
                    //db.SaveChanges();
                    //-- Отмена бонусов по заявке



                    result = Warnings.Count == 0 ? "OK" : "OKWithWarnings";
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
                finally
                {

                    foreach (var l in lr)
                    {
                        UnlockRecord(l.Record, l.Table, usercode);
                    }
                    
                    db.Database.Connection.Close();
                }
            }
            
        }

        private void CancelClaimDocument(int claiminc, int usercode)
        {
            SqlParameter claimInc = new SqlParameter("@Claim", claiminc);
            SqlParameter userCode = new SqlParameter("@user", usercode);
            db.Database.ExecuteSqlCommand("up_CancelClaimDocuments @Claim, @user", claimInc, userCode);

        }


        private List<FrClosedName> GetFClosedNames(int claiminc)
        {
            List<FrClosedName> result = new List<FrClosedName>();
            SqlParameter claimInc = new SqlParameter("@Claim", claiminc);
            var r = db.Database.SqlQuery<FrClosedName>("up_GetFClosedNames @Claim", claimInc).ToList();
            foreach (var item in r)
            {
                result.Add(item);
            }
            return result;
        }

        private void UnlockRecord(int inc, string tablename, int usercode)
        {
            SqlParameter user = new SqlParameter("@US_CODE", usercode);
            SqlParameter table = new SqlParameter("@TB_NAME", tablename);
            SqlParameter tablecode = new SqlParameter("@TB_UCODE", 1);
            SqlParameter record = new SqlParameter("@REC_CODE", inc);
            db.Database.ExecuteSqlCommand("up_unlock @US_CODE, @TB_NAME, @TB_UCODE, @REC_CODE", user, table, tablecode, record);
        }

        private void LockRecord(int inc, string tablename, int usercode)
        {
            SqlParameter user = new SqlParameter("@US_CODE", usercode);
            SqlParameter table = new SqlParameter("@TB_NAME", tablename);
            SqlParameter tablecode = new SqlParameter("@TB_UCODE", 1);
            SqlParameter record = new SqlParameter("@REC_CODE", inc);
            var lockresult = db.Database.SqlQuery<LockResult>("up_rlock @US_CODE, @TB_NAME, @TB_UCODE, @REC_CODE", user, table, tablecode, record).First();
            if (lockresult.result == 0)
            {
                lr.Add(new LockedResource() { Record = inc, Table = tablename });
                return;
            }
            if (lockresult.result == -1)
            {
                throw new Exception(string.Format("Не удалось заблокировать таблицу {0}", tablename));
            }
            if (lockresult.result == 1)
	        {
                throw new Exception(string.Format("Запись {0} таблицы {1}  заблокирована пользователем {2}", inc, tablename, lockresult.user));
	        }
        }

        private void WriteLog(DateTime logtime, Int16 user, Int16 code, string parms)
        {
            SqlParameter p_logdatetime = new SqlParameter("@p_logdatetime", System.Data.SqlDbType.DateTime);
            p_logdatetime.Value = logtime;
            SqlParameter p_user = new SqlParameter("@p_user", System.Data.SqlDbType.SmallInt);
            p_user.Value = user;
            SqlParameter p_code = new SqlParameter("@p_code", System.Data.SqlDbType.SmallInt);
            p_code.Value = code;
            SqlParameter p_params = new SqlParameter("@p_params", System.Data.SqlDbType.VarChar);
            p_params.Size = 255;
            p_params.Value = parms;
            //SqlParameter p_cleardatetime = new SqlParameter("@p_cleardatetime", System.Data.SqlDbType.DateTime);
            //p_cleardatetime = null;
            //p_cleardatetime.DbType = System.Data.DbType.DateTime;
            db.Database.ExecuteSqlCommand("up_writelog @p_logdatetime, @p_user, @p_code, @p_params", p_logdatetime, p_user, p_code, p_params);
            //, @p_cleardatetime
            //, p_cleardatetime
        }

        private DateTime GetServerDateTime()
        {
            return db.Database.SqlQuery<DateTime>("SELECT GETDATE()").First(); 
        }
    }

    internal class LockResult
    {
        public int result { get; set; }
        public int user { get; set; }
        public DateTime? locktime { get; set; }
    }

    internal class LockedResource
    {
        public int Record { get; set; }
        public string Table { get; set; }
    }
    
}