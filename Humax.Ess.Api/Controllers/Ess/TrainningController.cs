using Humax.Ess.Api.Helpers;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Services.Ess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class TrainningController : BaseApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("api/Trainning/Open")]
        public async Task<object> GetOpenning()
        {
            var data = TrainningService.get("Openning");
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        [HttpGet]
        [Route("api/Trainning/Finished")]
        public async Task<object> GetFinished()
        {
            var data = TrainningService.get("Finished");
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        [HttpGet]
        [Route("api/Trainning/Joinning")]
        public async Task<object> GetJoinning()
        {
            var data = TrainningService.get("Joinning");
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        // GET api/<controller>/5
        [HttpPost]
        [Route("api/Trainning/Register")]
        public async Task<object> PostRegister(int id) // DotDaoTaoId
        {
            if(await TrainningService.register(id))
            {
                return new
                {
                    Status = 1,
                    Message = "Register Success",
                    Data = id
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "Register Error",
                    Data = id
                };
            }
        }

        [HttpPost]
        [Route("api/Trainning/Reject")]
        public async Task<object> PostReject(int id, string reason = "")
        {
            if (await TrainningService.reject(id))
            {
                return new
                {
                    Status = 1,
                    Message = "Reject Success",
                    Data = id
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "Reject Error",
                    Data = id
                };
            }
        }

        //select* from NS_DT_DsTieuChiDanhGia -- cac tieu chi
        //select* from NS_DT_DotDaoTao_NhanVienDanhGia -- Luu ket qua danh gia

        [HttpGet]
        [Route("api/Trainning/Evaluate")]
        public async Task<object> GetEvaluate(int id, int DotDaoTaoId = 0) // id: NoiDungId; DotDaoTaoId: DotDaoTaoId, 
        {
            var data = await TrainningService.getEvaluate(id, DotDaoTaoId);
            return new
            {
                Status = 1,
                Message = "GetEvaluate",
                Data = data
            };
        }

        [HttpGet]
        [Route("api/Trainning/ListEvaluate")]
        public async Task<object> GetListEvaluate(int id) // id: DotDaoTaoId, 
        {
            var data = await TrainningService.getEvaluate(DotDaoTaoId: id);
            return new
            {
                Status = 1,
                Message = "GetListEvaluate",
                Data = data
            };
        }

        [HttpPost]
        [Route("api/Trainning/Evaluate")]
        public async Task<object> PostEvaluate(int id, [FromBody] List<TrainingEvaluateModel> models)
        {
            if (ModelState.IsValid)
            {
                if(await TrainningService.saveEvaluate(id, models))
                {
                    return new
                    {
                        Status = 1,
                        Message = "SaveEvaluate",
                        Data = id
                    };
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = "Error",
                        Data = id
                    };
                }
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                    Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                };
            }
        }
        
        // POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}