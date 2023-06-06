namespace OOS.GHR.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NS_DGNL_NangLuc_MoTa
    {
        [Key]
        public int NangLucMoTaID { get; set; }

        public int? NangLucID { get; set; }

        [StringLength(550)]
        public string TenMoTa { get; set; }

        public int? DiemSo { get; set; }

        [StringLength(500)]
        public string DienGiai { get; set; }

        public int? SoThangNangCapLevel { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyByID { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
