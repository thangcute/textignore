using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OOS.GHR.Services.Utilitys
{
    public class Commons
    {
        public static string getIpAddress()
        {
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }

        public static string Encrypt(string toEncrypt, string key)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt, string key)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string DecryptPassword(string pasword, string hash)
        {
            string output = string.Empty;

            byte[] byte_salt = Encoding.UTF8.GetBytes(hash);
            byte[] byte_password = Encoding.UTF8.GetBytes(pasword);

            byte[] byte_concat = new byte[byte_salt.Length + byte_password.Length];

            System.Buffer.BlockCopy(byte_salt, 0, byte_concat, 0, byte_salt.Length);
            System.Buffer.BlockCopy(byte_password, 0, byte_concat, byte_salt.Length, byte_password.Length);

            MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();
            output = BitConverter.ToString(hashMd5.ComputeHash(byte_concat));

            return output;
        }

        public static string GetHash(int length)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static long GetTransactionId()
        {
            long transactionId = 0;
            try
            {
                Random rand = new System.Random();
                byte[] buf = new byte[8];
                rand.NextBytes(buf);
                long longRand = BitConverter.ToInt64(buf, 0);
                transactionId = (Math.Abs(longRand % (Int64.MaxValue - 1)) + 1);
            }
            catch (Exception ex)
            {

            }
            return transactionId;
        }

        public static string GetGuid(int length = 0)
        {
            string guid = Guid.NewGuid().ToString("N").ToUpper();
            if (length == 0)
                return guid;
            else
                return guid.Substring(0, length);
        }

        public static string GetCode(string prefix = "", int userId = 0)
        {
            //MM/dd/yyyy HH:mm:ss
            int length = 5;
            string output = "";
            if (string.IsNullOrEmpty(prefix))
                output = string.Format("{0}-{1}", userId.ToString().PadLeft(length, '0'), DateTime.Now.ToString("yyMMddHHmmss"));
            else
                output = string.Format("{0}-{1}-{2}", prefix, userId.ToString().PadLeft(length, '0'), DateTime.Now.ToString("yyMMddHHmmss"));
            return output;
        }

        public static string GetCode(string input = "", int length = 5, string prefix = "")
        {
            string output = "";
            if (string.IsNullOrEmpty(prefix))
                output = input.ToString().PadLeft(length, '0');
            else
                output = prefix + "-" + input.ToString().PadLeft(length, '0');
            return output;
        }

        public static string SaveFile(HttpPostedFileBase _file, string _mapPath, string prefix = "")
        {
            string filePath = "";
            if (_file != null & _file.ContentLength > 0)
            {
                string _pathDirectory = System.Web.HttpContext.Current.Server.MapPath("~/" + _mapPath);
                if (!Directory.Exists(_pathDirectory))
                    Directory.CreateDirectory(_pathDirectory);
                DirectoryInfo dinfo = new DirectoryInfo(_pathDirectory);
                DirectorySecurity dSecurity = dinfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dinfo.SetAccessControl(dSecurity);
                //
                string _extension = Path.GetExtension(_file.FileName);
                string _fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), Commons.GetGuid().ToLower(), _extension);
                if (!string.IsNullOrEmpty(prefix))
                    _fileName = string.Format("{0}-{1}", prefix, _fileName);
                string _pathFile = Path.Combine(_pathDirectory, _fileName);

                //if (System.IO.File.Exists(_pathFile))
                //    System.IO.File.Delete(_pathFile);

                _file.SaveAs(_pathFile);

                filePath = string.Format("/{0}/{1}", _mapPath, _fileName);
            }
            return filePath;
        }

        public static List<string> SaveFileList(List<HttpPostedFileBase> _file, string _mapPath, string prefix = "")
        {
            List<string> filePath = new List<string>();
            if (_file.Any())
            {
                string _pathDirectory = System.Web.HttpContext.Current.Server.MapPath("~/" + _mapPath);
                if (!Directory.Exists(_pathDirectory))
                    Directory.CreateDirectory(_pathDirectory);
                DirectoryInfo dinfo = new DirectoryInfo(_pathDirectory);
                DirectorySecurity dSecurity = dinfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dinfo.SetAccessControl(dSecurity);
                //
                foreach(var _item in _file)
                {
                    if(_item != null && _item.ContentLength > 0)
                    {
                        try
                        {
                            string _extension = Path.GetExtension(_item.FileName);
                            string _fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), Commons.GetGuid().ToLower(), _extension);
                            if (!string.IsNullOrEmpty(prefix))
                                _fileName = string.Format("{0}-{1}", prefix, _fileName);
                            string _pathFile = Path.Combine(_pathDirectory, _fileName);
                            _item.SaveAs(_pathFile);
                            string _filePath = string.Format("/{0}/{1}", _mapPath, _fileName);
                            filePath.Add(_filePath);
                        }catch(Exception ex)
                        {

                        }
                    }
                }
            }
            return filePath;
        }

        public static bool DeleteFile(string _mapPath, string _fileName)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(_fileName))
            {
                string _pathDirectory = System.Web.HttpContext.Current.Server.MapPath("~/" + _mapPath);
                if (!Directory.Exists(_pathDirectory))
                    Directory.CreateDirectory(_pathDirectory);
                DirectoryInfo dinfo = new DirectoryInfo(_pathDirectory);
                DirectorySecurity dSecurity = dinfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dinfo.SetAccessControl(dSecurity);
                //
                string _pathFile = Path.Combine(_pathDirectory, _fileName);

                if (System.IO.File.Exists(_pathFile))
                    System.IO.File.Delete(_pathFile);
                rs = true;
            }
            return rs;
        }

        public static byte[] AvatarToBinary(object Avatar, char c)
        {
            if (Avatar == null || Avatar == DBNull.Value)
            {
                return new byte[] { };
            }
            var data = (byte[])Avatar;
            return data;
        }

        /**
         * Extract the MIME type from a base64 string
         * @param encoded Base64 string
         * @return MIME type string
         */
        public static string Base64Extension(string Base64String)
        {
            string Extension = string.Empty;
            if (!string.IsNullOrEmpty(Base64String))
            {
                var ExtensionType = Base64String.Substring(0, 5);
                switch (ExtensionType.ToUpper())
                {
                    case "IVBOR":
                        Extension = "png";
                        break;
                    case "/9J/4":
                        Extension = "jpg";
                        break;
                    case "AAAAF":
                        Extension = "mp4";
                        break;
                    case "JVBER":
                        Extension = "pdf";
                        break;
                    case "AAABA":
                        Extension = "ico";
                        break;
                    case "UMFYI":
                        Extension = "rar";
                        break;
                    case "E1XYD":
                        Extension = "rtf";
                        break;
                    case "U1PKC":
                        Extension = "txt";
                        break;
                    case "MQOWM":
                    case "77U/M":
                        Extension = "srt";
                        break;
                    default:
                        Extension = string.Empty;
                        break;
                }
            }
            return Extension;
            //string rs = "";
            //Match match = Regex.Match(encoded, @"^data:([a-zA-Z0-9]+/[a-zA-Z0-9]+).*,.*$", RegexOptions.IgnoreCase);
            //if (match.Success)
            //    rs = match.Groups[1].Value;
            //return rs;
        }

        /// <summary>
        /// To demonstrate extraction of file extension from base64 string.
        /// </summary>
        /// <param name="base64String">base64 string.</param>
        /// <returns>Henceforth file extension from string.</returns>

        public static bool SaveBase64(string Base64String, string filePath = "")
        {
            bool rs = false;
            try
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(Base64String));
                return true;
            }
            catch
            {

            }
            return rs;
        }

        public static void WLog(string logMsg = "", string fileName = "log.txt")
        {
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Logs/" + DateTime.Now.ToString("yyyy-MM-dd") + "/");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string filePath = root + fileName;
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, string.Format("Start - {0} - \n\r", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            try
            {
                using (StreamWriter w = System.IO.File.AppendText(filePath))
                {
                    w.WriteLine("{0} - {1}", DateTime.Now.ToString("yyyyMMddHHmmss"), logMsg);
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                //ex.Message 
            }
        }
    }
}
