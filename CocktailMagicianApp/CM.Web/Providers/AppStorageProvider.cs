using CM.Web.Providers.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers
{
    public class AppStorageProvider : IStorageProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AppStorageProvider(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        public string GenerateRelativePath(string path, string fileName, string newName)
        {
            //if (string.IsNullOrEmpty(fileName))
            //    throw new ArgumentNullException("No file provided.");

            var fileExtension = Path.GetExtension(fileName);
            var newFileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")
                                 + "_"
                                 + newName
                                 + fileExtension;

            return Path.Combine(path, newFileName);
        }

        public async Task StoreImageAsync(string filePath, IFormFile file)
        {
            filePath = "wwwroot" + filePath;

            var absolutePath = Path.Combine(_webHostEnvironment.ContentRootPath, filePath);

            await file?.CopyToAsync(new FileStream(absolutePath, FileMode.Create));
        }

        public void DeleteImage(string relativePath)
        {
            if (relativePath == null ||
                relativePath.EndsWith("DefaultCocktail.png") ||
                relativePath.EndsWith("DefaultIngredients.jpg")||
                relativePath.EndsWith("DefaultBar.jpg"))
                return;

            relativePath = "wwwroot" + relativePath;

            var absolutePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);

            if (File.Exists(absolutePath))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(absolutePath);
            }
        }
    }
}
