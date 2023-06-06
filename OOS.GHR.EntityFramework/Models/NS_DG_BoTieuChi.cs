namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_BoTieuChi
	{
		[Key]
		public int BoTieuChiID { get; set; }

		public string TenBoTieuChi { get; set; }

		public string MaBoTieuChi { get; set; }

		public bool? IsUse { get; set; }

		public bool? IsDeleted { get; set; }

		public string FieldNameAssign { get; set; }

		public string FieldNameView { get; set; }

		public string FieldNameEvaluation { get; set; }

		public bool? ShowDeadline { get; set; }

		public string HideFieldNameEvaluation { get; set; }

		public int? CongTyID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
