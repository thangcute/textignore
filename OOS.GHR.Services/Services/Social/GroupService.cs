using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.EntityFramework.Models.Social;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Social
{
    public class GroupService
    {
        public static async Task<List<SO_Group>> GetGroupByUserAsync(int userId, int limit = 10, int offset = 0)
        {
            List<SO_Group> rs = new List<SO_Group>();
            using (var db = new OosContext())
            {

            }
            return rs;
        }

        public static async Task<List<int>> GetMemberByGroupAsync(int id)
        {
            List<int> rs = new List<int>();
            using (var db = new OosContext())
            {
                var data = db.SO_Member.Where(x => x.GroupId == id && x.IsDeleted == false).Select(x => (int)x.EmployeeId);
                if (data.Any())
                    rs = data.ToList();
            }
            return rs;
        }

        public static async Task<SO_Group> GetGroupAsync(int id)
        {
            SO_Group rs = new SO_Group();
            using (var db = new OosContext())
            {
                rs = await db.SO_Group.FindAsync(id);
            }
            return rs;
        }

        public static async Task<int> PostGroupAsync(SO_Group model)
        {
            int rs = -1;
            using (var db = new OosContext())
            {
                db.SO_Group.Add(model);
                rs = await db.SaveChangesAsync() > 0 ? model.Id : 0;
            }
            return rs;
        }

        public static async Task<bool> PutGroupAsync(int id, SO_Group model)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                var _data = await db.SO_Group.FindAsync(id);
                db.Entry(_data).CurrentValues.SetValues(model);
                db.Entry(_data).State = EntityState.Modified;
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> DeleteIconByGroupAsync(int id, string _mapPath)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                var data = await db.SO_Group.FindAsync(id);
                if(data != null)
                {
                    string _iconPath = data.IconPath;
                    if (!string.IsNullOrEmpty(_iconPath))
                    {
                        List<string> fileSplit = _iconPath.Split('/').ToList();
                        string _fileName = fileSplit[fileSplit.Count() - 1];
                        rs = Commons.DeleteFile(_mapPath, _fileName);
                    }
                }
            }
            return rs;
        }

        public static async Task<bool> DeleteGroupAsync(int id)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                var _data = await db.SO_Group.FindAsync(id);
                if(_data != null)
                {
                    _data.IsDeleted = true;
                    rs = await db.SaveChangesAsync() > 0 ? true : false;
                }
            }
            return rs;
        }
    }
}
