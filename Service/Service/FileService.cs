using Microsoft.AspNetCore.Http;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class FileService : IFileService
    {
        public string folderSaveImage = "wwwroot/images/koi";
        public string urlImage = "/images/koi/";

        public async Task<byte[]> GetKoiAvatar(string fileName)
        {
            // Đường dẫn đầy đủ tới ảnh
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderSaveImage, fileName);

            // Kiểm tra nếu file không tồn tại
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            // Đọc file và trả về dưới dạng byte[]
            return await System.IO.File.ReadAllBytesAsync(filePath);
        }

        public async Task<string> SaveKoiAvatar(IFormFile file)
        {
            // Đường dẫn thư mục lưu ảnh
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), folderSaveImage);

            // Đảm bảo thư mục tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file duy nhất dựa trên thời gian và tên gốc của file
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn URL để lưu vào cơ sở dữ liệu
            return urlImage + uniqueFileName; // Đường dẫn tương đối
        }

        public Task DeleteImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return Task.CompletedTask;

            // Chuyển đường dẫn URL thành đường dẫn file trong hệ thống
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }
    }
}
