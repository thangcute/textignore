namespace OOS.GHR.EntityFramework.Models.Task
{
    using System;
    using System.ComponentModel.DataAnnotations;
    //using System.Data.Entity.Spatial;

    public partial class TSK_CongViec
    {
        [Key]
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
    }
}
