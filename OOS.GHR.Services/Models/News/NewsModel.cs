using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OOS.GHR.Services.Models.News
{
    public class NewsModel
    {
        public int idPost { set; get; }
        public UserInfoModel userPost { set; get; }
        public DateTime? CreatedDate { set; get; }
        public String Title { set; get; }
        public string Content { set; get; }
        public List<string> Files { set; get; }
        public List<FileImages> PostImages { set; get; }
        public List<UserInfoModel> Tags { set; get; }
        public int? CountCmt { set; get; }
        public int? CountReaction { set; get; }
        public int? CountShare { set; get; }
        public int? CountViews { set; get; }
        public List<Comment> Comments { set; get; }
        public List<ListReaction> listLike { set; get; }
    }

    public class ListReaction
    {
        public int Type { get; set; }
        public int flag { get; set; }
        public string FullName { get; set; }
        public byte[] Picture { get; set; }
    }

    public class Comment
    {
        public int PostId { set; get; }
        public int LikeCmt { set; get; } = 0;
        public int IsDeleted { set; get; } = 0;
        public string Content { set; get; }
        public DateTime CreatedDate { set; get; }
        public int CreatedBy { set; get; }
        public string FullName { get; set; }
        public byte[] Picture { set; get; }
    }

    public class FileImages
    {
        public string FilePath { set; get; }
    }

    public class PostNewModel
    {
        /// <summary>
        /// Id bài viết
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tiêu đề bài viết
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung bài viết
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Danh sách file Media
        /// </summary>
        //public List<HttpPostedFileBase> Media { get; set; }
        public List<string> UrlMedia { get; set; }
    }

    public class PostLike
    {
        public int      PostId  { set; get; }
        public Reaction Type    { set; get; }
    }
}
