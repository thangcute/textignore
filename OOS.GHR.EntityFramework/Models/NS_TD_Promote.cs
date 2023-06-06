namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_Promote
	{
		[Key]
		public int PromoteID { get; set; }

		public int? NhuCauTuyenDungID { get; set; }

		public int? NhanVienID { get; set; }

		public int? UngVienID { get; set; }

		public DateTime? NgayYeuCau { get; set; }

		public int? ChucVuID { get; set; }

		public int? ChucDanhID { get; set; }

		public int? PhongBanID { get; set; }

		public string LyDo { get; set; }

		public string LyDoPhuHop { get; set; }

		public string KeHoachPhatTrien { get; set; }

		public string VaiTroSauKhiThangChuc { get; set; }

		public string MaNhanVien { get; set; }

		public string HoVaTen { get; set; }

		public string TenChucVu { get; set; }

		public string TenPhongBan { get; set; }

		public int? XetDuyet { get; set; }

		public string CongViecHienTai { get; set; }

		public string SuPhuHopVoiChucVuCaoHon { get; set; }

		public string LyDoKhac { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
