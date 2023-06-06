using OOS.GHR.Master.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using OOS.GHR.Library;
using OOS.GHR.Framework.DynamicUI;
using System.Collections;

namespace OOS.GHR.SelfService.Models
{
    public class RequestModel : OOS.GHR.Framework.Mvc.OOSBaseModel
    {
        public string TieuDe { get; set; }

        public Int32 DeXuatID { get; set; }

        public Int32 LoaiDeXuatID { get; set; }

        public DateTime? NgayDeXuat { get; set; }

        public Int32 NguoiDeXuatID { get; set; }

        public string NoiDung { get; set; }
        public string NoiDung_ThuGon { get; set; }

        public Int32 XetDuyet { get; set; }

        public Int32 QuyTrinhID { get; set; }

        public DataTable ApprovalUsers = new DataTable();

        public DataTable ReferrerUsers = new DataTable();

        public UIControlsModel Controls = new UIControlsModel();

        public void LoadControls(bool LoadValue = false)
        {
            DX_LoaiDeXuat_TruongDuLieuList ttList = null;
            ttList = DX_LoaiDeXuat_TruongDuLieuList.GetDX_LoaiDeXuat_TruongDuLieuListFromWhere
                ("LoaiDeXuatID = " + LoaiDeXuatID + " Order By STT", null);

            DX_DeXuat_DuLieuDeXuatList data = null;
            if (LoadValue)
                data = OOS.GHR.BusinessAdapter.Request.RequestService.LoadValues(DeXuatID);

            foreach(DX_LoaiDeXuat_TruongDuLieu TT in ttList)
            {
                ControlModel CM = ControlModel.GetFrom_DXTruongDuLieu(TT);
                CM.ControlID = "RF_" + TT.TruongDuLieuID.ToString();

                #region Load Data From DB
                if (LoadValue && data != null && data.Count>0)
                {
                    foreach(DX_DeXuat_DuLieuDeXuat dl in data)
                    {
                        if (dl.TruongDuLieuID==TT.TruongDuLieuID)
                        {
                            object oValue = null;
                            if (TT.KieuDuLieu=="DateTime")
                            {
                                oValue = dl.GiaTriDateTime;
                            }
                            else oValue = OOS.GHR.BusinessAdapter.Global.TypeTools.ConvertDataTypeSystem(TT.KieuDuLieu, dl.GiaTri);
                            CM.Value = oValue;
                        }
                    }
                }
                #endregion

                if (CM.IsNeedDatasource())
                {
                    if (TT.TableName != "" && TT.TextField != "" && TT.ValueField != "")
                    {
                        CM.DataSource = Database.ExecTable($"SELECT '' Name, -1 ID union all SELECT {TT.TextField} Name, {TT.ValueField} ID FROM {TT.TableName}",
                        DatabaseCache.GlobalParams);
                    }
                    else
                    {
                        if (TT.TextField.Contains("{"))
                        {
                            if (TT.TextField.StartsWith("{") && TT.ValueField.StartsWith("{"))
                            {
                                string[] Names = TT.TextField.Replace("{", "").Replace("}", "").Split(',');
                                string[] Values = TT.ValueField.Replace("{", "").Replace("}", "").Split(',');

                                if (Names.Length == Values.Length)
                                {
                                    List<ObjectID> lstSource = new List<ObjectID>()
                                {
                                    new ObjectID(){ ID=0, Name="" }
                                };

                                    for (int i = 0; i < Names.Length; i++)
                                    {
                                        lstSource.Add(new ObjectID()
                                        {
                                            ID = int.Parse(Names[i]),
                                            Name = Values[i]
                                        });
                                    }
                                    CM.DataSource = lstSource;
                                }
                            }
                        }
                        else
                        {
                            if (TT.QueryData.ToLower().StartsWith("select"))
                            {
                                SortedList SL = new SortedList();
                                SL.Add("@LoaiDeXuatID", LoaiDeXuatID);
                                SL.Add("@NguoiDungID", DatabaseCache.DangNhap.NguoiDungID);

                                CM.DataSource = Database.ExecTable(TT.QueryData, SL);
                            }
                        }
                    }
                    CM.TextField = "Name";
                    CM.ValueField = "ID";
                }

                Controls.alControls.Add(CM);
            }
        }

        [UIHint("Comments")]
        public Admin.Models.CommentsModel cm { get; set; }
    }

    public class RequestInfo
    {
        public int NguoiDuocDeXuatID = 0;

        public DataTableSource Info = null;

        public RequestInfo(int nvID)
        {
            this.NguoiDuocDeXuatID = nvID;
        }

        public void LoadData(int NvID)
        {
            string strSQL =
            @"SELECT DeXuatID, TenLoaiDeXuat, NgayDeXuat, DX_DeXuat.TieuDe, NoiDung_ThuGon,
            case 
                when SYS_XetDuyet.TrangThai<>1 THEN ISNULL(SYS_XetDuyet.NguoiDangChoDuyet,'')
                when SYS_XetDuyet.TrangThai=1 THEN ISNULL(SYS_XetDuyet.NguoiDuyetCuoi,'')
            end as NguoiDangChoDuyet, ISNULL(SYS_XetDuyet.GhiChu,'') YKienPheDuyet, DX_DeXuat.XetDuyet, NguoiDeXuat_HoVaTen
            from DX_DeXuat
            inner join DX_LoaiDeXuat on DX_DeXuat.LoaiDeXuatID = DX_LoaiDeXuat.LoaiDeXuatID
            left join (SELECT NguoiDuyetCuoi, XetDuyetID, NguoiDangChoDuyet, ObjectID, TrangThai, GhiChu FROM SYS_XetDuyet WHERE ObjectCode='DX_DeXuat') 
            as SYS_XetDuyet on SYS_XetDuyet.ObjectID = DX_DeXuat.DeXuatID
            WHERE DX_DeXuat.NguoiDeXuatID=" + NvID + " Order By NgayDeXuat Desc";

            Info = new DataTableSource(Database.ExecTable(strSQL));
        }
    }
}