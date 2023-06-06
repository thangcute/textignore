namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_CC_TongHopTheoNgay_ChinhSua
	{
		[Key]
		public long TongHopTheoNgay_ChinhSuaID { get; set; }

		public string GiaTriCu { get; set; }

		public string GiaTriMoi { get; set; }

		public int? NguoiDungID { get; set; }

		public DateTime? NgayChinhSua { get; set; }

		public string LyDoChinhSua { get; set; }

		public DateTime? NgayChamCong { get; set; }

		public string FieldName { get; set; }

		public int? NhanVienID { get; set; }

		public int? XetDuyet { get; set; }
	}
}
