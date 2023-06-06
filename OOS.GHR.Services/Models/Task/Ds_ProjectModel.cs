using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Task
{
    public class Ds_ProjectModel
    {
        public int DuAnID { get; set; }

        public string MaDuAn { get; set; }

        public string TenDuAn { get; set; }

        public int? TrangThaiID { get; set; }

        public int? DuAnChaID { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayBatDau_ThucTe { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public byte? CachTinhTienDo { get; set; }

        public int? NguoiQuanTriDuAnID { get; set; }

        public string MoTa { get; set; }

        public string ImageURL { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyByID { get; set; }

        public DateTime? ModifyDate { get; set; }


        public string TrangThai { get; set; }
    }
}
