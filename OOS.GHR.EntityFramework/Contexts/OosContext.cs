using OOS.GHR.EntityFramework.Models;
using OOS.GHR.EntityFramework.Models.Social;
using OOS.GHR.EntityFramework.Models.Task;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace OOS.GHR.EntityFramework.Contexts
{
    public partial class OosContext : DbContext
    {
        //public OosContext() : base("name=OosContext")
        //{
        //}

        public OosContext()
        {
            //string _connectionString = ConfigurationManager.ConnectionStrings["OosContext"].ToString();

            //string _domain = "";
            //var _host = System.Web.HttpContext.Current.Request.Url.Host;
            //if (!string.IsNullOrEmpty(_host.ToString()))
            //    _domain = _host.ToString();

            //if (!string.IsNullOrEmpty(_domain) && !_domain.Contains("localhost"))
            //{
            //    if (ConfigurationManager.ConnectionStrings[_domain] != null)
            //        _connectionString = ConfigurationManager.ConnectionStrings[_domain].ToString();
            //}

            //this.Database.Connection.ConnectionString = _connectionString;
            this.Database.Connection.ConnectionString = OOS.GHR.Library.Database.Connection;
        }

        #region Task
        public virtual DbSet<TSK_ActivityLog> TSK_ActivityLog { get; set; }
        public virtual DbSet<TSK_CongViec> TSK_CongViec { get; set; }
        public virtual DbSet<TSK_CongViec_CheckList> TSK_CongViec_CheckList { get; set; }
        public virtual DbSet<TSK_CongViec_LoaiCongViec> TSK_CongViec_LoaiCongViec { get; set; }
        public virtual DbSet<TSK_CongViec_NguoiPhoiHopThucHien> TSK_CongViec_NguoiPhoiHopThucHien { get; set; }
        public virtual DbSet<TSK_CongViec_NguoiTheoDoi> TSK_CongViec_NguoiTheoDoi { get; set; }
        public virtual DbSet<TSK_CongViec_Reminder> TSK_CongViec_Reminder { get; set; }
        public virtual DbSet<TSK_CongViec_TrangThai> TSK_CongViec_TrangThai { get; set; }
        public virtual DbSet<TSK_CongViecLap> TSK_CongViecLap { get; set; }
        public virtual DbSet<TSK_DuAn> TSK_DuAn { get; set; }
        public virtual DbSet<TSK_DuAn_NguoiTheoDoi> TSK_DuAn_NguoiTheoDoi { get; set; }
        public virtual DbSet<TSK_DuAn_NguoiThucHien> TSK_DuAn_NguoiThucHien { get; set; }
        public virtual DbSet<TSK_DuAn_TrangThai> TSK_DuAn_TrangThai { get; set; }
        #endregion

        #region SO
        public virtual DbSet<TSK_BaoCaoAttachment> SO_PostAttachment { get; set; }
        public virtual DbSet<SO_LinkPreview> SO_LinkPreview { get; set; }
        #endregion

        public virtual DbSet<BC_BaoCao> BC_BaoCao { get; set; }
        public virtual DbSet<BC_BaoCao_Parameter> BC_BaoCao_Parameter { get; set; }
        public virtual DbSet<BC_BaoCaoSQL> BC_BaoCaoSQL { get; set; }
        public virtual DbSet<BC_DataSet> BC_DataSet { get; set; }
        public virtual DbSet<BC_Field> BC_Field { get; set; }
        public virtual DbSet<BC_NhomBaoCao> BC_NhomBaoCao { get; set; }
        public virtual DbSet<BC_Parameter> BC_Parameter { get; set; }
        public virtual DbSet<BC_PopupMenu> BC_PopupMenu { get; set; }
        public virtual DbSet<BC_Relation> BC_Relation { get; set; }
        public virtual DbSet<BC_RelationDetail> BC_RelationDetail { get; set; }
        public virtual DbSet<BC_Table> BC_Table { get; set; }
        public virtual DbSet<CKS_eContract> CKS_eContract { get; set; }
        public virtual DbSet<CKS_NhanVien> CKS_NhanVien { get; set; }
        public virtual DbSet<DX_DeXuat> DX_DeXuat { get; set; }
        public virtual DbSet<DX_DeXuat_BuocThucHien> DX_DeXuat_BuocThucHien { get; set; }
        public virtual DbSet<DX_DeXuat_DuLieuDeXuat> DX_DeXuat_DuLieuDeXuat { get; set; }
        public virtual DbSet<DX_DeXuat_FollowUser> DX_DeXuat_FollowUser { get; set; }
        public virtual DbSet<DX_LoaiDeXuat> DX_LoaiDeXuat { get; set; }
        public virtual DbSet<DX_LoaiDeXuat_TruongDuLieu> DX_LoaiDeXuat_TruongDuLieu { get; set; }
        public virtual DbSet<EmailAttachment> EmailAttachments { get; set; }
        public virtual DbSet<EmailContent> EmailContents { get; set; }
        public virtual DbSet<EmailContentGroup> EmailContentGroups { get; set; }
        public virtual DbSet<EmailFilter> EmailFilters { get; set; }
        public virtual DbSet<EmailGroup> EmailGroups { get; set; }
        public virtual DbSet<EmailHistory> EmailHistories { get; set; }
        public virtual DbSet<EmailInbox> EmailInboxes { get; set; }
        public virtual DbSet<EmailInbox_UngVien> EmailInbox_UngVien { get; set; }
        public virtual DbSet<EmailSentFail> EmailSentFails { get; set; }
        public virtual DbSet<EmailSentSuccess> EmailSentSuccesses { get; set; }
        public virtual DbSet<EmailServerProfile> EmailServerProfiles { get; set; }
        public virtual DbSet<EmailTask> EmailTasks { get; set; }
        public virtual DbSet<EmailToSend> EmailToSends { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhanVien_History> NhanVien_History { get; set; }
        public virtual DbSet<NS_BH_BHXHTongHop> NS_BH_BHXHTongHop { get; set; }
        public virtual DbSet<NS_BH_ChotSo> NS_BH_ChotSo { get; set; }
        public virtual DbSet<NS_BH_DonViDangKyBHXH> NS_BH_DonViDangKyBHXH { get; set; }
        public virtual DbSet<NS_BH_HuuTri> NS_BH_HuuTri { get; set; }
        public virtual DbSet<NS_BH_LichSuThayDoiThongTin> NS_BH_LichSuThayDoiThongTin { get; set; }
        public virtual DbSet<NS_BH_LoaiBienDong> NS_BH_LoaiBienDong { get; set; }
        public virtual DbSet<NS_BH_NguoiThamGia> NS_BH_NguoiThamGia { get; set; }
        public virtual DbSet<NS_BH_NoiKhamChuaBenh> NS_BH_NoiKhamChuaBenh { get; set; }
        public virtual DbSet<NS_BH_TangGiamBHYT> NS_BH_TangGiamBHYT { get; set; }
        public virtual DbSet<NS_BH_ThoaiTruyThu> NS_BH_ThoaiTruyThu { get; set; }
        public virtual DbSet<NS_BH_TNLDBNN> NS_BH_TNLDBNN { get; set; }
        public virtual DbSet<NS_BH_TuTuat> NS_BH_TuTuat { get; set; }
        public virtual DbSet<NS_CaLamViec> NS_CaLamViec { get; set; }
        public virtual DbSet<NS_CaLamViec_NhomCa> NS_CaLamViec_NhomCa { get; set; }
        public virtual DbSet<NS_ChucVu_BoTieuChi> NS_ChucVu_BoTieuChi { get; set; }
        public virtual DbSet<NS_ChucVu_CongViec> NS_ChucVu_CongViec { get; set; }
        public virtual DbSet<NS_ChucVu_GiamKhao> NS_ChucVu_GiamKhao { get; set; }
        public virtual DbSet<NS_ChucVu_KhoaDaoTao> NS_ChucVu_KhoaDaoTao { get; set; }
        public virtual DbSet<NS_ChucVu_MonThi> NS_ChucVu_MonThi { get; set; }
        public virtual DbSet<NS_ChucVu_NangLuc> NS_ChucVu_NangLuc { get; set; }
        public virtual DbSet<NS_ChucVu_NguoiChamDiem> NS_ChucVu_NguoiChamDiem { get; set; }
        public virtual DbSet<NS_ChucVu_NhomNangLuc> NS_ChucVu_NhomNangLuc { get; set; }
        public virtual DbSet<NS_ChucVu_OnBoarding> NS_ChucVu_OnBoarding { get; set; }
        public virtual DbSet<NS_ChucVu_VongThi> NS_ChucVu_VongThi { get; set; }
        public virtual DbSet<NS_ChucVu_YeuCau> NS_ChucVu_YeuCau { get; set; }
        public virtual DbSet<NS_DG_BoTieuChi> NS_DG_BoTieuChi { get; set; }
        public virtual DbSet<NS_DG_DanhGia> NS_DG_DanhGia { get; set; }
        public virtual DbSet<NS_DG_DanhGia_Huy> NS_DG_DanhGia_Huy { get; set; }
        public virtual DbSet<NS_DG_DanhGia_KetQua> NS_DG_DanhGia_KetQua { get; set; }
        public virtual DbSet<NS_DG_DanhGia_KetQua_Huy> NS_DG_DanhGia_KetQua_Huy { get; set; }
        public virtual DbSet<NS_DG_DoiTuongDanhGia> NS_DG_DoiTuongDanhGia { get; set; }
        public virtual DbSet<NS_DG_DotDanhGia> NS_DG_DotDanhGia { get; set; }
        public virtual DbSet<NS_DG_DotDanhGia_DuyetMucTieu> NS_DG_DotDanhGia_DuyetMucTieu { get; set; }
        public virtual DbSet<NS_DG_LoaiQuyTrinh> NS_DG_LoaiQuyTrinh { get; set; }
        public virtual DbSet<NS_DG_NhanVien_KeHoach> NS_DG_NhanVien_KeHoach { get; set; }
        public virtual DbSet<NS_DG_NhanVien_KhongDanhGia> NS_DG_NhanVien_KhongDanhGia { get; set; }
        public virtual DbSet<NS_DG_NhanVien_MucTieu> NS_DG_NhanVien_MucTieu { get; set; }
        public virtual DbSet<NS_DG_NhomDanhGia> NS_DG_NhomDanhGia { get; set; }
        public virtual DbSet<NS_DG_NhomXepLoaiDanhGia> NS_DG_NhomXepLoaiDanhGia { get; set; }
        public virtual DbSet<NS_DG_PhanQuyen_ChucVu> NS_DG_PhanQuyen_ChucVu { get; set; }
        public virtual DbSet<NS_DG_PhanQuyen_NhanVien> NS_DG_PhanQuyen_NhanVien { get; set; }
        public virtual DbSet<NS_DG_PhongBan_KeHoach> NS_DG_PhongBan_KeHoach { get; set; }
        public virtual DbSet<NS_DG_QuyTrinh> NS_DG_QuyTrinh { get; set; }
        public virtual DbSet<NS_DG_QuyTrinh_BuocThucHien> NS_DG_QuyTrinh_BuocThucHien { get; set; }
        public virtual DbSet<NS_DG_TieuChi> NS_DG_TieuChi { get; set; }
        public virtual DbSet<NS_DG_XepLoaiDanhGia> NS_DG_XepLoaiDanhGia { get; set; }
        public virtual DbSet<NS_DGNL_DanhGia> NS_DGNL_DanhGia { get; set; }
        public virtual DbSet<NS_DGNL_DanhGia_KetQua> NS_DGNL_DanhGia_KetQua { get; set; }
        public virtual DbSet<NS_DGNL_DotDanhGia> NS_DGNL_DotDanhGia { get; set; }
        public virtual DbSet<NS_DGNL_DotDanhGia_ChucVu_DiemChuan> NS_DGNL_DotDanhGia_ChucVu_DiemChuan { get; set; }
        public virtual DbSet<NS_DGNL_DotDanhGia_NhanVien> NS_DGNL_DotDanhGia_NhanVien { get; set; }
        public virtual DbSet<NS_DGNL_NangLuc> NS_DGNL_NangLuc { get; set; }
        public virtual DbSet<NS_DGNL_NangLuc_MoTa> NS_DGNL_NangLuc_MoTa { get; set; }
        public virtual DbSet<NS_DGNL_NhomNangLuc> NS_DGNL_NhomNangLuc { get; set; }
        public virtual DbSet<NS_DinhBienNhanSu> NS_DinhBienNhanSu { get; set; }
        public virtual DbSet<NS_DsCapPhongBan> NS_DsCapPhongBan { get; set; }
        public virtual DbSet<NS_DsChucDanh> NS_DsChucDanh { get; set; }
        public virtual DbSet<NS_DsChucVu> NS_DsChucVu { get; set; }
        public virtual DbSet<NS_DsChucVu_ChucNang> NS_DsChucVu_ChucNang { get; set; }
        public virtual DbSet<NS_DsChucVuDang> NS_DsChucVuDang { get; set; }
        public virtual DbSet<NS_DsChucVuQuanDoi> NS_DsChucVuQuanDoi { get; set; }
        public virtual DbSet<NS_DsChungChi> NS_DsChungChi { get; set; }
        public virtual DbSet<NS_DsChuyenMon> NS_DsChuyenMon { get; set; }
        public virtual DbSet<NS_DsChuyenNganh> NS_DsChuyenNganh { get; set; }
        public virtual DbSet<NS_DsDanToc> NS_DsDanToc { get; set; }
        public virtual DbSet<NS_DsDoiTac> NS_DsDoiTac { get; set; }
        public virtual DbSet<NS_DsDoiTuongChinhSach> NS_DsDoiTuongChinhSach { get; set; }
        public virtual DbSet<NS_DsDotKhamSucKhoe> NS_DsDotKhamSucKhoe { get; set; }
        public virtual DbSet<NS_DsHangTotNghiep> NS_DsHangTotNghiep { get; set; }
        public virtual DbSet<NS_DsHeDaoTao> NS_DsHeDaoTao { get; set; }
        public virtual DbSet<NS_DsHinhThucDaoTao> NS_DsHinhThucDaoTao { get; set; }
        public virtual DbSet<NS_DsHinhThucKhenThuong> NS_DsHinhThucKhenThuong { get; set; }
        public virtual DbSet<NS_DsHinhThucKyLuat> NS_DsHinhThucKyLuat { get; set; }
        public virtual DbSet<NS_DsHinhThucNghiViec> NS_DsHinhThucNghiViec { get; set; }
        public virtual DbSet<NS_DsHocHam> NS_DsHocHam { get; set; }
        public virtual DbSet<NS_DsHocKhoi> NS_DsHocKhoi { get; set; }
        public virtual DbSet<NS_DsHocVi> NS_DsHocVi { get; set; }
        public virtual DbSet<NS_DsHoSoThuTuc> NS_DsHoSoThuTuc { get; set; }
        public virtual DbSet<NS_DsKhoanMucThuChi> NS_DsKhoanMucThuChi { get; set; }
        public virtual DbSet<NS_DsKhoi> NS_DsKhoi { get; set; }
        public virtual DbSet<NS_DsKhuVuc> NS_DsKhuVuc { get; set; }
        public virtual DbSet<NS_DsLoaiHopDong> NS_DsLoaiHopDong { get; set; }
        public virtual DbSet<NS_DsLoaiKhamSucKhoe> NS_DsLoaiKhamSucKhoe { get; set; }
        public virtual DbSet<NS_DsLoaiNghiPhep> NS_DsLoaiNghiPhep { get; set; }
        public virtual DbSet<NS_DsLoaiQuyetDinh> NS_DsLoaiQuyetDinh { get; set; }
        public virtual DbSet<NS_DsLoaiThiThuc> NS_DsLoaiThiThuc { get; set; }
        public virtual DbSet<NS_DsLyDoChuyenCanBo> NS_DsLyDoChuyenCanBo { get; set; }
        public virtual DbSet<NS_DsLyDoCongTac> NS_DsLyDoCongTac { get; set; }
        public virtual DbSet<NS_DsLyDoNghiViec> NS_DsLyDoNghiViec { get; set; }
        public virtual DbSet<NS_DsLyDoTangCa> NS_DsLyDoTangCa { get; set; }
        public virtual DbSet<NS_DsLyDoThayDoiLuong> NS_DsLyDoThayDoiLuong { get; set; }
        public virtual DbSet<NS_DsNgachCongChuc> NS_DsNgachCongChuc { get; set; }
        public virtual DbSet<NS_DsNganhHoc> NS_DsNganhHoc { get; set; }
        public virtual DbSet<NS_DsNganhNghe> NS_DsNganhNghe { get; set; }
        public virtual DbSet<NS_DsNgoaiNgu> NS_DsNgoaiNgu { get; set; }
        public virtual DbSet<NS_DsNgoaiNgu_TrinhDo> NS_DsNgoaiNgu_TrinhDo { get; set; }
        public virtual DbSet<NS_DsNguoiThamKhao> NS_DsNguoiThamKhao { get; set; }
        public virtual DbSet<NS_DsNguonHoSo> NS_DsNguonHoSo { get; set; }
        public virtual DbSet<NS_DsNhomCaLamViec> NS_DsNhomCaLamViec { get; set; }
        public virtual DbSet<NS_DsNhomChucDanh> NS_DsNhomChucDanh { get; set; }
        public virtual DbSet<NS_DsNhomChucVu> NS_DsNhomChucVu { get; set; }
        public virtual DbSet<NS_DsQuanHuyen> NS_DsQuanHuyen { get; set; }
        public virtual DbSet<NS_DsQuocTich> NS_DsQuocTich { get; set; }
        public virtual DbSet<NS_DsSoQuyetDinh> NS_DsSoQuyetDinh { get; set; }
        public virtual DbSet<NS_DsThamNien> NS_DsThamNien { get; set; }
        public virtual DbSet<NS_DsThonXom> NS_DsThonXom { get; set; }
        public virtual DbSet<NS_DsThuTucNghiViec> NS_DsThuTucNghiViec { get; set; }
        public virtual DbSet<NS_DsTinh> NS_DsTinh { get; set; }
        public virtual DbSet<NS_DsTinHoc> NS_DsTinHoc { get; set; }
        public virtual DbSet<NS_DsTinHoc_TrinhDo> NS_DsTinHoc_TrinhDo { get; set; }
        public virtual DbSet<NS_DsTonGiao> NS_DsTonGiao { get; set; }
        public virtual DbSet<NS_DsTrangThai> NS_DsTrangThai { get; set; }
        public virtual DbSet<NS_DsTrinhDo> NS_DsTrinhDo { get; set; }
        public virtual DbSet<NS_DsTruongDaoTao> NS_DsTruongDaoTao { get; set; }
        public virtual DbSet<NS_DsVanBang> NS_DsVanBang { get; set; }
        public virtual DbSet<NS_DsXaPhuong> NS_DsXaPhuong { get; set; }
        public virtual DbSet<NS_DT_BoCauHoiTracNghiem> NS_DT_BoCauHoiTracNghiem { get; set; }
        public virtual DbSet<NS_DT_CauHoiTracNghiem> NS_DT_CauHoiTracNghiem { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao> NS_DT_DotDaoTao { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_ChiPhi> NS_DT_DotDaoTao_ChiPhi { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_DangKyThamGia> NS_DT_DotDaoTao_DangKyThamGia { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_NhanVien> NS_DT_DotDaoTao_NhanVien { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_NhanVienDanhGia> NS_DT_DotDaoTao_NhanVienDanhGia { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_NoiDung> NS_DT_DotDaoTao_NoiDung { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_ThiTracNghiem> NS_DT_DotDaoTao_ThiTracNghiem { get; set; }
        public virtual DbSet<NS_DT_DotDaoTao_TraLoiTracNghiem> NS_DT_DotDaoTao_TraLoiTracNghiem { get; set; }
        public virtual DbSet<NS_DT_DsTieuChiDanhGia> NS_DT_DsTieuChiDanhGia { get; set; }
        public virtual DbSet<NS_DT_KeHoach> NS_DT_KeHoach { get; set; }
        public virtual DbSet<NS_DT_KeHoach_PhongBan> NS_DT_KeHoach_PhongBan { get; set; }
        public virtual DbSet<NS_DT_KeHoachNhuCau> NS_DT_KeHoachNhuCau { get; set; }
        public virtual DbSet<NS_DT_KetQuaDaoTao> NS_DT_KetQuaDaoTao { get; set; }
        public virtual DbSet<NS_DT_KhoaDaoTao> NS_DT_KhoaDaoTao { get; set; }
        public virtual DbSet<NS_DT_NhuCau> NS_DT_NhuCau { get; set; }
        public virtual DbSet<NS_DT_NhuCau_NhanVien> NS_DT_NhuCau_NhanVien { get; set; }
        public virtual DbSet<NS_DT_NoiDung_KetQua> NS_DT_NoiDung_KetQua { get; set; }
        public virtual DbSet<NS_FM_FileShare> NS_FM_FileShare { get; set; }
        public virtual DbSet<NS_FM_FileStore> NS_FM_FileStore { get; set; }
        public virtual DbSet<NS_FM_Folder> NS_FM_Folder { get; set; }
        public virtual DbSet<NS_FM_NguoiDung_Folder> NS_FM_NguoiDung_Folder { get; set; }
        public virtual DbSet<NS_HopDong> NS_HopDong { get; set; }
        public virtual DbSet<NS_News_Article> NS_News_Article { get; set; }
        public virtual DbSet<NS_News_Category> NS_News_Category { get; set; }
        public virtual DbSet<NS_NhanVien_BoTieuChi> NS_NhanVien_BoTieuChi { get; set; }
        public virtual DbSet<NS_NhanVien_ChuHoKhau> NS_NhanVien_ChuHoKhau { get; set; }
        public virtual DbSet<NS_NhanVien_ChuyenMon> NS_NhanVien_ChuyenMon { get; set; }
        public virtual DbSet<NS_NhanVien_DatCoc> NS_NhanVien_DatCoc { get; set; }
        public virtual DbSet<NS_NhanVien_FileHoSo> NS_NhanVien_FileHoSo { get; set; }
        public virtual DbSet<NS_NhanVien_Follow> NS_NhanVien_Follow { get; set; }
        public virtual DbSet<NS_NhanVien_HoSoThuTuc> NS_NhanVien_HoSoThuTuc { get; set; }
        public virtual DbSet<NS_NhanVien_KyQuyetDinh> NS_NhanVien_KyQuyetDinh { get; set; }
        public virtual DbSet<NS_NhanVien_LoaiThongTin> NS_NhanVien_LoaiThongTin { get; set; }
        public virtual DbSet<NS_NhanVien_MayChamCong> NS_NhanVien_MayChamCong { get; set; }
        public virtual DbSet<NS_NhanVien_NangLuc> NS_NhanVien_NangLuc { get; set; }
        public virtual DbSet<NS_NhanVien_NganhHoc> NS_NhanVien_NganhHoc { get; set; }
        public virtual DbSet<NS_NhanVien_NghiViec> NS_NhanVien_NghiViec { get; set; }
        public virtual DbSet<NS_NhanVien_NgoaiNgu> NS_NhanVien_NgoaiNgu { get; set; }
        public virtual DbSet<NS_NhanVien_OnBoardingItem> NS_NhanVien_OnBoardingItem { get; set; }
        public virtual DbSet<NS_NhanVien_ThongTinKhac> NS_NhanVien_ThongTinKhac { get; set; }
        public virtual DbSet<NS_NhanVien_ThuChi> NS_NhanVien_ThuChi { get; set; }
        public virtual DbSet<NS_OnBoardingGroup> NS_OnBoardingGroup { get; set; }
        public virtual DbSet<NS_OnBoardingItem> NS_OnBoardingItem { get; set; }
        public virtual DbSet<NS_Private_Message> NS_Private_Message { get; set; }
        public virtual DbSet<NS_QTCaLamViec> NS_QTCaLamViec { get; set; }
        public virtual DbSet<NS_QTChuyenCanBo> NS_QTChuyenCanBo { get; set; }
        public virtual DbSet<NS_QTCongTac> NS_QTCongTac { get; set; }
        public virtual DbSet<NS_QTDaoTao> NS_QTDaoTao { get; set; }
        public virtual DbSet<NS_QTDienBienLuong> NS_QTDienBienLuong { get; set; }
        public virtual DbSet<NS_QTDienBienLuongChiTiet> NS_QTDienBienLuongChiTiet { get; set; }
        public virtual DbSet<NS_QTDongBHXH> NS_QTDongBHXH { get; set; }
        public virtual DbSet<NS_QTHoatDongDoanThe> NS_QTHoatDongDoanThe { get; set; }
        public virtual DbSet<NS_QTKetQuaKhamSucKhoe> NS_QTKetQuaKhamSucKhoe { get; set; }
        public virtual DbSet<NS_QTKhac> NS_QTKhac { get; set; }
        public virtual DbSet<NS_QTKhenThuong> NS_QTKhenThuong { get; set; }
        public virtual DbSet<NS_QTKhenThuongCon> NS_QTKhenThuongCon { get; set; }
        public virtual DbSet<NS_QTKhongDongBHXH> NS_QTKhongDongBHXH { get; set; }
        public virtual DbSet<NS_QTKiemNhiem> NS_QTKiemNhiem { get; set; }
        public virtual DbSet<NS_QTKinhNghiem> NS_QTKinhNghiem { get; set; }
        public virtual DbSet<NS_QTKyLuat> NS_QTKyLuat { get; set; }
        public virtual DbSet<NS_QTLoaiQuaTrinh> NS_QTLoaiQuaTrinh { get; set; }
        public virtual DbSet<NS_QTNghienCuuKhoaHoc> NS_QTNghienCuuKhoaHoc { get; set; }
        public virtual DbSet<NS_QTNghiPhep> NS_QTNghiPhep { get; set; }
        public virtual DbSet<NS_QTQuanHeGiaDinh> NS_QTQuanHeGiaDinh { get; set; }
        public virtual DbSet<NS_QTQuanHeThanNhanNuocNgoai> NS_QTQuanHeThanNhanNuocNgoai { get; set; }
        public virtual DbSet<NS_QTThaiSan> NS_QTThaiSan { get; set; }
        public virtual DbSet<NS_QTThamGiaLLVT> NS_QTThamGiaLLVT { get; set; }
        public virtual DbSet<NS_TD_ChiPhiTuyenDung> NS_TD_ChiPhiTuyenDung { get; set; }
        public virtual DbSet<NS_TD_DotTuyenDung> NS_TD_DotTuyenDung { get; set; }
        public virtual DbSet<NS_TD_DotTuyenDung_NguonUngVien> NS_TD_DotTuyenDung_NguonUngVien { get; set; }
        public virtual DbSet<NS_TD_HinhThucTuyenDung> NS_TD_HinhThucTuyenDung { get; set; }
        public virtual DbSet<NS_TD_KeHoachNhuCau> NS_TD_KeHoachNhuCau { get; set; }
        public virtual DbSet<NS_TD_KeHoachTuyenDung> NS_TD_KeHoachTuyenDung { get; set; }
        public virtual DbSet<NS_TD_KetQuaDanhGiaDaoTao> NS_TD_KetQuaDanhGiaDaoTao { get; set; }
        public virtual DbSet<NS_TD_LoaiDaoTao> NS_TD_LoaiDaoTao { get; set; }
        public virtual DbSet<NS_TD_LopDaoTao> NS_TD_LopDaoTao { get; set; }
        public virtual DbSet<NS_TD_LopDaoTao_ThiSinh> NS_TD_LopDaoTao_ThiSinh { get; set; }
        public virtual DbSet<NS_TD_MonThi> NS_TD_MonThi { get; set; }
        public virtual DbSet<NS_TD_MonThi_ThiSinh> NS_TD_MonThi_ThiSinh { get; set; }
        public virtual DbSet<NS_TD_NhomThiSinh> NS_TD_NhomThiSinh { get; set; }
        public virtual DbSet<NS_TD_NhuCauTuyenDung> NS_TD_NhuCauTuyenDung { get; set; }
        public virtual DbSet<NS_TD_Promote> NS_TD_Promote { get; set; }
        public virtual DbSet<NS_TD_ThiSinh> NS_TD_ThiSinh { get; set; }
        public virtual DbSet<NS_TD_ThiSinh_DaoTao> NS_TD_ThiSinh_DaoTao { get; set; }
        public virtual DbSet<NS_TD_ThiSinh_DotTuyenDung> NS_TD_ThiSinh_DotTuyenDung { get; set; }
        public virtual DbSet<NS_TD_ThiSinh_KinhNghiem> NS_TD_ThiSinh_KinhNghiem { get; set; }
        public virtual DbSet<NS_TD_ThiSinh_NguoiChamDiem> NS_TD_ThiSinh_NguoiChamDiem { get; set; }
        public virtual DbSet<NS_TD_ThiSinh_QuanHeGiaDinh> NS_TD_ThiSinh_QuanHeGiaDinh { get; set; }
        public virtual DbSet<NS_TD_TrangThai_UngVien> NS_TD_TrangThai_UngVien { get; set; }
        public virtual DbSet<NS_TD_TrangThaiNghiHoc> NS_TD_TrangThaiNghiHoc { get; set; }
        public virtual DbSet<NS_TD_VongThi> NS_TD_VongThi { get; set; }
        public virtual DbSet<NS_TD_VongThi_MonThi> NS_TD_VongThi_MonThi { get; set; }
        public virtual DbSet<NS_TD_VongThi_NguoiChamDiem> NS_TD_VongThi_NguoiChamDiem { get; set; }
        public virtual DbSet<NS_TD_VongThi_ThiSinh> NS_TD_VongThi_ThiSinh { get; set; }
        public virtual DbSet<NS_TienTe> NS_TienTe { get; set; }
        public virtual DbSet<NS_TienTe_TyGia> NS_TienTe_TyGia { get; set; }
        public virtual DbSet<NS_TL_BangChamCong> NS_TL_BangChamCong { get; set; }
        public virtual DbSet<NS_TL_BangLuong> NS_TL_BangLuong { get; set; }
        public virtual DbSet<NS_TL_CC_DuLieuChamCong> NS_TL_CC_DuLieuChamCong { get; set; }
        public virtual DbSet<NS_TL_CC_MayChamCong> NS_TL_CC_MayChamCong { get; set; }
        public virtual DbSet<NS_TL_CC_MayChamCong_DownloadTuDong> NS_TL_CC_MayChamCong_DownloadTuDong { get; set; }
        public virtual DbSet<NS_TL_CC_TongHopTheoNgay> NS_TL_CC_TongHopTheoNgay { get; set; }
        public virtual DbSet<NS_TL_CC_TongHopTheoNgay_ChinhSua> NS_TL_CC_TongHopTheoNgay_ChinhSua { get; set; }
        public virtual DbSet<NS_TL_ChamCong> NS_TL_ChamCong { get; set; }
        public virtual DbSet<NS_TL_ChiTietCongThucLuong> NS_TL_ChiTietCongThucLuong { get; set; }
        public virtual DbSet<NS_TL_ChiTietCongThucLuong_LichSu> NS_TL_ChiTietCongThucLuong_LichSu { get; set; }
        public virtual DbSet<NS_TL_ChiTietLuong> NS_TL_ChiTietLuong { get; set; }
        public virtual DbSet<NS_TL_ChiTietLuong_ChinhSua> NS_TL_ChiTietLuong_ChinhSua { get; set; }
        public virtual DbSet<NS_TL_CongDoan> NS_TL_CongDoan { get; set; }
        public virtual DbSet<NS_TL_CongThucLuongHeThong> NS_TL_CongThucLuongHeThong { get; set; }
        public virtual DbSet<NS_TL_DangKyCong> NS_TL_DangKyCong { get; set; }
        public virtual DbSet<NS_TL_KhoaLuong> NS_TL_KhoaLuong { get; set; }
        public virtual DbSet<NS_TL_KyHieu_LoaiChamCong> NS_TL_KyHieu_LoaiChamCong { get; set; }
        public virtual DbSet<NS_TL_KyHieuChamCong> NS_TL_KyHieuChamCong { get; set; }
        public virtual DbSet<NS_TL_LenhSanXuat> NS_TL_LenhSanXuat { get; set; }
        public virtual DbSet<NS_TL_LoaiChamCong> NS_TL_LoaiChamCong { get; set; }
        public virtual DbSet<NS_TL_LoaiLuong> NS_TL_LoaiLuong { get; set; }
        public virtual DbSet<NS_TL_NganhHang> NS_TL_NganhHang { get; set; }
        public virtual DbSet<NS_TL_NgayLe> NS_TL_NgayLe { get; set; }
        public virtual DbSet<NS_TL_NhomCongThucLuong> NS_TL_NhomCongThucLuong { get; set; }
        public virtual DbSet<NS_TL_NhomLuong> NS_TL_NhomLuong { get; set; }
        public virtual DbSet<NS_TL_SanPham> NS_TL_SanPham { get; set; }
        public virtual DbSet<NS_TL_SanPham_NhanVien> NS_TL_SanPham_NhanVien { get; set; }
        public virtual DbSet<NS_TL_SanPhamCongDoan> NS_TL_SanPhamCongDoan { get; set; }
        public virtual DbSet<NS_TL_SanPhamCongDoanLSX> NS_TL_SanPhamCongDoanLSX { get; set; }
        public virtual DbSet<NS_TL_ThangLuong> NS_TL_ThangLuong { get; set; }
        public virtual DbSet<NS_TL_TongHopChamCong> NS_TL_TongHopChamCong { get; set; }
        public virtual DbSet<NS_TL_TongHopDangKyCong> NS_TL_TongHopDangKyCong { get; set; }
        public virtual DbSet<NS_TL_TongHopLuong> NS_TL_TongHopLuong { get; set; }
        public virtual DbSet<NS_TL_YeuCauChinhSuaLuong> NS_TL_YeuCauChinhSuaLuong { get; set; }
        public virtual DbSet<NS_TLBD_BangLuong> NS_TLBD_BangLuong { get; set; }
        public virtual DbSet<NS_TLBD_ChamCong> NS_TLBD_ChamCong { get; set; }
        public virtual DbSet<NS_TLBD_ChiTietLuong> NS_TLBD_ChiTietLuong { get; set; }
        public virtual DbSet<NS_TLBD_QTDienBienLuong> NS_TLBD_QTDienBienLuong { get; set; }
        public virtual DbSet<NS_TLBD_QTDienBienLuongChiTiet> NS_TLBD_QTDienBienLuongChiTiet { get; set; }
        public virtual DbSet<NS_TS_ChiTietThanhLy> NS_TS_ChiTietThanhLy { get; set; }
        public virtual DbSet<NS_TS_ChuyenThietBi> NS_TS_ChuyenThietBi { get; set; }
        public virtual DbSet<NS_TS_KiemKe> NS_TS_KiemKe { get; set; }
        public virtual DbSet<NS_TS_KiemKeChiTiet> NS_TS_KiemKeChiTiet { get; set; }
        public virtual DbSet<NS_TS_NhaCungCap> NS_TS_NhaCungCap { get; set; }
        public virtual DbSet<NS_TS_NhomThietBi> NS_TS_NhomThietBi { get; set; }
        public virtual DbSet<NS_TS_SuaChuaThietBi> NS_TS_SuaChuaThietBi { get; set; }
        public virtual DbSet<NS_TS_ThanhLyThietBi> NS_TS_ThanhLyThietBi { get; set; }
        public virtual DbSet<NS_TS_ThietBi> NS_TS_ThietBi { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<PhongBan_ChucVu> PhongBan_ChucVu { get; set; }
        public virtual DbSet<PhongBan_DiChuyen> PhongBan_DiChuyen { get; set; }
        public virtual DbSet<PhongBan_History> PhongBan_History { get; set; }
        public virtual DbSet<Poll_Item> Poll_Item { get; set; }
        public virtual DbSet<Poll_Poll> Poll_Poll { get; set; }
        public virtual DbSet<Poll_PollUser> Poll_PollUser { get; set; }
        public virtual DbSet<SO_Group> SO_Group { get; set; }
        public virtual DbSet<SO_Member> SO_Member { get; set; }
        public virtual DbSet<SO_Post> SO_Post { get; set; }
        public virtual DbSet<SO_PostComment> SO_PostComment { get; set; }
        public virtual DbSet<SO_PostImage> SO_PostImage { get; set; }
        public virtual DbSet<SO_PostLike> SO_PostLike { get; set; }
        //public virtual DbSet<SO_PostTag> SO_PostTag { get; set; }
        public virtual DbSet<SO_PostView> SO_PostView { get; set; }
        public virtual DbSet<SYS_Activity> SYS_Activity { get; set; }
        public virtual DbSet<SYS_API> SYS_API { get; set; }
        public virtual DbSet<SYS_API_Authority> SYS_API_Authority { get; set; }
        public virtual DbSet<SYS_API_Domain> SYS_API_Domain { get; set; }
        public virtual DbSet<SYS_APILog> SYS_APILog { get; set; }
        public virtual DbSet<SYS_CanhBao> SYS_CanhBao { get; set; }
        public virtual DbSet<SYS_Comment> SYS_Comment { get; set; }
        public virtual DbSet<SYS_Data> SYS_Data { get; set; }
        public virtual DbSet<SYS_FileStore> SYS_FileStore { get; set; }
        public virtual DbSet<SYS_FileUpdate> SYS_FileUpdate { get; set; }
        public virtual DbSet<SYS_Language> SYS_Language { get; set; }
        public virtual DbSet<SYS_LichSuThayDoiThongTin> SYS_LichSuThayDoiThongTin { get; set; }
        public virtual DbSet<SYS_LichSuThayDoiThongTinChiTiet> SYS_LichSuThayDoiThongTinChiTiet { get; set; }
        public virtual DbSet<SYS_LoaiCanhBao> SYS_LoaiCanhBao { get; set; }
        public virtual DbSet<SYS_LocaleStringResource> SYS_LocaleStringResource { get; set; }
        public virtual DbSet<SYS_Log> SYS_Log { get; set; }
        public virtual DbSet<SYS_LogPage> SYS_LogPage { get; set; }
        public virtual DbSet<SYS_MauBaoCao> SYS_MauBaoCao { get; set; }
        public virtual DbSet<SYS_NguoiDung> SYS_NguoiDung { get; set; }
        public virtual DbSet<SYS_NguoiDung_BaoCao> SYS_NguoiDung_BaoCao { get; set; }
        public virtual DbSet<SYS_NguoiDung_File> SYS_NguoiDung_File { get; set; }
        public virtual DbSet<SYS_NguoiDung_NhomQuyen> SYS_NguoiDung_NhomQuyen { get; set; }
        public virtual DbSet<SYS_NguoiDung_NhomXetDuyet> SYS_NguoiDung_NhomXetDuyet { get; set; }
        public virtual DbSet<SYS_NguoiDung_PhongBan> SYS_NguoiDung_PhongBan { get; set; }
        public virtual DbSet<SYS_NguoiDung_Quyen> SYS_NguoiDung_Quyen { get; set; }
        public virtual DbSet<SYS_NhomNguoiDung> SYS_NhomNguoiDung { get; set; }
        public virtual DbSet<SYS_NhomPhanQuyen> SYS_NhomPhanQuyen { get; set; }
        public virtual DbSet<SYS_NhomQuyen> SYS_NhomQuyen { get; set; }
        public virtual DbSet<SYS_NhomThongSo> SYS_NhomThongSo { get; set; }
        public virtual DbSet<SYS_NhomXetDuyet> SYS_NhomXetDuyet { get; set; }
        public virtual DbSet<SYS_PlugIn> SYS_PlugIn { get; set; }
        public virtual DbSet<SYS_Process> SYS_Process { get; set; }
        public virtual DbSet<SYS_ProcessFailItem> SYS_ProcessFailItem { get; set; }
        public virtual DbSet<SYS_ProcessLog> SYS_ProcessLog { get; set; }
        public virtual DbSet<SYS_Quyen> SYS_Quyen { get; set; }
        public virtual DbSet<SYS_StoreChangeHistory> SYS_StoreChangeHistory { get; set; }
        public virtual DbSet<SYS_ThongSo> SYS_ThongSo { get; set; }
        public virtual DbSet<SYS_ThongTinCongTy> SYS_ThongTinCongTy { get; set; }
        public virtual DbSet<SYS_UserSettings> SYS_UserSettings { get; set; }
        public virtual DbSet<SYS_XetDuyet> SYS_XetDuyet { get; set; }
        public virtual DbSet<SYS_XetDuyet_UyQuyen> SYS_XetDuyet_UyQuyen { get; set; }
        public virtual DbSet<WF_LoaiQuyTrinh> WF_LoaiQuyTrinh { get; set; }
        public virtual DbSet<WF_NguoiDung_QuyTrinh> WF_NguoiDung_QuyTrinh { get; set; }
        public virtual DbSet<WF_QuyTrinh> WF_QuyTrinh { get; set; }
        public virtual DbSet<WF_QuyTrinh_BuocThucHien> WF_QuyTrinh_BuocThucHien { get; set; }
        public virtual DbSet<WF_QuyTrinh_BuocThucHien_PhongBan> WF_QuyTrinh_BuocThucHien_PhongBan { get; set; }
        public virtual DbSet<WF_QuyTrinh_ReferrerUser> WF_QuyTrinh_ReferrerUser { get; set; }
        public virtual DbSet<WF_QuyTrinh_ThucHien> WF_QuyTrinh_ThucHien { get; set; }
        public virtual DbSet<NS_QTKhamSucKhoe> NS_QTKhamSucKhoe { get; set; }
        public virtual DbSet<NS_QTLoaiKhamSucKhoe> NS_QTLoaiKhamSucKhoe { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<NhanVien>()
            //    .Property(e => e.SoPhepChuyenTuNamTruoc)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NhanVien>()
            //    .Property(e => e.SoPhepBanDau)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NhanVien>()
            //    .Property(e => e.SoPhepTheoQuyDinh)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NhanVien>()
            //    .Property(e => e.SoPhepDaNghi)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NhanVien>()
            //    .Property(e => e.SoPhepConLai)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NhanVien>()
            //    .Property(e => e.ThamNien)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_BH_BHXHTongHop>()
            //    .Property(e => e.MucDongBH_Cu)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_BH_BHXHTongHop>()
            //    .Property(e => e.MucDongBH_Moi)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_BH_BHXHTongHop>()
            //    .Property(e => e.TyleDong)
            //    .HasPrecision(10, 3);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHXH_QL)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHXH_PD)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHYT_QL)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHYT_PD)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHTN_QL)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHTN_PD)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ChotSo>()
            //    .Property(e => e.BHXL_QL)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_BH_HuuTri>()
            //    .Property(e => e.LuongToiThieu)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_HuuTri>()
            //    .Property(e => e.LuongBinhQuan)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_HuuTri>()
            //    .Property(e => e.TongTienThanhToan)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_HuuTri>()
            //    .Property(e => e.MucLuongDongBHXH)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_NguoiThamGia>()
            //    .Property(e => e.MucLuongDongBH)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_NguoiThamGia>()
            //    .Property(e => e.MucLuongDongBHCu)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_NguoiThamGia>()
            //    .Property(e => e.TyleDong)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_BH_NguoiThamGia>()
            //    .Property(e => e.MucLuongTBBanDau)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_TangGiamBHYT>()
            //    .Property(e => e.MucDong)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_BH_TangGiamBHYT>()
            //    .Property(e => e.Tyle)
            //    .HasPrecision(10, 3);

            //modelBuilder.Entity<NS_BH_ThoaiTruyThu>()
            //    .Property(e => e.MucLuong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_ThoaiTruyThu>()
            //    .Property(e => e.TyleDong)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_BH_ThoaiTruyThu>()
            //    .Property(e => e.Tyle)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_BH_TNLDBNN>()
            //    .Property(e => e.MucLuongDongBHXH)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_BH_TNLDBNN>()
            //    .Property(e => e.PhanTramThuongTat)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_BH_TuTuat>()
            //    .Property(e => e.MucLuongDongBHXH)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_ChucVu_GiamKhao>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_ChucVu_MonThi>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_ChucVu_NangLuc>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_ChucVu_NguoiChamDiem>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_ChucVu_VongThi>()
            //    .Property(e => e.TongDiemYeuCau)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DG_DanhGia>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_Huy>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.DiemNhap)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.TyLeChatLuong)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.GiaTriLonNhat)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.MucTieu)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua>()
            //    .Property(e => e.TyleHoanThanh)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.DiemNhap)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.TyLeChatLuong)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.GiaTriLonNhat)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.MucTieu)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_DanhGia_KetQua_Huy>()
            //    .Property(e => e.TyleHoanThanh)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_NhanVien_KeHoach>()
            //    .Property(e => e.MucTieu)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DG_NhanVien_KeHoach>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DG_NhanVien_MucTieu>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_NhanVien_MucTieu>()
            //    .Property(e => e.MucTieu)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_NhanVien_MucTieu>()
            //    .Property(e => e.KH_MucTieu)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_NhanVien_MucTieu>()
            //    .Property(e => e.GiaTriLonNhat)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_NhanVien_MucTieu>()
            //    .Property(e => e.DiemTru)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_NhomDanhGia>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_PhongBan_KeHoach>()
            //    .Property(e => e.MucTieu)
            //    .HasPrecision(18, 10);

            //modelBuilder.Entity<NS_DG_PhongBan_KeHoach>()
            //    .Property(e => e.TyTrong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DG_TieuChi>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(10, 3);

            //modelBuilder.Entity<NS_DG_TieuChi>()
            //    .Property(e => e.MucTieu)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_DG_TieuChi>()
            //    .Property(e => e.DiemTru)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DG_TieuChi>()
            //    .Property(e => e.GiaTriLonNhat)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DG_XepLoaiDanhGia>()
            //    .Property(e => e.HeSoTinhLuong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DGNL_DanhGia>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DGNL_DanhGia>()
            //    .Property(e => e.DiemChuan)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DGNL_DanhGia_KetQua>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DGNL_DanhGia_KetQua>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DGNL_DotDanhGia_NhanVien>()
            //    .Property(e => e.DiemChuan)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DGNL_NangLuc>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DGNL_NangLuc>()
            //    .Property(e => e.DiemKyVong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DsChucVuDang>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsChucVuQuanDoi>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsDanToc>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsDoiTuongChinhSach>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsHinhThucDaoTao>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsKhoi>()
            //    .Property(e => e.MaKhoi)
            //    .IsFixedLength();

            //modelBuilder.Entity<NS_DsKhuVuc>()
            //    .Property(e => e.Latitude)
            //    .HasPrecision(12, 8);

            //modelBuilder.Entity<NS_DsKhuVuc>()
            //    .Property(e => e.Longtitude)
            //    .HasPrecision(12, 8);

            //modelBuilder.Entity<NS_DsNgachCongChuc>()
            //    .Property(e => e.ThoiHanNangLuong)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsNgachCongChuc>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsNgoaiNgu>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsQuanHuyen>()
            //    .Property(e => e.MaHuyen)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsQuocTich>()
            //    .Property(e => e.MaNuoc)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsSoQuyetDinh>()
            //    .Property(e => e.SoQuyetDinh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsThamNien>()
            //    .Property(e => e.TuNam)
            //    .HasPrecision(10, 2);

            //modelBuilder.Entity<NS_DsThamNien>()
            //    .Property(e => e.ToiNam)
            //    .HasPrecision(10, 2);

            //modelBuilder.Entity<NS_DsThamNien>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(10, 4);

            //modelBuilder.Entity<NS_DsTinh>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsTinh>()
            //    .Property(e => e.MaVung)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsTinHoc>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsTrangThai>()
            //    .Property(e => e.Ma)
            //    .IsFixedLength();

            //modelBuilder.Entity<NS_DsTrinhDo>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DsVanBang>()
            //    .Property(e => e.Ma)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_DT_DotDaoTao_ChiPhi>()
            //    .Property(e => e.SoTien)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_DT_DotDaoTao_NhanVien>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DT_DotDaoTao_NoiDung>()
            //    .Property(e => e.ChiPhiCongTy)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DT_DotDaoTao_NoiDung>()
            //    .Property(e => e.ChiPhiCaNhan)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_DT_DotDaoTao_ThiTracNghiem>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_DT_DotDaoTao_TraLoiTracNghiem>()
            //    .Property(e => e.DiemSo)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_News_Article>()
            //    .Property(e => e.Image)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_NhanVien_DatCoc>()
            //    .Property(e => e.SoTien)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_NhanVien_NangLuc>()
            //    .Property(e => e.TrongSo)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_NhanVien_NangLuc>()
            //    .Property(e => e.DiemKyVong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_NhanVien_NghiViec>()
            //    .Property(e => e.SoQuyetDinh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_NhanVien_ThuChi>()
            //    .Property(e => e.SoTien)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTCongTac>()
            //    .Property(e => e.SoNgayCongTac)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTDienBienLuongChiTiet>()
            //    .Property(e => e.GiaTri)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_QTDienBienLuongChiTiet>()
            //    .Property(e => e.GiaTriMoi)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_QTDongBHXH>()
            //    .Property(e => e.TyleDong)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_QTDongBHXH>()
            //    .Property(e => e.MucLuongBD)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTKhenThuong>()
            //    .Property(e => e.SoQuyetDinh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_QTKhenThuong>()
            //    .Property(e => e.SoTienThuong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTKinhNghiem>()
            //    .Property(e => e.MucLuong)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTKyLuat>()
            //    .Property(e => e.SoTienPhat)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTNghienCuuKhoaHoc>()
            //    .Property(e => e.SoQuyetDinh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_QTNghiPhep>()
            //    .Property(e => e.SoQuyetDinh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_QTNghiPhep>()
            //    .Property(e => e.MucDongBHXH)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTNghiPhep>()
            //    .Property(e => e.SoTienDuocHuong)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_QTNghiPhep>()
            //    .Property(e => e.SoTienDuocHuongBD)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTNghiPhep>()
            //    .Property(e => e.SoNgayNghi)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_QTNghiPhep>()
            //    .Property(e => e.MucLuongToiThieu)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTThaiSan>()
            //    .Property(e => e.TongSoTienThanhToan)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<NS_QTThaiSan>()
            //    .Property(e => e.MucLuongDongBHXH)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTThaiSan>()
            //    .Property(e => e.MucLuongBinhQuan)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTThaiSan>()
            //    .Property(e => e.MucLuongToiThieu)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_QTThaiSan>()
            //    .Property(e => e.TongSoTienThanhToanBD)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<NS_TD_ChiPhiTuyenDung>()
            //    .Property(e => e.SoTien)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_KeHoachTuyenDung>()
            //    .Property(e => e.ChiPhiDuKien)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao>()
            //    .Property(e => e.TongDiemYeuCau)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao_ThiSinh>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao_ThiSinh>()
            //    .Property(e => e.ViTinh)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao_ThiSinh>()
            //    .Property(e => e.DiemKhac)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao_ThiSinh>()
            //    .Property(e => e.Diem4)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao_ThiSinh>()
            //    .Property(e => e.Diem5)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_LopDaoTao_ThiSinh>()
            //    .Property(e => e.Diem6)
            //    .HasPrecision(10, 0);

            //modelBuilder.Entity<NS_TD_MonThi_ThiSinh>()
            //    .Property(e => e.DiemThi)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_TD_ThiSinh>()
            //    .Property(e => e.SoBaoDanh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_TD_ThiSinh>()
            //    .Property(e => e.Email)
            //    .IsUnicode(false);

            //modelBuilder.Entity<NS_TD_ThiSinh_DotTuyenDung>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TD_ThiSinh_NguoiChamDiem>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(10, 3);

            //modelBuilder.Entity<NS_TD_ThiSinh_NguoiChamDiem>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(10, 3);

            //modelBuilder.Entity<NS_TD_ThiSinh_NguoiChamDiem>()
            //    .Property(e => e.TongDiemChuaNhanHS)
            //    .HasPrecision(10, 3);

            //modelBuilder.Entity<NS_TD_VongThi_NguoiChamDiem>()
            //    .Property(e => e.HeSo)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_TD_VongThi_ThiSinh>()
            //    .Property(e => e.TongDiem)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TienTe_TyGia>()
            //    .Property(e => e.TyGia)
            //    .HasPrecision(18, 3);

            ////modelBuilder.Entity<NS_TL_CC_DuLieuChamCong>()
            ////    .Property(e => e.Latitude)
            ////    .HasPrecision(10, 8);

            ////modelBuilder.Entity<NS_TL_CC_DuLieuChamCong>()
            ////    .Property(e => e.Longtitude)
            ////    .HasPrecision(11, 8);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.TGDiMuon)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.TGVeSom)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.TGLamViec)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.TGLamThem)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.TGRaNgoai)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.TGLamThemDem)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_CC_TongHopTheoNgay>()
            //    .Property(e => e.SoPhutLamViecThucTe)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_ChiTietLuong>()
            //    .Property(e => e.TienLuong)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_TL_ChiTietLuong_ChinhSua>()
            //    .Property(e => e.TienLuong)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_TL_ChiTietLuong_ChinhSua>()
            //    .Property(e => e.GiaTriGoc)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_TL_DangKyCong>()
            //    .Property(e => e.GioLamThem)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_DangKyCong>()
            //    .Property(e => e.GioLamThemTruocCa)
            //    .HasPrecision(18, 1);

            //modelBuilder.Entity<NS_TL_SanPham>()
            //    .Property(e => e.DonGia)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TL_SanPhamCongDoan>()
            //    .Property(e => e.DonGia)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_TL_SanPhamCongDoanLSX>()
            //    .Property(e => e.DonGia)
            //    .HasPrecision(18, 3);

            //modelBuilder.Entity<NS_TL_ThangLuong>()
            //    .Property(e => e.BacLuong)
            //    .HasPrecision(10, 1);

            //modelBuilder.Entity<NS_TL_TongHopLuong>()
            //    .Property(e => e.TienLuong)
            //    .HasPrecision(10, 2);

            //modelBuilder.Entity<NS_TL_YeuCauChinhSuaLuong>()
            //    .Property(e => e.GiaTri)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TL_YeuCauChinhSuaLuong>()
            //    .Property(e => e.GiaTriGoc)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TLBD_QTDienBienLuong>()
            //    .Property(e => e.SoQuyetDinh)
            //    .IsUnicode(false);

            ////modelBuilder.Entity<NS_TLBD_QTDienBienLuong>()
            ////    .Property(e => e.MaLoaiLuong)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<NS_TLBD_QTDienBienLuongChiTiet>()
            //    .Property(e => e.GiaTri)
            //    .HasPrecision(18, 5);

            //modelBuilder.Entity<NS_TS_ChiTietThanhLy>()
            //    .Property(e => e.DonGia)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TS_SuaChuaThietBi>()
            //    .Property(e => e.DonGia)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TS_ThanhLyThietBi>()
            //    .Property(e => e.DonGia)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TS_ThanhLyThietBi>()
            //    .Property(e => e.TongSoTien)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TS_ThietBi>()
            //    .Property(e => e.ThoiGianKhauHao)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<NS_TS_ThietBi>()
            //    .Property(e => e.GiaTri)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<SYS_Activity>()
            //    .Property(e => e.RowIndex)
            //    //.IsFixedLength()
            //    ;

            //modelBuilder.Entity<SYS_LichSuThayDoiThongTinChiTiet>()
            //    .Property(e => e.GiaTriDouble)
            //    .HasPrecision(18, 5);
        }
    }
}
