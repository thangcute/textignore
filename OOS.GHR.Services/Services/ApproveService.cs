using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services
{
    public class ApproveService
    {
        private readonly OosContext con = null;
        public ApproveService()
        {
            con = new OosContext();
        }
        public static async Task<bool> AvailableNew(string code = "")
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(code))
            {
                SqlConnection con = new SqlConnection();
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ConnectionString;
                con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
                //var con = new OosContext();
                con.Open();
                try
                    {
                    
                        SqlCommand cm = new SqlCommand("SELECT Count(*) FROM WF_LoaiQuyTrinh where MaLoaiQuyTrinh = '" + code + "' AND IsUse = 1", con);

                        //sử dụng phương thức executeReader để lấy dữ liệu từ SQL
                        int result = Convert.ToInt32(cm.ExecuteScalar());
                        if(result > 0)
                        {
                        con.Close();
                        rs = true;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
               con.Close(); 
            }
            
            return rs;
        }

        public static async Task<NS_DsXaPhuong> select_NS_DsXaPhuong(int? XaPhuongID)
        {
            if(XaPhuongID ==null) { return null; };
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ConnectionString;
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            con.Open();
            try
            {
                SqlCommand cm = new SqlCommand("select * from NS_DsXaPhuong where XaPhuongID = " + XaPhuongID, con);
                SqlDataReader dr = cm.ExecuteReader();
                NS_DsXaPhuong xaPhuong = new NS_DsXaPhuong();
                while (dr.Read())
                {
                    
                    xaPhuong.XaPhuongID = (int)dr["XaPhuongID"];
                    xaPhuong.QuanHuyenID = (int?)dr["QuanHuyenID"];
                    xaPhuong.TenXaPhuong = (string)dr["TenXaPhuong"];
                    xaPhuong.MaXaPhuong = (string)dr["MaXaPhuong"];
                    xaPhuong.CreatedByID = (int?)dr["CreatedByID"];
                    xaPhuong.CreatedDate = (DateTime?)dr["CreatedDate"];
                    xaPhuong.ModifyByID = (int?)dr["ModifyByID"];
                    xaPhuong.ModifyDate = (DateTime?)dr["ModifyDate"];
                    xaPhuong.Keyword = (string)dr["Keyword"];
                    xaPhuong.TinhID = (int?)dr["TinhID"];
                    xaPhuong.XaHuyenTinh = (string)dr["XaHuyenTinh"];
                }
                con.Close(); 
                return xaPhuong;
            }
            catch 
            {
                con.Close();
                return null;
            }
        }

        public static async Task<bool> Available(string code = "")
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    using (var db = new OosContext())
                    {
                        var xdObj = db.WF_LoaiQuyTrinh.Where(x => x.MaLoaiQuyTrinh == code && x.IsUse == true);
                        if (xdObj.Any())
                            rs = true;
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return rs;
        }
    }
}
