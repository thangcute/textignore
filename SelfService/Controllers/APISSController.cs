using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace OOS.GHR.SelfService.Controllers
{
    public class APISSController : ApiController
    {
        [System.Web.Mvc.Route("api/apiss/updatevalue")]
        [System.Web.Http.HttpPost]
        public void UpdateValue(string name, string value)
        {
            Console.WriteLine(Request);
        }
    }
}
