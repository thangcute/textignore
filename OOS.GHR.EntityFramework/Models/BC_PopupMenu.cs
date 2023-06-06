namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class BC_PopupMenu
	{
		[Key]
		public int PopupMenuID { get; set; }

		public string Ten { get; set; }

		public string Ma { get; set; }

		public byte[] Icon { get; set; }

		public int? STT { get; set; }

		public bool? IsGroup { get; set; }

		public string SQLCommand { get; set; }

		public int? BaoCaoID { get; set; }

		public bool? IsChildGrid { get; set; }

		public int CustomerID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
