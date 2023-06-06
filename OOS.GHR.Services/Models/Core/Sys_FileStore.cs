using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Core
{
    public class Sys_FileStore
    {
        /// <summary>
        /// Filename
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File content base64
        /// </summary>
        public string FileContent { get; set; }


        /// <summary>
        /// ObjectID trong hệ thống
        /// </summary>
        public long ObjectID { get; set; }


        /// <summary>
        /// Thuộc module nào ?
        /// </summary>
        public string Module { get; set; }


        /// <summary>
        /// Đường dẫn view file content
        /// </summary>
        public string FileURL { get; set; }


        /// <summary>
        /// FileStoreID trong hệ thống
        /// </summary>
        public string FileStoreID { get; set; }

        /// <summary>
        /// Convert từ String sang Byte
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytesContent()
        {
            if (!string.IsNullOrEmpty(FileContent))
            {
                byte[] byt = Convert.FromBase64String(FileContent);
                return byt;
            }
            return null;
        }
    }
}
