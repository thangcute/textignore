using OOS.GHR.BusinessAdapter.iSign;
using OOS.GHR.Library;
using OOS.GHR.Master.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OOS.GHR.SelfService.Models
{
    public class eContractModel : OOS.GHR.Framework.Mvc.OOSBaseModel
    {
        public CKSNhanVienInfo SignatureInfo { get; set; }

        public List<CKSeContractInfo> Waitting { get; set; }

        public List<CKSeContractInfo> SignedList { get; set; }

        public int CKSNhanVienID { get; set; }

        public int TrangThai { get; set; }

        public void LoadData(int TrangThaiStatus)
        {
            SignatureInfo = new CKSNhanVienInfo();
            SignatureInfo.LoadInfo(DatabaseCache.NhanVien.NhanVienID);

            this.CKSNhanVienID = SignatureInfo.CKSNhanVienID;

            DataTable dtContractList = OOS.GHR.BusinessAdapter.iSign.iSignService.GetContractList
            (0, 0, "", 0, 0, DatabaseCache.NhanVien.NhanVienID, 0);

            Waitting = new List<CKSeContractInfo>();
            foreach(DataRow dr in dtContractList.Rows)
            {
                CKSeContractInfo CI = new CKSeContractInfo();
                OOS.GHR.BusinessAdapter.Global.Reflection.SetPropertiesFromRow(CI, dr);
                Waitting.Add(CI);
            }

            SignedList = new List<CKSeContractInfo>();
            dtContractList = OOS.GHR.BusinessAdapter.iSign.iSignService.GetContractList
            (0, 0, "", 0, 0, DatabaseCache.NhanVien.NhanVienID, 33);
            foreach (DataRow dr in dtContractList.Rows)
            {
                CKSeContractInfo CI = new CKSeContractInfo();
                OOS.GHR.BusinessAdapter.Global.Reflection.SetPropertiesFromRow(CI, dr);
                SignedList.Add(CI);
            }
        }
    }
}