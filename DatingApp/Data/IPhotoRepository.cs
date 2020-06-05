using DatingApp.DTOS;
using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public interface IPhotoRepository
    {
        PhotoForCreationDTO AddPhoto(PhotoForCreationDTO photo);

        string DeletePhoto(UserPhoto photo);
    }
}
