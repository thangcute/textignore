using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Utilitys
{
    public class DataConvert
    {
        private static readonly string[] vnReplace = new string[] { "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };

        public static string NumberConvert(int input, string format = "{0:#,0.00}")
        {
            return string.Format(format, input); // 123,456.00
        }

        public static decimal StringToDecimal(string input = "")
        {
            decimal rs = 0;
            try
            {
                if(!string.IsNullOrEmpty(input)){
                    decimal.TryParse(input, out rs);
                }
            }catch(Exception ex)
            {

            }
            return rs;
        }

        public static string NumberToText(string total)
        {
            try
            {
                string rs = "";
                //total = Math.Round(total, 2);
                string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
                string[] u = { "", "mươi", "trăm", "ngàn", "", "", "triệu", "", "", "tỷ", "", "", "ngàn", "", "", "triệu" };
                string nstr = total.ToString();

                int[] n = new int[nstr.Length];
                int len = n.Length;
                for (int i = 0; i < len; i++)
                {
                    n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
                }

                for (int i = len - 1; i >= 0; i--)
                {
                    if (i % 3 == 2)// số 0 ở hàng trăm
                    {
                        if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) continue;//nếu cả 3 số là 0 thì bỏ qua không đọc
                    }
                    else if (i % 3 == 1) // số ở hàng chục
                    {
                        if (n[i] == 0)
                        {
                            if (n[i - 1] == 0) { continue; }// nếu hàng chục và hàng đơn vị đều là 0 thì bỏ qua.
                            else
                            {
                                rs += " " + rch[n[i]]; continue;// hàng chục là 0 thì bỏ qua, đọc số hàng đơn vị
                            }
                        }
                        if (n[i] == 1)//nếu số hàng chục là 1 thì đọc là mười
                        {
                            rs += " mười"; continue;
                        }
                    }
                    else if (i != len - 1)// số ở hàng đơn vị (không phải là số đầu tiên)
                    {
                        if (n[i] == 0)// số hàng đơn vị là 0 thì chỉ đọc đơn vị
                        {
                            if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 1)// nếu là 1 thì tùy vào số hàng chục mà đọc: 0,1: một / còn lại: mốt
                        {
                            rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 5) // cách đọc số 5
                        {
                            if (n[i + 1] != 0) //nếu số hàng chục khác 0 thì đọc số 5 là lăm
                            {
                                rs += " " + rch[n[i]];// đọc số 
                                rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                                continue;
                            }
                        }
                    }

                    rs += (rs == "" ? "" : " ") + ch[n[i]];// đọc số
                    rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                }
                return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
            }
            catch
            {
                return "";
            }
        }

        public static string NumberToTextConvert(decimal total)
        {
            string result = "không đồng";
            string[] len = total.ToString().Split(',');
            if (len.Count() <= 1)
                len = total.ToString().Split('.');

            if (len.Count() == 2)
            {
                int isLast = 0;
                int.TryParse(len[1].ToString(), out isLast);
                if (isLast > 0)
                {
                    result = NumberToText(len[0]) + " phẩy " + NumberToText(len[1]) + " đồng.";
                }
                else
                {
                    result = NumberToText(len[0]) + " đồng chẵn.";
                }
            }
            else if (len.Count() == 1)
            {
                result = NumberToText(len[0]) + " đồng chẵn.";
            }

            if (result.Length > 1)
            {
                string rs1 = result.Substring(0, 1);
                rs1 = rs1.ToUpper();
                result = result.Substring(1);
                result = rs1 + result;
            }

            return result;
        }

        public static string VnConvert(string str)
        {
            for (int i = 1; i < vnReplace.Length; i++)
            {
                for (int j = 0; j < vnReplace[i].Length; j++)
                    str = str.Replace(vnReplace[i][j], vnReplace[0][i - 1]);
            }
            return str;
        }

        public static string ToUrlConvert(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = Regex.Replace(str, @"\s+", " ");
                str = VnConvert(str).Replace(" ", "-").ToLower();
                str = Regex.Replace(str, @"[^a-z0-9-]", "");
            }
            return str;
        }

        public static DateTime DateConvert(string input, out bool isValid)
        {
            isValid = false;
            DateTime output = DateTime.MinValue;
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    var tem = input.Split('-');
                    if (tem.Count() == 3)
                    {
                        if (tem[0].Length == 2 && int.Parse(tem[0]) <= 31 && int.Parse(tem[0]) > 0
                            && tem[1].Length == 2 && int.Parse(tem[1]) <= 12 && int.Parse(tem[1]) > 0
                            && tem[2].Length == 4 && int.Parse(tem[2]) > 0)
                        {
                            output = DateTime.Parse(string.Format("{0}-{1}-{2}", tem[2], tem[1], tem[0]));
                            isValid = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return output;
        }

        public static string DayOfWeek(DateTime dateTime, int type = 1)
        {
            string rs = "";
            if (!string.IsNullOrEmpty(dateTime.ToString()))
            {
                switch (dateTime.DayOfWeek)
                {
                    case System.DayOfWeek.Monday:
                        rs = type > 0 ? "Mon" : "T2";
                        break;
                    case System.DayOfWeek.Tuesday:
                        rs = type > 0 ? "Tue" : "T3";
                        break;
                    case System.DayOfWeek.Wednesday:
                        rs = type > 0 ? "Wed" : "T4";
                        break;
                    case System.DayOfWeek.Thursday:
                        rs = type > 0 ? "Thu" : "T5";
                        break;
                    case System.DayOfWeek.Friday:
                        rs = type > 0 ? "Fri" : "T6";
                        break;
                    case System.DayOfWeek.Saturday:
                        rs = type > 0 ? "Sat" : "T7";
                        break;
                    case System.DayOfWeek.Sunday:
                        rs = type > 0 ? "Sun" : "CN";
                        break;
                    default:
                        break;
                }
            }
            return rs;
        }

        public static List<T> ListConvert<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row => {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }

        public static T Cast<T>(object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}
