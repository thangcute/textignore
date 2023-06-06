using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class RequestModel
    {
        public int TruongDuLieuID { get; set; }
        public string TenTruongDuLieu { get; set; }
        public string KieuDuLieu { get; set; }
        public string QueryData { get; set; }
        public string ControlType { get; set; }
        public string FormatString { get; set; }
        public string GiaTriMacDinh { get; set; }
        public string TableName { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public string MoTaNgan { get; set; }
    }
}
