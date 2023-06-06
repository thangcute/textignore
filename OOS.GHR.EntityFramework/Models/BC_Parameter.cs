namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class BC_Parameter
	{
		[Key]
		public int ParameterID { get; set; }

		public int? BaoCaoID { get; set; }

		public int? STT { get; set; }

		public string Name { get; set; }

		public string NameTA { get; set; }

		public string TableName { get; set; }

		public string ValueField { get; set; }

		public string NameField { get; set; }

		public string DataType { get; set; }

		public string ControlType { get; set; }

		public string SQLQuery { get; set; }

		public bool? AllowNull { get; set; }

		public string ParamName { get; set; }

		public bool? IsGroupParameter { get; set; }

		public string sWhereCommand { get; set; }

		public string ID { get; set; }

		public string ParentID { get; set; }

		public string FormatString { get; set; }

		public int? Width { get; set; }

		public int? Height { get; set; }

		public bool? IsMultiSelect { get; set; }

		public string Description { get; set; }

		public int CustomerID { get; set; }

		public string Ten { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
