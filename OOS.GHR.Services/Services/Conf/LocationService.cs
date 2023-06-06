using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Services.Models;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Conf
{
    public class LocationService
    {
        public static async Task<object> get(string search, int limit, int page)
        {
            if (limit > 100)
                limit = 100;
            int offset = (page - 1) * limit;
            search = DataConvert.VnConvert(search);
            search = Regex.Replace(search, @"\s+", " ");
            using (var db = new OosContext())
            {
                var data = db.NS_DsXaPhuong.Where(x => x.Keyword.Contains(search)).Select(x => new DefaultModel
                {
                    Id = x.XaPhuongID,
                    Name = x.XaHuyenTinh,
                    Des = x.MaXaPhuong
                });

                if (data != null && data.Any())
                    return data.ToList();
            }
            return null;
        }

        public static async Task<object> getProvince()
        {
            using (var db = new OosContext())
            {
                var data = db.NS_DsTinh.Select(x => new DefaultModel {
                    Id = x.TinhID,
                    Name = x.TenTinh,
                    Des = x.MaVung
                });

                if (data != null && data.Any())
                    return data.ToList();
            }
            return null;
        }

        public static async Task<object> getDistrict(int id = 0)
        {
            using (var db = new OosContext())
            {
                var data = db.NS_DsQuanHuyen.Where(x => x.TinhID == id).Select(x => new DefaultModel
                {
                    Id = x.QuanHuyenID,
                    Name = x.TenQuanHuyen,
                    Des = x.MaHuyen
                });

                if (data != null && data.Any())
                    return data.ToList();
            }
            return null;
        }

        public static async Task<object> getWard(int id = 0)
        {
            using (var db = new OosContext())
            {
                var data = db.NS_DsXaPhuong.Where(x => x.QuanHuyenID == id).Select(x => new DefaultModel
                {
                    Id = x.XaPhuongID,
                    Name = x.TenXaPhuong,
                    Des = x.MaXaPhuong
                });

                if (data != null && data.Any())
                    return data.ToList();
            }
            return null;
        }
    }
}
