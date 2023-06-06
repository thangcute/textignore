namespace OOS.GHR.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhongBan_DiChuyen
    {
        [Key]
        public int PhongBanDiChuyenID { get; set; }

        public int? PhongBanID { get; set; }

        public int? PhongBanChaCuID { get; set; }

        public int? PhongBanChaMoiID { get; set; }

        public DateTime? NgayDiChuyen { get; set; }

        public int? NguoiDiChuyenID { get; set; }
    }
}
