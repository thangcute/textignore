namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class BC_BaoCao
	{
		[Key]
		public int BaoCaoID { get; set; }

		public string TenBaoCao { get; set; }

		public string TenTA { get; set; }

		public string Data { get; set; }

		public int? NhomBaoCaoID { get; set; }

		public string SQLCommand { get; set; }

		public string SQLCmdGroup { get; set; }

		public string GroupID1 { get; set; }

		public string GroupID2 { get; set; }

		public string GroupID3 { get; set; }

		public byte[] HeaderData { get; set; }

		public byte[] DetailData { get; set; }

		public int? Type { get; set; }

		public string MaBaoCao { get; set; }

		public int? DataSetID { get; set; }

		public int? DetailsTableID { get; set; }

		public int? STT { get; set; }

		public string GridLayoutData { get; set; }

		public byte[] HeaderData_EN { get; set; }

		public byte[] DetailDAta_EN { get; set; }

		public int CustomerID { get; set; }

		public bool? Visible { get; set; }

		public bool? UseEncryptData { get; set; }

		public int? DashboardHeight { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
