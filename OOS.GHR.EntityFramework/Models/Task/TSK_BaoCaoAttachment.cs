using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.EntityFramework.Models.Task
{
    public class TSK_BaoCaoAttachment
    {
        public int Id { get; set; }
        public int? BaoCaoID { get; set; }
        public string FilePath { get; set; }
        public string Extension { get; set; }
        public int? Type { get; set; }  // 0: Photo, 1: file
    }
}
