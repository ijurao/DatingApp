using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Data;
using DatingApp.DTOS;
using DatingApp.Helpers;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _pr;


        public PhotosController(IDatingRepository repository, IMapper mapper, IPhotoRepository pr)
        {
            _repo = repository;
            _mapper = mapper;
            _pr = pr;

          


        }
        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);
            var photoToReturn = _mapper.Map<PhotoForReturnDTO>(photoFromRepo);
            return Ok(photoToReturn);
        }


        [HttpPost("{id}")]

        public async Task<IActionResult> AddPhotoForUser(int id, [FromForm] PhotoForCreationDTO photoForCreationDTO)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var userFromRepo = await this._repo.GetUser(id);


            var savedPhoto = this._pr.AddPhoto(photoForCreationDTO);

                var photo = _mapper.Map<UserPhoto>(savedPhoto);
                if (!userFromRepo.Photos.Any(u => u.IsMain))
                {
                    photo.IsMain = true;
                }
                userFromRepo.Photos.Add(photo);
                if (await this._repo.SaveAll())
                {
                    var photoToReturn = _mapper.Map<PhotoForReturnDTO>(photo);

                    return CreatedAtRoute("GetPhoto", new { userId = userFromRepo.Id, Id = photo.Id }, photoToReturn);
                }


            
            return BadRequest();
        }

        [HttpPost("{userId}/{id}/setMain")]

        public async Task<IActionResult> setMainPhoto (int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await _repo.GetUser(userId);
            if(! user.Photos.Any(x=> x.Id == id))
            {
                return Unauthorized();

            }

            var photoFromRepo = await _repo.GetPhoto(id);
            if (photoFromRepo.IsMain)
            {
                return BadRequest("This is main photo");
            }

            var currenPhotoAsMain = await _repo.GetMainPhotoForUser(userId);
            currenPhotoAsMain.IsMain = false;
            photoFromRepo.IsMain = true;
            if(await _repo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Problem ");

            


        }
        [HttpDelete("{userId}/{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var user = await _repo.GetUser(userId);
            if (!user.Photos.Any(x => x.Id == id))
            {
                return Unauthorized();

            }
            var photoFromRepo = await _repo.GetPhoto(id);
            if (photoFromRepo.IsMain)
            {
                return BadRequest("You can not delete main photo");
            }

            

            string resDelete = this._pr.DeletePhoto(photoFromRepo);
            if(resDelete == "ok" || resDelete == "1" || resDelete == "")
            {
                _repo.Delete(photoFromRepo);
            }


            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Fail to delete photo");

        }
    }
}

