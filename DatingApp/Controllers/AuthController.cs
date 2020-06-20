using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOS;
using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository,IConfiguration config, IMapper mapper)
        {
            _authRepository = authRepository;
            _configuration= config;
            _mapper= mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userDto)
        {

            //IS NOT NECESSRYYYYYY por que herada de apicontroller
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(new Error(HttpStatusCode.BadRequest.ToString(), "Los datos ingresados tienen un formato incorrecto"));

            //}
            if (await _authRepository.UserExists(userDto.UserName))
            {
                return BadRequest(new Error(HttpStatusCode.BadRequest.ToString(),"User " + userDto.UserName + "already exists"));
            }

            var userToCreate = _mapper.Map<UserApplication>(userDto);
            
            var registerdUser = await this._authRepository.RegisterUser(userToCreate, userDto.Password);
            UserPhoto photo = new UserPhoto
            {
                IsMain = true,
                UserApplicationId = registerdUser.Id,
                PublicId = "i0qwn3iomugvu4ps04p8",
                DateAdded = DateTime.Now,
                user = registerdUser,
                Url = "http://res.cloudinary.com/dapyqzhn4/image/upload/v1591522415/i0qwn3iomugvu4ps04p8.png"
            };

            registerdUser.Photos = new List<UserPhoto>();
            registerdUser.Photos.Add(photo);
            await  _authRepository.SaveAll();
            var userToReturn = _mapper.Map<UserDetailsDTO>(userToCreate);
            return CreatedAtRoute("GetUser", new { controller = "users", id = userToCreate.Id}, userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (UserForLogin userForLogin)
        {
           // throw new Exception("ssssss");
            var user = await this._authRepository.Login(userForLogin.UserName.ToLower(), userForLogin.Password);
            if(user == null)
            {
                return BadRequest(new Error(HttpStatusCode.Unauthorized.ToString(), "Credentials are not stored!"));
              
            }
            else
            {
                var token = buildToken(user);
                var json = JsonConvert.SerializeObject(token);
                return new OkObjectResult(json);
            }
        }

        private string buildToken(UserApplication user)
        {
            var mainPhoto = user.Photos.FirstOrDefault(p => p.IsMain);
           
            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(ClaimTypes.Name,user.UserName),
               new Claim("UrlMainPhoto",mainPhoto.Url)

           };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Appsetings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires =DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
             return tokenHandler.WriteToken(token);
        }
    }
}