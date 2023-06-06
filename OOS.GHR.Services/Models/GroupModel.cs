using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OOS.GHR.Services.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public HttpPostedFileBase Icon { get; set; }

        public string IconPath { get; set; }

        public string Description { get; set; }
    }
}
