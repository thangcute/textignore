using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using OOS.GHR.Library;
namespace OOS.GHR.SelfService.Models
{
    public class ExamOnlineModel
    {
        public int NoiDungDaoTaoID { get; set; }

        public int DotDaoTaoID { get; set; }

        public int BoCauHoiTracNghiemID { get; set; }

        public int ThiTracNghiemID { get; set; }

        public DateTime? BatDau { get; set; }

        public DateTime? KetThuc { get; set; }

        public int ThoiGianConLai { get; set; }

        public int ThoiGianHoanThanh { get; set; }

        public TimeSpan TGHoanThanh { get; set; }

        //STT câu hỏi đang sử dụng
        public int QuestionIndex { get; set; }

        //Tổng số câu trả lời
        public int TotalAnswers { get; set; }

        //Tổng số câu hỏi trong bộ trắc nghiệm
        public int TotalQuestion { get; set; }

        public string TenDotDaoTao { get; set; }

        public string TenNoiDung { get; set; }

        public int Status { get; set; }

        public TimeSpan TGSuDung { get; set; }

        public List<ObjectID> Questions = new List<ObjectID>();

        public List<ExamOnlineItemModel> AnswerList = new List<ExamOnlineItemModel>();

        public ExamOnlineItemModel Item = new ExamOnlineItemModel();

        public NS_DT_DotDaoTao_TraLoiTracNghiem CurrentItem = null;

        public int TongDiem { get; set; }

        public int DapAnDung { get; set; }

        public int ResetIndex { get; set; }

        public void LoadFinishedAnswer()
        {
            AnswerList = new List<ExamOnlineItemModel>();
            string strSQL =
            string.Format(
            @"SELECT DapAn1, DapAn2, DapAn3, DapAn4, DapAn5, NS_DT_DotDaoTao_TraLoiTracNghiem.DapAn,  NS_DT_CauHoiTracNghiem.DapAnDung,
            NS_DT_CauHoiTracNghiem.TenCauHoi, NS_DT_DotDaoTao_TraLoiTracNghiem.QuestionIndex, NS_DT_DotDaoTao_TraLoiTracNghiem.TraLoiTracNghiemID,
            NS_DT_CauHoiTracNghiem.Type
            FROM NS_DT_DotDaoTao_TraLoiTracNghiem
            inner join NS_DT_CauHoiTracNghiem on NS_DT_CauHoiTracNghiem.CauHoiTracNghiemID=NS_DT_DotDaoTao_TraLoiTracNghiem.CauHoiTracNghiemID
            inner join NS_DT_DotDaoTao_ThiTracNghiem on NS_DT_DotDaoTao_ThiTracNghiem.ThiTracNghiemID = NS_DT_DotDaoTao_TraLoiTracNghiem.ThiTracNghiemID
            WHERE (NS_DT_DotDaoTao_ThiTracNghiem.NhanVienID={0}) AND (NS_DT_DotDaoTao_ThiTracNghiem.NoiDungDaoTaoID={1}) AND (NS_DT_DotDaoTao_ThiTracNghiem.TrangThai>=0)", DatabaseCache.NhanVien.NhanVienID, this.NoiDungDaoTaoID);

            TongDiem = 0;
            DataTable dt = Database.ExecTable(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                ExamOnlineItemModel item = new ExamOnlineItemModel();
                item.DapAn = Database.ToInt(dr["DapAn"]);
                item.DapAnDung = Database.ToInt(dr["DapAnDung"]);
                item.ID = Database.ToInt(dr["TraLoiTracNghiemID"]);
                item.Type = Database.ToInt(dr["Type"]);
                item.Ten = dr["TenCauHoi"].ToString();
                item.QuestionIndex = Database.ToInt(dr["QuestionIndex"]);
                item.TotalCount = this.TotalAnswers;
                item.TotalAnswers = this.TotalAnswers;
                item.IsReadOnly = true;

                item.CauHoiList = new List<ObjectID>();
                item.CauHoiList.Add(new ObjectID(dr["DapAn1"].ToString(), 1));
                item.CauHoiList.Add(new ObjectID(dr["DapAn2"].ToString(), 2));

                if (dr["DapAn3"].ToString()!="")
                    item.CauHoiList.Add(new ObjectID(dr["DapAn3"].ToString(), 3));

                if (dr["DapAn4"].ToString() != "")
                    item.CauHoiList.Add(new ObjectID(dr["DapAn4"].ToString(), 4));

                AnswerList.Add(item);

                if (item.DapAn == item.DapAnDung)
                    TongDiem++;
            }
        }
    }

    public class ExamOnlineItemModel
    {
        public string Ten { get; set; }

        public string MoTa { get; set; }

        public int TotalCount { get; set; }

        public int QuestionIndex { get; set; }

        public int TotalAnswers { get; set; }

        public List<ObjectID> CauHoiList = new List<ObjectID>();

        public int Type { get; set; }

        public int ID { get; set; }

        public int DapAn { get; set; }

        public string DapAnStr { get; set; }

        public bool IsReadOnly { get; set; }

        public int DapAnDung { get; set; }
    }
}