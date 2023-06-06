namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class WF_QuyTrinh_BuocThucHien
	{
		[Key]
		public int QuyTrinhBuocThucHienID { get; set; }

		public int? QuayTroLaiBuocID { get; set; }

		public int? QuyTrinhID { get; set; }

		public string TenBuocThucHien { get; set; }

		public int? NguoiDungXetDuyetID { get; set; }

		public bool? ChoPhepQuayLai { get; set; }

		public int? ThuTu { get; set; }

		public string SQLNguoiXetDuyet { get; set; }

		public string SQLTuChoi { get; set; }

		public string SQLXetDuyet { get; set; }

		public int? EmailContentID { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public bool? IsNhomXetDuyet { get; set; }

		public bool? NhayQuaNeuKhongThayNguoiPheDuyet { get; set; }

		public bool? NhayQuaNeuTrungNguoiXetDuyet { get; set; }

		public string ThuTuXetDuyet { get; set; }

		public string EmailCC { get; set; }

		public int? LoaiPheDuyetID { get; set; }

		public int? CapPhongBanID { get; set; }

		public int? ThoiHanCanhBao { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
