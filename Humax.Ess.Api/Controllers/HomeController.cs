using Humax.Ess.Api.Helpers;
using Humax.Ess.Api.Models;
using Humax.Ess.Api.Models.Ess;
//using Humax.Ess.Api.Models.Social;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers
{
    public class HomeController : BaseApiController
    {
        /// <summary>
        /// GET api/<controller>
        /// Lấy danh sách bài viết của nhóm (groupId > 0), trang cá nhân (userId > 0), 
        /// hoặc trang tin (homepage) của người dùng hiện tại (groupId ==0 && userId == 0)
        /// </summary>
        /// <param name="groupId">Lấy danh sách bài viết của nhóm</param>
        /// <param name="userId">Lấy danh sách bài viết của trang cá nhân của người dùng</param>
        /// <param name="afterPostId">Lấy các bài viết mới hơn (đăng sau) bài viết này</param>
        /// <param name="beforePostId">Lấy các bài viết cũ hơn (đăng trước) bài viết này</param>
        /// <param name="pageSize">Số lượng bài viết</param>
        /// <returns></returns>
        //public ApiResponse Get(int groupId = 0, int userId = 0, int afterPostId = 0, int beforePostId = 0, int pageSize = 5)
        //{
        //    List<PostModel> posts = new List<PostModel>();

        //    // Sample data for testing
        //    int startId = afterPostId > 0 ? afterPostId + 1 : (beforePostId > 0 ? beforePostId - pageSize - 1 : 1000);
        //    for (int i = 0; i <= pageSize; i++)
        //    {
        //        int postId = startId + i;
        //        int postType = postId % 5;
        //        posts.Add(new PostModel
        //        {
        //            Id = postId,
        //            GroupId = groupId,
        //            Type = postType,
        //            User = new UserInfoModel
        //            {
        //                FullName = "Phạm Trâm Anh",
        //                Picture = "/img/ava-12.png"
        //            },
        //            CreatedDate = DateTime.Now.Subtract(new TimeSpan(postId - 1000, 1, 10, 0, 0)),
        //            Scope = Scope.Public,
        //            Title = "Title #" + postId,
        //            Content = "One of the perks of working in an international company is sharing knowledge with your colleagues.",
        //            Photos = new List<string> { },
        //            LinkPreview = postType == 0 ? new LinkPreviewModel
        //            {
        //                Link = "http://phanmemnhansu.com/ngan-hang-thuong-mai-co-phan-quoc-dan/",
        //                Thumbnail = "http://phanmemnhansu.com/wp-content/uploads/2021/01/NCB1.jpg",
        //                Author = "PHANMEMNHANSU.COM",
        //                Title = "Triển khai phần mềm nhân sự OOS tại Ngân hàng TMCP Quốc Dân – NCB",
        //                Description = "Với sự thông minh và giao diện thân thiện với người dùng, OOS đã nhanh chóng chiếm được cảm tình của NCB từ những buổi đầu tiên làm việc."
        //            } : null
        //        });
        //    }


        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = posts
        //    };
        //}
    }
}