using OOS.GHR.Services.Helpers;
using OOS.GHR.Services.Services.Conf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Conf
{
    public class CategoryController : BaseApiController
    {
        public async Task<object> Get(string Type = "")
        {
            if (string.IsNullOrEmpty(Type))
                return new
                {
                    Status = 0,
                    Message = "Không lấy được thông tin danh mục",
                    Data = (object)null
                };

            List<string> categoryList = new List<string>(){
                "NS_DsQuocTich",
                "NS_DsDanToc",
                "NS_DsTonGiao",

                "NS_DsTrinhDo",
                "NS_DsTruongDaoTao",
                "NS_DsHangTotNghiep",
                "NS_DsChuyenNganh",
                "NS_CaLamViec",
                "NS_TL_KyHieuChamCong",
                "NS_DsLyDoTangCa",

                "NS_DsLyDoCongTac",
                "NS_DsNganhNghe",
                "NS_DT_KhoaDaoTao",
                "NS_DsHeDaoTao",
                "NS_DsNganhHoc",
                "NS_DsVanBang",
                "NS_DsChungChi",
                "NS_DT_KetQuaDaoTao",
                "PhongBan",
                "NS_DsChucVu",
                "NS_DsChucDanh",
                "NS_DsLyDoChuyenCanBo",

                "DX_LoaiDeXuat",

                "NS_DsNganHang",

                "NS_DsLoaiHopDong"

            };

            if(!categoryList.Contains(Type))
            {
                return new
                {
                    Status = 0,
                    Message = "Danh mục không tìm thấy",
                    Data = (object)null
                };
            }

            //if (CacheHelper.GetValue(string.Format("Category-{0}", Type)) != null)
            //{
            //    return new
            //    {
            //        Status = 0,
            //        Message = "Dữ liệu danh mục",
            //        Data = CacheHelper.GetValue(string.Format("Category-{0}", Type))
            //    };
            //}

            string sqlQuery = "";
            switch (Type)
            {
                case "DX_LoaiDeXuat":
                    sqlQuery = string.Format("SELECT [LoaiDeXuatID] AS [Id], [TenLoaiDeXuat] AS [Name], '' AS [Des]  FROM {0}", Type);
                    break;

                case "NS_DsQuocTich":
                    sqlQuery = string.Format("SELECT [QuocTichID] AS [Id], [TenNuoc] AS [Name], '' AS [Des]  FROM {0}", Type);
                    break;

                case "NS_DsDanToc":
                    sqlQuery = string.Format("SELECT [DanTocID] AS [Id], [Ten] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsTonGiao":
                    sqlQuery = string.Format("SELECT [TonGiaoID] AS [Id], [Ten] AS [Name], [MoTa] AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsTrinhDo":
                    sqlQuery = string.Format("SELECT [TrinhDoID] AS [Id], [TenTrinhDo] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsTruongDaoTao":
                    sqlQuery = string.Format("SELECT [TruongDaoTaoID] AS [Id], [Ten] AS [Name], [MoTa] AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsHangTotNghiep":
                    sqlQuery = string.Format("SELECT [HangTotNghiepID] AS [Id], [Ten] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsChuyenNganh":
                    sqlQuery = string.Format("SELECT [ChuyenNganhID] AS [Id], [Ten] AS [Name], [MoTa] AS [Des] FROM {0}", Type);
                    break;

                case "NS_CaLamViec":
                    sqlQuery = string.Format("SELECT [CaLamViecID] AS [Id], [TenCa] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_TL_KyHieuChamCong":
                    sqlQuery = string.Format("SELECT [KyHieuChamCongID] AS [Id], [KyHieu] AS [Name], [MoTa] AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsLyDoTangCa":
                    sqlQuery = string.Format("SELECT [LyDoTangCaID] AS [Id], [TenLyDoTangCa] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsLyDoCongTac":
                    sqlQuery = string.Format("SELECT [LyDoCongTacID] AS [Id], [TenLyDoCongTac] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsNganhNghe":
                    sqlQuery = string.Format("SELECT [NganhngheID] AS [Id], [TenNganhNghe] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DT_KhoaDaoTao":
                    sqlQuery = string.Format("SELECT [KhoaDaoTaoID] AS [Id], [TenKhoaDaoTao] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsHeDaoTao":
                    sqlQuery = string.Format("SELECT [HeDaoTaoID] AS [Id], [Ten] AS [Name], [MoTa] AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsNganhHoc":
                    sqlQuery = string.Format("SELECT [NganhHocID] AS [Id], [TenNganhHoc] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsVanBang":
                    sqlQuery = string.Format("SELECT [VanBangID] AS [Id], [TenVanBang] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsChungChi":
                    sqlQuery = string.Format("SELECT [ChungChiID] AS [Id], [Ten] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DT_KetQuaDaoTao":
                    sqlQuery = string.Format("SELECT [KetQuaDaoTaoID] AS [Id], [TenKetQuaDaoTao] AS [Name], [GhiChu] AS [Des] FROM {0}", Type);
                    break;

                case "PhongBan":
                    sqlQuery = string.Format("SELECT [PhongBanID] AS [Id], [Ten] AS [Name], CONCAT('',[PhongBanChaID]) AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsChucVu":
                    sqlQuery = string.Format("SELECT [ChucVuID] AS [Id], [TenChucVu] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsChucDanh":
                    sqlQuery = string.Format("SELECT [ChucDanhID] AS [Id], [TenChucDanh] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsLyDoChuyenCanBo":
                    sqlQuery = string.Format("SELECT [LyDoChuyenCanBoID] AS [Id], [TenLyDoChuyenCanBo] AS [Name], [GhiChu] AS [Des] FROM {0}", Type);
                    break;

                case "NS_DsNganHang":
                    sqlQuery = string.Format("SELECT [NganHangID] AS [Id], [TenNganHang] AS [Name], [MaNganHang] AS [Des] FROM {0}", Type);
                    break;
                case "NS_DsLoaiHopDong":
                    sqlQuery = string.Format("SELECT [LoaiHopDongID] AS [Id], [TenLoaiHopDong] AS [Name], '' AS [Des] FROM {0}", Type);
                    break;

                default:
                    break;
            }

            if (string.IsNullOrEmpty(sqlQuery))
            {
                return new
                {
                    Status = 0,
                    Message = "Danh mục không tìm thấy",
                    Data = (object)null
                };
            }

            string conID = OOS.GHR.Library.Database.Connection;
            DataTable data = OOS.GHR.Library.Database.ExecTable(sqlQuery, false, conID);

            //CacheHelper.Add(string.Format("Category-{0}", Type), data, DateTimeOffset.UtcNow.AddMinutes(15));

            return new
            {
                Status = 1,
                Message = "Dữ liệu danh mục",
                Data = data
            };
            //return "value";
        }

        [HttpGet]
        [Route("api/Category/TrainingContent")]
        public async Task<object> GetTrainingContent(int id)// id : DotDaoTaoID
        {
            var data = CategoryService.getTrainingContent(id);
            return new
            {
                Status = 1,
                Message = "Nội dung đào tạo",
                Data = data.Result
            };
        }
    }
}