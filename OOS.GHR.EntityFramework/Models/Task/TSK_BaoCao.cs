namespace OOS.GHR.EntityFramework.Models.Task
{
    using System;
    using System.ComponentModel.DataAnnotations;
    //using System.Data.Entity.Spatial;

    public partial class TSK_BaoCao
    {
        [Key]
        public int BaoCaoID { get; set; }

        public int? CongViecID { get; set; }

        public string TieuDeBaoCao { get; set; }

        public string NoiDungBaoCao { get; set; }

        public string GhiChu { get; set; }

        public int? ReplyID { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateByID { get; set; }

        public DateTime? ModifieDate { get; set; }

        public int? ModifieByID { get; set; }

        public bool? IsDelete { get; set; }
    }
}
