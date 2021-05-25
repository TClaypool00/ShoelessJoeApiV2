using Microsoft.AspNetCore.Http;
using ShoelessJoeWebApi.App.ApiModels;
using System;
using System.IO;
using System.Reflection;
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
            string ext = Path.GetExtension(fileName);

            return withoutExt + Guid.NewGuid() + ext;
        }

        public async static Task<string> FileUpload(IFormFile file, string shoeFolder)
        {
            string uniqueFileName = UniqueFileName(file.FileName);
            using var stream = new FileStream($@"{shoeFolder}\{uniqueFileName}", FileMode.Create);
            await file.CopyToAsync(stream);

            return uniqueFileName;

        }
    }
}
