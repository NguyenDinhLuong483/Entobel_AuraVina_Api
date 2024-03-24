using AutoMapper;
using ENTOBEL_AURAVINA_API.Domains;
using ENTOBEL_AURAVINA_API.Domains.Models;
using ENTOBEL_AURAVINA_API.Domains.Services;
using ENTOBEL_AURAVINA_API.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ENTOBEL_AURAVINA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public ApplicationDbContext _context;
        private readonly AppSetting _appSetting;

        public UsersController(IUserService userService, ApplicationDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _userService = userService;
            _context = context;
            _appSetting = optionsMonitor.CurrentValue;
        }
        [HttpGet]
        [Route("GetUser")]
        public async Task<List<User>> GetAllUser()
        {
            return await _userService.GetAllUser();
        }
        [HttpPost]
        [Route("CreateUserAccount")]
        public async Task<IActionResult> CreateUserAccount([FromBody] AddUserViewModel userViewModel)
        {
            try
            {
                return Ok(await _userService.CreateUserAccount(userViewModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> Login([FromBody] Credential credential)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(p => p.UserName == credential.Username && p.Password == credential.Password);
                if (user is null)
                {
                    throw new ArgumentException("Username or password is invalid");
                }
                return Ok(new JwtToken { Success = true, Message = "SignIn success", Token = GenerateToken(user)});
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var tokenDecription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserName", user.UserName),
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha384Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDecription);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
