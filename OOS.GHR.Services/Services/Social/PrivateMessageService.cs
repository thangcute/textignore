using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Models.Social;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Social
{
    public class PrivateMessageService
    {
		public static async Task<object> get(int limit, int offset)
		{
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			using (var db = new OosContext())
			{
				var distinctData = db.NS_Private_Message.Where(x => 
					(
						x.UserToID == nhanvienId ||
						x.UserFromID == nhanvienId
					)
					&& x.UserToID != x.UserFromID
					//&& x.IsRead == false
					&& !x.DateSent.Equals(null)
				).GroupBy(x => (x.UserFromID + x.UserToID)).Select(g => new {
					UserFromID = g.Key.Value,
					PrivateMessageID = g.Max(x => x.PrivateMessageID),
					Count = g.Count()
				});

				if(distinctData != null && distinctData.Any())
                {
					var _obj = (from pms in db.NS_Private_Message
								join dis in distinctData on pms.PrivateMessageID equals dis.PrivateMessageID
								//join nv in db.NhanViens on pms.UserFromID equals nv.NhanVienID
								join nv in db.NhanViens on (dis.UserFromID - nhanvienId) equals nv.NhanVienID
								//where distinctData.Select(c => c.PrivateMessageID).ToList().Contains(pms.PrivateMessageID)
								select new
								{
									Id = pms.PrivateMessageID,
									NgayThang = pms.DateSent,
									FromUser = nv.Ho + " " + nv.HoTen,
									FromUserID = nv.NhanVienID,
									UserAvatar = nv.Anh,
									LatestMessage = pms.Message,
									TotalRecord = dis.Count
								});

					if(_obj != null && _obj.Any())
                    {
						int total = _obj.Count();
						var _data = _obj.AsEnumerable().Select(c => new
						{
							Id = c.Id,
							NgayThang = c.NgayThang,
							FromUser = c.FromUser,
							FromUserID = c.FromUserID,
							UserAvatar = c.UserAvatar,
							LatestMessage = c.LatestMessage,
							ThoiGian = c.NgayThang.Value.ToString("dd-MM-yyyy HH:mm:ss"),
							TotalRecords = c.TotalRecord //distinctData.Where(d => d.PrivateMessageID == c.Id).FirstOrDefault().Count
						});

						if (_data.Any())
							return _data.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToList();
					}
				}
			}
			return null;
		}

		public static async Task<object> get(int fromId, int limit, int offset)
		{
			if(fromId > 0)
            {
				int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
				using (var db = new OosContext())
				{
					var data = (from pms in db.NS_Private_Message
								join nv in db.NhanViens on pms.UserFromID equals nv.NhanVienID
								where (
									(pms.UserToID == nhanvienId
									&& pms.UserFromID == fromId) ||
									(pms.UserToID == fromId
									&& pms.UserFromID == nhanvienId)
								)
								&& !pms.DateSent.Equals(null)
								select new
								{
									Id = pms.PrivateMessageID,
									FromID = pms.UserFromID,
									ToID = pms.UserToID,
									Content = pms.Message,
									Date = pms.DateSent,
									FileName = "",
									FileURL = ""
								});

					if (data.Any())
					{
						int total = data.Count();
						return data.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToList();
					}
				}
			}
			return null;
		}

		//Read message
		public static async Task<bool> read(int userFromId)
		{
			bool rs = false;
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			DateTime actionTime = DateTime.Now;
			using (var db = new OosContext())
			{
                try
                {
					db.NS_Private_Message.Where(x => x.UserFromID == userFromId && x.UserToID == nhanvienId && x.IsRead == false).ToList().ForEach(x => {
						x.IsRead = true;
					});
					await db.SaveChangesAsync();
					rs = true;
				}
				catch(Exception ex)
                {

                }
			}
			return rs;
		}

		public static async Task<bool> save(PrivateMessageModel model)
		{
			bool rs = false;
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			DateTime actionTime = DateTime.Now;
			using (var db = new OosContext())
			{
				NS_Private_Message entry = new NS_Private_Message();
				entry.PrivateMessageID = 0;
				entry.Message = model.Content;
				entry.UserFromID = nhanvienId;
				entry.UserToID = model.ToUserID;
				entry.IsRead = false;
				entry.IsSentMessage = true;
				entry.DateSent = actionTime;

				db.NS_Private_Message.Attach(entry);
				db.Entry(entry).State = EntityState.Added;
				rs = await db.SaveChangesAsync() > 0 ? true : false;
			}
			return rs;
		}

		public static async Task<bool> delete(int id)
        {
			bool rs = false;
			using (var db = new OosContext())
            {
				NS_Private_Message entry = db.NS_Private_Message.Find(id);
				if(entry != null)
                {
					db.NS_Private_Message.Remove(entry);
					rs = await db.SaveChangesAsync() > 0 ? true : false;
				}					
				//db.NS_Private_Message.Remove(db.)
            }
			return rs;
        }
	}
}
