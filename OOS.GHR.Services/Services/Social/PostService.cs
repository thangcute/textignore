using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.EntityFramework.Models.Social;
using OOS.GHR.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Social
{
    public class PostService
    {
        public static async Task<bool> PostAsync(string content, int userId, List<string> fileList, List<int> employeeId)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                DateTime actionTime = DateTime.Now;
                using (DbContextTransaction tst = db.Database.BeginTransaction())
                {
                    SO_Post _post = new SO_Post()
                    {
                        Content = content,
                        CreatedDate = actionTime,
                        CreatedBy = userId,
                        IsDeleted = false,
                        LikeCount = 0,
                        CommentCount = 0,
                        ViewCount = 0
                    };
                    db.SO_Post.Add(_post);
                    await db.SaveChangesAsync();

                    if (fileList.Any())
                    {
                        foreach(var item in fileList)
                        {
                            db.SO_PostImage.Add(new SO_PostImage() { 
                                PostId = _post.Id,
                                ImagePath = item
                            });
                            await db.SaveChangesAsync();
                        }
                    }

                    //if (employeeId.Any())
                    //{
                    //    foreach(int epy in employeeId)
                    //    {
                    //        db.SO_PostTag.Add(new SO_PostTag() { 
                    //            PostId = _post.Id,
                    //            EmployeeId = epy
                    //        });
                    //        await db.SaveChangesAsync();
                    //    }
                    //}

                    tst.Commit();
                    rs = true;
                }
            }
            return rs;
        }

        public static async Task<bool> PutAsync(int postId, string content, List<string> fileList = null, List<int> employeeId = null)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                DateTime actionTime = DateTime.Now;
                using (DbContextTransaction tst = db.Database.BeginTransaction())
                {
                    SO_Post _post = await db.SO_Post.FindAsync(postId);
                    _post.Content = content;
                    await db.SaveChangesAsync();

                    if (fileList.Any())
                    {

                    }

                    if (employeeId.Any())
                    {

                    }

                    tst.Commit();
                    rs = true;
                }
            }
            return rs;
        }

    //    public static async Task<SoPostDetailModel> GetAsync(int id)
    //    {
    //        SoPostDetailModel rs = new SoPostDetailModel();
    //        using (var db = new OosContext())
    //        {

    //        }
    //        return rs;
    //    }
    }
}
