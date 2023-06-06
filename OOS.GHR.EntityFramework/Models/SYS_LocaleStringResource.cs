namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_LocaleStringResource
	{
		[Key]
		public int Id { get; set; }

		public int LanguageId { get; set; }

		public string ResourceName { get; set; }

		public string ResourceValue { get; set; }
	}
}
