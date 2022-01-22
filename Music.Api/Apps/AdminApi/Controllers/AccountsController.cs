using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Music.Api.Apps.AdminApi.DTOs.AccountDtos;
using Music.Core.Entities;
using Music.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Music.Api.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountsController(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username && x.IsAdmin);
            if (admin == null)
            {
                return NotFound();
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,admin.Id),
                new Claim(ClaimTypes.Name,admin.UserName),
                new Claim("FullName",admin.Fullname),
            };
            var adminRoles = _userManager.GetRolesAsync(admin).Result;
            var roleClaims = adminRoles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            string keyStr = _configuration.GetSection("JWT:secretKey").Value;
            SymmetricSecurityKey Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
            (
                claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.UtcNow.AddDays(3),
                    issuer: _configuration.GetSection("JWT:issuer").Value,
                    audience: _configuration.GetSection("JWT:audience").Value
                    );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new {token =tokenStr });
        }

        //public async Task<IActionResult> CreateRole()
        //{

        //    //var result1 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    //var result2 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    //var result3 = await _roleManager.CreateAsync(new IdentityRole("Member"));

        //    var admin = new AppUser { Fullname = "Super Admin", UserName = "SuperAdmin" };
        //    var result = await _userManager.CreateAsync(admin, "Admin123");
        //    var roleResult = await _userManager.AddToRoleAsync(admin, "SuperAdmin");


        //    return Ok(roleResult);
        //}
    }
}
