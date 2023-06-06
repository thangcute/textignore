using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Conf
{
    public class CategoryService
    {
		public static async Task<object> getTrainingContent(int DotDaoTaoId)
		{
			using (var db = new OosContext())
			{
                try
                {
					var _object = db.NS_DT_DotDaoTao_NoiDung
						.Where(x => x.DotDaoTaoID == DotDaoTaoId)
						.Select(x => new DefaultModel
						{ 
							Id = x.NoiDungDaoTaoID,
							Name = x.TenNoiDungDaoTao,
							Des = ""
						});

					if (_object.Any())
                    {
						//var data = _object.ToList().Concat(new List<DefaultModel>() {
						//	new DefaultModel() {
						//		Id = 0,
						//		Name = "Tổng hợp",
						//		Des = ""
						//	}
						//});
						return _object.ToList();
					}
				}
				catch(Exception ex)
                {

                }
			}
			return null;
		}
	}
}
