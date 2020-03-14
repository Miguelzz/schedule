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
    public class RegisterController : ControllerBase
    {

        private readonly IValidator<User> _userValidator;
        private readonly UserContext _user;
        private readonly AplicationSettings _appSettings;

        public RegisterController(IValidator<User> userValidator, IOptions<AplicationSettings> appSettings, UserContext user)
        {
            _userValidator = userValidator;
            _appSettings = appSettings.Value;
            _user = user;
        }


        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            var validate = _userValidator.Validate(user);
            if (validate.IsValid)
            {
                var slave = await _user.User.FirstOrDefaultAsync(x => x.EMail == user.EMail || x.DocumentNumber == user.DocumentNumber);
                if (slave != null)
                    return BadRequest(new { message = "Usuario ya existe." });
                _user.Add(user);
                _user.SaveChanges();
                return StatusCode(200);
            }
            return BadRequest(new { message = validate.Errors });
        }

    }


    
}