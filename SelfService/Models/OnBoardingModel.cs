using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOS.GHR.Framework.Mvc;
using OOS.GHR.HRIS.Models;
using OOS.GHR.Library;
using OOS.GHR.Master.Models;
using OOS.GHR.Framework.Mvc;
using OOS.GHR.Framework.Controllers;
using OOS.GHR.Framework;
namespace OOS.GHR.SelfService.Models
{
    public class OnBoardingModel : OOSBaseModel
    {
        public List<ObjectID> Items = null;

        public NS_CaLamViec caLV = null;

        public NS_DsChucVu ChucVu = null;

        public string DiaChiCongTy {
            get
            {
                return SYS_ThongTinCongTy.GetDiaChiByID(DatabaseCache.CongTyID);
            }
        }

        public NhanVien CurrentNhanVien { get; set; }

        public string QLTrucTiep_Ten { get; set; }

        public string QLTrucTiep_ChucVu { get; set; }

        public string QLTrucTiep_DienThoai { get; set; }

        public string QLTrucTiep_Email { get; set; }

        public int QLTrucTiepID { get; set; }

        public NhanVienEditModel EditInfo = null;

        public OnBoardingModel()
        {
            Items = new List<ObjectID>()
            {
                new ObjectID("Wellcome", 1, "Wellcome", Translate("Thông điệp chào mừng.<br\\>Thông tin giờ / địa điểm làm việc, người quản lý trực tiếp.")),
                new ObjectID("PersonalInfo", 2, "EditInfo", Translate("Cập nhật / Chỉnh sửa thông tin cá nhân")),
                new ObjectID("JobInfo", 3, "JobInfo", Translate("Mô tả công việc, thông tin vị trí công việc")),
                new ObjectID("CareerPath", 4, "CareerPath", Translate("Lộ trình thăng tiến")),
                new ObjectID("Asset", 5, "Asset", Translate("Cấp phát tài sản, trang thiết bị cá nhân")),
                new ObjectID("Finished", 6, "Finished", Translate("Kiểm tra thông tin, kết thúc OnBoarding"))
            };

            caLV = NS_CaLamViec.GetNS_CaLamViec(DatabaseCache.NhanVien.CaLVID);

            ChucVu = NS_DsChucVu.GetNS_DsChucVu(DatabaseCache.NhanVien.ChucVuID);

            CurrentNhanVien = DatabaseCache.NhanVien;

            if (CurrentNhanVien.BaoCaoTrucTiepID>0)
            {
                QLTrucTiepID = SYS_NguoiDung.GetNhanVienIDByID(CurrentNhanVien.BaoCaoTrucTiepID);


                dynamic D = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetThongTinNhanVienByID(QLTrucTiepID);
                QLTrucTiep_ChucVu = D.TenChucVu;
                QLTrucTiep_DienThoai = D.DienThoai;
                QLTrucTiep_Email = D.Email;
                QLTrucTiep_Ten = D.HoVaTen;
            }

            EditInfo = CurrentNhanVien.ToModel<NhanVienEditModel, NhanVien>();
            EditInfo.FullName = CurrentNhanVien.Ho + " " + CurrentNhanVien.HoTen;

            if (CurrentNhanVien.Anh != null)
            {
                string header = string.Format("data:image/{0};base64,", "unknown");
                EditInfo.AnhPro = string.Format("{0}{1}", header, Convert.ToBase64String(CurrentNhanVien.Anh));
            }

            EditInfo.DiaChiTT = new DiaChi_ChiTietModel
            ("DiaChiTT", "DiaChiThuongTru", Translate("Địa chỉ thường trú:"), CurrentNhanVien.DiaChiThuongTru, CurrentNhanVien.DiaChiTT_XaPhuongID,
            CurrentNhanVien.DiaChiTT_QuanHuyenID, CurrentNhanVien.DiaChiTT_TinhID, CurrentNhanVien.DiaChiTT_SoNha);

            EditInfo.DiaChiHT = new DiaChi_ChiTietModel
            ("DiaChi", "DiaChi", Translate("Địa chỉ hiện tại:"), CurrentNhanVien.DiaChi, CurrentNhanVien.DiaChi_XaPhuongID,
            CurrentNhanVien.DiaChi_QuanHuyenID, CurrentNhanVien.DiaChi_TinhID, CurrentNhanVien.DiaChi_SoNha);
        }

        public string GetArrayItems()
        {
            string strItems = "";
            foreach(ObjectID id in Items)
            {
                strItems += "\""+id.StrID + "\",";
            }
            strItems = strItems.Substring(0, strItems.Length - 2);

            return strItems;
        }
    }
}