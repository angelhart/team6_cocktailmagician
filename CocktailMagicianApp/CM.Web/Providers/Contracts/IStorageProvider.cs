using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers.Contracts
{
    public interface IStorageProvider
    {
        void DeleteImage(string path);
        string GenerateRelativePath(string path, string fileName, string newName);
        Task StoreImageAsync(string path, IFormFile file);
    }
}
