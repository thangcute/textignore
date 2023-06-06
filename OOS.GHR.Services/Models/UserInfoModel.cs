using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models
{
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public byte[] Picture { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
