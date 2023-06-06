using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Helpers;
using OOS.GHR.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.System
{
    public class UserService
    {
        public static async Task<string> EncodePassword(string password)
        {
            byte[] encoded = ASCIIEncoding.UTF8.GetBytes(password);

            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] hashCode = md5Provider.ComputeHash(encoded);

            string _password = "";
            foreach (byte a in hashCode)
                _password += String.Format("{0:X2}", a);

            return string.Format("[{0}]", _password);
        }

        public static async Task<(bool rs, string message)> ChangePassword(int id, string oldPassword, string newPassword)
        {
            bool _rs = false;
            string _message = "Có lỗi trong quá trình thực hiện.";

            using (var db = new OosContext())
            {
                try
                {
                    SYS_NguoiDung nguoiDung = await db.SYS_NguoiDung.FindAsync(id);

                    if (nguoiDung != null && nguoiDung.NguoiDungID > 0)
                    {
                        if (nguoiDung.MatKhau == await EncodePassword(oldPassword))
                        {
                            string _newPassword = await EncodePassword(newPassword);
                            nguoiDung.MatKhau = _newPassword;

                            db.SYS_NguoiDung.Attach(nguoiDung);
                            db.Entry(nguoiDung).State = EntityState.Modified;
                            await db.SaveChangesAsync();

                            _rs = true;
                            _message = "";
                        }
                        else
                            _message = "Mật khẩu cũ không đúng.";
                    }
                }
                catch
                {

                }
            }

            return (_rs, _message);
        }

        public static UserInfoModel GetUserInfo(int NguoiDungID)
        {
            UserInfoModel rs = new UserInfoModel();
            var dataCache = CacheHelper.GetValue(string.Format("{0}-UserInfo", NguoiDungID.ToString()));
            if (dataCache != null)
                rs = (UserInfoModel)dataCache;
            else
            {
                using (var db = new OosContext())
                {
                    var data = (from ac in db.SYS_NguoiDung
                                join em in db.NhanViens on ac.NhanVienID equals em.NhanVienID
                                join cv in db.NS_DsChucVu on em.ChucVuID equals cv.ChucVuID
                                where ac.NguoiDungID == NguoiDungID
                                select new UserInfoModel
                                {
                                    UserId = (int)ac.NhanVienID,
                                    AccountId = ac.NguoiDungID,
                                    EmployeeCode = ac.ND_MaNhanVien,
                                    FullName = em.Ho + " " + em.HoTen,
                                    JobTitle = cv.TenChucVu,
                                    Picture = em.Anh,
                                });
                    if (data.Any())
                    {
                        rs = data.FirstOrDefault();
                        var permission = db.SYS_NguoiDung_Quyen.Where(x => x.NguoiDungID == rs.AccountId && x.ThaoTac == 1).Select(x => x.QuyenID);
                        if (permission.Any())
                        {
                            rs.Permissions = permission.ToList();
                        }

                        CacheHelper.Add(string.Format("{0}-UserInfo", NguoiDungID), rs, DateTimeOffset.UtcNow.AddMinutes(20));
                    }
                }
            }

            return rs;
        }
    }
}
