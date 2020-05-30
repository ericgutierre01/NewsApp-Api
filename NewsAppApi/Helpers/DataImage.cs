using LazZiya.ImageResize;
using LazZiya.ImageResize.Watermark;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
namespace NewsAppApi.Helpers
{
    public class DataImage
    {
        private static readonly Regex DataUriPattern = new Regex(@"^data\:(?<type>(image|application)\/(png|tiff|jpg|jpeg|gif|pdf));base64,(?<data>[A-Z0-9\+\/\=]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        private DataImage(string mimeType, byte[] rawData)
        {
            MimeType = mimeType;
            RawData = rawData;
            Extesion = mimeType.Split("/")[1];
        }

        public string MimeType { get; }
        public byte[] RawData { get; }
        public string Extesion { get; set; }

        public Image Image => Image.FromStream(new MemoryStream(RawData));

        public static DataImage TryParse(string dataUri)
        {
            if (string.IsNullOrWhiteSpace(dataUri)) return null;

            Match match = DataUriPattern.Match(dataUri);
            if (!match.Success) return null;

            string mimeType = match.Groups["type"].Value;
            string base64Data = match.Groups["data"].Value;

            try
            {
                byte[] rawData = Convert.FromBase64String(base64Data);
                if (rawData.Length / 1024 > (1024 * 4)) throw new Exception("La imagen que intentas subir es muy grande.-");
                return rawData.Length == 0 ? null : new DataImage(mimeType, rawData);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static byte[] SignatureImage(byte[] byteImage, int width)
        {
            using (var ms = new MemoryStream(byteImage))
            {
                var image = Image.FromStream(ms);
                if (image.Width < width) width = image.Width;
                var newImg = ImageResize.ScaleByWidth(image, width);
                //add text watermark
                newImg.TextWatermark("RxFarmcias.com",
                        "#77ffffff", //text color hex8 value, 77 is opacity (00-FF)
                        "#22000000", //background color, 22 is for opacity (00-FF)
                        "Calibri",   //font family
                        34,          //font size
                        TargetSpot.BottomLeft, //target spot to place text watermark text
                        FontStyle.Italic,      //font style
                        10);                   //bg margin from border

                image.Dispose();
                return ImageToByteArray(newImg);
            }
        }

        public static byte[] ImageToByteArray(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
