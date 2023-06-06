namespace OOS.GHR.EntityFramework.Models.Social
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SO_PostLike
    {
        public int Id { get; set; }

        public int? PostId { get; set; }

        public int? EmployeeId { get; set; }

        public int? ImageId { get; set; }

        public int? Type { get; set; }

        public DateTime? Datetime { get; set; }
    }
}
