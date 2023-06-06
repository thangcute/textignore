using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.EntityFramework.Models.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Social
{
    public class MemberService
    {
        public static async Task<bool> PostMemberAsync(int groupId, int memberId)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.SO_Member.Add(new SO_Member() { 
                    EmployeeId = memberId,
                    GroupId = groupId,
                    IsDeleted = false
                });
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        public static async Task<bool> DeleteMemberAsync(int groupId, int memberId)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                var _data = db.SO_Member.Where(x => x.GroupId == groupId && x.EmployeeId == memberId).FirstOrDefault();
                if (_data != null)
                {
                    _data.IsDeleted = true;
                    rs = await db.SaveChangesAsync() > 0 ? true : false;
                }
            }
            return rs;
        }
    }
}
