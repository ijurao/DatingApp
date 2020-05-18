using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Data;
using DatingApp.DTOS;
using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository,IConfiguration config)
        {
            _authRepository = authRepository;
            _configuration= config;
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

            var userToCreate = new UserApplication() { UserName = userDto.UserName.ToLower() };
            var registerdUser = await this._authRepository.RegisterUser(userToCreate, userDto.Password);
            return Ok(registerdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (UserForLogin userForLogin)
        {
            var user = await this._authRepository.Login(userForLogin.UserName.ToLower(), userForLogin.Password);
            if(user == null)
            {
                return Unauthorized();
            }
            else
            {
                var token = buildToken(user);
                return Ok(token);
            }
        }

        private string buildToken(UserApplication user)
        {
            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(ClaimTypes.Name,user.UserName),

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