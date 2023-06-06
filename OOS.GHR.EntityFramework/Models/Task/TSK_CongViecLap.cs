namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_CongViecLap
	{
		[Key]
		public int CongViecLapID { get; set; }

		public int? CongViecID { get; set; }

		public string MoTa { get; set; }

		public DateTime? ThoiGianBatDau { get; set; }

		public DateTime? ThoiGIanKetThuc { get; set; }

		public int? HinhThucLap { get; set; }

		public int? TanSuatLap { get; set; }

		public string ThuTrongTuan { get; set; }

		public DateTime? ThoiGianTaoCongViec { get; set; }

		public byte? NgayLap { get; set; }

		public byte? TuanLap { get; set; }

		public byte? ThangLap { get; set; }

		public bool? IsKhongKetThuc { get; set; }

		public DateTime? KetThucToiNgay { get; set; }

		public int? KetThucSoLanLap { get; set; }

		public bool? IsActive { get; set; }

		public int? ThoiGianTaoTruoc { get; set; }
	}
}
