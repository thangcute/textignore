using OOS.GHR.BusinessAdapter.API;
using OOS.GHR.BusinessAdapter.iSign;
using OOS.GHR.Framework;
using OOS.GHR.Library;
using OOS.GHR.SelfService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOS.GHR.SelfService.Controllers
{
    public class eContractController : OOS.GHR.Framework.Controllers.BaseController
    {
        [OOSAuthorization(Code ="PORTAL_eContract")]
        public ActionResult Index()
        {
            eContractModel cm = new eContractModel();
            cm.LoadData(0);

            return View(cm);
        }

        [OOSAuthorization(Code = "PORTAL_eContract")]
        public ActionResult eContractWaitting()
        {
            eContractModel cm = new eContractModel();
            cm.LoadData(11);

            return PartialView("_eContractList", cm);
        }

        public ActionResult EmployeeSignContractInfo(int? eContractID)
        {
            CKS_eContract ct = CKS_eContract.GetCKS_eContract(eContractID.Value);

            CKSeContractInfo CI = new CKSeContractInfo();
            ct.CopyPropertiesTo(CI);

            return PartialView("_ContractInfo", CI);
        }

        [HttpPost]
        public JsonResult SignerSignContract(int? ID, string Code)
        {
            if (ID!=null && Code!="")
            {
                IContractProvider CP = eContractProvider.GetProvider(ConfigClass.ISIGN_PROVIDER);

                string errMsg = CP.SignContract(ID.Value, Code);

                if (errMsg == "" && errMsg != null)
                    return ReturnJSValue(0);
                else
                    return ReturnErrorMessage(errMsg);
            }
            return ReturnErrorMessage(Translate("Có lỗi trong khi ký Hợp đồng số !"));
        }

        [HttpPost]
        public JsonResult EmployeeSignContract(int? ID, string Code)
        {
            if (ID != null && Code != "")
            {
                IContractProvider CP = eContractProvider.GetProvider(ConfigClass.ISIGN_PROVIDER);
                string errMsg = CP.StaffSignContract(ID.Value, Code);
                if (errMsg == "" && errMsg != null)
                    return ReturnJSValue(0);
                else
                    return ReturnErrorMessage(errMsg);
            }
            return ReturnErrorMessage(Translate("Có lỗi trong khi ký Hợp đồng số !"));
        }
    }
}