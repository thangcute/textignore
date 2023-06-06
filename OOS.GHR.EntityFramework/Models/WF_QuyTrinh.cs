namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class WF_QuyTrinh
	{
		[Key]
		public int QuyTrinhID { get; set; }

		public string TenQuyTrinh { get; set; }

		public string MaQuyTrinh { get; set; }

		public string MaLoaiQuyTrinh { get; set; }

		public string SQLXetDuyetThanhCong { get; set; }

		public string SQLTuChoiXetDuyet { get; set; }

		public int? HTMLMotaID { get; set; }

		public int? HTMLKetQuaPheDuyetID { get; set; }

		public string SQLKiemTraTrung { get; set; }

		public string SQLGetParameters { get; set; }

		public bool? IsUserDefine { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? HTMLMoTaTuChoiID { get; set; }

		public int? HTMLMoTaThanhCongID { get; set; }

		public string Mota { get; set; }

		public bool? KhongGuiEmailPheDuyet { get; set; }

		public bool? KhongGuiEmailThongBaoDuyetThanhCong { get; set; }

		public bool? KhongGuiEmailThongBaoTuChoi { get; set; }
	}
}
