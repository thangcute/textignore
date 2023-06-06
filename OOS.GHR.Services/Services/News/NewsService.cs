using Newtonsoft.Json;
using OOS.GHR.EntityFramework.Models.Social;
using OOS.GHR.Services.Models;
using OOS.GHR.Services.Models.News;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.News
{
    public class NewsService
    {
        public static async Task<List<NewsModel>> GetNews(int page, int employeeId)
        {
            List<NewsModel> model = new List<NewsModel>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand($@"
                    Select p.*,nv.Ho+' '+nv.HoTen HoVaTen, nv.Anh Picture, 
					(select pa.FilePath from [SO_PostAttachment] pa where pa.PostId=p.Id For Json Auto) as PostImages,
					(select * from 
					(select pcmt.PostId, pcmt.Content, pcmt.CreatedDate, pcmt.IsDeleted, NhanVienID CreatedBy, Ho+' '+HoTen FullName ,userCmt.Anh Picture from [SO_PostComment] pcmt
					left join NhanVien userCmt on userCmt.NhanVienID = pcmt.CreatedBy) as tb
					where tb.PostId = p.Id
					for json auto) as Comments,
                    (select * from (
					select pl.PostId, pl.Type, nvLike.Ho+' '+nvLike.HoTen FullName, nvLike.Anh Picture,
					case when pl.PostId = p.Id and pl.EmployeeId = {employeeId} then 0
										ELSE 1 
										END AS flag
					 from [SO_PostLike] pl
					left join NhanVien nvLike on pl.EmployeeId = nvLike.NhanVienID
					where pl.Type != 0
					) as tbLikes where tbLikes.PostId = p.Id
					order by tbLikes.flag offset 0 rows FETCH NEXT 5 rows only
					for json auto) as listLike
					from [SO_Post] p
					left join [NhanVien] nv on nv.NhanVienID = p.CreatedBy 
					order by p.Id DESC offset {(page - 1) * 5} rows FETCH NEXT 5 rows only", con);
                //  SqlCommand cm = new SqlCommand($@"
                //    Select p.*,nv.Ho+' '+nv.HoTen HoVaTen, nv.Anh Picture, 
					//(select pa.FilePath from [SO_PostAttachment] pa where pa.PostId=p.Id For Json Auto) as PostImages,
					//(select * from 
					//(select pcmt.PostId, pcmt.Content, pcmt.CreatedDate, pcmt.IsDeleted, NhanVienID CreatedBy, Ho+' '+HoTen FullName ,userCmt.Anh Picture from [SO_PostComment] pcmt
					//left join NhanVien userCmt on userCmt.NhanVienID = pcmt.CreatedBy) as tb
					//where tb.PostId = p.Id
					//for json auto) as Comments,
					//(select pl.Type , nv.Ho+' '+nv.HoTen FullName, nv.Anh Picture,
					//case when pl.PostId = p.Id and pl.EmployeeId = {employeeId} then 0
					//ELSE 1 
					//END AS flag
					//from [dbo].[SO_PostLike] pl
					//left join NhanVien nv on pl.EmployeeId = nv.NhanVienID
					//where pl.PostId = p.Id
					//order by flag offset 0 rows FETCH NEXT 5 rows only
					//for json auto) as listLike
					//from [SO_Post] p
					//left join [NhanVien] nv on nv.NhanVienID = p.CreatedBy 
					//order by p.Id DESC offset {(page - 1) * 5} rows FETCH NEXT 5 rows only", con);

                SqlDataReader dr = cm.ExecuteReader();
               
                while (dr.Read())
                {
                    string a = "";
                    string b = "";
                    string c = "";

                    a = dr["PostImages"].ToString();
                    var abc = JsonConvert.DeserializeObject<List<FileImages>>(a);
                    b = dr["Comments"].ToString();
                    var xyz = JsonConvert.DeserializeObject<List<Comment>>(b);
                    c = dr["listLike"].ToString();
                    var ghj = JsonConvert.DeserializeObject<List<ListReaction>>(c);

                    UserInfoModel infoUserPost = new UserInfoModel();
                    infoUserPost.UserId = (int)dr["CreatedBy"];
                    infoUserPost.FullName = (string)dr["HoVaTen"];
                    infoUserPost.Picture = (byte[])dr["Picture"];

                    model.Add(new NewsModel() { 
                        idPost = (int)dr["Id"], 
                        userPost = infoUserPost, 
                        CreatedDate = (DateTime)dr["CreatedDate"], 
                        Title = (string)dr["Title"], 
                        Content = (string)dr["Content"],
                        PostImages = abc,
                        CountCmt = (int)dr["CommentCount"],
                        CountReaction = (int)dr["LikeCount"],
                        CountViews = (int)dr["ViewCount"],
                        Comments = xyz,
                        listLike = ghj
                    });
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                string mgs = ex.Message;
            }
            return model;
        }

        public static async Task<(bool rs, int idPost)> PostNew(int employeeId, PostNewModel model)
        {
            bool rs = false;
            int idPostNews = 0;
            DateTime actionTime = DateTime.Now;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert SO_Post(Title,Content,CreatedDate,CreatedBy,IsDeleted,LikeCount,CommentCount,ViewCount) values(N'"+model.Title+"', N'"+model.Content+"', '"+ actionTime + "', "+ employeeId + ", 0, 0, 0, 0); SELECT SCOPE_IDENTITY();";
                cmd.Connection = con;
                SqlTransaction transaction;
                transaction = con.BeginTransaction();
                cmd.Transaction = transaction;
                
                try
                {
                    idPostNews = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    idPostNews = 0;
                    return (rs, idPostNews);
                }
                if (model.UrlMedia != null && model.UrlMedia.Any())
                {
                    List<SO_PostAttachment> photos = new List<SO_PostAttachment>();
                    foreach (string _path in model.UrlMedia)
                    {
                        string _extension = _path.Substring(_path.LastIndexOf('.') + 1);
                        photos.Add(new SO_PostAttachment()
                        {
                            PostId = idPostNews,
                            FilePath = _path,
                            Extension = _extension,
                            Type = 0
                        });
                    }

                    if (photos.Any())
                    {
                        cmd.CommandText = "";
                        foreach (var item in photos)
                        {
                            cmd.CommandText += "insert SO_PostAttachment(PostId,FilePath,Extension,Type) values ("+ item.PostId + ",'"+item.FilePath+"','"+item.Extension+"',"+item.Type+");";
                        }
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        transaction.Rollback();
                        idPostNews = 0;
                        return (rs, idPostNews);
                    }
                }
                transaction.Commit();
                rs = true;
            }
            catch(Exception ex)
            {
                string mess = ex.Message;
            }
            
            return (rs, idPostNews);
        }

        public static async Task<(bool rs, int typeLike)> countLike(int postId, int employeeId)
        {
            bool rs = false;
            int typeLike = 0;
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ConnectionString;
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            con.Open();
            try
            {
                SqlCommand cm = new SqlCommand("SELECT Type, COUNT(*) from SO_PostLike where PostId = '" + postId + "' and EmployeeId = '" + employeeId + "' group by Type", con);
                SqlDataReader reader = cm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        typeLike = Convert.ToInt32(reader["Type"]);
                    }
                    rs = true;
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
            return (rs, typeLike);

        }
        public static async Task<bool> countLikedelete(int postId, int employeeId, int IdLike)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ConnectionString;
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            con.Open();
            try
            {
                SqlCommand cm = new SqlCommand("select * from SO_PostLike where PostId = '" + postId + "' and EmployeeId = '" + employeeId + "' and Type='" + IdLike + "'", con);
                int result = Convert.ToInt32(cm.ExecuteScalar());


                if (result > 0)
                {
                    con.Close();
                    rs = true;
                }

            }
            catch (Exception ex)
            {

                string msg = ex.Message;

            }
            return rs;

        }
        public static async Task<bool> Like(int postId, int employeeId, int IdLike)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            try
            {
                con.Open();
                var likeCount = countLike(postId, employeeId).Result;

                SqlCommand cmLike = null;
                // likecount = false : no post like yet
                if (!likeCount.rs)
                {
                    // chua co react
                    cmLike = new SqlCommand("insert into SO_PostLike(PostId, EmployeeId, Datetime, Type) values (" + postId + ", " + employeeId + ", '" + DateTime.Now + "', " + IdLike + "); update [dbo].[SO_Post] set LikeCount = LikeCount + 1 where Id = " + postId, con);
                }
                else
                {
                    // da ton tai react
                    // react da co trung react post
                    if (likeCount.typeLike == IdLike)
                    {
                        cmLike = new SqlCommand("update SO_PostLike set Datetime = '" + DateTime.Now + "', Type = 0 where PostId = " + postId + " and EmployeeId = " + employeeId + "; " +
                       "update [dbo].[SO_Post] set LikeCount = LikeCount - 1 where Id = " + postId, con);
                    }
                    else
                    {
                        // react da co khac react post
                        cmLike = new SqlCommand("update SO_PostLike set Datetime = '" + DateTime.Now + "', Type = " + IdLike + " where PostId = " + postId + " and EmployeeId = " + employeeId + "; " +
                        "update [dbo].[SO_Post] set LikeCount = case when " + IdLike + " <> 0 and " + likeCount.typeLike + " = 0 then LikeCount + 1 else LikeCount end where Id = " + postId, con);
                    }
                }

                if (cmLike != null)
                {
                    int result = cmLike.ExecuteNonQuery();
                    if (result > 0)
                    {
                        rs = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        public static async Task<bool> Comment(string content, int postId, int EmployeeId)
        {
            bool rs = false;
            var date = DateTime.Now;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into SO_PostComment(PostId,Content,CreatedDate, CreatedBy) values(" + postId + ", N'" + content + "', '" + date + "', " + EmployeeId + " ); update [dbo].[SO_Post] set CommentCount = CommentCount +1 where Id =  " + postId, con);
                int result = cmd.ExecuteNonQuery();
                if (result <= 0)
                {
                    con.Close();
                    return rs;
                }
                rs = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return rs;
            }
            return rs;
        }
    }
}
