namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_CaLamViec_NhomCa
	{
		[Key]
		public int CaLamViecNhomCaID { get; set; }

		public int? CaLamViecID { get; set; }

		public int? NhomCaLamViecID { get; set; }
	}
}
