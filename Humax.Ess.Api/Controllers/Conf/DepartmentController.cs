using Humax.Ess.Api.Models;
using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Conf
{
    public class DepartmentController : BaseApiController
    {
        // GET api/<controller> -- GetDepartmentsList
        public async Task<ApiResponse> Get()
        {
            return new ApiResponse()
            {
                Status = 1,
                Data = PhongBanList.GetPhongBanList().Select(x => new
                {
                    Id = x.PhongBanID,
                    Name = x.Ten,
                    ParentId = x.PhongBanChaID
                })
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