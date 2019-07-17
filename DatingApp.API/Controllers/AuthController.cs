
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Model;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
            public AuthController(IAuthRepository Repo,IConfiguration config )
            {
            _repo = Repo;
            _config=config;
            }
           
        
            [HttpPost("register")]
            public async Task<IActionResult> Register(UserForRegisterDtos user){
                user.username = user.username.ToLower();
                if( await _repo.UserExists(user.username)){

                   BadRequest("Username already exist");
                }
               var userCreate = new Users(){
                   Username = user.username
                   
               };
               var createdUser = await _repo.RegisterAsync(userCreate,user.password);
            
                return StatusCode(201);

                   
            }
            [HttpPost("login")]
            public async Task<IActionResult> Login(UserForLoginDtos userForLoginDtos){

            
        //    throw new Exception("computer says no !");
            var userFromRepo = await _repo.Login(userForLoginDtos.Username.ToLower(),userForLoginDtos.Password);
            if(userFromRepo== null)
            return Unauthorized();
          var clims = new[]
           {
             new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
             new Claim(ClaimTypes.Name,userFromRepo.Username)
           };
           var key =
           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
           var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha512);
           var tokenDiscriptor = new SecurityTokenDescriptor{
           Subject = new ClaimsIdentity(clims),
           Expires=DateTime.Now.AddDays(1),
           SigningCredentials = creds
           } ;
           var tokenHandler = new JwtSecurityTokenHandler();
           var token =  tokenHandler.CreateToken(tokenDiscriptor);

           return Ok(new{token=tokenHandler.WriteToken(token)});
            }
            
        
    }
}