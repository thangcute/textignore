using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Models
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Total { get; set; }

        /// <summary>
        /// Convert Datatable to JSON Object
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string AsDynamicEnumerable(DataTable table)
        {
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
        }

        /// <summary>
        /// Convert FirstRow (Rows[0]) to JSON Object
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string ConvertFirstRowToObject(DataTable table)
        {
            if (table == null || table.Rows.Count <= 0)
                return "";

            Dictionary<string, object> row = new Dictionary<string, object>();
            DataRow dr = table.Rows[0];

            foreach (DataColumn col in table.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(row, Formatting.None);
        }
    }
}