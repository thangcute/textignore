namespace OOS.GHR.EntityFramework.Models.Social
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SO_PostImage
    {
        public int Id { get; set; }

        public int? PostId { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public string ImagePath { get; set; }
    }
}
