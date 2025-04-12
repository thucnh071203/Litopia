﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace PostService.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var settings = config.GetSection("Cloudinary");
            var account = new Account(
                settings["CloudName"],
                settings["ApiKey"],
                settings["ApiSecret"]);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                //Transformation = new Transformation().Crop("limit").Width(800).Height(800),
                Folder = "Litopia" // Tuỳ chọn
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}
