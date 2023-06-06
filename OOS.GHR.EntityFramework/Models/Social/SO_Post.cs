namespace OOS.GHR.EntityFramework.Models.Social
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SO_Post
    {
        public int Id { get; set; }

        [StringLength(2000)]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public int? LikeCount { get; set; }

        public int? CommentCount { get; set; }

        public int? ViewCount { get; set; }

        public int? PollID { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public int? Type { get; set; }

        public int? Scope { get; set; }

        public int? SharedPostId { get; set; }

        public int? ReactionCount { get; set; }
        public int? ShareCount { get; set; }
    }
}
