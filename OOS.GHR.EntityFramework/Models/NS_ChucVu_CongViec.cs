namespace OOS.GHR.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NS_ChucVu_CongViec
    {
        [Key]
        public int ChucVuCongViecID { get; set; }

        public int? ChucVuID { get; set; }

        public int? CongViecID { get; set; }
    }
}
