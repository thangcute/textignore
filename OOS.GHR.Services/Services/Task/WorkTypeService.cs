using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Task
{
    public class WorkTypeService
    {
        public static async Task<(List<TSK_CongViec_LoaiCongViec> rs, int total)> get(string _conditions = "", int _limit = 15, int _offset = 0, bool _all = false)
        {
            int _total = 0;
            List<TSK_CongViec_LoaiCongViec> _rs = new List<TSK_CongViec_LoaiCongViec>();

            try
            {
                using (var db = new OosContext())
                {
                    string sql = "SELECT * FROM [dbo].[TSK_CongViec_LoaiCongViec]";
                    if (!string.IsNullOrEmpty(_conditions))
                        sql = string.Format("{0} WHERE {1}", sql, _conditions);
                    var _data = db.TSK_CongViec_LoaiCongViec.SqlQuery(sql);
                    //var _object = db.Database.SqlQuery<TSK_DuAn_TrangThai>(sql);
                    if (_data.Any())
                    {
                        _total = _data.Count();
                        if (_all)
                            _rs = _data.OrderBy(x => x.LoaiCongViecID).ToList();
                        else
                            _rs = _data.OrderBy(x => x.LoaiCongViecID).Skip(_offset).Take(_limit).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return (_rs, _total);
        }

        public static async Task<TSK_CongViec_LoaiCongViec> detail(int id = 0)
        {
            TSK_CongViec_LoaiCongViec rs = new TSK_CongViec_LoaiCongViec();
            using (var db = new OosContext())
            {
                rs = await db.TSK_CongViec_LoaiCongViec.FindAsync(id);
            }
            return rs;
        }

        public static async Task<bool> post(TSK_CongViec_LoaiCongViec model)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViec_LoaiCongViec.Add(model);
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> put(int id = 0, TSK_CongViec_LoaiCongViec model = null)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_CongViec_LoaiCongViec
                    .Where(x => x.LoaiCongViecID == id)
                    .ToList().ForEach(x => {
                        x.MaLoaiCongViec = model.MaLoaiCongViec;
                        x.TenLoaiCongViec = model.TenLoaiCongViec;
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
                db.TSK_CongViec_LoaiCongViec.RemoveRange(db.TSK_CongViec_LoaiCongViec.Where(x => x.LoaiCongViecID == id));
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }
    }
}
