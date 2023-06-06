using OOS.GHR.Services.Services.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Conf
{
    public class LocationController : ApiController
    {
        public async Task<object> Get(string search="", int limit = 100, int page = 1)
        {
            if(search == null || search.Trim().Length < 2)
                return new
                {
                    Status = 0,
                    Message = "Giá trị tìm kiếm phải >= 2 ký tự",
                    Data = (object)null
                };

            var data = await LocationService.get(search.Trim(), limit, page);
            return new
            {
                Status = 1,
                Message = "Location",
                Data = data
            };
        }

        // GET api/<controller>
        [HttpGet]
        [Route("api/Location/Province")]
        public async Task<object> GetProvince()
        {
            var data = await LocationService.getProvince();
            return new
            {
                Status = 1,
                Message = "Province",
                Data = data
            };
        }

        [HttpGet]
        [Route("api/Location/District")]
        public async Task<object> GetDistrict(int id = 0)
        {
            var data = await LocationService.getDistrict(id);
            return new
            {
                Status = 1,
                Message = "District",
                Data = data
            };
        }

        [HttpGet]
        [Route("api/Location/Ward")]
        public async Task<object> GetWard(int id = 0)
        {
            var data = await LocationService.getWard(id);
            return new
            {
                Status = 1,
                Message = "Ward",
                Data = data
            };
        }
    }
}