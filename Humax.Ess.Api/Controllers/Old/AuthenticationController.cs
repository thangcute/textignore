using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOS.GHR.Library;
using System.Data;
using OOS.GHR;

namespace Humax.Ess.Api.Controllers.Old
{
    public class AuthenticationController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            Response.StatusCode = 500;
            filterContext.Result = new JsonResult
            {
                Data = new { success = false, error = filterContext.Exception.Message },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult Test()
        {
            return Content(DatabaseCache.Version);
        }

        [HttpPost]
        public ActionResult CheckDomain()
        {
            OOSSessionManager.AddNewHostname(Database.HostName);
            var logo = (byte[])Database.ExecScalar("SELECT TOP 1 Logo FROM SYS_ThongTinCongTy");
            return Json(new { Status = 1, Logo = logo });
        }

        [HttpPost]
        public ActionResult Login(string DeviceId, string FireBaseToken, string UserName, string Password)
        {
            string lDAPAddress = System.Configuration.ConfigurationManager.AppSettings["LDAP"];
            try
            {
                SYS_NguoiDung nd = OOS.GHR.Core.Authentication.Login(UserName, Password, lDAPAddress != "" && lDAPAddress != null, lDAPAddress);
                if (nd != null && nd.NhanVienID <= 0)
                {
                    return Json(new { Status = 0, Message = "User không có thông tin nhân viên !" });
                }
                if (nd != null && !nd.ActiveMobileApp)
                {
                    return Json(new { Status = 0, Message = "Bạn không có quyền truy cập vào tính năng này !" });
                }
                if (nd != null && nd.NguoiDungID > 0)
                {
                    OOSDirectory.CreateDefaultFolder();

                    var token = OOS.GHR.Core.Authentication.CreatePublicKey();

                    Request.Headers.Add("Token", token);

                    ///Khởi tạo Hostname nếu chưa có
                    HostnameData host = OOSSessionManager.AddNewHostname(Database.HostName);

                    //Gọi hàm đăng nhập: Tìm kiếm nhân viên, Init phân quyền, ...
                    UserSessionData User = OOS.GHR.Core.Authentication.DoLogin(FireBaseToken, nd, token, null, host);

                    if (!string.IsNullOrEmpty(DeviceId) && !string.IsNullOrEmpty(FireBaseToken))
                    {
                        nd.LastLogin = DateTime.Now;
                        nd.DeviceId = FireBaseToken;
                        nd.Token = "";
                        nd.Do_Update();
                    }

                    return Json(new
                    {
                        Status = 1,
                        Token = token,
                        Message = "Thành công",
                        EmployeeCode = User.NhanVien.MaNhanVien,
                        FullName = User.NhanVien.ToString(),
                        JobTitle = User.NhanVien.TenChucVu,
                        Picture = User.NhanVien.Anh,
                        Permissions = User.ToListQuyen()
                    });

                }
                return Json(new { Status = 0, Message = "Wrong UserName Or Password" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        SortedList<string, string> GetQuyenList(int NguoiDungID)
        {
            var QuyenList = new SortedList<string, string>();
            string sSelect = "SELECT DISTINCT QuyenID, ThaoTac FROM SYS_NguoiDung_Quyen Where NguoiDungID=" + NguoiDungID + " Order By QuyenID";
            var dtt = Database.ExecTable(sSelect);
            foreach (DataRow dr in dtt.Rows)
            {
                if (!QuyenList.ContainsKey(dr["QuyenID"].ToString()))
                    QuyenList.Add(dr["QuyenID"].ToString(), dr["ThaoTac"].ToString());
            }
            return QuyenList;
        }

        [HttpPost]
        public ActionResult CheckAlive()
        {
            var Token = Request.Headers["Token"];
            return Json(new { Status = OOSSessionManager.GetSessionData(Token) != null ? 1 : 0 });
        }
    }
}