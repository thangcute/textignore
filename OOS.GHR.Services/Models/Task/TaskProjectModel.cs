using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOS.GHR.Services.Models.Task
{
    public class TaskProjectModel : TSK_DuAn
    {
        //public override int DuAnID { get; set; }    //{ get => base.DuAnID; set => base.DuAnID = value; }
        // ngay thang view
        public string V_NgayBatDau { get; set; }
        public string V_NgayKetThuc { get; set; }

        public int NgayQuaHan { get; set; }

        public string TenTrangThai { get; set; }
        public string TenNguoiQuanTri { get; set; }
        public string HoNguoiQuanTri { get; set; }

        public List<DependentModel> Followers { get; set; }

        public List<DependentModel> Executors { get; set; }
    }
}