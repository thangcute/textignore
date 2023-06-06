using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOS.GHR.EntityFramework.Models.Task
{
    public partial class TSK_DuAn_Comment
    {
        [Key]
        public int CommentID { get; set; }

        public int? DuAnID { get; set; }

        public int? ReplyID { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string formatCreateDate { get { return CreatedDate != null ? CreatedDate.Value.ToString("yyyy/mm/dd") : "N/a"; } }

        public int? CreatedBy { get; set; }

        public bool? IsDelete { get; set; }

    }
}
