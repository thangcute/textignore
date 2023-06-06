using OOS.GHR.BusinessAdapter.HeThong;
using OOS.GHR.Library;
using OOS.GHR.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class NotificationController : BaseApiController
    {
        // GET api/<controller>
        public async Task<object> Get(string type="")
        {
            if(!string.IsNullOrEmpty(type) && type.ToLower() == "notification")
            {
                int nguoidungId = OOS.GHR.Library.DatabaseCache.DangNhap.NguoiDungID;
                List<ActivityItem> warning = OOS.GHR.Library.ActivityManagement.GetActivity(nguoidungId, 0, 0);

                return new
                {
                    Status = 1,
                    Message = "",
                    Data = warning.Select(x => new {
                        LoaiCanhBaoID = x.ActivityID,
                        TenLoaiCanhBao = x.NoiDung,
                        Link = x.Link,
                        SoLuong = warning.Count
                    })
                };
            }
            else
            {
                List<NotifyInfo> notifyInfo = OOS.GHR.BusinessAdapter.HeThong.CanhBaoHeThong.LoadAllCanhBao();
                return new
                {
                    Status = 1,
                    Message = "",
                    Data = notifyInfo.Select(x => new
                    {
                        LoaiCanhBaoID = x.ID,
                        TenLoaiCanhBao = x.Name,
                        SoLuong = x.ResultCount
                    })
                };
            }
        }

        [Route("api/Notification/ActivityList")]
        [HttpGet]
        public async Task<object> ActivityList()
        {
            int nguoidungId = OOS.GHR.Library.DatabaseCache.DangNhap.NguoiDungID;
            List<ActivityItem> warning = OOS.GHR.Library.ActivityManagement.GetActivity(nguoidungId, 0, 0);
            return new
            {
                Status = 1,
                Message = "",
                Data = warning.Select(x => new {
                    LoaiCanhBaoID = x.ActivityID,
                    TenLoaiCanhBao = x.NoiDung,
                    Link = x.Link,
                    SoLuong = warning.Count
                })
            };
        }

        [Route("api/Notification/GetActivity")]
        [HttpGet]
        public async Task<object> GetActivity(int id)
        {
            List<object> rs = new List<object>();

            SYS_Activity ac = SYS_Activity.GetSYS_Activity(id);
            if (ac.ActivityID>0)
            {
                var item = new
                {
                    CanhBaoID = ac.ActivityID,
                    TieuDe = ac.TieuDe,
                    DienGiai = ac.NoiDung
                };
                rs.Add(item);
            }
            return new
            {
                Status = 1,
                Data = rs
            };
        }

        // GET api/<controller>/5
        public async Task<object> Get(int id)
        {
            if(id > 0)
            {
                SortedList slParams = new SortedList();
                slParams.Add("@NguoiDungID", DatabaseCache.DangNhap.NguoiDungID);
                slParams.Add("@NhanVienID", DatabaseCache.DangNhap.NhanVienID);
                object strCommand = DatabaseBase.ExecScalar("SELECT TOP 1 SQLCommand FROM BC_BaoCaoSQL WHERE BaoCaoID=" + id);

                DataTable data = new DataTable();
                if (strCommand != null && strCommand != DBNull.Value)
                    data = Database.ExecTable(strCommand.ToString(), slParams);

                List<object> rs = new List<object>();
                if (data != null && data.Rows.Count > 0)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        int _CanhBaoID = 0;
                        string _DienGiai = "";
                        foreach (DataColumn col in data.Columns)
                        {
                            string colName = col.ColumnName;
                            string colValue = row[colName].ToString();

                            if (colName.ToUpper() != "ID")
                            {
                                if (string.IsNullOrEmpty(_DienGiai))
                                    _DienGiai = string.Format("<b>{0}</b>: {1}", colName, colValue);
                                else
                                    _DienGiai += string.Format("<br/><b>{0}</b>: {1}", colName, colValue);
                            }
                            else
                                int.TryParse(row[colName].ToString(), out _CanhBaoID);
                        }

                        if (_CanhBaoID > 0)
                        {
                            var item = new
                            {
                                CanhBaoID = _CanhBaoID,
                                TieuDe = "",
                                DienGiai = _DienGiai
                            };
                            rs.Add(item);
                        }
                    }
                }

                return new
                {
                    Status = 1,
                    Data = rs
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "CanhBaoId không tìm thấy",
                    Data = (object)null
                };
            }
        }
    }
}