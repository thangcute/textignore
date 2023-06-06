using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.News
{
    public class LinkPreviewModel
    {
        /// <summary>
        /// Địa chỉ liên kết
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Mã nhúng (video YouTube, Twiter, FB...)
        /// </summary>
        public string EmbedCode { get; set; }

        /// <summary>
        /// Trên trang web, kênh, tác giả...
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
