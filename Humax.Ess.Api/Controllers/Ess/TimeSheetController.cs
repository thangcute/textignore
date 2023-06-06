using Humax.Ess.Api.Helpers;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Services.Ess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class TimeSheetController : BaseApiController
    {
        [HttpGet]
        [Route("api/TimeSheet/Summary")]
        public async Task<object> GetSummary(string MonthYear = "")
        {
            List<TimeSheetSummaryModel> data = new List<TimeSheetSummaryModel>();

            if (string.IsNullOrEmpty(MonthYear))
                MonthYear = DateTime.Now.ToString("yyyy-MM");
            //MonthYear = "2012-07";
            DateTime timeMonth = DateTime.Parse(MonthYear + "-01");

            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            //nhanvienId = 101;
            var dataSummary = OOS.GHR.BusinessAdapter.ChamCong.ChamCong.GetBangTongHopCongThangWithDetails(timeMonth, 0, "", 0, OOS.GHR.Library.DatabaseCache.NhanVien.NhanVienID, out string ColumnCC);
            if (dataSummary != null && dataSummary.Rows.Count > 0)
            {
                List<string> unShowColumn = new List<string>() { "STT", "Mã", "Họ và tên", "Phòng ban", "NhanVienID", "NvID", "ThuTuPhongBan" };
                System.Data.DataRow[] rowSummary = dataSummary.Select("NvID=" + nhanvienId); //dataSummary.AsEnumerable().Where(p => p.Field<Int32>("NvID") == nhanvienId).ToList();
                if (rowSummary.Count() > 0)
                {
                    for (int i = 0; i < dataSummary.Columns.Count; i++)
                    {
                        string _ColumnName = dataSummary.Columns[i].ColumnName;
                        string _ColumnData = rowSummary[0][_ColumnName].ToString();
                        if (string.IsNullOrEmpty(_ColumnData))
                            _ColumnData = "0";

                        if (!unShowColumn.Contains(_ColumnName))
                        {
                            decimal _valCheck = 0;
                            decimal.TryParse(_ColumnData, out _valCheck);
                            if(_valCheck > 0)
                            {
                                data.Add(new TimeSheetSummaryModel()
                                {
                                    Key = _ColumnName,
                                    Value = _ColumnData
                                });
                            }
                        }
                    }

                    return new
                    {
                        Status = 1,
                        Message = "Summary",
                        Data = data
                    };
                }
            }

            return new
            {
                Status = 0,
                Message = "Empty",
                Data = (object)null
            };
        }

        // GET api/<controller>
        public async Task<object> Get(string MonthYear="")
        {
            if (string.IsNullOrEmpty(MonthYear))
                MonthYear = DateTime.Now.ToString("yyyy-MM");
            var data = await TimeSheetService.get(MonthYear); //"2013-11"
            //var data = OOS.GHR.BusinessAdapter.ChamCong.ChamCong.GetDSTongHopCong_TheoNgay(0, DateTime.Parse("2013-11-01"), DateTime.Parse("2013-11-30"), 0, 656, 0, "", "");
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data : null
            };
        }

        [Route("api/TimeSheet/Mobile")]
        public async Task<object> GetMobile(string MonthYear = "", int EmployeeId = 0)
        {
            if (string.IsNullOrEmpty(MonthYear))
                MonthYear = DateTime.Now.ToString("yyyy-MM");

            //MonthYear = "2013-11";
            var data = await TimeSheetService.getMobile(MonthYear, EmployeeId);

            string _html = "<table style='border-collapse: collapse; border: 1px none #000000;' width='100%' cellspacing='0' cellpadding='0'>";
            _html += "<thead><tr>";
            _html += "<th style='background-color: #dddddda6; color: #666; height: 34px; border: 1px solid #000;' width='100px'>Ngày</th>";
            _html += "<th style='background-color: #dddddda6; color: #666; height: 34px; border: 1px solid #000;'>Thời gian</th>";
            _html += "<th style='background-color: #dddddda6; color: #666; height: 34px; border: 1px solid #000;' width='120px'>#</th>";
            _html += "<tr></thead>";
            _html += "<tbody>";
            if (data != null)
            {
                int _row = 0;
                foreach (var item in (List<TimeSheetModel>)data)
                {
                    if (_row % 2 == 0)
                    {
                        if (_row > 0)
                            _html += "</tr><tr>";
                        else
                            _html += "<tr>";
                    }

                    _html += "<td style='border: 1px solid #000000; height: 32px; text-align: center; vertical-align: middle;' valign='middle'><b>" + item.Thu + "</b><br/>" + item.NgayThang.ToString("dd/MM") + "</td>";

                    string _time = "";
                    if (item.TG_Den != null)
                        _time += "<span style='border: 1px solid #428bca; background-color: #428bca; margin: 3px; padding: 2px 6px; border-radius: 4px; color: #FFF;'>" + item.TG_Den.Value.ToString().Substring(0, 5) + "</span>";
                    if (item.TG_Ra != null)
                        _time += "<span style='border: 1px solid #428bca; background-color: #428bca; margin: 3px; padding: 2px 6px; border-radius: 4px; color: #FFF;'>" + item.TG_Ra.Value.ToString().Substring(0, 5) + "</span>";
                    if (item.TG_Vao != null)
                        _time += "<span style='border: 1px solid #428bca; background-color: #428bca; margin: 3px; padding: 2px 6px; border-radius: 4px; color: #FFF;'>" + item.TG_Vao.Value.ToString().Substring(0, 5) + "</span>";
                    if (item.TG_Ve != null)
                        _time += "<span style='border: 1px solid #428bca; background-color: #428bca; margin: 3px; padding: 2px 6px; border-radius: 4px; color: #FFF;'>" + item.TG_Ve.Value.ToString().Substring(0, 5) + "</span>";

                    _html += "<td style='border: 1px solid #000000; height: 32px; text-align: left; vertical-align: middle;' valign='middle'>" + _time + "</td>";

                    _time = "Ca:" + item.CaLamViec + " - CLV:" + item.TG_LamViec;
                    if (item.DiMuon > 0)
                        _time += "<br/>Đi muộn:" + item.DiMuon + " Phút";
                    if(item.VeSom > 0)
                        _time += "<br/>Về sớm:" + item.VeSom + " Phút";
                    _html += "<td style='border: 1px solid #000000; height: 32px; text-align: left; vertical-align: middle; padding-left: 3px;' valign='middle'>" + _time + "</td>";
                }

                if (_row > 0)
                    _html += "</tr>";
            }
            else
            {
                _html += "<tr><td colspan='3' style='border: 1px solid #000000; height: 9.4pt; text-align: left; vertical-align: middle; padding-left: 3px;' width='100%' valign='middle'>Không có dữ liệu chấm công</td></tr>";
            }
            _html += "</tbody></table>";

            return new
            {
                Status = 1,
                Message = "",
                Data = _html
            };
        }

        /// <summary>
        /// Giai trinh di som ve muon
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("api/TimeSheet/Explain")]
        public async Task<object> PostExplain([FromBody] ExplainModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime _NgayChamCong = DateTime.Parse(model.NgayThang.ToString("yyyy-MM-dd"));
                DateTime _NgayHienTai = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                if (_NgayChamCong >= _NgayHienTai)
                {
                    ModelState.Remove("Date");
                    ModelState.AddModelError("Date", "Ngày Giải trình phải nhỏ hơn ngày hiện tại");
                }

                if (ModelState.IsValid)
                {
                    if (await TimeSheetService.Explain(model))
                    {
                        return new
                        {
                            Status = 1,
                            Message = "Giải trình thành công. Đang chờ phê duyệt !",
                            Data = (object)null
                        };
                    }
                    else
                    {
                        return new
                        {
                            Status = 0,
                            Message = "Giải trình không thành công, hãy thử lại sau !",
                            Data = (object)null
                        };
                    }
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                        Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                    };
                }
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                    Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                };
            }
        }

        [HttpPost]
        [Route("api/TimeSheet/LostFinger")]
        public async Task<object> PostLostFinger([FromBody] LostFingerModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime _NgayChamCong = DateTime.Parse(model.NgayThang.ToString("yyyy-MM-dd"));
                DateTime _NgayHienTai = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                if(_NgayChamCong >= _NgayHienTai)
                {
                    ModelState.Remove("Date");
                    ModelState.AddModelError("Date", "Ngày Giải trình phải nhỏ hơn ngày hiện tại");
                }

                if (ModelState.IsValid)
                {
                    string msg = await TimeSheetService.LostFinger(model);
                    if (string.IsNullOrEmpty(msg))
                    {
                        return new
                        {
                            Status = 1,
                            Message = "Giải trình thành công. Đang chờ phê duyệt !",
                            Data = (object)null
                        };
                    }
                    else
                    {
                        return new
                        {
                            Status = 0,
                            Message = msg,
                            Data = (object)null
                        };
                    }
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                        Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                    };
                }
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                    Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                };
            }
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}