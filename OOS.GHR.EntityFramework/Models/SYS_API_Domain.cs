namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_API_Domain
	{
		[Key]
		public int APIDomainID { get; set; }

		public int? APIID { get; set; }

		public string DomainName { get; set; }
	}
}
