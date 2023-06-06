using DevExpress.Utils.OAuth;
using OOS.GHR.Library;
using OOS.GHR.Services.Models.News;
using OOS.GHR.Services.Services.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Social
{
    public class PostController: BaseApiController
    {
        public async Task<object> Get(int page)
        {
            var employeeId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            var repon = await NewsService.GetNews(page, employeeId);
            if(repon == null)
            {
                return new
                {
                    Status = 0,
                    Message = "Không có bài Post nào!!"
                };
            }
            return new
            {
                Status = 1,
                Message = "Thành công!",
                data = repon
            };
        }

        // POST api/<controller>
        public async Task<object> Post([FromBody] PostNewModel model)
        {
            var employeeId = DatabaseCache.NhanVien.NhanVienID;
            if (ModelState.IsValid)
            {
                var repon = await NewsService.PostNew(employeeId, model);
                if (repon.rs)
                {
                    return new
                    {
                        Status = 1,
                        Message = "Thêm mới bài đăng thành công !",
                        data = repon.idPost
                    };
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = "Thêm mới bài đăng không thành công !"
                    };
                }
            
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "Thêm mới bài đăng không thành công !"
                };
            }
            
        }


        //comment post
        [HttpPost]
        [Route("api/Post/Comment")]
        public async Task<object> Comment([FromBody] Comment model)
        {
            model.CreatedBy = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            if (model.PostId == 0)
            {
                ModelState.AddModelError("ID", "Không có thông tin đối tượng tương tác");
            }
            if (ModelState.IsValid)
            {
                bool thang = await NewsService.Comment(model.Content, model.PostId, model.CreatedBy);
                if (thang)
                {
                    return new
                    {
                        Status = 1,
                        Message = "comment thành công!"
                    };
                }
                return new
                {
                    Status = 0,
                    Message = "comment thất bại!"
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "không hợp lệ!!"

                };
            }

        }

        /// Like
        [HttpPost]
        [Route("api/Post/Likes")]
        public async Task<object> Likes([FromBody] PostLike model)
        {

            int employeeId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            if (model.PostId == 0)
            {
                ModelState.AddModelError("ID", "Không có thông tin đối tượng tương tác");
            }
            if (ModelState.IsValid)
            {
                bool thang = await NewsService.Like((int)model.PostId, employeeId, (int)model.Type);
                if (thang)
                {
                    return new
                    {
                        Status = 1,
                        Message = "thành công!"

                    };
                }
                return new
                {
                    Status = 0,
                    Message = "không thành công!"
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "không hợp lệ!!"

                };
            }


        }


    }
}