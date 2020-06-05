using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.DTOS;
using DatingApp.Helpers;
using DatingApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class CloudinaryPhotoRepository : IPhotoRepository
    {
        private readonly IOptions<CloudinarySettings> _options;
        private readonly Cloudinary cloudinary;


        public CloudinaryPhotoRepository(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _options = cloudinaryConfig;
            Account account = new Account(
                _options.Value.CloudNanme,
                _options.Value.Key,
                _options.Value.ApiSecret
                );
            cloudinary = new Cloudinary(account);
        }
        public PhotoForCreationDTO AddPhoto(PhotoForCreationDTO photo)
        {
            var file = photo.File;

            var result = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    result = cloudinary.Upload(uploadParams);
                }

                photo.Url = result.Url.ToString();
                photo.PublicId = result.PublicId;


            }

            return photo;
        }

        public string  DeletePhoto(UserPhoto photo)
        {

            var deletgeParams = new DeletionParams(photo.PublicId);
            if(photo.PublicId != null)
            { 
                var resul =  this.cloudinary.Destroy(deletgeParams);
                if(resul.Result == "ok")
                {
                    return "ok";
                }
                return "";
            }
            else
            {
                return "1";
            }
        }
    }
}
