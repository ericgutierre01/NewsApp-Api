using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Helpers
{
    public class Utils
    {
        public static DateTime StringToDate(string strDate)
        {
            try
            {
                var res = DateTime.ParseExact(strDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return res;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static string GenerateGuid()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");

            return GuidString;
        }

        public static bool IsAllowedMimeType(string base64string)
        {
            if (string.IsNullOrWhiteSpace(base64string))
                throw new Exception("Imagen invalida!.");

            string data = base64string.Split(',')[1].Substring(0, 5);
            switch (data.ToUpper())
            {
                case "IVBOR":
                    //png
                    return true;
                case "/9J/4":
                    //jpg
                    return true;
                default:
                    return false;
            }
        }

        public static bool Base64FileValidate(string base64string)
        {
            if (base64string.Length <= 0)
                throw new Exception($"Se envió una imagen invalida!.");

            //Validar Imagen!.-
            var sizeFile = base64string.Length / 1024;
            if (sizeFile > (1024 * 4))
                throw new InvalidOperationException("Solo puedes cargar archivos de hasta 4 MB.");

            if (!Utils.IsAllowedMimeType(base64string))
                throw new Exception("Formato de imagen no permitido, solo se permiten imagenes.");

            return true;
        }
    }
}
