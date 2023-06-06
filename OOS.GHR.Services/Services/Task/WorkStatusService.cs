using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Task
{
    public class WorkStatusService
    {
        public static async Task<(List<TSK_CongViec_TrangThai> rs, int total)> get(string _conditions = "", int _limit = 15, int _offset = 0, bool _all = false)
        {
            int _total = 0;
            List<TSK_CongViec_TrangThai> _rs = new List<TSK_CongViec_TrangThai>();

            try
            {
                using (var db = new OosContext())
                {
                    string sql = "SELECT * FROM [dbo].[TSK_CongViec_TrangThai]";
                    if (!string.IsNullOrEmpty(_conditions))
                        sql = string.Format("{0} WHERE {1}", sql, _conditions);
                    var _data = db.TSK_CongViec_TrangThai.SqlQuery(sql);
                    //var _object = db.Database.SqlQuery<TSK_DuAn_TrangThai>(sql);
                    if (_data.Any())
                    {
                        _total = _data.Count();
                        if (_all)
                            _rs = _data.OrderBy(x => x.TrangThaiID).ToList();
                        else
                            _rs = _data.OrderBy(x => x.TrangThaiID).Skip(_offset).Take(_limit).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return (_rs, _total);
        }

        public static async Task<TSK_CongViec_TrangThai> detail(int id = 0)
        {
            TSK_CongViec_TrangThai rs = new TSK_CongViec_TrangThai();
            using (var db = new OosContext())
            {
                rs = await db.TSK_CongViec_TrangThai.FindAsync(id);
            }
            return rs;
        }

        public static async Task<bool> post(TSK_CongViec_TrangThai model)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViec_TrangThai.Add(model);
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> put(int id = 0, TSK_CongViec_TrangThai model = null)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViec_TrangThai
                    .Where(x => x.TrangThaiID == id)
                    .ToList().ForEach(x => {
                        x.MaTrangThai = model.MaTrangThai;
                        x.TenTrangThai = model.TenTrangThai;
                        x.STT = model.STT;
                        x.Status = model.Status;
                        x.MoTa = model.MoTa;
                        x.Color = model.Color;
                        x.ModifyByID = model.ModifyByID;
                        x.ModifyDate = model.ModifyDate;
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
                db.TSK_CongViec_TrangThai.RemoveRange(db.TSK_CongViec_TrangThai.Where(x => x.TrangThaiID == id));
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> updateStatus(int id = 0, Int16 statusId = 0, int userId = 0)
        {
            bool rs = false;
            DateTime actionTime = DateTime.Now;
            using (var db = new OosContext())
            {
                db.TSK_CongViec_TrangThai
                    .Where(x => x.TrangThaiID == id)
                    .ToList().ForEach(x =>
                    {
                        x.Status = (byte)statusId;
                        x.ModifyByID = userId;
                        x.ModifyDate = actionTime;
                    });
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }
    }
}
