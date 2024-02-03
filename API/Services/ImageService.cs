using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;
  
   public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinarySettings> configuration)
        {
           var  account =
                new Account(
                    configuration.Value.CloudName,
                    configuration.Value.ApiKey,
                    configuration.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            ImageUploadResult uploadResualt = new();
            if (file.Length > 0)
            {
                using Stream stream = file.OpenReadStream();
              var     uploadParams =
                    new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation()
                            .Height(500)
                            .Width(500)
                            .Crop("fill")
                            .Gravity("face"),
                        Folder = "internetprogramming-tinner-example"
                    };
                uploadResualt = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResualt;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            DeletionParams deleteParams = new(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
