using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class RelationshipModel
    {
        public int? QTQuanHeGiaDinhID { get; set; }
        public string QuanHe { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string CMND { get; set; }
        public string DienThoaiLienHe { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string NgheNghiepHienNay { get; set; }
        public string MaSoThue { get; set; }
        public string GiayKhaiSinhSo { get; set; }
        public string NoiDangKyGiayKhaiSinh { get; set; }
        public string QuocTich { get; set; }
        public int? TinhID { get; set; }
        public int? QuanHuyenID { get; set; }
        public int? XaPhuongID { get; set; }
        public string QuanHe_ChuHo { get; set; }
        public int? LoaiGiayToID { get; set; }
        public string SoHoKhau { get; set; }
        public string QuyenSo { get; set; }
        public string MaSoBHXH { get; set; }
        public string GhiChu { get; set; }
        public bool? GiamTru { get; set; }
        public string FileAttachment { get; set; }
        public string FileName { get; set; } = "";
    }
}
