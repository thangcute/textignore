using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOS.GHR.Library;
using System.Data;

namespace OOS.GHR.SelfService.Models
{
    public class CareerPathInfo
    {
        public int ChucVuID { get; set; }

        public string TenChucVu { get; set; }

        public string MoTaCongViec { get; set; }

        public string TenNhomChucVu { get; set; }

        public string TenChucDanh { get; set; }

        public string Color { get; set; }

        public List<ObjectID> YeuCauDaoTao { get; set; }

        public List<ObjectID> KPI { get; set; }

        public List<ObjectID> Competency { get; set; }

        public bool IsTop { get; set; }

        public bool IsLast { get; set; }

        public bool IsCurrent { get; set; }

        public void LoadInfo()
        {
            YeuCauDaoTao = new List<ObjectID>();
            DataTable dt = OOS.GHR.BusinessAdapter.DanhMuc.DanhMuc.GetChucVu_DaoTao(ChucVuID);
            foreach(DataRow dr in dt.Rows)
            {
                if ((bool)dr["Chon"])
                {
                YeuCauDaoTao.Add(new ObjectID()
                {
                    Name = dr["TenKhoaDaoTao"].ToString(),
                    ID = (int)dr["ID"]
                });
                }
            }

            Competency = new List<ObjectID>();
            dt = OOS.GHR.BusinessAdapter.DanhGia.CapacityService.GetNangLuc_FromChucVu(ChucVuID, true, true);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TenMoTa"].ToString() != "")
                {
                    Competency.Add(new ObjectID()
                    {
                        Name = dr["TenNangLuc"].ToString(),
                        ID = (int)dr["NangLucID"],
                        StrID = dr["TenMoTa"].ToString()
                    });
                }
            }
        }
    }

    public class CareerPathModel
    {
        public List<CareerPathInfo> CareerPath = new List<CareerPathInfo>();

        public string TenChucVu { get; set; }

        public void LoadData(int ChucVuID)
        {
            string strSQL =
            @"declare @cvID int = " + ChucVuID + @"

            declare @tbl TABLE (id int, valu int, prID int)
            declare @chucvuid int
            set @chucvuid = @cvid
            while (1=1)
            begin
	            declare @chucvuconid int = (select top 1 isnull(ChucVuID,0) from ns_dschucvu where ChucVuChaID=@chucvuid)
	            if (@chucvuconid<=0 or @chucvuconid is null or @chucvuconid=@chucvuid)
		            break;
	            if ((select count(*) from @tbl where id=@chucvuconid)>0)
		            break;
	            set @chucvuid = @chucvuconid
	            insert into @tbl(id, valu, prID) values (@chucvuconid, 0, 0)
            end

            set @chucvuid = @cvid
            insert into @tbl(id, valu, prID) values (@cvID, 0, 0)
            while (1=1)
            begin
	            declare @cvchaID int = (select isnull(ChucVuChaID,0) from ns_dschucvu where ChucVuID=@chucvuid)
	            if (@cvchaID<=0 or @cvchaID is null or @cvchaID=@chucvuid)
		            break;
	            if ((select count(*) from @tbl where id=@cvchaID)>0)
		            break;
	            set @chucvuid = @cvchaid
	            insert into @tbl(id, valu, prID) values (@cvchaid, 0, 0)
            end

            select ChucVuID, TenChucVu, MaChucVu, DienGiai, ISNULL(NS_DsNhomChucVu.TenNhomChucVu,'') TenNhomChucVu, ISNULL(NS_DsChucDanh.TenChucDanh,'') TenChucDanh from @tbl as tb
            inner join NS_DsChucVu on ChucVuID=tb.id
            left join NS_DsNhomChucVu on NS_DsNhomChucVu.NhomChucVuID = NS_DsChucVu.NhomChucVuID
            left join NS_DsChucDanh on NS_DsChucDanh.ChucDanhID = NS_DsChucVu.ChucDanhID
            Order By NS_DsChucDanh.ThuTu
            ";

            string[] colors = new string[] { "grey", "purple", "green", "yellow", "blue" };
            DataTable dt = Database.ExecTable(strSQL);

            bool found = true;

            int IndexColor = 0;
            for(int index=0; index<dt.Rows.Count; index++)
            {
                DataRow dr = dt.Rows[index];
                CareerPathInfo cc = new CareerPathInfo()
                {
                    ChucVuID = (int)dr["ChucVuID"],
                    TenChucVu = dr["TenChucVu"].ToString(),
                    MoTaCongViec = dr["DienGiai"].ToString(),
                    TenNhomChucVu = dr["TenNhomChucVu"].ToString(),
                    TenChucDanh = dr["TenChucDanh"].ToString(),
                    IsTop = index==0,
                    IsLast = index == dt.Rows.Count-1
                };
                cc.LoadInfo();

                if (!found)
                    cc.Color = colors[0];
                else
                {
                    IndexColor++;
                    if (IndexColor >= 4) IndexColor = 1;
                    cc.Color = colors[IndexColor];
                }

                if (cc.ChucVuID == ChucVuID)
                {
                    found = false;
                    this.TenChucVu = cc.TenChucVu;
                    cc.IsCurrent = true;
                    cc.Color = "blue";
                }
                CareerPath.Add(cc);
            }
        }
    }
}