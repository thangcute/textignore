using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Models
{
    public class ApiResponseMessage
    {
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }

        public object data { get; set; }
    }
}