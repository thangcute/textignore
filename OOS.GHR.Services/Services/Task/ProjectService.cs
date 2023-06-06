using Newtonsoft.Json;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models.Task;
using OOS.GHR.Services.Models.Task;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Task
{
    public class ProjectService
    {
        #region thắng viết
        //get all trạng thái
        public static async Task<List<TSK_DuAn_TrangThai>> getTT()
        {
            List<TSK_DuAn_TrangThai> rs = new List<TSK_DuAn_TrangThai>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select TrangThaiID, TenTrangThai from TSK_DuAn_TrangThai", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    object Null = null;
                    rs.Add(new TSK_DuAn_TrangThai()
                    {
                        TrangThaiID= (int)dr["TrangThaiID"],
                        TenTrangThai = (string)dr["TenTrangThai"],

                    });
                }

            }catch(Exception ex)
            {
                string msg = ex.Message;

            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        //get dự án
        //sử dụng model tự tạo
        public static async Task<List<Ds_ProjectModel>> getDA(int page, int userId, int trangthaiId)
        {
            List<Ds_ProjectModel> rs = new List<Ds_ProjectModel>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = null;
                string sql = "";
                sql += "select da.DuAnID, da.DuAnChaID, da.TenDuAn, da.TrangThaiID, da.NgayBatDau," +
                            "(select min(cv.NgayBatDau_ThucTe) from TSK_CongViec cv where cv.DuAnID=da.DuAnID) " +
                            "as NgayBatDau_ThucTe, da.NgayKetThuc, tt.TenTrangThai from TSK_DuAn da left join TSK_DuAn_TrangThai tt " +
                            "on tt.TrangThaiID=da.TrangThaiID ";
                switch (trangthaiId)
                {
                    case 1:
                        sql += "where da.TrangThaiID=" + trangthaiId + " ";
                        break;
                    case 2:
                        sql += "where da.NguoiQuanTriDuAnID = " + userId + " ";
                        break;
                    case 3:
                        sql += "left join TSK_DuAn_NguoiTheoDoi ntd on ntd.DuAnID=da.DuAnID where ntd.NhanVienID="+userId+" ";
                        break;
                    case 4:
                        
                        break;

                }
                if (page == 0)
                {
                    sql += "order by da.DuAnID";
                }
                else
                {
                    sql += "order by da.DuAnID offset " + (page - 1) * 10 + " rows FETCH NEXT 10 rows only";
                }
                
               

                cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                    object Null = null;
                    rs.Add(new Ds_ProjectModel()
                    {
                        DuAnID = (int)dr["DuAnID"],
                        DuAnChaID = (int)dr["DuAnChaID"],
                        TenDuAn = (string)dr["TenDuAn"],
                        TrangThaiID = (int)dr["TrangThaiID"],
                        NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau"],
                        NgayBatDau_ThucTe = dr["NgayBatDau_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau_ThucTe"],
                        NgayKetThuc = dr["NgayKetThuc"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc"],
                        TrangThai = dr["TenTrangThai"] == DBNull.Value ? (string)Null : (string)dr["TenTrangThai"]

                    });
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        //lấy chi tiết dự án
        //đã sửa model (thêm một vài trường cần thiết)
        public static async Task<List<TSK_DuAn>> getchitiet(int DuAnId)
        {
            List<TSK_DuAn> rs = new List<TSK_DuAn>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select da.TenDuAn, da.NgayBatDau, da.MoTa, da.ImageURL, da.CachTinhTienDo, " +
                    "(select nv.Ho + ' ' + nv.HoTen HoTen, nv.NhanVienID, nv.Anh from NhanVien nv left join TSK_DuAn da " +
                    "on da.CreatedByID=nv.NhanVienID where da.DuAnID=" + DuAnId+ " for json auto) as nguoigiao, " +
                    "(select * from (select nv.Ho + ' ' + nv.HoTen HoTen, nv.NhanVienID, nv.Anh, nt.DuAnID " +
                    "from NhanVien nv left join TSK_DuAn_NguoiThucHien nt " +
                    "on nt.NhanVienID=nv.NhanVienID " +
                    "left join TSK_DuAn da on da.DuAnID=nt.DuAnID) as A where A.DuAnID="+DuAnId+" for json auto) as nguoithuchien, " +
                    "(select * from(select nv.Ho + ' ' + nv.HoTen HoTen, nv.NhanVienID, nv.Anh, ntd.DuAnID from NhanVien nv " +
                    "left join TSK_DuAn_NguoiTheoDoi ntd on ntd.NhanVienID=nv.NhanVienID " +
                    "left join TSK_DuAn da on da.DuAnID = ntd.DuAnID) as B where B.DuAnID=10 for json auto) as nguoitheodoi " +
                    "from TSK_DuAn da where da.DuAnID="+DuAnId+"", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string a = "";
                    string b = "";
                    string c = "";
                    a = dr["nguoigiao"].ToString();
                    b = dr["nguoithuchien"].ToString();
                    c = dr["nguoitheodoi"].ToString();
                    var nguoigiao = JsonConvert.DeserializeObject<List<nguoigiao>>(a);
                    var nguoithuchien = JsonConvert.DeserializeObject<List<nguoithuchien>>(b);
                    var nguoitheodoi = JsonConvert.DeserializeObject<List<nguoitheodoi>>(c);

                    object Null = null;
                    rs.Add(new TSK_DuAn()
                    {
                        TenDuAn = (string)dr["TenDuAn"],
                        NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau"],
                        MoTa = (string)dr["MoTa"],
                        ImageURL = dr["ImageURL"] == DBNull.Value ? (string)Null : (string)dr["ImageURL"],
                        CachTinhTienDo = (byte?)dr["CachTinhTienDo"],
                        NguoiGiao = nguoigiao,
                        NguoiTheoDoi = nguoitheodoi,
                        NguoiThucHien = nguoithuchien

                    });
                }
                dr.Close();
            }catch(Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        //update trạng thái
        public static async Task<bool> updatetrangthai(int? trangthaiId, int? duanId)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update TSK_DuAn set TrangThaiID = "+ trangthaiId + " where DuAnID = "+ duanId + "", con);
                int run = cmd.ExecuteNonQuery();
                if (run > 0)
                {
                    rs = true;
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
        //thêm người thực hiện
        public static async Task<bool> insert(TaskProjectModel model, int userId)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = null;
                string sql = "";
                
                for (int i = 0; i < model.Executors.Count(); i++)
                {
                    if (model.Executors[i].NhanVienID == 0 || model.Executors[i].ChucVu == null)
                    {
                        rs = false;
                        break;
                    }
                    else
                    {
                        sql = "select * from [TSK_DuAn_NguoiThucHien] where NhanVienID = " + model.Executors[i].NhanVienID + " and DuAnID = " + model.DuAnID + "";
                        cmd = new SqlCommand(sql, con);
                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        if (result > 0)
                        {
                            rs = false;
                            break;
                        }
                        else
                        {
                            sql = "insert into [TSK_DuAn_NguoiThucHien](DuAnID, NhanVienID, ChucVuDuAn, NgayGiaoNhiemVu, IsActive, CreatedByID, CreatedDate) " +
                                "values(" + model.DuAnID + ", " + model.Executors[i].NhanVienID + ", N'" + model.Executors[i].ChucVu + "', '" + DateTime.Now + "', 0, " + userId + ", '" + DateTime.Now + "')";

                        }
                        cmd = new SqlCommand(sql, con);
                        int run = cmd.ExecuteNonQuery();
                        if (run > 0)
                        {
                            rs = true;
                            break;
                        }
                    }
                        
                }
                
                
                
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;

        }
        public static async Task<bool> updatetiendo(byte? t, int duanId)
        {
            bool rs = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update TSK_DuAn set CachTinhTienDo=" + t + " where DuAnID=" + duanId + "", con);
                int result = cmd.ExecuteNonQuery();
                if(result > 0)
                {
                    rs = true;
                }

            }catch (Exception e)
            {
                string msg = e.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        //public static async Task<bool> insertduancon(TSK_DuAn model)
        //{
        //    bool rs = false;
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("insert into TSK_DuAn()", con);
        //    }catch(Exception ex)
        //    {
        //        string msg = ex.Message;

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return rs;

        //}
        #endregion

        #region code cũ
        // Danh sach du an

        //var _object = db.Database.SqlQuery<TSK_DuAn_TrangThai>(sql);
        public static async Task<(List<TSK_DuAn> rs, int total)> get(string _conditions = "", int _limit = 15, int _offset = 0, bool _all = false)
        {
            int _total = 0;
            List<TSK_DuAn> _rs = new List<TSK_DuAn>();

            try
            {
                using (var db = new OosContext())
                {
                    string sql = "SELECT * FROM [dbo].[TSK_DuAn]";
                    if (!string.IsNullOrEmpty(_conditions))
                        sql = string.Format("{0} WHERE {1}", sql, _conditions);
                    var _data = db.TSK_DuAn.SqlQuery(sql);
                    if (_data.Any())
                    {
                        _total = _data.Count();
                        if (_all)
                            _rs = _data.OrderBy(x => x.DuAnID).ToList();
                        else
                            _rs = _data.OrderBy(x => x.DuAnID).Skip(_offset).Take(_limit).ToList();
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return (_rs, _total);
        }

        /*
         * type=0 quan tri du an
         * type=1 nguoi theo doi
         * type=2 nguoi thuc hien
         */
        public static async Task<(List<TaskProjectModel> rs, int total)> getAll(int UserId, int type=0)
        {
            int _total = 0;
            List<TaskProjectModel> _rs = new List<TaskProjectModel>();
            List<int> projectList = new List<int>();
            try
            {
                using (var db = new OosContext())
                {
                    switch (type)
                    {
                        case 1:
                            var _project_follower = db.TSK_DuAn_NguoiTheoDoi.Where(x => x.NhanVienID == UserId && x.IsActive == true);
                            if (_project_follower != null && _project_follower.Any())
                                projectList = _project_follower.Select(x => (int)x.DuAnID).ToList();
                            break;
                        case 2:
                            var _project_executor = db.TSK_DuAn_NguoiThucHien.Where(x => x.NhanVienID == UserId && x.IsActive == true);
                            if (_project_executor != null && _project_executor.Any())
                                projectList = _project_executor.Select(x => (int)x.DuAnID).ToList();
                            break;
                        default:
                            break;
                    }

                    var data = (from a in db.TSK_DuAn
                                join n in db.NhanViens on a.NguoiQuanTriDuAnID equals n.NhanVienID into ns
                                from n in ns.DefaultIfEmpty()
                                join b in db.TSK_DuAn_TrangThai on a.TrangThaiID equals b.TrangThaiID into bs
                                from b in bs.DefaultIfEmpty()                                
                                where (type==0 && a.NguoiQuanTriDuAnID == UserId)
                                || (type > 0 && projectList.Contains(a.DuAnID))
                                select new TaskProjectModel
                                {
                                    DuAnID = a.DuAnID,
                                    MaDuAn = a.MaDuAn,
                                    TenDuAn = a.TenDuAn,
                                    TrangThaiID = a.TrangThaiID,
                                    TenTrangThai = b.TenTrangThai,
                                    DuAnChaID = a.DuAnChaID,
                                    NgayBatDau = a.NgayBatDau,
                                    NgayKetThuc = a.NgayKetThuc,
                                    NgayQuaHan = 0,
                                    CachTinhTienDo = a.CachTinhTienDo,
                                    NguoiQuanTriDuAnID = a.NguoiQuanTriDuAnID,
                                    TenNguoiQuanTri = n.HoTen,
                                    HoNguoiQuanTri = n.Ho,
                                    MoTa = a.MoTa,
                                    ImageURL = a.ImageURL,
                                    CreatedByID = a.CreatedByID,
                                    CreatedDate = a.CreatedDate,
                                    ModifyByID = a.ModifyByID,
                                    ModifyDate = a.ModifyDate
                                }).AsEnumerable().Select(x => new TaskProjectModel() {
                                    DuAnID = x.DuAnID,
                                    MaDuAn = x.MaDuAn,
                                    TenDuAn = x.TenDuAn,
                                    TrangThaiID = x.TrangThaiID,
                                    TenTrangThai = x.TenTrangThai,
                                    DuAnChaID = x.DuAnChaID,
                                    NgayBatDau = x.NgayBatDau,
                                    V_NgayBatDau = string.IsNullOrEmpty(x.NgayBatDau.ToString()) ? "" : x.NgayBatDau.Value.ToString("dd/MM/yyyy"),
                                    NgayKetThuc = x.NgayKetThuc,
                                    V_NgayKetThuc = string.IsNullOrEmpty(x.NgayKetThuc.ToString()) ? "" : x.NgayKetThuc.Value.ToString("dd/MM/yyyy"),
                                    NgayQuaHan = string.IsNullOrEmpty(x.NgayKetThuc.ToString()) ? 0 : (DateTime.Now - x.NgayKetThuc.Value).Days,
                                    CachTinhTienDo = x.CachTinhTienDo,
                                    NguoiQuanTriDuAnID = x.NguoiQuanTriDuAnID,
                                    TenNguoiQuanTri = x.TenNguoiQuanTri,
                                    HoNguoiQuanTri = x.HoNguoiQuanTri,
                                    MoTa = x.MoTa,
                                    ImageURL = x.ImageURL,
                                    CreatedByID = x.CreatedByID,
                                    CreatedDate = x.CreatedDate,
                                    ModifyByID = x.ModifyByID,
                                    ModifyDate = x.ModifyDate
                                });

                    if (data != null && data.Any())
                        _rs = data.ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return (_rs, _total);
        }

        // Chi tiet du an
        public static async Task<TaskProjectModel> detail(int projectId = 0)
        {
            TaskProjectModel rs = new TaskProjectModel();
            try
            {
                using (var db = new OosContext())
                {
                    // Thong tin du an
                    var DuAn = await db.TSK_DuAn.FindAsync(projectId);
                    if (DuAn != null && DuAn.DuAnID > 0)
                    {
                        rs = (TaskProjectModel)DuAn;

                        //// Danh sach nguoi theo doi
                        //var NguoiTheoDoi = db.TSK_DuAn_NguoiTheoDoi.Where(x => x.DuAnID == projectId && x.IsActive == true);
                        //if (NguoiTheoDoi.Any())
                        //    rs.Followers = NguoiTheoDoi.Select(x => x.NguoiTheoDoiID).ToList();

                        //// Danh sach nguoi thuc hien
                        //var NguoiThucHien = db.TSK_DuAn_NguoiThucHien.Where(x => x.DuAnID == projectId && x.IsActive == true);
                        //if (NguoiThucHien.Any())
                        //    rs.Executors = NguoiThucHien.Select(x => x.NguoiThucHienID).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return rs;
        }

        // Them moi du an
        public static async Task<bool> post(TaskProjectModel model)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        TSK_DuAn _model = new TSK_DuAn()
                        {
                            DuAnID = model.DuAnID,
                            MaDuAn = model.MaDuAn,
                            TenDuAn = model.TenDuAn,
                            TrangThaiID = model.TrangThaiID,
                            DuAnChaID = model.DuAnChaID,
                            NgayBatDau = model.NgayBatDau,
                            NgayKetThuc = model.NgayKetThuc,
                            CachTinhTienDo = model.CachTinhTienDo,
                            NguoiQuanTriDuAnID = model.NguoiQuanTriDuAnID,
                            MoTa = model.MoTa,
                            ImageURL = model.ImageURL,
                            CreatedByID = model.CreatedByID,
                            CreatedDate = model.CreatedDate,
                            ModifyByID = model.ModifyByID,
                            ModifyDate = model.ModifyDate
                        };

                        db.TSK_DuAn.Attach(_model);
                        db.Entry(_model).State = EntityState.Added;
                        await db.SaveChangesAsync();

                        if (model.Followers.Any())
                        {
                            List<TSK_DuAn_NguoiTheoDoi> listFollower = new List<TSK_DuAn_NguoiTheoDoi>();
                            foreach (DependentModel _item in model.Followers)
                            {
                                TSK_DuAn_NguoiTheoDoi _follower = new TSK_DuAn_NguoiTheoDoi();
                                _follower.DuAnID = model.DuAnID;
                                _follower.NhanVienID = _item.NhanVienID;
                                _follower.ChucVuNguoiTheoDoi = _item.ChucVu;
                                _follower.CreatedByID = model.CreatedByID;
                                _follower.CreatedDate = model.CreatedDate;
                                _follower.IsActive = true;

                                listFollower.Add(_follower);
                                //db.TSK_DuAn_NguoiTheoDoi.Add(_item);
                            }

                            if (listFollower.Any()) {
                                db.TSK_DuAn_NguoiTheoDoi.AddRange(listFollower);
                                await db.SaveChangesAsync();
                            }
                        }

                        if (model.Executors.Any())
                        {
                            List<TSK_DuAn_NguoiThucHien> listExecutor = new List<TSK_DuAn_NguoiThucHien>();
                            foreach (DependentModel _item in model.Executors)
                            {
                                TSK_DuAn_NguoiThucHien _executor = new TSK_DuAn_NguoiThucHien();
                                _executor.DuAnID = model.DuAnID;
                                _executor.NhanVienID = _item.NhanVienID;
                                _executor.ChucVuDuAn = _item.ChucVu;
                                _executor.CreatedByID = model.CreatedByID;
                                _executor.CreatedDate = model.CreatedDate;
                                _executor.IsActive = true;

                                listExecutor.Add(_executor);
                                //db.TSK_DuAn_NguoiThucHien.Add(item);
                            }

                            if (listExecutor.Any())
                            {
                                db.TSK_DuAn_NguoiThucHien.AddRange(listExecutor);
                                await db.SaveChangesAsync();
                            }
                            //await db.SaveChangesAsync();
                        }

                        transaction.Commit();
                        rs = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return rs;
        }

        //// Sua du an
        //public static async Task<bool> put(int projectId = 0, TaskProjectModel model = null)
        //{
        //    bool rs = false;
        //    DateTime actionTime = DateTime.Now;
        //    using (var db = new OosContext())
        //    {
        //        using (var transaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Cap nhan DuAn
        //                db.TSK_DuAn
        //                    .Where(x => x.DuAnID == projectId)
        //                    .ToList().ForEach(x =>
        //                    {
        //                        x.MaDuAn = model.DuAn.MaDuAn;
        //                        x.TenDuAn = model.DuAn.TenDuAn;
        //                        x.TrangThaiID = model.DuAn.TrangThaiID;
        //                        x.NgayBatDau = model.DuAn.NgayBatDau;
        //                        x.NgayKetThuc = model.DuAn.NgayKetThuc;
        //                        x.CachTinhTienDo = model.DuAn.CachTinhTienDo;
        //                        x.NguoiQuanTriDuAnID = model.DuAn.NguoiQuanTriDuAnID;
        //                        x.MoTa = model.DuAn.MoTa;
        //                        //x.ImageURL = "";
        //                        x.ModifyByID = model.DuAn.ModifyByID;
        //                        x.ModifyDate = model.DuAn.ModifyDate;
        //                    });
        //                await db.SaveChangesAsync();

        //                bool flag = false;
        //                // Cap nhat nguoi theo doi
        //                db.TSK_DuAn_NguoiTheoDoi
        //                    .Where(x => 
        //                        x.DuAnID == projectId 
        //                        && x.IsActive == true
        //                        && !model.NguoiTheoDoi.Select(i => i.NguoiTheoDoiID).ToList().Contains(x.NguoiTheoDoiID)
        //                    )
        //                    .ToList().ForEach(x =>
        //                    {
        //                        x.IsActive = false;
        //                    });
        //                await db.SaveChangesAsync();
        //                if (model.NguoiTheoDoi.Any())
        //                {
        //                    foreach (var item in model.NguoiTheoDoi)
        //                    {
        //                        if (item.NguoiTheoDoiID == 0)
        //                        {
        //                            item.CreatedByID = model.DuAn.ModifyByID;
        //                            item.CreatedDate = model.DuAn.ModifyDate;
        //                            item.IsActive = true;

        //                            db.TSK_DuAn_NguoiTheoDoi.Add(item);
        //                            if (!flag)
        //                                flag = true;
        //                        }
        //                    }

        //                    if (flag)
        //                        await db.SaveChangesAsync();
        //                    flag = false;
        //                }

        //                // cap nhat nguoi thuc hien
        //                db.TSK_DuAn_NguoiThucHien
        //                    .Where(x => 
        //                        x.DuAnID == projectId 
        //                        && x.IsActive == true
        //                        && !model.NguoiThucHien.Select(i => i.NguoiThucHienID).ToList().Contains(x.NguoiThucHienID)
        //                    )
        //                    .ToList().ForEach(x =>
        //                    {
        //                        x.IsActive = false;
        //                    });
        //                await db.SaveChangesAsync();
        //                if (model.NguoiThucHien.Any())
        //                {
        //                    foreach (var item in model.NguoiThucHien)
        //                    {
        //                        if(item.NguoiThucHienID == 0)
        //                        {
        //                            item.CreatedByID = model.DuAn.ModifyByID;
        //                            item.CreatedDate = model.DuAn.ModifyDate;
        //                            item.IsActive = true;

        //                            db.TSK_DuAn_NguoiThucHien.Add(item);
        //                            if (!flag)
        //                                flag = true;
        //                        }
        //                    }

        //                    if (flag)
        //                        await db.SaveChangesAsync();
        //                    flag = false;
        //                }

        //                transaction.Commit();
        //                rs = true;
        //            }catch(Exception ex)
        //            {
        //                transaction.Rollback();
        //            }
        //        }
        //    }
        //    return rs;
        //}

        // Xoa du an
        /*
         * string deleteQuery = "DELETE FROM [dbo].[TSK_DuAn_NguoiTheoDoi] WHERE [DuAnID]={0}";
            var rows = db.Database.ExecuteSqlCommand(deleteQuery, projectId);
         */
        public static async Task<bool> delete(int projectId=0)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.TSK_DuAn.RemoveRange(db.TSK_DuAn.Where(x => x.DuAnID == projectId));
                        await db.SaveChangesAsync();

                        db.TSK_DuAn_NguoiTheoDoi.RemoveRange(db.TSK_DuAn_NguoiTheoDoi.Where(x => x.DuAnID == projectId));
                        await db.SaveChangesAsync();

                        db.TSK_DuAn_NguoiThucHien.RemoveRange(db.TSK_DuAn_NguoiThucHien.Where(x => x.DuAnID == projectId));
                        await db.SaveChangesAsync();

                        transaction.Commit();
                        rs = true;
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return rs;
        }

        // Thay doi trang thai
        public static async Task<bool> updateStatus(int projectId = 0, int statusId = 0, int userId = 0)
        {
            bool rs = false;

            DateTime actionTime = DateTime.Now;
            using (var db = new OosContext())
            {
                db.TSK_DuAn.Where(x => x.DuAnID == projectId).ToList().ForEach(x => {
                    x.TrangThaiID = statusId;
                    x.ModifyByID = userId;
                    x.ModifyDate = actionTime;
                });

                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        // cap nhat nguoi thuc hien

        public static async Task<bool> updatePersons(ProjectPersonModel model)
        {
            bool rs = false;

            // check phan tram hoan thanh du an

            DateTime actionTime = DateTime.Now;
            using (var db = new OosContext())
            {
                TSK_DuAn _infomation = await db.TSK_DuAn.FindAsync(model.Id);
                List<TSK_DuAn_NguoiTheoDoi> _followers = new List<TSK_DuAn_NguoiTheoDoi>();
                List<TSK_DuAn_NguoiThucHien> _executors = new List<TSK_DuAn_NguoiThucHien>();

                var _data_followers = db.TSK_DuAn_NguoiTheoDoi.Where(x => x.DuAnID == model.Id);
                if (_data_followers != null && _data_followers.Any())
                    _followers = _data_followers.ToList();

                var _data_executors = db.TSK_DuAn_NguoiThucHien.Where(x => x.DuAnID == model.Id);
                if (_data_executors != null && _data_executors.Any())
                    _executors = _data_executors.ToList();

                using (var transaction = db.Database.BeginTransaction())
                {
                    // cap nhat nguoi theo doi
                    if (_followers.Any())
                    {
                        db.TSK_DuAn_NguoiTheoDoi
                        .Where(x => x.DuAnID == model.Id)
                        .ToList().ForEach(x => {
                            x.IsActive = false;
                        });
                        await db.SaveChangesAsync();
                    }
                    // cap nhat nguoi thuc hien
                    if (_executors.Any())
                    {
                        db.TSK_DuAn_NguoiThucHien
                        .Where(x => x.DuAnID == model.Id)
                        .ToList().ForEach(x => {
                            x.IsActive = false;
                        });
                        await db.SaveChangesAsync();
                    }
                    // kiem tra nguoi theo doi
                    if (model.Followers.Where(x => x.Xoa == 0) != null 
                        && model.Followers.Where(x => x.Xoa == 0).Any())
                    {
                        foreach(DependentModel _item in model.Followers)
                        {
                            TSK_DuAn_NguoiTheoDoi _entry = _followers.Where(x => x.NhanVienID == _item.NhanVienID).FirstOrDefault();
                            if (_entry.NguoiTheoDoiID > 0)
                            {
                                _entry.ChucVuNguoiTheoDoi = _item.ChucVu;
                                if(!_entry.IsActive == false)
                                {
                                    _entry.CreatedByID = model.ModifyByID;
                                    _entry.CreatedDate = actionTime;
                                }
                                _entry.IsActive = true;
                            }
                            else
                                _entry = new TSK_DuAn_NguoiTheoDoi()
                                {
                                    DuAnID = model.Id,
                                    NhanVienID = _item.NhanVienID,
                                    ChucVuNguoiTheoDoi = _item.ChucVu,
                                    IsActive = true,
                                    CreatedByID = model.ModifyByID,
                                    CreatedDate = actionTime
                                };

                            db.TSK_DuAn_NguoiTheoDoi.Attach(_entry);
                            db.Entry(_entry).State = _entry.NguoiTheoDoiID > 0 ? EntityState.Modified : EntityState.Added;
                        }

                        await db.SaveChangesAsync();
                    }
                    // kiem tra nguoi thuc hien
                    if (model.Executors.Where(x => x.Xoa == 0) != null 
                        && model.Executors.Where(x => x.Xoa == 0).Any())
                    {
                        foreach (DependentModel _item in model.Executors)
                        {
                            TSK_DuAn_NguoiThucHien _entry = _executors.Where(x => x.NhanVienID == _item.NhanVienID).FirstOrDefault();
                            if (_entry.NguoiThucHienID > 0)
                            {
                                _entry.ChucVuDuAn = _item.ChucVu;
                                if (!_entry.IsActive == false)
                                {
                                    _entry.CreatedByID = model.ModifyByID;
                                    _entry.CreatedDate = actionTime;
                                }
                                _entry.IsActive = true;
                            }
                            else
                                _entry = new TSK_DuAn_NguoiThucHien()
                                {
                                    DuAnID = model.Id,
                                    NhanVienID = _item.NhanVienID,
                                    ChucVuDuAn = _item.ChucVu,
                                    IsActive = true,
                                    CreatedByID = model.ModifyByID,
                                    CreatedDate = actionTime
                                };

                            db.TSK_DuAn_NguoiThucHien.Attach(_entry);
                            db.Entry(_entry).State = _entry.NguoiThucHienID > 0 ? EntityState.Modified : EntityState.Added;
                        }

                        await db.SaveChangesAsync();
                    }
                }
            }
            return rs;
        }

        // Them nguoi theo doi
        public static async Task<bool> addControl(int projectId = 0, int employeeId = 0, string position = "", int userId = 0)
        {
            bool rs = false;

            DateTime actionTime = DateTime.Now;
            using (var db = new OosContext())
            {
                db.TSK_DuAn_NguoiTheoDoi.Add(
                    new TSK_DuAn_NguoiTheoDoi()
                    {
                        DuAnID = projectId,
                        NhanVienID = employeeId,
                        ChucVuNguoiTheoDoi = position,
                        IsActive = true,
                        CreatedByID = userId,
                        CreatedDate = actionTime
                    }
                );
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        
        // Xoa nguoi theo doi
        public static async Task<bool> deleteControl(int projectId = 0, int controlId = 0)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_DuAn_NguoiTheoDoi.RemoveRange(db.TSK_DuAn_NguoiTheoDoi.Where(x => x.DuAnID == projectId && x.NguoiTheoDoiID == controlId));
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        // Nguoi theo goi theo Du an
        public static async Task<List<TSK_DuAn_NguoiTheoDoi>> getControlByProject(int projectId = 0)
        {
            List<TSK_DuAn_NguoiTheoDoi> rs = new List<TSK_DuAn_NguoiTheoDoi>();
            using (var db = new OosContext())
            {
                var _data = db.TSK_DuAn_NguoiTheoDoi.Where(x => x.DuAnID == projectId && x.IsActive == true);
                if (_data.Any())
                    rs = _data.ToList();
            }
            return rs;
        }

        // Them nguoi tham gia
        public static async Task<bool> addWork(int projectId, int employeeId, string position, DateTime focusDate, int userId)
        {
            bool rs = false;

            DateTime actionTime = DateTime.Now;

            using (var db = new OosContext())
            {
                db.TSK_DuAn_NguoiThucHien.Add(
                    new TSK_DuAn_NguoiThucHien()
                    {
                        DuAnID = projectId,
                        NhanVienID = employeeId,
                        ChucVuDuAn = position,
                        NgayGiaoNhiemVu = focusDate,
                        IsActive = true,
                        CreatedByID = userId,
                        CreatedDate = actionTime
                    }
                );

                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        // Xoa nguoi tham gia
        public static async Task<bool> deleteWork(int projectId, int workId)
        {
            bool rs = false;
            using (var db = new OosContext())
            {
                db.TSK_DuAn_NguoiThucHien.RemoveRange(db.TSK_DuAn_NguoiThucHien.Where(x => x.DuAnID == projectId && x.NguoiThucHienID == workId));
                rs = await db.SaveChangesAsync() > 0 ? true : false;
            }
            return rs;
        }

        // Nguoi thuc hien theo Du an
        public static async Task<List<TSK_DuAn_NguoiThucHien>> getWorkByProject(int projectId = 0)
        {
            List<TSK_DuAn_NguoiThucHien> rs = new List<TSK_DuAn_NguoiThucHien>();
            using (var db = new OosContext())
            {
                var _data = db.TSK_DuAn_NguoiThucHien.Where(x => x.DuAnID == projectId && x.IsActive == true);
                if (_data.Any())
                    rs = _data.ToList();
            }
            return rs;
        }
        #endregion
    }
}
