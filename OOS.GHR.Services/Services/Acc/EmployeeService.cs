using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Framework.DynamicUI;
using OOS.GHR.Services.Helpers;
//using OOS.GHR.HRIS.Models;
using OOS.GHR.Services.Models.Ess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Acc
{
    public class EmployeeService
    {
        //var _object = db.Database.SqlQuery<TSK_DuAn_TrangThai>(sql);
        public static async Task<(List<NhanVien> rs, int total)> get(string _conditions = "", int _limit = 15, int _offset = 0, bool _all = false)
        {
            int _total = 0;
            List<NhanVien> _rs = new List<NhanVien>();

            try
            {
                using (var db = new OosContext())
                {
                    string sql = "SELECT * FROM [dbo].[NhanVien]";
                    if (!string.IsNullOrEmpty(_conditions))
                        sql = string.Format("{0} WHERE {1}", sql, _conditions);
                    var _data = db.NhanViens.SqlQuery(sql);
                    if (_data.Any())
                    {
                        _total = _data.Count();
                        if (_all)
                            _rs = _data.OrderBy(x => x.NhanVienID).ToList();
                        else
                            _rs = _data.OrderBy(x => x.NhanVienID).Skip(_offset).Take(_limit).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return (_rs, _total);
        }

        public static async void fileAttachment(SYS_FileStore model)
        {
            using (var db = new OosContext())
            {
                SYS_FileStore detail = db.SYS_FileStore.Where(x => x.NhanVienID == model.NhanVienID && x.Module == "NhanViens").FirstOrDefault();
                if(detail != null)
                {
                    detail.FileSourceName = model.FileSourceName;
                    detail.Directory = model.Directory;
                    detail.Module = model.Module;
                    detail.Size = model.Size;

                    db.SYS_FileStore.Attach(detail);
                    db.Entry(detail).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else
                {
                    db.SYS_FileStore.Add(model);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên -> Hiển thị lên Mobile và Portal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<object> getInfo(int id)
        {
            string strSQL = $@"SELECT A.*, XD.NgayDuyetCuoi ApprovedDate, XD.GhiChu ApproveComment, XD.TrangThai XeDuyet, case when XD.TrangThai=1 then XD.NguoiDuyetCuoi else XD.NguoiDangChoDuyet end Approver
            FROM(
            SELECT n1.MaNhanVien, n1.Ho+' '+n1.HoTen HoVaTen, n1.PhongBanID, n1.ChucVuID, n1.ChucDanhID, n1.LoaiHopDongID, n1.QueQuan, n1.SoPhepTheoQuyDinh, n1.SoPhepDaNghi, n1.NghiViec, n1.BaoCaoTrucTiepID,n2.Ho+' '+n2.HoTen as TenNguoiBaoCaoTrucTiep, 
            n1.NoiSinh, n1.GioiTinh, n1.QuocTichID, n1.DanTocID, n1.TonGiaoID, n1.TinhTrangHonNhan, n1.NgaySinh, n1.CMTND, n1.NgayCapCMT,n1.NoiCapCMT, 
            n1.DiaChiTT_SoNha ThuongTruSoNha, n1.DiaChiTT_TinhID ThuongTruTinhID, n1.DiaChiTT_QuanHuyenID ThuongTruQuanHuyenID, n1.DiaChiTT_XaPhuongID ThuongTruXaPhuongID,
            n1.DiaChiThuongTru,
            n1.DiaChi_SoNha HienTaiSoNha, n1.DiaChi_TinhID HienTaiTinhID, n1.DiaChi_QuanHuyenID HienTaiQuanHuyenID, n1.DiaChi_XaPhuongID HienTaiXaPhuongID,
            n1.DiaChi, n1.DienThoai, n1.Email, n1.EmailCaNhan, CV.TenChucVu, CD.TenChucDanh, PB.Ten TenPhongBan,
            n1.NgayBatDauLam, n1.NgayHetHanThuViec NgayChinhThuc, n1.MaSoThue, n1.MaBHXH,
            LHD.TenLoaiHopDong, n1.NgayKyHopDongLD, n1.NgayHetHanHopDong, n1.TrinhDoID, n1.TruongDaoTaoID, n1.ChuyenNganhID, n1.NamTotNghiep, HTN.Ten TenHangTotNghiep,
            n1.HangTotNghiepID, n1.TaiKhoanNganHang, n1.TenNganHang, n1.DiaChiNganHang, n1.Anh Picture, '' FileAttachment,
            (SELECT Max(LichSuThayDoiThongTinID) FROM SYS_LichSuThayDoiThongTin WHERE TenBang='NhanVien' AND KeyFieldName='NhanVienID' AND n1.NhanVienID={id}) as LichSuThayDoiID
            FROM  NhanVien n2, NhanVien n1
            left join NS_DsChucVu CV on CV.ChucVuID = n1.ChucVuID
            left join NS_DsChucDanh CD on CD.ChucDanhID = n1.ChucDanhID
            left join PhongBan PB on PB.PhongBanID = n1.PhongBanID
            left join NS_DsLoaiHopDong LHD on LHD.LoaiHopDongID = n1.LoaiHopDongID
            left join NS_DsHangTotNghiep HTN on HTN.TenHangTotNghiepTA=n1.HangTotNghiepID
            WHERE n1.NhanVienID={id} AND n1.BaoCaoTrucTiepID = n2.NhanVienID) as A
            left join SYS_XetDuyet XD on XD.ObjectID=A.LichSuThayDoiID AND XD.ObjectCode='SYS_LichSuThayDoiThongTin'";
            
            DataTable dtResult = OOS.GHR.Library.Database.ExecTable(strSQL);
            dynamic employee = new ExpandoObject();
            foreach(DataColumn dc in dtResult.Columns)
            {
                ((IDictionary<String, Object>)employee).Add(dc.ColumnName, dtResult.Rows[0][dc]);
            }
            return employee;
        }

        public static async Task<object> getEdit()
        {
            var rs = new object();
            using (var db = new OosContext())
            {

            }
            return rs;
        }

        public static async Task<bool> put(int id, ProfilePutModel model)
        {
            bool rs = false;
            DateTime actionTime = DateTime.Now;
            int result = -1;
            
            bool QTDuyet = ApproveService.AvailableNew("XD_SUATTNHANVIEN").Result;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            con.Open();
            try
            {
                int ThuongTruTinhID = 0;
                int ThuongTruQuanHuyenID = 0;
                int ThuongTruXaPhuongID = model.ThuongTruXaPhuongID ?? 0;
                NS_DsXaPhuong ThuongTruXP = await ApproveService.select_NS_DsXaPhuong(ThuongTruXaPhuongID);
                
                if (ThuongTruXP != null)
                {
                    ThuongTruTinhID = ThuongTruXP.TinhID ?? 0;
                    ThuongTruQuanHuyenID = ThuongTruXP.QuanHuyenID ?? 0;
                }

                int HienTaiTinhID = 0;
                int HienTaiQuanHuyenID = 0;
                int HienTaiXaPhuongID = model.HienTaiXaPhuongID ?? 0;
                NS_DsXaPhuong HienTaiXP = await ApproveService.select_NS_DsXaPhuong(HienTaiXaPhuongID);
                
                if (HienTaiXP != null)
                {
                    HienTaiTinhID = HienTaiXP.TinhID ?? 0;
                    HienTaiQuanHuyenID = HienTaiXP.QuanHuyenID ?? 0;
                }
                SqlCommand cm = new SqlCommand($@"select * from SYS_LichSuThayDoiThongTin where TenBang = 'NhanVien' and IDValue = {id} and XetDuyet = 0", con);

                SqlDataReader dr = cm.ExecuteReader();
                SYS_LichSuThayDoiThongTin lstd = new SYS_LichSuThayDoiThongTin();
                while (dr.Read())
                {
                    lstd.LichSuThayDoiThongTinID = (int)dr["LichSuThayDoiThongTinID"];
                    //lstd.TenBang = (string)dr["TenBang"];
                    //lstd.KeyFieldName = (string)dr["KeyFieldName"];
                    //lstd.IDValue = (int)dr["IDValue"];
                    //lstd.Mota = (string)dr["Mota"];
                    //lstd.NgayThayDoi = (DateTime)dr["NgayThayDoi"];
                    //lstd.XetDuyet = (int)dr["XetDuyet"];
                    //lstd.CreatedByID = (int)dr["CreatedByID"];
                    //lstd.CreatedDate = (DateTime)dr["CreatedDate"];
                    //lstd.ModifyByID = (int)dr["ModifyByID"];
                    //lstd.ModifyDate = (DateTime)dr["ModifyDate"];
                }
                dr.Close();
                cm = new SqlCommand("select * from NhanVien where NhanVienID = " + id, con);

                SqlDataReader dbemployee = cm.ExecuteReader();
                List<SYS_LichSuThayDoiThongTinChiTiet> lstdObj = new List<SYS_LichSuThayDoiThongTinChiTiet>();
                NhanVien employee = new NhanVien();
                
                while (dbemployee.Read())
                {
                    employee.Ho = (string)dbemployee["Ho"];
                    employee.HoTen = (string)dbemployee["HoTen"];
                    var pis = model.GetType().GetProperties();
                    foreach (var pi in pis)
                    {
                        try
                        {
                            if (dbemployee[pi.Name] != null)
                            {
                                object oNewValue = pi.GetValue(model);
                                if (oNewValue == null)
                                    oNewValue = "";
                                var opi = dbemployee[pi.Name];

                                if (opi != null)
                                {
                                    object oOldValue = dbemployee[pi.Name]; 
                                    if (oOldValue == null)
                                        oOldValue = "";
                                    if (oNewValue.ToString() != oOldValue.ToString())
                                    {
                                        string datatype = oNewValue.GetType().Name.ToLower();
                                        if (datatype.IndexOf(".") > 0)
                                            datatype = datatype.Substring(datatype.IndexOf(".") + 1); 
                                        var a = oNewValue.GetType();
                                        lstdObj.Add(new SYS_LichSuThayDoiThongTinChiTiet()
                                        {
                                            LichSuThayDoiThongTinID = lstd.LichSuThayDoiThongTinID,
                                            TruongThongTin = pi.Name,
                                            GiaTriCu = oOldValue.ToString(),
                                            GiaTriMoi = oNewValue.ToString(),
                                            MoTaGiaTriCu = "",
                                            MoTaGiaTriMoi = "",
                                            KieuDuLieu = datatype,
                                            GiaTriDouble = (oNewValue.GetType() == typeof(decimal) || oNewValue.GetType() == typeof(double)) ? (decimal)oNewValue : 0,
                                            GiaTriDateTime = oNewValue.GetType() == typeof(DateTime) ? (DateTime)oNewValue : (DateTime?)null
                                        });
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            string msg = ex.Message;
                            continue;
                        }
                    }
                }
                dbemployee.Close();
                if (lstdObj.Any())
                {
                    #region Cap nhat lich su thay doi
                    if (QTDuyet)
                    {
                        // Table: SYS_LichSuThayDoiThongTin
                        lstd.TenBang = "NhanVien";
                        lstd.KeyFieldName = "NhanVienID";
                        lstd.NgayThayDoi = actionTime;
                        lstd.XetDuyet = 0;
                        lstd.IDValue = id;
                        lstd.Mota = "Xét duyệt thay đổi thông tin nhân viên: " + string.Format("{0} {1}", employee.Ho, employee.HoTen);
                        if (lstd.LichSuThayDoiThongTinID > 0)
                        {
                            lstd.ModifyByID = id;
                            lstd.ModifyDate = actionTime;
                        }
                        else
                        {
                            lstd.CreatedByID = id;
                            lstd.CreatedDate = actionTime;
                        }

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        if (lstd.LichSuThayDoiThongTinID > 0)
                        { 
                            cmd.CommandText = "UPDATE SYS_LichSuThayDoiThongTin SET TenBang = '" + lstd.TenBang + "', KeyFieldName = '" + lstd.KeyFieldName + "', NgayThayDoi = '" + lstd.NgayThayDoi + "',XetDuyet = '" + lstd.XetDuyet + "', IDValue = '" + lstd.IDValue + 
                                "', Mota = '" + lstd.Mota + "' WHERE LichSuThayDoiThongTinID = " + lstd.LichSuThayDoiThongTinID;
                            
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO SYS_LichSuThayDoiThongTin (TenBang, KeyFieldName, NgayThayDoi, XetDuyet, IDValue, Mota, CreatedByID, ModifyByID, ModifyDate) " +
                                "VALUES ('" + lstd.TenBang + "', '" + lstd.KeyFieldName + "', '" + lstd.NgayThayDoi + "','" + lstd.XetDuyet + "','" + lstd.IDValue + "', '" + lstd.Mota + "', '" + lstd.CreatedByID + "', '" + lstd.ModifyByID + "', '" + lstd.ModifyDate + "');";
                        }

                        cmd.Connection = con;
                        result = cmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            con.Close();
                            return rs;
                        }

                        // Xet duyệt
                        BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet("XD_SUATTNHANVIEN", 0, new string[] { "@ID", "@ObjectID" }, new int[] { lstd.LichSuThayDoiThongTinID, lstd.IDValue.GetValueOrDefault(0) }, lstd.Mota, lstd.Mota, false, "SYS_LichSuThayDoiThongTin");
                    }
                    #endregion
                    #region Cap nhat thong tin tai khoan
                    if (!QTDuyet)
                    {
                        //NhanVien employee = new NhanVien();
                        employee.QueQuan = model.QueQuan;
                        employee.GioiTinh = model.GioiTinh;
                        employee.QuocTichID = model.QuocTichID;
                        employee.DanTocID = model.DanTocID;
                        employee.TonGiaoID = model.TonGiaoID;
                        employee.TinhTrangHonNhan = model.TinhTrangHonNhan;
                        employee.NgaySinh = model.NgaySinh;
                        employee.NoiSinh = model.NoiSinh;
                        employee.CMTND = model.CMTND;
                        employee.NgayCapCMT = model.NgayCapCMT;
                        employee.NoiCapCMT = model.NoiCapCMT;
                        employee.DiaChiTT_SoNha = model.ThuongTruSoNha;
                        employee.DiaChiTT_TinhID = ThuongTruTinhID;
                        employee.DiaChiTT_QuanHuyenID = ThuongTruQuanHuyenID;
                        employee.DiaChiTT_XaPhuongID = ThuongTruXaPhuongID;
                        employee.DiaChiThuongTru = model.DiaChiThuongTru;
                        employee.DiaChi_SoNha = model.HienTaiSoNha;
                        employee.DiaChi_TinhID = HienTaiTinhID;
                        employee.DiaChi_QuanHuyenID = HienTaiQuanHuyenID;
                        employee.DiaChi_XaPhuongID = HienTaiXaPhuongID;
                        employee.DiaChi = model.DiaChi;
                        employee.DienThoai = model.DienThoai;
                        employee.Email = model.Email;
                        employee.EmailCaNhan = model.EmailCaNhan;
                        employee.MaSoThue = model.MaSoThue;
                        employee.MaBHXH = model.MaBHXH;
                        employee.TrinhDoID = model.TrinhDoID;
                        employee.TruongDaoTaoID = model.TruongDaoTaoID;
                        employee.ChuyenNganhID = model.ChuyenNganhID;
                        employee.NamTotNghiep = model.NamTotNghiep;
                        employee.HangTotNghiepID = model.HangTotNghiepID;
                        employee.TaiKhoanNganHang = model.TaiKhoanNganHang;
                        employee.TenNganHang = model.TenNganHang;
                        employee.ModifyByID = id;
                        employee.ModifyDate = actionTime;
                        //if (!string.IsNullOrEmpty(model.Picture))
                        //{
                        //    string reSize = FileSizeHelpers.ResizeImage(Convert.FromBase64String(model.Picture));
                        //    byte[] newImg = Convert.FromBase64String(reSize);
                        //    if (employee.Anh != newImg)
                        //        employee.Anh = Convert.FromBase64String(reSize);
                        //    //employee.Anh = Convert.FromBase64String(model.Picture);
                        //}
                        string sql1 = "UPDATE NhanVien SET QueQuan = N'" + employee.QueQuan + "',NgaySinh = '"+employee.NgaySinh+"',GioiTinh = N'" + employee.GioiTinh + "',QuocTichID = " + employee.QuocTichID + 
                            ",DanTocID =" + employee.DanTocID + ",TonGiaoID = " + employee.TonGiaoID + ",TinhTrangHonNhan = N'" + employee.TinhTrangHonNhan + "',NoiSinh = N'" + employee.NoiSinh + "',CMTND = N'" + employee.CMTND + 
                            "',NgayCapCMT = '" + employee.NgayCapCMT + "',NoiCapCMT = N'" + employee.NoiCapCMT + "',DiaChiTT_SoNha = N'" + employee.DiaChiTT_SoNha + "',DiaChiTT_TinhID = " + employee.DiaChiTT_TinhID + 
                            ",DiaChiTT_QuanHuyenID = " + employee.DiaChiTT_QuanHuyenID + ",DiaChiThuongTru = N'" + employee.DiaChiThuongTru + "',DiaChi_SoNha = N'" + employee.DiaChi_SoNha + "',DiaChi_TinhID = " + employee.DiaChi_TinhID + 
                            ",DiaChi_QuanHuyenID = " + employee.DiaChi_QuanHuyenID + ",DiaChi_XaPhuongID = " + employee.DiaChi_XaPhuongID + ",DiaChi = N'" + employee.DiaChi + "',DienThoai = N'" + employee.DienThoai + 
                            "',Email = N'" + employee.Email + "',EmailCaNhan = N'" + employee.EmailCaNhan + "',MaSoThue= N'" + employee.MaSoThue + "',MaBHXH = N'" + employee.MaBHXH + "',TrinhDoID = " + employee.TrinhDoID + 
                            ",TruongDaoTaoID = " + employee.TruongDaoTaoID + ",ChuyenNganhID = " + employee.ChuyenNganhID + ",NamTotNghiep = " + employee.NamTotNghiep + ",HangTotNghiepID = " + employee.HangTotNghiepID + 
                            ",TaiKhoanNganHang = N'" + employee.TaiKhoanNganHang + "',TenNganHang = N'" + employee.TenNganHang + "',ModifyByID = " + employee.ModifyByID + ",ModifyDate = '" + employee.ModifyDate + "' where NhanVienID = " + id; //sửa đầy đủ đang dùng tạm
                        SqlCommand Sqlcmd = new SqlCommand();
                        Sqlcmd.CommandType = CommandType.Text;
                        Sqlcmd.CommandText = sql1;
                        Sqlcmd.Connection = con;
                        result = Sqlcmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            con.Close();
                            return rs;
                        }
                    }
                    #endregion
                    if (lstd.LichSuThayDoiThongTinID > 0)
                    {
                        SqlCommand Sqlcmd = new SqlCommand();
                        Sqlcmd.CommandType = CommandType.Text;
                        string sql = "";
                        
                        foreach (var item in lstdObj)
                        {
                            if(item.GiaTriDateTime != null)
                            {
                                sql += "INSERT INTO SYS_LichSuThayDoiThongTinChiTiet (LichSuThayDoiThongTinID, TruongThongTin, GiaTriCu, GiaTriMoi, MoTaGiaTriCu, MoTaGiaTriMoi, KieuDuLieu, GiaTriDouble, GiaTriDateTime) " +
                                    "VALUES ('" + item.LichSuThayDoiThongTinID + "', '"+item.TruongThongTin+"', '"+item.GiaTriCu+"', '"+item.GiaTriMoi+"','"+item.MoTaGiaTriCu+"', '"+item.MoTaGiaTriMoi+"', '"+item.KieuDuLieu+"', '"+item.GiaTriDouble+"','"+item.GiaTriDateTime+"');";
                            }
                            else
                            {
                                sql += "INSERT INTO SYS_LichSuThayDoiThongTinChiTiet (LichSuThayDoiThongTinID, TruongThongTin, GiaTriCu, GiaTriMoi, MoTaGiaTriCu, MoTaGiaTriMoi, KieuDuLieu, GiaTriDouble) " +
                                    "VALUES ('" + item.LichSuThayDoiThongTinID + "', '" + item.TruongThongTin + "', '" + item.GiaTriCu + "', '" + item.GiaTriMoi + "','" + item.MoTaGiaTriCu + "', '" + item.MoTaGiaTriMoi + "', '" + item.KieuDuLieu + "', '" + item.GiaTriDouble + "');";
                            }
                            
                        }

                        Sqlcmd.CommandText = sql;
                        Sqlcmd.Connection = con;
                        result = Sqlcmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            con.Close();
                            return rs;
                        }
                    }
                    rs = true;
                }
                else
                {
                    //update ảnh
                    //if (!string.IsNullOrEmpty(model.Picture))
                    //{
                    //    string reSize = FileSizeHelpers.ResizeImage(Convert.FromBase64String(model.Picture));
                    //    byte[] newImg = Convert.FromBase64String(reSize);
                    //    if (employee.Anh != newImg)
                    //    {
                    //        employee.Anh = Convert.FromBase64String(reSize);
                    //        db.NhanViens.Attach(employee);
                    //        db.Entry(employee).State = EntityState.Modified;
                    //        await db.SaveChangesAsync();
                    //    }
                    //}
                }

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            con.Close();
            return rs;
        }

        public static async Task<bool> put_Avata(int id, string Picture)
        {
            bool rs = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            con.Open();
            try
            {
            string reSize = FileSizeHelpers.ResizeImage(Convert.FromBase64String(Picture));
            byte[] newImg = Convert.FromBase64String(reSize);
                //string sql = "UPDATE NhanVien SET [Anh] = '" + newImg + "' where NhanVienID = " + id;
                SqlCommand Sqlcmd = new SqlCommand();
                Sqlcmd.CommandType = CommandType.Text;
                Sqlcmd.CommandText = "UPDATE NhanVien SET [Anh] = @newImg where NhanVienID = @idNV";
                SqlParameter Par_Avata = new SqlParameter("@newImg", SqlDbType.Image);
                SqlParameter Par_id = new SqlParameter("@idNV", SqlDbType.Int);
                Par_Avata.Value = newImg;
                Par_id.Value = id;
                Sqlcmd.Parameters.Add(Par_Avata);
                Sqlcmd.Parameters.Add(Par_id);
                Sqlcmd.Connection = con;
                int result = Sqlcmd.ExecuteNonQuery();
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
            }

            return rs;
        }

        public static async Task<bool> put_TTHopDong(int id, ProfilePutModel model)
        {
            bool rs = false;
            DateTime actionTime = DateTime.Now;
            int result = -1;

            bool QTDuyet = ApproveService.AvailableNew("XD_SUATTNHANVIEN").Result;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["ConnStr"].ToString();
            con.Open();
            try
            {
                int ThuongTruTinhID = 0;
                int ThuongTruQuanHuyenID = 0;
                int ThuongTruXaPhuongID = model.ThuongTruXaPhuongID ?? 0;
                NS_DsXaPhuong ThuongTruXP = await ApproveService.select_NS_DsXaPhuong(ThuongTruXaPhuongID);

                if (ThuongTruXP != null)
                {
                    ThuongTruTinhID = ThuongTruXP.TinhID ?? 0;
                    ThuongTruQuanHuyenID = ThuongTruXP.QuanHuyenID ?? 0;
                }

                int HienTaiTinhID = 0;
                int HienTaiQuanHuyenID = 0;
                int HienTaiXaPhuongID = model.HienTaiXaPhuongID ?? 0;
                NS_DsXaPhuong HienTaiXP = await ApproveService.select_NS_DsXaPhuong(HienTaiXaPhuongID);

                if (HienTaiXP != null)
                {
                    HienTaiTinhID = HienTaiXP.TinhID ?? 0;
                    HienTaiQuanHuyenID = HienTaiXP.QuanHuyenID ?? 0;
                }
                SqlCommand cm = new SqlCommand("select * from SYS_LichSuThayDoiThongTin where TenBang = 'NhanVien' and IDValue = " + id + " and XetDuyet = 0", con);

                SqlDataReader dr = cm.ExecuteReader();
                SYS_LichSuThayDoiThongTin lstd = new SYS_LichSuThayDoiThongTin();
                while (dr.Read())
                {
                    lstd.LichSuThayDoiThongTinID = (int)dr["LichSuThayDoiThongTinID"];
                    //lstd.TenBang = (string)dr["TenBang"];
                    //lstd.KeyFieldName = (string)dr["KeyFieldName"];
                    //lstd.IDValue = (int)dr["IDValue"];
                    //lstd.Mota = (string)dr["Mota"];
                    //lstd.NgayThayDoi = (DateTime)dr["NgayThayDoi"];
                    //lstd.XetDuyet = (int)dr["XetDuyet"];
                    //lstd.CreatedByID = (int)dr["CreatedByID"];
                    //lstd.CreatedDate = (DateTime)dr["CreatedDate"];
                    //lstd.ModifyByID = (int)dr["ModifyByID"];
                    //lstd.ModifyDate = (DateTime)dr["ModifyDate"];
                }
                dr.Close();
                cm = new SqlCommand("select * from NhanVien where NhanVienID = " + id, con);

                SqlDataReader dbemployee = cm.ExecuteReader();
                List<SYS_LichSuThayDoiThongTinChiTiet> lstdObj = new List<SYS_LichSuThayDoiThongTinChiTiet>();
                NhanVien employee = new NhanVien();
                List<string> flag = new List<string>() 
                {
                    "PhongBanID",
                    "ChucVuID",
                    "ChucDanhID",
                    "LoaiHopDongID",
                    "NgaybatDauLam",
                    "NgayKyhopDongLD",
                    "SoPhepDaNghi",
                    "SoPhepTheoQuyDinh"
                };
                while (dbemployee.Read())
                {
                    employee.Ho = (string)dbemployee["Ho"];
                    employee.HoTen = (string)dbemployee["HoTen"];
                    var pis = model.GetType().GetProperties();
                    foreach (var pi in pis)
                    {
                        try
                        {
                            if(flag.Contains(pi.Name))
                                if (dbemployee[pi.Name] != null)
                                {
                                    object oNewValue = pi.GetValue(model);
                                    if (oNewValue == null)
                                        oNewValue = "";
                                    var opi = dbemployee[pi.Name];

                                    if (opi != null)
                                    {
                                        object oOldValue = dbemployee[pi.Name];
                                        if (oOldValue == null)
                                            oOldValue = "";
                                        if (oNewValue.ToString() != oOldValue.ToString())
                                        {
                                            string datatype = oNewValue.GetType().Name.ToLower();
                                            if (datatype.IndexOf(".") > 0)
                                                datatype = datatype.Substring(datatype.IndexOf(".") + 1);
                                            var a = oNewValue.GetType();
                                            lstdObj.Add(new SYS_LichSuThayDoiThongTinChiTiet()
                                            {
                                                LichSuThayDoiThongTinID = lstd.LichSuThayDoiThongTinID,
                                                TruongThongTin = pi.Name,
                                                GiaTriCu = oOldValue.ToString(),
                                                GiaTriMoi = oNewValue.ToString(),
                                                MoTaGiaTriCu = "",
                                                MoTaGiaTriMoi = "",
                                                KieuDuLieu = datatype,
                                                GiaTriDouble = (oNewValue.GetType() == typeof(decimal) || oNewValue.GetType() == typeof(double)) ? (decimal)oNewValue : 0,
                                                GiaTriDateTime = oNewValue.GetType() == typeof(DateTime) ? (DateTime)oNewValue : (DateTime?)null
                                            });
                                        }
                                    }
                                }
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                            continue;
                        }
                    }
                }
                dbemployee.Close();
                if (lstdObj.Any())
                {
                    #region Cap nhat lich su thay doi
                    if (QTDuyet)
                    {
                        // Table: SYS_LichSuThayDoiThongTin
                        lstd.TenBang = "NhanVien";
                        lstd.KeyFieldName = "NhanVienID";
                        lstd.NgayThayDoi = actionTime;
                        lstd.XetDuyet = 0;
                        lstd.IDValue = id;
                        lstd.Mota = "Xét duyệt thay đổi thông tin nhân viên: " + string.Format("{0} {1}", employee.Ho, employee.HoTen);
                        if (lstd.LichSuThayDoiThongTinID > 0)
                        {
                            lstd.ModifyByID = id;
                            lstd.ModifyDate = actionTime;
                        }
                        else
                        {
                            lstd.CreatedByID = id;
                            lstd.CreatedDate = actionTime;
                        }

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        if (lstd.LichSuThayDoiThongTinID > 0)
                        {
                            cmd.CommandText = "UPDATE SYS_LichSuThayDoiThongTin SET TenBang = '" + lstd.TenBang + "', KeyFieldName = '" + lstd.KeyFieldName + "', NgayThayDoi = '" + lstd.NgayThayDoi + 
                                "',XetDuyet = '" + lstd.XetDuyet + "', IDValue = '" + lstd.IDValue + "', Mota = '" + lstd.Mota + "' WHERE LichSuThayDoiThongTinID = " + lstd.LichSuThayDoiThongTinID;

                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO SYS_LichSuThayDoiThongTin (TenBang, KeyFieldName, NgayThayDoi, XetDuyet, IDValue, Mota, CreatedByID, ModifyByID, ModifyDate) " +
                                "VALUES ('" + lstd.TenBang + "', '" + lstd.KeyFieldName + "', '" + lstd.NgayThayDoi + "','" + lstd.XetDuyet + "','" + lstd.IDValue + "', '" + lstd.Mota + "', '" + lstd.CreatedByID + "', '" + lstd.ModifyByID + "', '" + lstd.ModifyDate + "');";
                        }

                        cmd.Connection = con;
                        result = cmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            con.Close();
                            return rs;
                        }

                        // Xet duyệt
                        BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet("XD_SUATTNHANVIEN", 0, new string[] { "@ID", "@ObjectID" }, new int[] { lstd.LichSuThayDoiThongTinID, lstd.IDValue.GetValueOrDefault(0) }, lstd.Mota, lstd.Mota, false, "SYS_LichSuThayDoiThongTin");
                    }
                    #endregion

                    #region Cap nhat thong tin tai khoan
                    if (!QTDuyet)
                    {
                        employee.NgayKyHopDongLD = model.NgayKyHopDongLD;
                        employee.NgayBatDauLam = model.NgaybatDauLam;
                        employee.SoPhepTheoQuyDinh = model.SoPhepTheoQuyDinh;
                        employee.SoPhepDaNghi = model.SoPhepDaNghi;
                        employee.PhongBanID = model.PhongBanID;
                        employee.ChucVuID = model.ChucVuID;
                        employee.ChucDanhID = model.ChucDanhID;
                        employee.LoaiHopDongID = model.LoaiHopDongID;
                        employee.ModifyByID = id;
                        employee.ModifyDate = actionTime;
                        //if (!string.IsNullOrEmpty(model.Picture))
                        //{
                        //    string reSize = FileSizeHelpers.ResizeImage(Convert.FromBase64String(model.Picture));
                        //    byte[] newImg = Convert.FromBase64String(reSize);
                        //    if (employee.Anh != newImg)
                        //        employee.Anh = Convert.FromBase64String(reSize);
                        //    //employee.Anh = Convert.FromBase64String(model.Picture);
                        //}
                        string sql1 = "UPDATE NhanVien SET PhongBanID = " + employee.PhongBanID + " ,SoPhepTheoQuyDinh='"+employee.SoPhepTheoQuyDinh + "' ,NgayBatDauLam='"+employee.NgayBatDauLam + "' ,NgayKyHopDongLD='"+employee.NgayKyHopDongLD + 
                            "' ,SoPhepDaNghi='" + employee.SoPhepDaNghi + "' ,ChucVuID = " + employee.ChucVuID + " ,ChucDanhID = " + employee.ChucDanhID + " ,LoaiHopDongID = " + employee.LoaiHopDongID + " ,ModifyByID = " + employee.ModifyByID + 
                            ",ModifyDate = '" + employee.ModifyDate + "' where NhanVienID = " + id;
                        SqlCommand Sqlcmd = new SqlCommand();
                        Sqlcmd.CommandType = CommandType.Text;
                        Sqlcmd.CommandText = sql1;
                        Sqlcmd.Connection = con;
                        result = Sqlcmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            con.Close();
                            return rs;
                        }
                       
                    }
                    #endregion
                    if (lstd.LichSuThayDoiThongTinID > 0)
                    {
                        SqlCommand Sqlcmd = new SqlCommand();
                        Sqlcmd.CommandType = CommandType.Text;
                        string sql = "";

                        foreach (var item in lstdObj)
                        {
                            if (item.GiaTriDateTime != null)
                            {
                                sql += "INSERT INTO SYS_LichSuThayDoiThongTinChiTiet (LichSuThayDoiThongTinID, TruongThongTin, GiaTriCu, GiaTriMoi, MoTaGiaTriCu, MoTaGiaTriMoi, KieuDuLieu, GiaTriDouble, GiaTriDateTime) " +
                                    "VALUES ('" + item.LichSuThayDoiThongTinID + "', '" + item.TruongThongTin + "', '" + item.GiaTriCu + "', '" + item.GiaTriMoi + "','" + item.MoTaGiaTriCu + "', '" + item.MoTaGiaTriMoi + "', '" + item.KieuDuLieu + "', '" + item.GiaTriDouble + "','" + item.GiaTriDateTime + "');";
                            }
                            else
                            {
                                sql += "INSERT INTO SYS_LichSuThayDoiThongTinChiTiet (LichSuThayDoiThongTinID, TruongThongTin, GiaTriCu, GiaTriMoi, MoTaGiaTriCu, MoTaGiaTriMoi, KieuDuLieu, GiaTriDouble) " +
                                    "VALUES ('" + item.LichSuThayDoiThongTinID + "', '" + item.TruongThongTin + "', '" + item.GiaTriCu + "', '" + item.GiaTriMoi + "','" + item.MoTaGiaTriCu + "', '" + item.MoTaGiaTriMoi + "', '" + item.KieuDuLieu + "', '" + item.GiaTriDouble + "');";
                            }

                        }

                        Sqlcmd.CommandText = sql;
                        Sqlcmd.Connection = con;
                        result = Sqlcmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            con.Close();
                            return rs;
                        }
                    }
                    rs = true;
                }
                else
                {
                    //if (!string.IsNullOrEmpty(model.Picture))
                    //{
                    //    string reSize = FileSizeHelpers.ResizeImage(Convert.FromBase64String(model.Picture));
                    //    byte[] newImg = Convert.FromBase64String(reSize);
                    //    if (employee.Anh != newImg)
                    //    {
                    //        employee.Anh = Convert.FromBase64String(reSize);
                    //        db.NhanViens.Attach(employee);
                    //        db.Entry(employee).State = EntityState.Modified;
                    //        await db.SaveChangesAsync();
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            con.Close();
            return rs;
        }
    }
}
