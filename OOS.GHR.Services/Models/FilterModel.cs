using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models
{
    public class FilterModel
    {

    }

    public class SelectFilterModel
    {

        public string search { get; set; }

        public int page { get; set; }

        public int? size { get; set; }

        public bool? all { get; set; }
    }

    public class GridFilterModel
    {
        public int page { get; set; }
        public int size { get; set; }
        public string filter { get; set; }
    }
}
