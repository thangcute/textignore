namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Language
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string LanguageCulture { get; set; }

		public string UniqueSeoCode { get; set; }

		public string FlagImageFileName { get; set; }

		public bool Rtl { get; set; }

		public bool LimitedToCompanies { get; set; }

		public bool Published { get; set; }

		public int DisplayOrder { get; set; }
	}
}
