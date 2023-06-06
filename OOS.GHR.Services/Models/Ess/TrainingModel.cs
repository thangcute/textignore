using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class TrainingModel
    {
    }

    public class TrainingEvaluateModel
    {
        public int Id { get; set; } // ID tiêu chí đánh giá

        public string DanhGia { get; set; }

        public int? DotDaoTaoId { get; set; } = 0;
    }

    public class TrainingJoinningModel
    {
        public int Id { get; set; }
        public string TenKhoaHoc { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? ToiNgay { get; set; }
        public decimal? SoThangCamKet { get; set; }
        public string DiaDiemDaoTao { get; set; }
        public string NguoiLienHe { get; set; }
        public decimal? TongDiem { get; set; }
        public string KetQua { get; set; }
        public string NhanXet { get; set; }
        public int? XetDuyet { get; set; }
        public int? TrangThai { get; set; }
        public bool? IsBatBuocDangKy { get; set; }
        public List<TrainingContentModel> NoiDung { get; set; }
    }

    public class TrainingContentModel
    {
        public int Id { get; set; }
        public string TenNoiDung { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? ToiNgay { get; set; }
        public decimal? TongDiem { get; set; }
        public string KetQua { get; set; }
        public string NhanXet { get; set; }
        public string TaiLieu { get; set; }
    }
}
