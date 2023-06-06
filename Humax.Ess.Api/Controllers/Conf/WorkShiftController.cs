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
    public class WorkShiftController : BaseApiController
    {
        // GET api/<controller> -- GetWorkShiftList
        public async Task<ApiResponse> Get()
        {
            return new ApiResponse()
            {
                Status = 1,
                Data = NS_CaLamViecList.GetNS_CaLamViecList().Select(x => new
                {
                    Id = x.CaLamViecID,
                    Name = x.TenCa
                })
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/WorkShift/GetTimeSheet")]
        [HttpGet]
        public async Task<ApiResponse> GetTimeSheet(int Month, int Year)
        {
            return new ApiResponse()
            {
                Status = 0,
                Message = DatabaseCache.GetText("Không có dữ liệu")
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