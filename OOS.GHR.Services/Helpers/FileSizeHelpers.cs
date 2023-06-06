using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Helpers
{
    public static class FileSizeHelpers
    {
        public static string ResizeImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var image = Image.FromStream(ms);

                var ratioX = (double)150 / image.Width;
                var ratioY = (double)150 / image.Height;

                var ratio = Math.Min(ratioX, ratioY);

                var width = (int)(image.Width * ratio);
                var height = (int)(image.Height * ratio);

                var newImage = new Bitmap(width, height);

                Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);

                Bitmap bmp = new Bitmap(newImage);
                //bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

                ImageConverter converter = new ImageConverter();

                data = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                //return "data:image/*;base64," + Convert.ToBase64String(data);
                return Convert.ToBase64String(data);
            }
        }

    }
    public enum UnitsOfMeasurement
    {
        /// <summary>
        /// B.
        /// </summary>
        Byte = 1,
        /// <summary>
        /// KB.
        /// </summary>
        KiloByte = 1_024,
        /// <summary>
        /// MB.
        /// </summary>
        MegaByte = 1_048_576
    }
}
