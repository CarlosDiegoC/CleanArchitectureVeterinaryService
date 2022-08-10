using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetDream.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using PetDream.Domain.Account;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using PetDream.Application.Interfaces;

namespace PetDream.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;
        private readonly IPetOwnerService _petOwnerService;
        private readonly IVeterinarianService _veterinarianService;

        public TokenController(IAuthenticate authentication, IConfiguration configuration, IPetOwnerService perOwnerService, IVeterinarianService veterinarianService)
        {
            _authentication = authentication ?? throw new ArgumentException(nameof(authentication));
            _configuration = configuration;
            _petOwnerService = perOwnerService;
            _veterinarianService = veterinarianService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);

            if (result)
            {                
                return Ok($"User {userInfo.Email} was created successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// You must login here.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login ([FromBody] LoginModel userInfo)
        {
            var petOwner = await _petOwnerService.GetByEmail(userInfo.Email);            
            var veterinary = await _veterinarianService.GetByEmailAsync(userInfo.Email);
            if(petOwner != null) userInfo.IsPetOwner = true;           
            if(veterinary != null) userInfo.IsPetOwner = false;

            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);
            if(result)
            {
                return GenerateToken(userInfo);
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new List<Claim>();
            if(userInfo.IsPetOwner == true)
            {
                claims.Add(new Claim("email", userInfo.Email));
                claims.Add(new Claim(ClaimTypes.Role, "PetOwner"));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            }
            else if (userInfo.IsPetOwner == false)
            {
                claims.Add(new Claim("email", userInfo.Email));
                claims.Add(new Claim(ClaimTypes.Role, "Veterinary"));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            }
            
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));


            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);


            var expiration = DateTime.UtcNow.AddMinutes(10);


            JwtSecurityToken token = new JwtSecurityToken(

                issuer: _configuration["Jwt:Issuer"],

                audience : _configuration["Jwt:Audience"],

                claims: claims,

                expires : expiration,

                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }      
    }
}
