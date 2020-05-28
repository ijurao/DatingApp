using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOS;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await this._repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserListDTO>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
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

    }
}