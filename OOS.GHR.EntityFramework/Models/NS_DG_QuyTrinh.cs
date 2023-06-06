namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_QuyTrinh
	{
		[Key]
		public int QuyTrinhID { get; set; }

		public string MaQuyTrinh { get; set; }

		public string TenQuyTrinh { get; set; }

		public string TenQuyTrinhTA { get; set; }

		public string MaLoaiQuyTrinh { get; set; }

		public string SQLXetDuyetThanhCong { get; set; }

		public string SQLTuChoiXetDuyet { get; set; }

		public string Mota { get; set; }

		public bool? IsActive { get; set; }
	}
}
