namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Comment
	{
		[Key]
		public long CommentID { get; set; }

		public string Module { get; set; }

		public int? ObjectID { get; set; }

		public string Content { get; set; }

		public string ReferenceUserName { get; set; }

		public string UserName { get; set; }

		public string AvatarSource { get; set; }

		public int? ParentCommentID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
