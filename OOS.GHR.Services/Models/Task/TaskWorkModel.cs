using OOS.GHR.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOS.GHR.Services.Models.Task
{
    public class TaskWorkModel
    {

        public string TenCongViec { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime? NgayBatDau_ThucTe { get; set; }
        public DateTime? NgayKetThuc_ThucTe { get; set; }
        public string MoTaCongViec { get; set; }
        public string Nguoigiaoviec { get; set; }
        public byte CachTinhTyLeHoanThanh { get; set; }
        public string Hotennguoithuchien { get; set; }
        public List<checklist> CheckList { get; set; }


    }
    public class checklist
    {
        public int CheckListID { get; set; }

        public int? CongViecID { get; set; }
        public int? CreatedByID { get; set; }

        public string MoTa { get; set; }

        public int? Tyle { get; set; }

        public bool? IsHoanThanh { get; set; }
        public DateTime? CreatedDate { get; set; }
        

    }
   


}