using Newtonsoft.Json;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.EntityFramework.Models.Task;
using OOS.GHR.Library;
using OOS.GHR.Services.Models.News;
using OOS.GHR.Services.Models.Task;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using NhanVien = OOS.GHR.EntityFramework.Models.NhanVien;

namespace OOS.GHR.Services.Services.Task
{
    public class WorkService
    {
        #region thắng

        //----------thắng viết------------------------
        //ì null thì trả về null ngược lại thì data
        //public static DateTime trydate(SqlDataReader value, object Null)
        //{
        //    return value.IsDBNull(value) ? (DateTime?)Null : value;
        //}
        //get công việc quá hạn
        public static async Task<List<TSK_CongViec>> getquahan(int page, int userId)
        {
            List<TSK_CongViec> rs = new List<TSK_CongViec>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = null;
                string sql = "";
                sql += "select cv.CongViecID, cv.TenCongViec, cv.TrangThaiID, cv.CongViecChaID, cv.NgayBatDau, cv.NgayBatDau_ThucTe, cv.NgayKetThuc from [TSK_CongViec] cv left join [TSK_CongViec_TrangThai] cvtt on cv.TrangThaiID=cvtt.TrangThaiID where cv.NguoiThucHienID = " + userId + " and DATEDIFF(day, cv.NgayKetThuc, GETDATE() ) > 0 order by cv.CongViecID ";
                switch (page)
                {
                    case 0:
                        break;
                    default:
                        sql += "offset " + (page - 1) * 10 + " rows FETCH NEXT 10 rows only";
                        break;
                }
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    object Null = null;
                    rs.Add(new TSK_CongViec()
                    {
                        CongViecID = (int)dr["CongViecID"],
                        TenCongViec = (string)dr["TenCongViec"],
                        NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau"],
                        NgayKetThuc = dr["NgayKetThuc"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc"],
                        NgayBatDau_ThucTe = dr["NgayBatDau_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau_ThucTe"],
                        TrangThaiID = (int)dr["TrangThaiID"],
                        CongViecChaID = (int)dr["CongViecChaID"],
                    });
                };
                dr.Close();
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

        // lấy danh sách công việc theo trạng thái
        public static async Task<List<TSK_CongViec>> getcv(int page, int userId, int trangthaiId)
        {
            List<TSK_CongViec> rs = new List<TSK_CongViec>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = null;
                string sql = "";

                sql += "select cv.CongViecID, cv.TenCongViec, cv.TrangThaiID, cv.CongViecChaID, cv.NgayBatDau, cv.NgayBatDau_ThucTe, cv.NgayKetThuc from [TSK_CongViec] cv left join [TSK_CongViec_TrangThai] cvtt on cv.TrangThaiID=cvtt.TrangThaiID where cv.NguoiThucHienID = " + userId;
                switch (trangthaiId)
                {
                    case 0:
                        break;
                    default:
                        sql += " and cv.TrangThaiID = " + trangthaiId;
                        break;
                }
                if (page == 0)
                {
                    sql += " order by cv.CongViecID";
                }
                else
                {
                    sql += " order by cv.CongViecID offset " + (page - 1) * 10 + " rows FETCH NEXT 10 rows only";
                }
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    object Null = null;
                    rs.Add(new TSK_CongViec()
                    {
                        CongViecID = (int)dr["CongViecID"],
                        TenCongViec = (string)dr["TenCongViec"],
                        NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau"],
                        NgayKetThuc = dr["NgayKetThuc"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc"],
                        NgayBatDau_ThucTe = dr["NgayBatDau_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau_ThucTe"],
                        TrangThaiID = (int)dr["TrangThaiID"],
                        CongViecChaID = (int)dr["CongViecChaID"],

                    });
                };
                dr.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return rs;
        }

        //lấy dữ liệu chi tiết công việc
        //dữ liệu đầu vào là id công việc
        public static async Task<List<TaskWorkModel>> getchitietcv(int id)
        {
            List<TaskWorkModel> rs = new List<TaskWorkModel>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select cv.TenCongViec, cv.NgayBatDau, cv.NgayKetThuc, cv.NgayBatDau_ThucTe, cv.NgayKetThuc_ThucTe, cv.MoTaCongViec, nv.Ho+' '+nv.HoTen as hotennguoithuchien, (select nv1.Ho+' '+nv1.HoTen as hotennguoigiaoviec " +
                    "from TSK_CongViec cv1 left join NhanVien nv1 on nv1.NhanVienID=cv1.NguoiGiaoViecID where cv1.CongViecID=" + id + ") as nguoigiaoviec, cv.CachTinhTyLeHoanThanh, (select cl.CheckListID, cl.MoTa, cl.Tyle, cl.CreatedByID, cl.CreatedDate from TSK_CongViec_CheckList cl " +
                    "where cl.CongViecID=" + id + " for json auto) as checklist " +
                    "from TSK_CongViec cv left join NhanVien nv on nv.NhanVienID=cv.NguoiThucHienID where cv.CongViecID=" + id + "", con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    object Null = null;
                    string a = "";
                    a = dr["checklist"].ToString();
                    var abc = JsonConvert.DeserializeObject<List<checklist>>(a);
                    rs.Add(new TaskWorkModel()
                    {
                        TenCongViec = (string)dr["TenCongViec"],
                        NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau"],
                        NgayKetThuc = dr["NgayKetThuc"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc"],
                        NgayBatDau_ThucTe = dr["NgayBatDau_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau_ThucTe"],
                        NgayKetThuc_ThucTe = dr["NgayKetThuc_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc_ThucTe"],
                        MoTaCongViec = (string)dr["MoTaCongViec"],
                        Hotennguoithuchien = (string)dr["hotennguoithuchien"],
                        Nguoigiaoviec = dr["nguoigiaoviec"] == DBNull.Value ? (string)Null : (string)dr["nguoigiaoviec"],
                        CachTinhTyLeHoanThanh = (byte)dr["CachTinhTyLeHoanThanh"],
                        CheckList = abc
                    });
                    
                    
                    
                };
                dr.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return rs;
        }

        //cập nhật thời gian
        public static async Task<bool> updatetime(int? id)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = null;
                string sql = "select Count(NgayBatDau_ThucTe) from TSK_CongViec where CongViecID=" + id + "";
                cmd = new SqlCommand(sql, con);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result<=0)
                {
                    sql = "update TSK_CongViec set NgayBatDau_ThucTe = '" + DateTime.Now + "' where CongViecID=" + id + "";
                    cmd = new SqlCommand(sql, con);
                    int run = cmd.ExecuteNonQuery();
                    if (run > 0)
                    {
                        rs = true;
                    }
                }
                else
                {
                    rs = false;
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
        //update check list
        public static async Task<bool> updatechecklist(int? id, int? tyle)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString= ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update TSK_CongViec_CheckList set Tyle = " + tyle + " where CheckListID = " + id + "", con);
                var result = cmd.ExecuteNonQuery();
                if(result > 0)
                {
                    rs = true;
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
        public static async Task<List<TienDoCongViecModel>> gettiendo()
        {
            List<TienDoCongViecModel> rs = new List<TienDoCongViecModel>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select cv.CongViecID, cv.CongViecChaID, cv.TenCongViec, cv.NgayBatDau, NgayBatDau_ThucTe, " +
                    "cv.NgayKetThuc, cv.NgayKetThuc_ThucTe, cv.CachTinhTyLeHoanThanh, " +
                    "(select loai.LoaiCongViecID, loai.TenLoaiCongViec from (select cv.CongViecID, l.LoaiCongViecID, l.TenLoaiCongViec from TSK_CongViec_LoaiCongViec l left join TSK_CongViec cv " +
                    "on cv.LoaiCongViecID = l.LoaiCongViecID) as loai where loai.CongViecID = cv.CongViecID for json auto) as loai from TSK_CongViec cv", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    object Null = null;
                    string a = "";
                    a = dr["loai"].ToString();
                    var abc = JsonConvert.DeserializeObject<List<loaicongviec>>(a);
                    rs.Add(new TienDoCongViecModel()
                    {
                        CongViecID = (int)dr["CongViecID"],
                        CongViecChaID = (int?)dr["CongViecChaID"],
                        TenCongViec = (string)dr["TenCongViec"],
                        NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau"],
                        NgayBatDau_ThucTe = dr["NgayBatDau_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayBatDau_ThucTe"],
                        NgayKetThuc = dr["NgayKetThuc"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc"],
                        NgayKetThuc_ThucTe = dr["NgayKetThuc_ThucTe"] == DBNull.Value ? (DateTime?)Null : (DateTime?)dr["NgayKetThuc_ThucTe"],
                        CachTinhTyLeHoanThanh = (byte?)dr["CachTinhTyLeHoanThanh"],
                        loaicongviecs=abc

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
        #endregion
        // Danh sách công việc
        //var _object = db.Database.SqlQuery<TSK_DuAn_TrangThai>(sql);
        //public static async Task<(List<TSK_CongViec> rs, int total)> get(string _conditions = "", int _limit = 15, int _offset = 0, bool _all = false)
        //{
        //    int _total = 0;
        //    List<TSK_CongViec> _rs = new List<TSK_CongViec>();

        //    try
        //    {
        //        using (var db = new OosContext())
        //        {
        //            string sql = "SELECT * FROM [dbo].[TSK_CongViec]";
        //            if (!string.IsNullOrEmpty(_conditions))
        //                sql = string.Format("{0} WHERE {1}", sql, _conditions);
        //            var _data = db.TSK_CongViec.SqlQuery(sql);
        //            if (_data.Any())
        //            {
        //                _total = _data.Count();
        //                if (_all)
        //                    _rs = _data.OrderBy(x => x.CongViecID).ToList();
        //                else
        //                    _rs = _data.OrderBy(x => x.CongViecID).Skip(_offset).Take(_limit).ToList();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return (_rs, _total);
        //}

        ////// Chi tiết công việc
        ////public static async Task<TaskWorkModel> detail(int id)
        ////{
        ////    TaskWorkModel rs = new TaskWorkModel();
        ////    using (var db = new OosContext())
        ////    {
        ////        var CongViec = db.TSK_CongViec.Find(id); //TSK_CongViec
        ////        if(CongViec != null && CongViec.CongViecID > 0)
        ////        {
        ////            rs.CongViec = CongViec;

        ////            var NguoiPhoiHopThucHien = db.TSK_CongViec_NguoiPhoiHopThucHien.Where(x => x.CongViecID == id && x.IsActive == true);
        ////            if (NguoiPhoiHopThucHien.Any())
        ////                rs.NguoiPhoiHopThucHien = NguoiPhoiHopThucHien.ToList();

        ////            var NguoiTheoDoi = db.TSK_CongViec_NguoiTheoDoi.Where(x => x.CongViecID == id && x.IsActive == true);
        ////            if (NguoiTheoDoi.Any())
        ////                rs.NguoiTheoDoi = NguoiTheoDoi.ToList();

        ////            var CheckList = db.TSK_CongViec_CheckList.Where(x => x.CongViecID == id);
        ////            if (CheckList.Any())
        ////                rs.CheckList = CheckList.ToList();
        ////        }
        ////    }
        ////    return rs;
        ////}

        ////// Thêm mới Công việc
        ////public static async Task<bool> post(TaskWorkModel model)
        ////{
        ////    bool rs = false;
        ////    using (var db = new OosContext())
        ////    {
        ////        using (var transaction = db.Database.BeginTransaction())
        ////        {
        ////            try
        ////            {
        ////                // cong viec
        ////                db.TSK_CongViec.Add(model.CongViec);
        ////                await db.SaveChangesAsync();

        ////                // NguoiPhoiHopThucHien
        ////                if (model.NguoiPhoiHopThucHien.Any())
        ////                {
        ////                    foreach (var item in model.NguoiPhoiHopThucHien)
        ////                    {
        ////                        item.CongViecID = model.CongViec.CongViecID;
        ////                        item.CreatedByID = model.CongViec.CreatedByID;
        ////                        item.CreatedDate = model.CongViec.CreatedDate;
        ////                        item.IsActive = true;

        ////                        db.TSK_CongViec_NguoiPhoiHopThucHien.Add(item);
        ////                    }
        ////                    await db.SaveChangesAsync();
        ////                }

        ////                // NguoiTheoDoi
        ////                if (model.NguoiTheoDoi.Any())
        ////                {
        ////                    foreach (var item in model.NguoiTheoDoi)
        ////                    {
        ////                        item.CongViecID = model.CongViec.CongViecID;
        ////                        item.CreatedByID = model.CongViec.CreatedByID;
        ////                        item.CreatedDate = model.CongViec.CreatedDate;
        ////                        item.IsActive = true;

        ////                        db.TSK_CongViec_NguoiTheoDoi.Add(item);
        ////                    }
        ////                    await db.SaveChangesAsync();
        ////                }

        ////                // CheckList
        ////                if (model.CheckList.Any())
        ////                {
        ////                    foreach (var item in model.CheckList)
        ////                    {
        ////                        item.CongViecID = model.CongViec.CongViecID;
        ////                        db.TSK_CongViec_CheckList.Add(item);
        ////                    }
        ////                    await db.SaveChangesAsync();
        ////                }

        ////                transaction.Commit();
        ////                rs = true;
        ////            }catch(Exception ex)
        ////            {
        ////                transaction.Rollback();
        ////            }
        ////        }
        ////    }
        ////    return rs;
        ////}

        ////// Sửa thông tin Công việc
        //////db.TSK_CongViec_NguoiTheoDoi.Attach(item);
        //////db.Entry(model.CongViec).State = item.NguoiTheoDoiID > 0 ? EntityState.Added : EntityState.Modified;
        ////public static async Task<bool> put(int id, TaskWorkModel model)
        ////{
        ////    bool rs = false;
        ////    using (var db = new OosContext())
        ////    {
        ////        using (var transaction = db.Database.BeginTransaction())
        ////        {
        ////            try
        ////            {
        ////                db.TSK_CongViec.Attach(model.CongViec);
        ////                db.Entry(model.CongViec).State = EntityState.Modified;
        ////                await db.SaveChangesAsync();

        ////                bool flag = false;

        ////                // Cap nhat nguoi theo doi
        ////                db.TSK_CongViec_NguoiTheoDoi
        ////                    .Where(x =>
        ////                        x.CongViecID == id
        ////                        && x.IsActive == true
        ////                        && !model.NguoiTheoDoi.Select(i => i.NguoiTheoDoiID).ToList().Contains(x.NguoiTheoDoiID)
        ////                    )
        ////                    .ToList().ForEach(x =>
        ////                    {
        ////                        x.IsActive = false;
        ////                    });
        ////                await db.SaveChangesAsync();
        ////                if (model.NguoiTheoDoi.Any())
        ////                {
        ////                    foreach (var item in model.NguoiTheoDoi)
        ////                    {
        ////                        if (item.NguoiTheoDoiID == 0)
        ////                        {
        ////                            item.CreatedByID = model.CongViec.ModifyByID;
        ////                            item.CreatedDate = model.CongViec.ModifyDate;
        ////                            item.IsActive = true;
        ////                            db.TSK_CongViec_NguoiTheoDoi.Add(item);

        ////                            if (!flag)
        ////                                flag = true;
        ////                        }
        ////                    }
        ////                    if (flag)
        ////                        await db.SaveChangesAsync();
        ////                    flag = false;
        ////                }

        ////                // nguoi thuc hien
        ////                db.TSK_CongViec_NguoiPhoiHopThucHien
        ////                    .Where(x =>
        ////                        x.CongViecID == id
        ////                        && x.IsActive == true
        ////                        && !model.NguoiPhoiHopThucHien.Select(i => i.NguoiPhoiHopThucHienID).ToList().Contains(x.NguoiPhoiHopThucHienID)
        ////                    )
        ////                    .ToList().ForEach(x =>
        ////                    {
        ////                        x.IsActive = false;
        ////                    });
        ////                await db.SaveChangesAsync();
        ////                if (model.NguoiPhoiHopThucHien.Any())
        ////                {
        ////                    foreach (var item in model.NguoiPhoiHopThucHien)
        ////                    {
        ////                        if (item.NguoiPhoiHopThucHienID == 0)
        ////                        {
        ////                            item.CreatedByID = model.CongViec.ModifyByID;
        ////                            item.CreatedDate = model.CongViec.ModifyDate;
        ////                            item.IsActive = true;
        ////                            db.TSK_CongViec_NguoiPhoiHopThucHien.Add(item);

        ////                            if (!flag)
        ////                                flag = true;
        ////                        }
        ////                    }
        ////                    if (flag)
        ////                        await db.SaveChangesAsync();
        ////                    flag = false;
        ////                }

        ////                // checklist
        ////                db.TSK_CongViec_CheckList.RemoveRange(db.TSK_CongViec_CheckList.Where(x =>
        ////                    x.CongViecID == id
        ////                    && !model.CheckList.Select(i => i.CheckListID).ToList().Contains(x.CheckListID)
        ////                ));
        ////                await db.SaveChangesAsync();
        ////                if (model.CheckList.Any())
        ////                {
        ////                    foreach (var item in model.CheckList)
        ////                    {
        ////                        if (item.CheckListID == 0)
        ////                        {
        ////                            db.TSK_CongViec_CheckList.Add(item);
        ////                            if (!flag)
        ////                                flag = true;
        ////                        }
        ////                    }
        ////                    if (flag)
        ////                        await db.SaveChangesAsync();
        ////                    flag = false;
        ////                }

        ////                transaction.Commit();
        ////                rs = true;
        ////            }catch(Exception ex)
        ////            {
        ////                transaction.Rollback();
        ////            }
        ////        }
        ////    }
        ////    return rs;
        ////}

        //// Xóa Công việc
        //public static async Task<bool> delete(int id)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        using (var transaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                db.TSK_CongViec.RemoveRange(db.TSK_CongViec.Where(x => x.CongViecID == id));
        //                await db.SaveChangesAsync();

        //                db.TSK_CongViecLap.RemoveRange(db.TSK_CongViecLap.Where(x => x.CongViecID == id));
        //                await db.SaveChangesAsync();

        //                db.TSK_CongViec_CheckList.RemoveRange(db.TSK_CongViec_CheckList.Where(x => x.CongViecID == id));
        //                await db.SaveChangesAsync();

        //                db.TSK_CongViec_NguoiPhoiHopThucHien.RemoveRange(db.TSK_CongViec_NguoiPhoiHopThucHien.Where(x => x.CongViecID == id));
        //                await db.SaveChangesAsync();

        //                db.TSK_CongViec_NguoiTheoDoi.RemoveRange(db.TSK_CongViec_NguoiTheoDoi.Where(x => x.CongViecID == id));
        //                await db.SaveChangesAsync();

        //                db.TSK_CongViec_Reminder.RemoveRange(db.TSK_CongViec_Reminder.Where(x => x.CongViecID == id));
        //                await db.SaveChangesAsync();

        //                transaction.Commit();
        //                rs = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //            }
        //        }
        //    }
        //    return rs;
        //}

        //// Thay đổi trạng thái
        //public static async Task<bool> updateStatus(int id, int statusId, int userId)
        //{
        //    bool rs = false;

        //    DateTime actionTime = DateTime.Now;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec.Where(x => x.CongViecID == id).ToList().ForEach(x => {
        //            x.TrangThaiID = statusId;
        //            x.ModifyByID = userId;
        //            x.ModifyDate = actionTime;
        //        });

        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Checklist theo cong viec - **
        //public static async Task<List<TSK_CongViec_CheckList>> getCheckList(int id)
        //{
        //    List<TSK_CongViec_CheckList> rs = new List<TSK_CongViec_CheckList>();
        //    using (var db = new OosContext())
        //    {
        //        var data = db.TSK_CongViec_CheckList.Where(x => x.CongViecID == id);
        //        if (data.Any())
        //            rs = data.ToList();
        //    }
        //    return rs;
        //}

        //// Thêm checklist
        //public static async Task<bool> addCheckList(TSK_CongViec_CheckList model)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec_CheckList.Add(model);
        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Xóa checklist
        //public static async Task<bool> deleteCheckList(int id, int checkListId)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec_CheckList.RemoveRange(db.TSK_CongViec_CheckList.Where(x => x.CongViecID == id && x.CheckListID == checkListId));
        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Nguoi lien quan theo cong viec - **
        //public static async Task<List<TSK_CongViec_NguoiPhoiHopThucHien>> getEmployeeRelated(int id)
        //{
        //    List<TSK_CongViec_NguoiPhoiHopThucHien> rs = new List<TSK_CongViec_NguoiPhoiHopThucHien>();
        //    using (var db = new OosContext())
        //    {
        //        var data = db.TSK_CongViec_NguoiPhoiHopThucHien.Where(x => x.CongViecID == id && x.IsActive == true);
        //        if (data.Any())
        //            rs = data.ToList();
        //    }
        //    return rs;
        //}

        //// Thêm người liên quan
        //public static async Task<bool> addEmployeeRelated(TSK_CongViec_NguoiPhoiHopThucHien model)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec_NguoiPhoiHopThucHien.Add(model);
        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Xóa người liên quan
        //public static async Task<bool> deleteEmployeeRelated(int id, int employeeId)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec_NguoiPhoiHopThucHien.RemoveRange(db.TSK_CongViec_NguoiPhoiHopThucHien.Where(x => x.CongViecID == id && x.NguoiPhoiHopThucHienID == employeeId));
        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Nguoi theo doi theo cong viec - **
        //public static async Task<List<TSK_CongViec_NguoiTheoDoi>> getEmployeeControl(int id)
        //{
        //    List<TSK_CongViec_NguoiTheoDoi> rs = new List<TSK_CongViec_NguoiTheoDoi>();
        //    using (var db = new OosContext())
        //    {
        //        var data = db.TSK_CongViec_NguoiTheoDoi.Where(x => x.CongViecID == id && x.IsActive == true);
        //        if (data.Any())
        //            rs = data.ToList();
        //    }
        //    return rs;
        //}

        //// Thêm người theo doi
        //public static async Task<bool> addEmployeeControl(TSK_CongViec_NguoiTheoDoi model)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec_NguoiTheoDoi.Add(model);
        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Xóa người theo doi
        //public static async Task<bool> deleteEmployeeControl(int id, int employeeId)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {
        //        db.TSK_CongViec_NguoiTheoDoi.RemoveRange(db.TSK_CongViec_NguoiTheoDoi.Where(x => x.CongViecID == id && x.NguoiTheoDoiID == employeeId));
        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}

        //// Cập nhật tiến độ

        //public static async Task<bool> changeProgress(int id, int progress)
        //{
        //    bool rs = false;
        //    using (var db = new OosContext())
        //    {

        //    }
        //    return rs;
        //}

        //// Coppy công việc

        //public static async Task<bool> copyWork(int id, int userId)
        //{
        //    bool rs = false;

        //    DateTime actionTime = DateTime.Now;
        //    using (var db = new OosContext())
        //    {
        //        TSK_CongViec CongViec = db.TSK_CongViec.Find(id);
        //        if(CongViec != null && CongViec.CongViecID > 0)
        //        {
        //            CongViec.CongViecID = 0;
        //            CongViec.CreatedByID = userId;
        //            CongViec.CreatedDate = actionTime;

        //            db.TSK_CongViec.Add(CongViec);
        //            rs = await db.SaveChangesAsync() > 0 ? true : false;
        //        }
        //    }
        //    return rs;
        //}

        //// Phê duyệt

        //// Comment

        //// Thay đổi Phân công
        //public static async Task<bool> changeEployeeAssign(int id, int employeeId, int userId)
        //{
        //    bool rs = false;

        //    DateTime actionTime = DateTime.Now;
        //    using (var db = new OosContext())
        //    {
        //        var detail = db.TSK_CongViec.Find(id);
        //        detail.NguoiThucHienID = employeeId;
        //        detail.NguoiGiaoViecID = userId;
        //        detail.ModifyByID = userId;
        //        detail.ModifyDate = actionTime;

        //        db.TSK_CongViec.Attach(detail);
        //        db.Entry(detail).State = detail.CongViecID > 0 ? EntityState.Modified : EntityState.Added;

        //        rs = await db.SaveChangesAsync() > 0 ? true : false;
        //    }
        //    return rs;
        //}
    }
}
