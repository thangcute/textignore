using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Humax.Ess.Api.Controllers.Old
{
    public class CustomAPIController : Controller
    {
        public string AsDynamicEnumerable(DataTable table)
        {
            //List<dynamic> dyns = new List<dynamic>();

            //foreach (DataRow row in table.Rows)
            //{
            //    IDictionary<string, object> dRow = new ExpandoObject();

            //    foreach (DataColumn column in table.Columns)
            //    {
            //        var value = row[column.ColumnName];
            //        dRow[column.ColumnName] = Convert.IsDBNull(value) ? null : value;
            //    }

            //    dyns.Add(dRow);
            //}

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);

            //return dyns;
        }

        public ActionResult GetCustomAPI(string apicode)
        {
            string HostName = Request.UserHostName;
            string tokenkey = Request.Headers["Token"];

            if (OOS.GHR.Security.checkForSQLInjection(tokenkey) || string.IsNullOrEmpty(tokenkey))
                return Json(new { Status = 0 }, JsonRequestBehavior.AllowGet);

            if (OOS.GHR.Security.checkForSQLInjection(apicode) || string.IsNullOrEmpty(apicode))
                return Json(new { Status = 0 }, JsonRequestBehavior.AllowGet);

            int apiID = Database.ToInt(Database.ExecScalar(
            string.Format("SELECT TOP 1 SYS_API.APIID FROM SYS_API INNER JOIN SYS_API_Domain ON SYS_API_Domain.APIID = SYS_API.APIID WHERE Tokenkey='{0}' AND Enable=1",
            tokenkey, HostName)));

            if (apiID > 0)
            {
                string strStoreView = Database.toString(Database.ExecScalar(
                string.Format("SELECT StoreView FROM SYS_API_Authority WHERE ApiID={0} AND CodeName='{1}' AND Enable=1 AND (DueDate>=GetDate() OR DueDate IS NULL)",
                apiID, apicode)));

                if (strStoreView != "")
                {
                    System.Collections.SortedList slParams = new System.Collections.SortedList();
                    foreach (string strP in Request.Form.AllKeys)
                    {
                        if (OOS.GHR.Security.checkForSQLInjection(strP))
                        {
                            return Json(new { Status = 0 }, JsonRequestBehavior.AllowGet);
                        }

                        if (strP != "tokenkey" && strP != "apicode")
                        {
                            if (!slParams.Contains(strP))
                                slParams.Add(strP, Request[strP]);
                        }
                    }

                    DataTable dtReturn = Database.ExecTable(strStoreView, slParams, true);
                    if (dtReturn == null)
                    {
                        return Json(new { Status = 0 }, JsonRequestBehavior.AllowGet);
                    }

                    SYS_APILog log = new SYS_APILog();
                    log.APIID = apiID;
                    log.IPAddress = HostName + " - " + apicode;
                    log.ThoiGian = DateTime.Now;
                    log.Do_Insert();

                    return Json(new
                    {
                        Status = 1,
                        Data = AsDynamicEnumerable(dtReturn)
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Status = 0, Message = "Does not allow domain: " + HostName }, JsonRequestBehavior.AllowGet);
        }
    }
}