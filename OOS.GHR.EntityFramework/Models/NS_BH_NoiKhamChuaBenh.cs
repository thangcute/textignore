namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_NoiKhamChuaBenh
	{
		[Key]
		public int NoiKhamChuaBenhID { get; set; }

		public string TenNoiKhamChuaBenh { get; set; }

		public string MaNoiKhamChuaBenh { get; set; }

		public int? TinhID { get; set; }
	}
}
