namespace OOS.GHR.EntityFramework.Models.Social
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SO_Group
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "image")]
        public byte[] Icon { get; set; }

        public string IconPath { get; set; }

        public string Description { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
