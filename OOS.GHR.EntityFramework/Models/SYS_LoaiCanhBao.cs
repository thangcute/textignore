namespace OOS.GHR.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_LoaiCanhBao
    {
        [Key]
        public int LoaiCanhBaoID { get; set; }

        [StringLength(250)]
        public string TenLoaiCanhBao { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string MaCanhBao { get; set; }
    }
}
