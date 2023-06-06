using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OOS.GHR.SelfService.Models
{
    public class ElearningModel
    {
        public List<OOS.GHR.Trainning.Models.TrainningCourseInfoModel> BatBuocDangKyList = null;

        public List<OOS.GHR.Trainning.Models.TrainningCourseInfoModel> DuocPhepDangKy = null;

        public List<OOS.GHR.Trainning.Models.TrainningCourseInfoModel> DangThamGia = null;

        public List<OOS.GHR.Trainning.Models.TrainningCourseInfoModel> DaThamGia = null;
    }
}