namespace OOS.GHR.EntityFramework.Models.Task
{
    using System;
    using System.ComponentModel.DataAnnotations;
    //using System.Data.Entity.Spatial;

    public partial class TSK_NguoiNhan
    {
        [Key]
        public int NguoiNhanID { get; set; }

        public int? BaoCaoID { get; set; }
    }
}
