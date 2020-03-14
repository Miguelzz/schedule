using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using documents.Models;
using EnumDescription;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace documents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly UserContext _user;
        private readonly AplicationSettings _appSettings;

        public LoginController(IOptions<AplicationSettings> appSettings, UserContext user)
        {
            _appSettings = appSettings.Value;
            _user = user;
        }

        [HttpPost]
        public IActionResult Post(Login user)
        {
            var slave = _user.User.FirstOrDefault(x => x.EMail == user.EMail && x.Password == user.Password);

            if(slave != null)
            {
                var key = _appSettings.JWT_Sercret;
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim("UserID", slave.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescription);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            return BadRequest(new { message = "User or passwor is incorrect." });
        }

    }


    
}