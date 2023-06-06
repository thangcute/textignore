using OOS.GHR.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Helpers
{
    public class Business
    {
        public static string GetHoVaTenByUser(int NguoiDungID, string conID = "")
        {
            object o = OOS.GHR.Library.Database.ExecScalar("SELECT Ho+' '+HoTen FROM NhanVien WHERE NhanVienID=(SELECT TOP 1 NhanVienID FROM SYS_NguoiDung WHERE NguoiDungID=" + NguoiDungID + ")", false, conID);
            if (o != null && o != DBNull.Value)
                return o.ToString();
            else
                return SYS_NguoiDung.GetTenDangNhapByID(NguoiDungID);
        }

        static string CanhBao_GioLamThem(int NhanVienID, decimal SoGioLam)
        {
            string CanhBaoStr = "";
            decimal LTNgay = OOS.GHR.Library.Database.ToDecimal(Database.ExecScalar(@"SELECT SUM(ISNULL(TGLamThem, 0)) FROM NS_TL_CC_TongHopTheoNgay WHERE 
                    NhanVienID = " + NhanVienID + @" AND day(NgayChamCong) = day(getdate()) and Month(NgayChamCong) = Month(getdate()) 
                    AND Year(NgayChamCong) = Year(getdate())) AND XetDuyet=1"));

            if (LTNgay + SoGioLam >= 2)
                CanhBaoStr = "TC nhiều 1 ngày/ 1 day OT too much: " + (LTNgay + SoGioLam).ToString("n2");

            if (CanhBaoStr == "")
            {
                decimal LTThang = Database.ToDecimal(Database.ExecScalar(@"SELECT SUM(ISNULL(TGLamThem, 0)) FROM NS_TL_CC_TongHopTheoNgay WHERE 
                    NhanVienID = " + NhanVienID + @" AND Month(NgayChamCong) = Month(getdate()) AND Year(NgayChamCong) = Year(getdate())) AND XetDuyet=1"));
                if (LTThang >= 25)
                    CanhBaoStr = "30hrs/month: " + LTThang.ToString("n2");
            }

            if (CanhBaoStr == "")
            {
                decimal LTNam = Database.ToDecimal(Database.ExecScalar(@"SELECT SUM(ISNULL(TGLamThem, 0)) FROM NS_TL_CC_TongHopTheoNgay 
                    WHERE NhanVienID = " + NhanVienID + @" AND Year(NgayChamCong) = Year(getdate())) AND XetDuyet=1"));
                if (LTNam >= 295)
                    CanhBaoStr = "300hrs/year: " + LTNam.ToString("n2");
            }

            if (CanhBaoStr == "")
            {
                decimal CheDo = Database.ToDecimal(Database.ExecScalar(@"SELECT Count(*) FROM NS_QTThaiSan 
                WHERE NhanVienID = " + NhanVienID + @" AND
                ((NgayBatDau<=GetDate() AND NgayKetThuc>=GetDate()) 
                OR (DATEADD(Year, 1, NgaySinhCon)>=GetDate() 
                OR (DATEADD(Month,-3,NgayDuKienSinh)<=GetDate() AND NgayDuKienSinh>=GetDate()) )) AND XetDuyet=1"));
                if (CheDo > 0)
                    CanhBaoStr = "Phụ nữ chế độ/ Preganancy & nursing";
            }

            if (CanhBaoStr == "")
            {
                int Count = Database.ToInt(Database.ExecScalar(@"select
	            case 
                    when((gioitinh = 'M' or gioitinh = 'Nam' or GioiTinh = 'Male') AND datediff(day, NgaySinh, Getdate()) / 365 >= 60) then 1
                    when((gioitinh = 'F' or gioitinh = N'Nữ' or GioiTinh = 'Female') AND datediff(day, NgaySinh, Getdate())/ 365 >= 55) then 1
		            else 0
                end
                from nhanvien where nhanvienid=" + NhanVienID));
                if (Count > 0)
                    CanhBaoStr = "Người cao tuổi/ Elder people";
            }

            return CanhBaoStr;
        }

        public static bool IsOKPermission(string ID, SortedList<string, string> QuyenList, bool NotExitsIsTrue = false)
        {
            if (QuyenList == null)
                return false;
            if (ID == null || ID == "")
                return true;

            if (ID.Contains("|"))
            {
                string[] codes = ID.Split('|');
                foreach (string cc in codes)
                {
                    if (QuyenList.ContainsKey(cc))
                    {
                        if (QuyenList[cc].ToString() == "1")
                            return true;
                    }
                }
                return NotExitsIsTrue;
            }

            if (QuyenList.ContainsKey(ID))
                return QuyenList[ID].ToString() == "1";

            return NotExitsIsTrue;
        }

        public static System.Data.DataTable GetNhanVienList(int DepartmentId, int PageIndex = 0, int PageSize = 10)
        {
            return OOS.GHR.Library.Database.ExecTable(@"SELECT NhanVienID, Ho, HoTen, DienThoai, Email, Anh, 
            NS_DsChucDanh.TenChucDanh FROM NhanVien INNER JOIN NS_DsChucDanh ON NS_DsChucDanh.ChucDanhID = NhanVien.ChucDanhID WHERE PhongBanID = " + DepartmentId +
            " And (NghiViec=0 OR NghiViec IS NULL) AND (XetDuyet=1) AND (IsNull(IsDeleted,0)=0)" +
            " ORDER BY NS_DsChucDanh.ThuTu, MaNhanVien OFFSET " + (PageIndex * PageSize) + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY");
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static byte[] AvatarToBinary(object Avatar, char c)
        {
            if (Avatar == null || Avatar == DBNull.Value)
            {
                return new byte[] { };
            }
            var data = (byte[])Avatar;
            return data;
        }

        public static string ToBase64String(object data)
        {
            return data != null && data != DBNull.Value ? Convert.ToBase64String((byte[])data) : "";
        }

        public static string AvatarToBase64String(object Avatar, char c)
        {
            return Convert.ToBase64String(AvatarToBinary(Avatar, c));
        }

        public static DataTable LoadBangLuongNV
(int PhongBanID, string PhongBanIDS, int Thang, int Nam, out SortedList<int, ArrayList> lstChinhSua,
 string CongThucLuongID = "", string ChucDanhID = "", string NhanVienStr = "", bool IsLocked = false, bool IncludeSub = true)
        {
            DataTable dtSourceAlls = new DataTable();
            dtSourceAlls.Columns.Add("Chon", typeof(bool));

            NS_TL_LoaiLuongList llList = NS_TL_LoaiLuongList.GetNS_TL_LoaiLuongListFromCommand
            ("SELECT * FROM NS_TL_LoaiLuong ORDER BY ThuTuHienThi");
            string LL = "";
            string LL3 = "";
            string LLNQD = "";
            string LLNQD1 = "";

            foreach (NS_TL_LoaiLuong lluong in llList)
            {
                if (lluong.ThuTuHienThi <= 0)
                    continue;
                if (lluong.LamTron != -100)
                {
                    LL += "ISNULL(pvtb.[" + lluong.MaSo + "],0) AS '" + lluong.MaSo + "',";
                    LL3 += "[" + lluong.MaSo + "],";
                }
                else
                {
                    LLNQD += "ISNULL(pvtb1.[" + lluong.MaSo + "],0) AS '" + lluong.MaSo + "',";
                    LLNQD1 += "[" + lluong.MaSo + "],";
                }
            }

            if (LL.Length > 0)
                LL = LL.Substring(0, LL.Length - 1);

            if (LL3.Length > 0)
                LL3 = LL3.Substring(0, LL3.Length - 1);

            if (LLNQD.Length > 0)
                LLNQD = LLNQD.Substring(0, LLNQD.Length - 1);

            if (LLNQD1.Length > 0)
                LLNQD1 = LLNQD1.Substring(0, LLNQD1.Length - 1);

            string strWherePB = "";

            if (PhongBanID > 0)
            {
                strWherePB = PhongBanIDS;
                if (strWherePB != "")
                    strWherePB = " AND NS_TL_BangLuong.PhongBanID in (" + strWherePB + ")";
                else
                {
                    if (IncludeSub)
                    {
                        string strPhongBanStr = "";
                        DataTable dtPbs = Database.ExecTable("SELECT Valu FROM GetPhongBanChild_WUser(" + PhongBanID + "," + DatabaseCache.DangNhap.NguoiDungID + ")");
                        if (dtPbs.Rows.Count <= 1) strPhongBanStr = "(" + PhongBanID.ToString() + ")";
                        else
                        {
                            foreach (DataRow dr in dtPbs.Rows)
                            {
                                strPhongBanStr += dr[0].ToString() + ",";
                            }
                            strPhongBanStr = "(" + strPhongBanStr.Remove(strPhongBanStr.Length - 1, 1) + ")";
                        }
                        strWherePB = " AND NS_TL_BangLuong.PhongBanID in " + strPhongBanStr;
                    }
                    else
                        strWherePB = " AND NS_TL_BangLuong.PhongBanID = " + PhongBanID;
                }
            }

            if (NhanVienStr != "")
                strWherePB += string.Format(" And (MaNhanVien like N'%{0}%' Or HoVaTen like N'%{0}%')", NhanVienStr);

            if (ChucDanhID != null && ChucDanhID.Length > 0)
            {
                if (ChucDanhID.Contains(","))
                    strWherePB += " AND NhanVien.ChucDanhID in (" + ChucDanhID + ")";
                else
                    if (Database.ToInt(ChucDanhID) > 0)
                    strWherePB += " AND NhanVien.ChucDanhID = " + ChucDanhID;
            }

            if (CongThucLuongID != null && CongThucLuongID.Length > 0)
            {
                if (CongThucLuongID.Contains(","))
                    strWherePB += " AND NS_TL_BangLuong.NhomLuongID in (" + CongThucLuongID + ")";
                else
                    if (Database.ToInt(CongThucLuongID) > 0)
                    strWherePB += " AND NS_TL_BangLuong.NhomLuongID = " + CongThucLuongID;
            }

            //Chức Danh >=0 -> Chỉ được phép xem lương của người có chức danh >= Định nghĩa
            if (DatabaseCache.DangNhap.CB_TuCapBac > 0)
                strWherePB += " AND ISNULL(NS_DsChucDanh.ThuTu,0)>=" + DatabaseCache.DangNhap.CB_TuCapBac;

            string SQL = "";

            int CC_TuNgay = ConfigClass.NBDCC;
            int CC_ToiNgay = ConfigClass.NKTCC;

            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            DateTime dtHienTai = new DateTime(Nam, Thang, 1);
            if (CC_TuNgay > 0 && CC_ToiNgay > 0)
            {
                dtFrom = DatabaseBase.GetFromDate(new DateTime(dtHienTai.AddMonths(-1).Year, dtHienTai.AddMonths(-1).Month, CC_TuNgay));
                dtTo = DatabaseBase.GetToDate(new DateTime(Nam, Thang, CC_ToiNgay));
            }
            else
            {
                dtFrom = DatabaseBase.GetFromDate(dtHienTai);
                dtTo = DatabaseBase.GetToDate(new DateTime(Nam, Thang, DateTime.DaysInMonth(Nam, Thang)));
            }

            SortedList slParamsDate = new SortedList();
            slParamsDate.Add("@FromDate", dtFrom);
            slParamsDate.Add("@ToDate", dtTo);

            string sqlthang = "";
            sqlthang = "(NS_TL_BangLuong.Thang = " + Thang + ") AND (NS_TL_BangLuong.Nam = " + Nam + ")";

            string strBL_NhanVienID = "";
            strBL_NhanVienID = "[IsEmailSent], [MaDotTinhLuong], [BangLuongID],[NhanVienID],[Thang],[Nam],[PhongBanID],[ChucVuID],[NgayQuyetDinh],[GhiChu],[QTDienBienLuongID],[ChucDanhID],[NhomLuongID]";

            if (LLNQD1 != "")
            {
                SQL =
                string.Format(
                @"SELECT pvtb.PhongBanID, ThangTinhLuong, NamTinhLuong, MaDotTinhLuong,
                         pvtb.MaNhanVien,  pvtb.HoVaTen, pvtb.Email, pvtb.ten AS TenPhongBan, pvtb.IsEmailSent,
                         pvtb.ThuTuPhongBan, pvtb.NgayHetHanHopDong, {0}, {1},
                         pvtb.BangLuongID AS ID,pvtb.NhanVienID, pvtb.QTDienBienLuongID, pvtb.GhiChu, Convert(bit, 0) as N'Chọn'
                FROM 
                (SELECT    NS_TL_ChiTietLuong.TienLuong, PhongBan.PhongBanID,
                            (PhongBan.MaPhongBan+' - '+ PhongBan.Ten) as Ten, PhongBan.ThuTu as ThuTuPhongBan, NhanVien.MaNhanVien,  NhanVien.HoVaTen, NS_TL_LoaiLuong.MaSo, 
                            NS_TL_BangLuong.BangLuongID, NhanVien.Email,
                            NS_TL_BangLuong.GhiChu, NS_TL_BangLuong.IsEmailSent,
                            NS_TL_BangLuong.NhanVienID,
                            NS_TL_BangLuong.QTDienBienLuongID,
                            NgayHetHanHopDong,NhanVien.MaNVCu,
                            NhanVien.ChucDanhID, NS_TL_BangLuong.MaDotTinhLuong,
                            ISNULL(NS_DsChucDanh.ThuTu,0) as TTChucDanh, NS_TL_BangLuong.Thang as ThangTinhLuong, NS_TL_BangLuong.Nam as NamTinhLuong
                FROM        (SELECT " + strBL_NhanVienID + @" FROM NS_TL_BangLuong) as NS_TL_BangLuong INNER JOIN
                            (SELECT * FROM NS_TL_ChiTietLuong WHERE ThangTL={6} AND NamTL={7}) as NS_TL_ChiTietLuong ON NS_TL_BangLuong.BangLuongID = NS_TL_ChiTietLuong.BangLuongID INNER JOIN
                            (SELECT     NhanVien_1.ChucDanhID, NhanVien_1.NhanVienID, NhanVien_1.MaNhanVien,
                                        NhanVien_1.Ho + ' ' + NhanVien_1.HoTen AS HoVaTen, NhanVien_1.Email,
                                        Convert(Nvarchar, NgayHetHanHopDong,103) as NgayHetHanHopDong,NhanVien_1.MaNVCu
                                FROM       NhanVien AS NhanVien_1
                                WHERE      (NhanVien_1.XetDuyet=1) AND (IsNull(NhanVien_1.IsDeleted,0)=0)
                            ) AS NhanVien ON NS_TL_BangLuong.NhanVienID = NhanVien.NhanVienID INNER JOIN
                        PhongBan ON PhongBan.PhongBanID = NS_TL_BangLuong.PhongBanID INNER JOIN
                        NS_TL_LoaiLuong ON NS_TL_ChiTietLuong.LoaiLuongID = NS_TL_LoaiLuong.LoaiLuongID
                        LEFT JOIN NS_DsChucDanh on NhanVien.ChucDanhID = NS_DsChucDanh.ChucDanhID
                WHERE     {2} {3} )AS A
                                    PIVOT (Max(TienLuong) FOR MaSo IN({4})) AS PVTB FULL JOIN
                (SELECT     ISNULL(NS_TL_ChiTietLuong.GhiChu,'') as TienLuong, (PhongBan.MaPhongBan+' - '+ PhongBan.Ten) as Ten, 
                            NS_TL_LoaiLuong.MaSo,
                            NS_TL_BangLuong.NhanVienID,
                            NS_TL_BangLuong.BangLuongID
                FROM        (SELECT " + strBL_NhanVienID + @" FROM NS_TL_BangLuong) as NS_TL_BangLuong INNER JOIN
                            (SELECT * FROM NS_TL_ChiTietLuong WHERE ThangTL={6} AND NamTL={7}) as NS_TL_ChiTietLuong ON NS_TL_BangLuong.BangLuongID = NS_TL_ChiTietLuong.BangLuongID INNER JOIN
                            (SELECT     NhanVien_1.ChucDanhID, NhanVien_1.NhanVienID, NhanVien_1.MaNhanVien, 
                                        NhanVien_1.Ho + ' ' + NhanVien_1.HoTen AS HoVaTen, NhanVien_1.Email,
                                        Convert(Nvarchar, NgayHetHanHopDong,103) as NgayHetHanHopDong
                                FROM  NhanVien as NhanVien_1
                                WHERE (NhanVien_1.XetDuyet=1) AND (IsNull(NhanVien_1.IsDeleted,0)=0)
                            ) AS NhanVien ON NS_TL_BangLuong.NhanVienID = NhanVien.NhanVienID INNER JOIN
                        PhongBan ON PhongBan.PhongBanID = NS_TL_BangLuong.PhongBanID INNER JOIN
                        NS_TL_LoaiLuong ON NS_TL_ChiTietLuong.LoaiLuongID = NS_TL_LoaiLuong.LoaiLuongID
                        LEFT JOIN NS_DsChucDanh on NhanVien.ChucDanhID = NS_DsChucDanh.ChucDanhID
                WHERE     {2} {3})AS A
                        PIVOT (Max(TienLuong) FOR MaSo IN({5})) AS PVTB1 ON PVTB.BangLuongID = PVTB1.BangLuongID
                Order By pvtb.TTChucDanh", LL, LLNQD, sqlthang, strWherePB, LL3, LLNQD1, Thang, Nam);
            }
            else
            {
                SQL = string.Format(
                @"SELECT pvtb.PhongBanID, ThangTinhLuong, NamTinhLuong, MaDotTinhLuong,
                pvtb.MaNhanVien,  pvtb.HoVaTen, pvtb.Email, pvtb.Ten AS TenPhongBan,  pvtb.IsEmailSent,
                pvtb.ThuTuPhongBan, pvtb.NgayHetHanHopDong, {0},
                pvtb.BangLuongID AS ID,pvtb.NhanVienID, pvtb.QTDienBienLuongID, pvtb.GhiChu, Convert(bit, 0) as N'Chọn'
                FROM 
                (SELECT     NS_TL_ChiTietLuong.TienLuong, NS_TL_BangLuong.MaDotTinhLuong,
                            (PhongBan.MaPhongBan+' - '+ PhongBan.Ten) as Ten, PhongBan.ThuTu as ThuTuPhongBan, NhanVien.MaNhanVien, PhongBan.PhongbanID,
                            NhanVien.HoVaTen, NS_TL_LoaiLuong.MaSo, NhanVien.MaNVCu, NhanVien.Email,
                            NS_TL_BangLuong.BangLuongID, NS_TL_BangLuong.IsEmailSent,
                            NS_TL_BangLuong.GhiChu,NS_TL_BangLuong.NhanVienID,NS_TL_BangLuong.QTDienBienLuongID,
                            NgayHetHanHopDong,
                            NhanVien.ChucDanhID,
                            ISNULL(NS_DsChucDanh.ThuTu,0) as TTChucDanh, NS_TL_BangLuong.Thang as ThangTinhLuong, NS_TL_BangLuong.Nam as NamTinhLuong
                FROM        (SELECT " + strBL_NhanVienID + @" FROM NS_TL_BangLuong) as NS_TL_BangLuong INNER JOIN
                            (SELECT * FROM NS_TL_ChiTietLuong WHERE ThangTL={4} AND NamTL={5}) NS_TL_ChiTietLuong ON NS_TL_BangLuong.BangLuongID = NS_TL_ChiTietLuong.BangLuongID INNER JOIN
                            (SELECT     NhanVien_1.ChucDanhID, NhanVien_1.NhanVienID, NhanVien_1.MaNhanVien, 
                                        NhanVien_1.Ho + ' ' + NhanVien_1.HoTen AS HoVaTen,  NhanVien_1.Email,
                                        Convert(Nvarchar, NgayHetHanHopDong,103) as NgayHetHanHopDong,NhanVien_1.MaNVCu
                                FROM       NhanVien AS NhanVien_1
                                WHERE      (NhanVien_1.XetDuyet=1) AND (IsNull(NhanVien_1.IsDeleted,0)=0)
                            ) AS NhanVien ON NS_TL_BangLuong.NhanVienID = NhanVien.NhanVienID INNER JOIN
                        PhongBan ON PhongBan.PhongBanID = NS_TL_BangLuong.PhongBanID INNER JOIN
                        NS_TL_LoaiLuong ON NS_TL_ChiTietLuong.LoaiLuongID = NS_TL_LoaiLuong.LoaiLuongID
                        LEFT JOIN NS_DsChucDanh on NhanVien.ChucDanhID = NS_DsChucDanh.ChucDanhID
                WHERE     {1} {2}) AS A
                        PIVOT (Max(TienLuong) FOR MaSo IN({3})) AS PVTB ORDER By PVTB.TTChucDanh", LL, sqlthang, strWherePB, LL3, Thang, Nam);
            }
            bool Encrypt = ConfigClass.ENCRYPTPAYROLL;
            string conID = ConnectionManager.BeginConnectionID();
            try
            {
                if (Encrypt)
                    OOS.GHR.Security.DecryptPayroll(Thang, Nam, 0, conID);

                OOSDataAdapter da = new OOSDataAdapter(SQL, slParamsDate, conID);
                da.GetTableAndFill(dtSourceAlls);
            }
            catch (Exception ex)
            {
                Database.log.Fatal($"{DatabaseCache.HostName} - API CMD: " + SQL, ex);
            }
            finally
            {
                if (Encrypt)
                    OOS.GHR.Security.EncryptPayroll(Thang, Nam, 0, conID);

                ConnectionManager.Close(conID);
            }
            lstChinhSua = OOS.GHR.BusinessAdapter.TienLuong.BangLuongNV.LoadLuongChinhSua(PhongBanID, false, true, Thang, Nam);

            return dtSourceAlls;
        }

        public static DataTable GetLeaveList(int EmployeeId)
        {
            string strNghiPhep =
            @"SELECT QTNghiPhepID, NgayBatDau, NgayKetThuc, SoNgayNghi, LyDoNghi DienGiai, NS_TL_KyHieuChamCong.MoTa as LyDo,
            ISNULL(SYS_XetDuyet.TrangThai, XetDuyet) as XetDuyet, 
            case 
                when SYS_XetDuyet.TrangThai<>1 THEN ISNULL(SYS_XetDuyet.NguoiDangChoDuyet,'')
                when SYS_XetDuyet.TrangThai=1 THEN ISNULL(SYS_XetDuyet.NguoiDuyetCuoi,'')
            end as NguoiDangChoDuyet, ISNULL(SYS_XetDuyet.GhiChu,'') YKienPheDuyet
            from ns_qtnghiphep
            inner join NS_TL_KyHieuChamCong on NS_TL_KyHieuChamCong.KyHieuChamCongID = NS_QTNghiPhep.KyHieuChamCongID
            left join (SELECT NguoiDuyetCuoi, XetDuyetID, NguoiDangChoDuyet, ObjectID, TrangThai, GhiChu FROM SYS_XetDuyet WHERE ObjectCode='NS_QTNghiPhep') 
            as SYS_XetDuyet on SYS_XetDuyet.ObjectID = ns_qtnghiphep.QTNghiPhepID
            WHERE NhanVienID=" + EmployeeId + " Order By NgayBatDau Desc";

            return Database.ExecTable(strNghiPhep);
        }

        //public static DataTable GetOTList(DateTime timeThang, int NhanVienID)
        //{
        //    int CC_TuNgay = ConfigClass.NBDCC;

        //    int CC_ToiNgay = ConfigClass.NKTCC;

        //    int BackDateDay = ConfigClass.NS_CC_SONGAYLUISUACHAMCONG;

        //    DateTime dtFrom = new DateTime();

        //    DateTime dtTo = new DateTime();

        //    if (CC_TuNgay > 0 && CC_ToiNgay > 0)
        //    {
        //        CC_ToiNgay = Math.Min(CC_ToiNgay, DateTime.Now.Day + BackDateDay);
        //        dtFrom = DatabaseBase.GetFromDate(new DateTime(timeThang.AddMonths(-1).Year, timeThang.AddMonths(-1).Month, CC_TuNgay));
        //        dtTo = DatabaseBase.GetToDate(new DateTime(timeThang.Year, timeThang.Month, CC_ToiNgay));
        //    }
        //    else
        //    {
        //        dtFrom = DatabaseBase.GetFromDate(new DateTime(timeThang.Year, timeThang.Month, 1));
        //        dtTo = DatabaseBase.GetToDate(new DateTime(timeThang.Year, timeThang.Month, DateTime.DaysInMonth(timeThang.Year, timeThang.Month)));
        //    }

        //    SortedList slParamsDate = new SortedList();
        //    slParamsDate.Add("@FromDate", dtFrom.AddMonths(-1));
        //    slParamsDate.Add("@ToDate", dtTo);

        //    string ColumnIn = "";
        //    ColumnIn = OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.CreateColumnDateTime(timeThang, CC_TuNgay, CC_ToiNgay);

        //    string Replace = "Convert(Nvarchar,GioLamThem)";

        //    if (Database.GetNFI().CurrencyDecimalSeparator == ",")
        //        Replace = "Replace(Convert(Nvarchar,GioLamThem),'.',',')";

        //    if (Database.DauPhay == "." || System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
        //        Replace = "Convert(Nvarchar, DKC.GioLamThem)";

        //    string sqlLoadChamCong =
        //    @"  SELECT NhanVien.MaNhanVien as MaNhanVien,
        //        ISNULL('CaLV='+NS_TL_KyHieuChamCong.KyHieu+';Mota='+NS_TL_KyHieuChamCong.MoTa+';ID='+Convert(nvarchar,ISNULL(DKC.DangKyCongID,0)),
        //        'CaLV='+NS_CaLamViec.MaCa+
        //        ';LamThem='+Convert(Nvarchar, DKC.GioLamThem)+
        //        ';LTTruocCa='+Convert(Nvarchar, ISNULL(DKC.GioLamThemTruocCa,0))+
        //        ';ID='+Convert(nvarchar,ISNULL(DKC.DangKyCongID,0))+
        //        ';MoTa='+ISNULL(NS_TL_KyHieuChamCong.MoTa, NS_CaLamViec.TenCa)+
        //        ';LamThemTruocCa='+Convert(nvarchar,ISNULL(DKC.LamThemTruocCa,''))+
        //        ';TimeTruocCa='+Convert(Nvarchar,ISNULL(DKC.BDLamThemTruocCa,''),108)+'-'+Convert(Nvarchar,ISNULL(DKC.KTLamThemTruocCa,''),108)+
        //        ';TimeSauCa='+Convert(Nvarchar,ISNULL(DKC.BDLamThemSauCa,''),108)+'-'+Convert(Nvarchar,ISNULL(DKC.KTLamThemSauCa,''),108)+
        //        ';XD='+Convert(nvarchar,ISNULL(DKC.XetDuyet,''))+
	       //     ';YK='+ISNULL(DKC.YKienXetDuyet,'')+
	       //     ';LyDo='+ISNULL(DKC.LyDoTangCa,'')) as MaCa,
        //        Case
			     //   when DATEPART(DW, DKC.NgayChamCong)=2 then 'Mon '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
			     //   when DATEPART(DW, DKC.NgayChamCong)=3 then 'Tue '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
			     //   when DATEPART(DW, DKC.NgayChamCong)=4 then 'Wed '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
			     //   when DATEPART(DW, DKC.NgayChamCong)=5 then 'Thu '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
			     //   when DATEPART(DW, DKC.NgayChamCong)=6 then 'Fri '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
			     //   when DATEPART(DW, DKC.NgayChamCong)=7 then 'Sat '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
			     //   when DATEPART(DW, DKC.NgayChamCong)=1 then 'Sun '+Convert(Nvarchar,Day(DKC.NgayChamCong))+'/'+Convert(Nvarchar,Month(DKC.NgayChamCong))
	       //     end as Ngay, 
	       //     NhanVien.Ho+' '+NhanVien.HoTen as HoVaTen,
        //        PhongBan.Ten as PhongBan, NhanVien.NhanVienID, PhongBan.ThuTu as ThuTuPhongBan, DKC.NgayChamCong, SYS_XetDuyet.*
	       //     FROM NhanVien
	       //     inner join
        //        ( 
        //            SELECT NS_TL_DangKyCong.* FROM NS_TL_DangKyCong 
        //            WHERE NS_TL_DangKyCong.NgayChamCong>=@FromDate And NS_TL_DangKyCong.NgayChamCong<=@ToDate
        //        ) as DKC on NhanVien.NhanVienID = DKC.NhanVienID
	       //     inner join PhongBan on PhongBan.PhongBanID = NhanVien.PhongBanID
	       //     left join NS_CaLamViec on NS_CaLamViec.CaLamViecID = DKC.CaLamViecID
        //        left join (SELECT KyHieuChamCongID, KyHieu, MoTa FROM NS_TL_KyHieuChamCong) as NS_TL_KyHieuChamCong on NS_TL_KyHieuChamCong.KyHieuChamCongID = DKC.KyHieuChamCongID
        //        left join (SELECT NguoiDuyetCuoi, XetDuyetID, NguoiDangChoDuyet, ObjectID, TrangThai, GhiChu FROM SYS_XetDuyet WHERE ObjectCode='NS_TL_DangKyCong') 
        //        as SYS_XetDuyet on SYS_XetDuyet.ObjectID = DKC.DangKyCongID
        //        WHERE (1=1) AND NhanVien.NhanVienID=" + NhanVienID + " Order By DKC.NgayChamCong Desc";

        //    return Database.ExecTable(sqlLoadChamCong, slParamsDate);
        //}
    }
}