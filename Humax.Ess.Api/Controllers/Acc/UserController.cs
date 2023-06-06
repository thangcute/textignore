using Humax.Ess.Api.Helpers;
using Humax.Ess.Api.Models;
using Humax.Ess.Api.Models.Ess;
using OOS.GHR.Library;
using OOS.GHR.Services.Services;
using OOS.GHR.Services.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Acc
{
    public class UserController : BaseApiController
    {
        // GET api/<controller> -- GetUserInfo
        public async Task<ApiResponse> Get()
        {
            var employee = DatabaseCache.NhanVien;
            if (employee == null)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Không tìm thấy thông tin.")
                };
            }
            return new ApiResponse()
            {
                Status = 1,
                Data = new
                {
                    FullName = employee.Ho + " " + employee.HoTen,
                    JobTitle = NS_DsChucVu.GetTenChucVuByID(employee.ChucVuID),
                    Phone = employee.DienThoai,
                    Email = employee.Email,
                    Skype = employee.Skype,
                    Address = employee.DiaChi,
                    Picture = Business.AvatarToBinary(employee.Anh, employee.HoTen[0])
                }
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5 -- UpdateUserInfo
        public async Task<ApiResponse> Put(string Phone, string Email, string Skype, string Address, string Picture)
        {
            var token = this.Token;
            var employee = OOSSessionManager.GetSessionData(token).NhanVien;
            if (employee == null)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Không tìm thấy thông tin.")
                };
            }
            if (Phone != null)
            {
                employee.DienThoai = Phone;
            }
            if (Email != null)
            {
                employee.Email = Email;
            }
            if (Skype != null)
            {
                employee.Skype = Skype;
            }
            if (Address != null)
            {
                employee.DiaChi = Address;
            }
            if (!string.IsNullOrEmpty(Picture))
            {
                employee.Anh = Convert.FromBase64String(Picture);
            }
            employee.Save();
            OOSSessionManager.GetSessionData(token).NhanVien = employee;
            return new ApiResponse()
            {
                Status = 1,
                Message = DatabaseCache.GetText("Cập nhật thành công.")
            };
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [Route("api/User/ChangePassword2")]
        [HttpPost]
        public async Task<object> ChangePassword([FromBody] UserChangePasswordModel model)
        {
            if(model.NewPassword == model.OldPassword)
                return new
                {
                    Status = 0,
                    Message = "Mật khẩu mới không được đặt trùng với mật khẩu trước đó!"
                };

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$"; //Tối thiểu tám ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường và một số
            Regex reg = new Regex(pattern);

            if (!reg.IsMatch(model.NewPassword))
                return new
                {
                    Status = 0,
                    Message = "Tối thiểu 6 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường và một số!"
                };

            if (ModelState.IsValid)
            {
                int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
                int nguoidungId = OOS.GHR.Library.DatabaseCache.DangNhap.NguoiDungID;

                var response = await UserService.ChangePassword(nguoidungId, model.OldPassword, model.NewPassword);

                if (response.rs)
                    return new
                    {
                        Status = 1,
                        Message = "Cập nhật mật khẩu thành công"
                    };
                else
                    return new
                    {
                        Status = 0,
                        Message = response.message
                    };
            }
            else
                return new
                {
                    Status = 0,
                    Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                    Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                };
        }

        [Route("api/User/ChangePassword")]
        [HttpPost, HttpGet]
        public async Task<ApiResponse> ChangePassword(string OldPassword = "", string NewPassword = "")
        {
            if (!ValidatePassword(NewPassword, DatabaseCache.DangNhap.TenDangNhap))
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Mật khẩu mới phải có độ dài tối thiểu 8 ký tự. Bao gồm chữ hoa, chữ thường, số không bao gồm các từ dễ nhận biết (123,abc) !")
                };
            }

            DatabaseCache.DangNhap.MustChangePassword = false;
            DatabaseCache.DangNhap.MatKhau = "[" + Database.GetMD5(NewPassword) + "]";
            DatabaseCache.DangNhap.IsDirty = true;
            DatabaseCache.DangNhap.Do_Update();

            return new ApiResponse()
            {
                Status = 0,
                Message = DatabaseCache.GetText("Thay đổi thành công.")
            };
        }
        public static bool ValidatePassword(string password, string username, int SecurityLevel = 2)
        {
            const int MIN_LENGTH = 7;
            const int MAX_LENGTH = 150;

            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;
            bool hasSpecialLetter = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                    else if (char.IsSymbol(c)) hasSpecialLetter = true;
                }
            }

            if (password.Contains("123"))
                return false;

            if (password.ToLower().Contains("abc"))
                return false;

            if (password.ToLower().Contains(username.ToLower()))
                return false;

            bool isValid = false;

            switch (SecurityLevel)
            {
                case 1:
                    isValid = meetsLengthRequirements;
                    break;

                case 2:
                    isValid = meetsLengthRequirements && hasUpperCaseLetter && hasLowerCaseLetter && hasDecimalDigit;
                    break;

                case 3:
                    isValid = meetsLengthRequirements && hasUpperCaseLetter && hasLowerCaseLetter && hasDecimalDigit && hasSpecialLetter;
                    break;
            }

            return isValid;
        }

    }
}