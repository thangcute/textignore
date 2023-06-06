using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOS.GHR.Library;
using System.Data;
using System.Collections;
using OOS.GHR.Master.Models;
using DevExpress.Web;
using OOS.GHR.Framework.DynamicUI;

namespace OOS.GHR.SelfService.Models
{
    public class EvaluationInfo
    {
        public int Month = 0;

        public string NhomDanhGia = "";

        public int DiemSo = 0;

        public string XepLoai = "";
    }

    public class SS_ProfilePreviewModel : OOS.GHR.HRIS.Models.ProfilePreviewModel
    {
    }
}
