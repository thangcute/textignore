using OOS.GHR.Framework.Mvc;
using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Models
{
    public class CheckLeaveModel
    {
        public int? QTNghiPhepID { get; set; } = 0;
        public string SoQuyetDinh { get; set; }
        public int? KyHieuChamCongID { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }

    public class LeavePostModel : OOSBaseModel
    {
        /// <summary>
        /// Lý do nghỉ phép ID | KyHieuChamCongID
        /// </summary>
        public int KyHieuChamCongID { get; set; }


        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public string NgayBatDau { get; set; }


        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public string NgayKetThuc { get; set; }


        /// <summary>
        /// Tổng số ngày nghỉ
        /// </summary>
        public decimal SoNgayNghi { get; set; }

        /// <summary>
        /// Ly do nghi
        /// </summary>
        public string DienGiai { get; set; } = "";

        /// <summary>
        /// Dia diem nghi
        /// </summary>
        public string DiaDiem { get; set; } = "";

        /// <summary>
        /// File Attachment
        /// </summary>
        public string FileAttachment { get; set; }

        /// <summary>
        /// Diễn giải nghỉ phép
        /// </summary>
        public string LyDoNghi { get; set; }


        /// <summary>
        /// File Attachment
        /// </summary>
        public string Attachment { get; set; }


        /// <summary>
        /// Tên file
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// QTNghiPhepID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Thông tin thay đổi từ ESS
        /// </summary>
        public string values { get; set; }
    }

    public class LeavePostModelOld : OOSBaseModel
    {
        /// <summary>
        /// Lý do nghỉ phép ID | KyHieuChamCongID
        /// </summary>
        public int KyHieuChamCongID { get; set; }


        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public string NgayBatDau { get; set; }


        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public string NgayKetThuc { get; set; }


        /// <summary>
        /// Tổng số ngày nghỉ
        /// </summary>
        public decimal SoNgayNghi { get; set; }


        /// <summary>
        /// Diễn giải nghỉ phép
        /// </summary>
        public string LyDoNghi { get; set; }


        /// <summary>
        /// File Attachment
        /// </summary>
        public string Attachment { get; set; }


        /// <summary>
        /// Tên file
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// QTNghiPhepID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Thông tin thay đổi từ ESS
        /// </summary>
        public string values { get; set; }
    }
}