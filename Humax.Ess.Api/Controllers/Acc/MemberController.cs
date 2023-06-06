using Humax.Ess.Api.Helpers;
using Humax.Ess.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Acc
{
    public class MemberController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Member/GetMemberList")]
        [HttpGet]
        public async Task<ApiResponse> GetMemberList(int DepartmentId, int PageIndex = 0, int PageSize = 10)
        {
            var memberList = Business.GetNhanVienList(DepartmentId, PageIndex, PageSize);
            return new ApiResponse()
            {
                Status = 1,
                Data = memberList.Select().Select(x => new
                {
                    Id = x["NhanVienID"],
                    Name = x["Ho"] + " " + x["HoTen"],
                    Email = x["Email"],
                    Phone = x["DienThoai"],
                    JobTitle = x["TenChucDanh"],
                    Picture = Business.AvatarToBinary(x["Anh"], x["HoTen"]?.ToString().FirstOrDefault() ?? 'O')
                })
            };
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}