using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOS.GHR.BusinessAdapter.iSign;
using OOS.GHR.BusinessAdapter;
using OOS.GHR.Framework;
using OOS.GHR.Library;

namespace OOS.GHR.SelfService.Models
{
    public class iSignDocument: OOS.GHR.BusinessAdapter.iSign.CKSVanBanInfo
    {

    }

    public class iSignModel
    {
        public CKSNhanVienInfo SignatureInfo { get; set; }

        public List<iSignDocument> Waitting { get; set; }

        public int CKSNhanVienID { get; set; }

        /// <summary>
        /// Load thông tin Chữ ký số của Nhân viên
        /// </summary>
        public void InitISign()
        {
            SignatureInfo = new CKSNhanVienInfo();

            CKS_NhanVien NV = CKS_NhanVien.GetCKS_NhanVienStr
            ("SELECT * FROM CKS_NhanVien WHERE NhanVienID = " + DatabaseCache.DangNhap.NhanVienID);

            ///Copy dữ liệu, sang SignatureInfo
            BusinessAdapter.Global.Reflection.SetPropertyFromObject(SignatureInfo, NV);

            CKSNhanVienID = SignatureInfo.CKSNhanVienID;

            Waitting = new List<iSignDocument>();

            ///Trạng thái chữ ký số phải là 1: Đã duyệt thì load danh sách
            if (CKSNhanVienID > 0 && SignatureInfo.TrangThai==1)
            {
                foreach (CKSVanBanInfo BI in iSignService.GetDocumentList(CKSNhanVienID, ISIGN_DOC_ENUM.CHUAKY))
                {
                    iSignDocument id = new iSignDocument();
                    BusinessAdapter.Global.Reflection.SetPropertyFromObject(id, BI);

                    Waitting.Add(id);
                }
            }
        }
    }
}