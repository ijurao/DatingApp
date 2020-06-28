using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.ActionFilters;
using DatingApp.Data;
using DatingApp.DTOS;
using DatingApp.Helpers;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof (LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams parameters)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _repo.GetUser(currentUserId);
            parameters.UserApplicationId = currentUserId;
            // filters 

            if (String.IsNullOrEmpty(parameters.Gender))
            {
                parameters.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }
            var users = await this._repo.GetUsers(parameters);
            var usersToReturn = _mapper.Map<IEnumerable<UserListDTO>>(users);
            Response.AddPaginationInfo(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}",Name ="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await this._repo.GetUser(id);
            var userToReturn = _mapper.Map<UserDetailsDTO>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdateDTO)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await this._repo.GetUser(id);
            _mapper.Map(userForUpdateDTO, userFromRepo);
           
            // if all right
            if (await this._repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest(new Error(HttpStatusCode.BadRequest.ToString(), "Some problem updating user {UserForUpdateDTO.id}!"));

        }

        [HttpPost("{liker}/like/{likee}")]

        public async Task<IActionResult> Like(int liker, int likee)
        {
            if (liker == likee)
            {
                return BadRequest("You can't like ypur self");

            }
            if (liker != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (await this._repo.GetLike(liker,likee) != null)
            {
                return BadRequest("You already Like from this user " + likee);
            }

            var userToLike = await  this._repo.GetUser(likee);
            var userLiker = await this._repo.GetUser(liker);
            if (userToLike == null)
            {
                return NotFound("User {likee} couldn't be founded");
            }

            Like like = new Like
            {
                Likee = userToLike,
                Liker = userLiker,
                LikeeId = userToLike.Id,
                LikerId = userLiker.Id,
            };

            _repo.Add<Like>(like);
            if ( !await _repo.SaveAll())
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}