using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Task
{
    public class WorkRepeatService
    {
        public static async Task<(List<TSK_CongViecLap> rs, int total)> get(string _conditions = "", int _limit = 15, int _offset = 0, bool _all = false)
        {
            int _total = 0;
            List<TSK_CongViecLap> _rs = new List<TSK_CongViecLap>();

            try
            {
                using (var db = new OosContext())
                {
                    string sql = "SELECT * FROM [dbo].[TSK_CongViecLap]";
                    if (!string.IsNullOrEmpty(_conditions))
                        sql = string.Format("{0} WHERE {1}", sql, _conditions);
                    var _data = db.TSK_CongViecLap.SqlQuery(sql);
                    if (_data.Any())
                    {
                        _total = _data.Count();
                        if (_all)
                            _rs = _data.OrderBy(x => x.CongViecID).ToList();
                        else
                            _rs = _data.OrderBy(x => x.CongViecID).Skip(_offset).Take(_limit).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return (_rs, _total);
        }

        public static async Task<TSK_CongViecLap> detail(int id = 0)
        {
            TSK_CongViecLap rs = new TSK_CongViecLap();
            try
            {
                using (var db = new OosContext())
                {
                    rs = db.TSK_CongViecLap.Find(id);
                }
            }
            catch (Exception ex)
            {

            }
            return rs;
        }

        public static async Task<bool> post(TSK_CongViecLap model)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViecLap.Add(model);
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> put(int id = 0, TSK_CongViecLap model = null)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViecLap
                    .Where(x => x.CongViecLapID == id)
                    .ToList().ForEach(x => {
                        x.CongViecID = model.CongViecID;
                        x.MoTa = model.MoTa;
                        x.ThoiGianBatDau = model.ThoiGianBatDau;
                        x.ThoiGIanKetThuc = model.ThoiGIanKetThuc;
                        x.HinhThucLap = model.HinhThucLap;
                        x.TanSuatLap = model.TanSuatLap;
                        x.ThuTrongTuan = model.ThuTrongTuan;
                        x.ThoiGianTaoCongViec = model.ThoiGianTaoCongViec;
                        x.NgayLap = model.NgayLap;
                        x.TuanLap = model.TuanLap;
                        x.ThangLap = model.ThangLap;
                        x.IsKhongKetThuc = model.IsKhongKetThuc;
                        x.KetThucToiNgay = model.KetThucToiNgay;
                        x.KetThucSoLanLap = model.KetThucSoLanLap;
                        x.ThoiGianTaoTruoc = model.ThoiGianTaoTruoc;
                    });
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> delete(int id = 0)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViecLap.RemoveRange(db.TSK_CongViecLap.Where(x => x.CongViecLapID == id));
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> changeActive(int id = 0)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                var _entry = db.TSK_CongViecLap.Find(id);
                if(_entry != null)
                {
                    if (_entry.IsActive.GetValueOrDefault(false))
                        _entry.IsActive = true;
                    else
                        _entry.IsActive = false;

                    db.TSK_CongViecLap.Attach(_entry);
                    db.Entry(_entry).State = EntityState.Modified;
                    rs = await db.SaveChangesAsync() > 0 ? true : false;
                }
            }
            return rs;
        }
    }
}
