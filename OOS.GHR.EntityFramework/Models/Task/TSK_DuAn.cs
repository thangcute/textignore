namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_DuAn
	{
		[Key]
		public int DuAnID { get; set; }

		public string MaDuAn { get; set; }

		public string TenDuAn { get; set; }

		public int? TrangThaiID { get; set; }

		public int? DuAnChaID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public byte? CachTinhTienDo { get; set; }

		public int? NguoiQuanTriDuAnID { get; set; }

		public string MoTa { get; set; }

		public string ImageURL { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public List<nguoithuchien> NguoiThucHien { get; set; }
		
		public List<nguoitheodoi> NguoiTheoDoi { get; set; }

		public List<nguoigiao> NguoiGiao { get; set; }
	}
	public class nguoithuchien
	{
        public int NhanVienID { get; set; }

        public string HoTen { get; set; }

        public string Anh { get; set; }

        public int DuAnID { get; set; }
    }
	public class nguoitheodoi
	{
        public int NguoiTheoDoiID { get; set; }

        public int? DuAnID { get; set; }

        public int? NhanVienID { get; set; }

        public string ChucVuNguoiTheoDoi { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
	public class nguoigiao
	{
		public int NhanVienID { get; set; }

        public string HoTen { get; set; }

        public string Anh { get; set; }
    }

}
