using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Social
{
    public class PrivateMessageModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn User gửi đến !")]
        public int ToUserID { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public string FileAttachment { get; set; }
    }
}
