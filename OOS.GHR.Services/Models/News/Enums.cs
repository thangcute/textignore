using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.News
{
    /// <summary>
    /// Tương tác (Biểu tượng cảm xúc)
    /// </summary>
    public enum Reaction
    {
        None = 0, // Chưa tương tác
        Like = 1, // Thích
        Love = 2, // Yêu
        Care = 3, // Quan tâm
        Haha = 4, // Vui vẻ
        Wow = 5, // Bất ngờ
        Sad = 6, // Buồn
        Angry = 7 // Giận dữ
    }

    /// <summary>
    /// Phạm vi xuất bản
    /// </summary>
    public enum Scope
    {
        Public = 1, // Công khai
        Private = 2, // Riêng tư
        Priends = 3, // Bạn bè
    }
}
