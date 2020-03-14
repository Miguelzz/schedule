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
    public class RelationController : ControllerBase
    {

        private readonly UserContext _user;

        public RelationController( UserContext user)
        {
            _user = user;
        }




        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Relations user)
        {            
            try
            {
                var userId = User.Claims.First(x => x.Type == "UserID").Value;
                await _user.Relations.AddAsync(new Relations {
                    UserId = int.Parse(userId),
                    Relation = user.Relation,
                    User2Id = user.User2Id
                });
                _user.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(Relations user)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(x => x.Type == "UserID").Value);

                var slave = await _user.Relations.FirstAsync(x => x.UserId == userId && x.User2Id == user.User2Id);
                _user.Relations.Remove(slave);
                
                await _user.Relations.AddAsync(new Relations
                {
                    UserId = userId,
                    Relation = user.Relation,
                    User2Id = user.User2Id
                });
                _user.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


    }


    
}