namespace OOS.GHR.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_CanhBao
    {
        [Key]
        public int CanhBaoID { get; set; }

        public int? LoaiCanhBaoID { get; set; }

        [StringLength(255)]
        public string TieuDe { get; set; }

        [Column(TypeName = "ntext")]
        public string CauLenhSQL { get; set; }

        [StringLength(250)]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string CauLenhSQLGrid { get; set; }
    }
}
