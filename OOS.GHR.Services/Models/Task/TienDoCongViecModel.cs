using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Task
{
    public class TienDoCongViecModel
    {
        public int CongViecID { get; set; }

        public int? TrangThaiID { get; set; }

        public int? LoaiCongViecID { get; set; }

        public int? CongViecChaID { get; set; }

        public int? NguoiThucHienID { get; set; }

        public int? NguoiGiaoViecID { get; set; }

        public string TenCongViec { get; set; }

        public string MoTaCongViec { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public DateTime? NgayBatDau_ThucTe { get; set; }

        public DateTime? NgayKetThuc_ThucTe { get; set; }

        public byte? Status { get; set; }

        public int? DuAnID { get; set; }

        public int? MucDoUuTien { get; set; }

        public byte? CachTinhTyLeHoanThanh { get; set; }

        public int? TyTrong { get; set; }

        public string DanhGiaCongViec { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyByID { get; set; }

        public DateTime? ModifyDate { get; set; }

        public List<loaicongviec> loaicongviecs { get; set; }

       
    }
    public class loaicongviec
    {
        public int LoaiCongViecID { get; set; }

        public string MaLoaiCongViec { get; set; }

        public string TenLoaiCongViec { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyByID { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
    
}
