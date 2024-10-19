using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IFileService
    {
        Task<string> SaveKoiAvatar(IFormFile file);
        Task<byte[]> GetKoiAvatar(string fileName);
        Task DeleteImage(string filePath);
    }
}
