namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_News_Article
	{
		[Key]
		public int ArticleId { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string ArticleContent { get; set; }

		public bool? Status { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }

		public int? CategoryId { get; set; }

		public string Image { get; set; }

		public bool? HotNews { get; set; }

		public bool? HotEvent { get; set; }

		public bool? ShowAtSlider { get; set; }

		public bool? ShowInHomePage { get; set; }

		public string Author { get; set; }

		public DateTime? DueDate { get; set; }

		public string PostForNhanVienID { get; set; }

		public string PostForPhongBanID { get; set; }

		public string PostForChucVuID { get; set; }

		public string PostForChucDanhID { get; set; }

		public string PostForLoaiHopDongID { get; set; }

		public string ScheduleCode { get; set; }

		public string Parameters { get; set; }

		public int? STT { get; set; }
	}
}
