namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_BoTieuChi
	{
		[Key]
		public int ChucVuBoTieuChiID { get; set; }

		public int? ChucVuID { get; set; }

		public string BoTieuChiStr { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public DateTime? NgayTao_DotDanhGia { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ChucDanhID { get; set; }
	}
}
