using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Models
{
    public class LoginModel
    {
        public string DeviceId { get; set; } = "";
        public string FireBaseToken { get; set; } = "";
        [Required(ErrorMessage = "Tên đăng nhập để trống.")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu để trống.")]
        [StringLength(150)]
        public string Password { get; set; }
        public string lDAPAddress { get; set; } = "";
    }
}