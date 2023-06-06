using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.EntityFramework.Models.Social
{
    public class SO_LinkPreview
    {
        [Key]
        public int LinkPreviewId { get; set; }
        public int? PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
    }
}
