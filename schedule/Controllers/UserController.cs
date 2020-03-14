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
    public class UserController : ControllerBase
    {

        private readonly IValidator<User> _userValidator;
        private readonly UserContext _user;
        private readonly AplicationSettings _appSettings;

        public UserController(IValidator<User> userValidator, IOptions<AplicationSettings> appSettings, UserContext user)
        {
            _userValidator = userValidator;
            _appSettings = appSettings.Value;
            _user = user;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = User.Claims.First(x => x.Type == "UserID").Value;

                var user = await _user.User
                    .Include(x => x.Relations)
                    .Select(x => new {
                        x.Id,
                        x.Name,
                        x.LastName,
                        x.Image,
                        x.Phone,
                        x.EMail,
                        DocumentType = x.DocumentType.GetDescription(),
                        x.DocumentNumber,
                        Relations = x.Relations.Select(u => new {
                            u.User2.Id,
                            u.User2.Name,
                            u.User2.LastName,
                            u.User2.Image,
                            u.User2.Phone,
                            u.User2.EMail,
                            DocumentType = u.User2.DocumentType.GetDescription(),
                            u.User2.DocumentNumber,
                            u.Relation,
                            RelationName = u.Relation.GetDescription(),
                        }).GroupBy(r => r.Relation)
                          .OrderBy(p => p.Key.ToString())
                          .ToDictionary(d => d.Key, d => d.ToList())
                    })
                    .FirstAsync(x => x.Id == int.Parse(userId));

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpGet("{search}")]
        [Authorize]
        public async Task<IActionResult> Get(string search)
        {            
            try
            {
                search = search.ToLower();
                var userId = User.Claims.First(x => x.Type == "UserID").Value;
                var slave = await _user.User.Include(x => x.Relations).FirstAsync(x => x.Id == int.Parse(userId));
                var relations = slave.Relations.Select(x => x.User2Id);

                var user = await _user.User
                    .Select(x => new
                    {
                        x.Id,
                        Name = x.Name.ToLower(),
                        LastName = x.LastName.ToLower(),
                        x.Image,
                        x.Phone,
                        x.EMail
                    })
                    .Where(x => x.Name.Contains(search) || x.LastName.Contains(search))
                    .Where(x => !relations.Contains(x.Id))
                    .Take(20)
                    
                    .ToListAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
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