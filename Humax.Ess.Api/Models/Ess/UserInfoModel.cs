using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Models.Ess
{
    public class UserChangePasswordModel
    {
        [Required(ErrorMessage = "Mật khẩu cũ không để trống.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không để trống.")]
        public string NewPassword { get; set; }
    }

    public class UserInfoModel
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id nhân viên
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Ảnh avatar
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string Username { get; set; }
    }
}