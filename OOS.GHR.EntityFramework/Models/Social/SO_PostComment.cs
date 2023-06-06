namespace OOS.GHR.EntityFramework.Models.Social
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SO_PostComment
    {
        public int Id { get; set; }

        public int? PostId { get; set; }

        public int? ReplyId { get; set; }

        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public int? ImageId { get; set; }
    }
}
