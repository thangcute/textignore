namespace OOS.GHR.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_NhomPhanQuyen
    {
        [Key]
        public int NhomPhanQuyenID { get; set; }

        [StringLength(50)]
        public string TenNhomPhanQuyen { get; set; }

        public string Quyen { get; set; }

        [Column(TypeName = "image")]
        public byte[] Data { get; set; }
    }
}
