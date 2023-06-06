using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services
{
    public class StoreService
    {
        public static List<T> get<T>(Dictionary<string, object> parameter = null, string procedure = "") where T : class
        {
            List<T> result = new List<T>();
            try
            {
                using (var context = new OosContext())
                {
                    context.Database.CommandTimeout = 300;
                    // Query
                    string sqlQuery = "EXEC " + procedure;

                    // SqlParameter
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    if (parameter != null && parameter.Count() > 0)
                    {
                        int i = 0;
                        foreach (KeyValuePair<string, object> entry in parameter)
                        {
                            string _key = entry.Key;
                            object _value = entry.Value;
                            if (!string.IsNullOrEmpty(_key))
                            {
                                sqlParams.Add(new SqlParameter(_key, _value));
                                if (i == 0)
                                    sqlQuery += " @" + _key;
                                else
                                    sqlQuery += ", @" + _key;
                                i++;
                            }
                        }
                    }
                    result = context.Database.SqlQuery<T>(sqlQuery, sqlParams.ToArray()).ToList();
                }
            }
            catch (Exception ex)
            {
                Commons.WLog(logMsg: string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message.ToString()), fileName: "store.txt");
            }
            return result;
        }

        public static List<T> grid<T>(Dictionary<string, object> parameter, string procedure, int limit, int offset, out int total) where T : class
        {
            total = 0;
            List<T> result = new List<T>();
            try
            {
                using (var context = new OosContext())
                {
                    context.Database.CommandTimeout = 300;
                    // Query
                    string sqlQuery = "EXEC " + procedure + " @limit, @offset, @total out";

                    // SqlParameter
                    List<SqlParameter> sqlParams = new List<SqlParameter>() {
                        new SqlParameter("@limit", limit),
                        new SqlParameter("@offset", offset),
                        new SqlParameter("@total", SqlDbType.Int)
                    };
                    sqlParams[2].Direction = ParameterDirection.Output;

                    if (parameter != null && parameter.Count() > 0)
                    {
                        int i = 3;
                        foreach (KeyValuePair<string, object> entry in parameter)
                        {
                            string _key = entry.Key;
                            object _value = entry.Value;
                            if (!string.IsNullOrEmpty(_key))
                            {
                                sqlParams.Add(new SqlParameter(_key, _value));
                                if (i == 0)
                                    sqlQuery += " @" + _key;
                                else
                                    sqlQuery += ", @" + _key;
                                i++;
                            }
                        }
                    }
                    result = context.Database.SqlQuery<T>(sqlQuery, sqlParams.ToArray()).ToList();
                    total = (int)sqlParams[2].Value;
                }
            }
            catch (Exception ex)
            {
                Commons.WLog(logMsg: string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message.ToString()), fileName: "store.txt");
            }
            return result;
        }

        public static int execute(Dictionary<string, object> parameter, string procedure)
        {
            int result = -1;
            try
            {
                using (var context = new OosContext())
                {
                    context.Database.CommandTimeout = 600;
                    // Query
                    string sqlQuery = "EXEC " + procedure;
                    // SqlParameter
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    if (parameter != null && parameter.Count() > 0)
                    {
                        int i = 0;
                        foreach (KeyValuePair<string, object> entry in parameter)
                        {
                            string _key = entry.Key;
                            object _value = entry.Value;
                            if (!string.IsNullOrEmpty(_key))
                            {
                                sqlParams.Add(new SqlParameter(_key, _value));
                                if (i == 0)
                                    sqlQuery += " @" + _key;
                                else
                                    sqlQuery += ", @" + _key;
                                i++;
                            }
                        }
                    }
                    result = context.Database.ExecuteSqlCommand(sqlQuery, sqlParams.ToArray());
                }
            }
            catch (Exception ex)
            {
                Commons.WLog(logMsg: string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message.ToString()), fileName: "store.txt");
            }
            return result;
        }

        public static DataTable get(Dictionary<string, object> parameter, string procedure)
        {
            DataTable result = new DataTable();
            try
            {
                string sqlQuery = "EXEC " + procedure;
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (parameter != null && parameter.Count() > 0)
                {
                    foreach (KeyValuePair<string, object> entry in parameter)
                    {
                        string _key = entry.Key;
                        object _value = entry.Value;
                        if (!string.IsNullOrEmpty(_key))
                        {
                            parameters.Add(new SqlParameter(_key, _value));
                        }
                    }
                }
                using (var context = new OosContext())
                {
                    context.Database.CommandTimeout = 300;
                    var con = (SqlConnection)context.Database.Connection;
                    using (var cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        con.Open();
                        using (var rdr = cmd.ExecuteReader())
                        {
                            result.Load(rdr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.WLog(logMsg: string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message.ToString()), fileName: "store.txt");
            }
            return result;
        }
    }
}
