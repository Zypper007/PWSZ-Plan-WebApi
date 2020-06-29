using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;

namespace PWSZ_Plan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> test()
        {
            _userRepository.Test();
            return Ok();
        }
        // POST api/Auth
        [HttpPost]
        public async Task<IActionResult> Auth()
        {
            string code;

            using(System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body, Encoding.UTF8))
            {
                code = await reader.ReadToEndAsync();
            }

            if (code == String.Empty)
                return Unauthorized();

            var user = await _userRepository.Auth(code);

            if (user == null)
                return Unauthorized();

            // Create token
            var tokenHendler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JsonWebToken").Value);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Permission.ToString()),
                        new Claim("Institute", (user.Institute != null)? user.Institute.ID.ToString() : "null")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHendler.CreateToken(tokenDescriptior);
            // End create token

            return Ok(new {
                user = _mapper.Map<UserSendDTO>(user),
                token = tokenHendler.WriteToken(token)
            });
        }
    }
}