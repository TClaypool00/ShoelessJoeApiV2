using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App
{
    public class Utils
    {
        public static string ServerFolder { get; } = @"C:\xampp\htdocs\ShoelessJoe-pictures\Shoes\";

        public static string ServerUrl { get; } = "http://localhost/ShoelessJoe-pictures/Shoes/";

        public static string UniqueFileName(string fileName)
        {
            string withoutExt = Path.GetFileNameWithoutExtension(fileName);

            return withoutExt + Guid.NewGuid() + Path.GetExtension(fileName);
        }

        public async static Task<string> FileUpload(IFormFile file, string shoeFolder)
        {
            string uniqueFileName = UniqueFileName(file.FileName);

            if (!IsJpeg(file))
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                using Image image = Image.FromStream(memoryStream);
                image.Save(uniqueFileName, ImageFormat.Jpeg);
            }

            using var stream = new FileStream($@"{shoeFolder}\{uniqueFileName}", FileMode.Create);
            await file.CopyToAsync(stream);

            return uniqueFileName;
        }

        public static string FormatFileName(string fileName, int userId, int shoeId)
        {
            return $"{ServerUrl}user{userId}/shoe{shoeId}/{fileName}";
        }

        private static bool IsJpeg(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);

            if (ext.Contains("jpg") || ext.Contains("JPG") || ext.Contains("jpeg"))
            {
                return true;
            }

            return false;
        }
    }
}
