namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_PlugIn
	{
		[Key]
		public int PlugInID { get; set; }

		public string Name { get; set; }

		public string MenuPath { get; set; }

		public string AttachUICode { get; set; }

		public byte[] Asm { get; set; }

		public string Code { get; set; }

		public string FromResource { get; set; }

		public string ExeType { get; set; }
	}
}
