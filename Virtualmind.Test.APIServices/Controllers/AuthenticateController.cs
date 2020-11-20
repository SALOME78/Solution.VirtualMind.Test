using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Virtualmind.Test.APIServices.Common;
using Virtualmind.Test.APIServices.Models;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Model.ViewModels;

namespace Virtualmind.Test.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly Models.AppSettings _appSettings;
        private readonly IUsers _users;
        public AuthenticateController(IOptions<AppSettings> appSettings, IUsers users)
        {
            _users = users;
            _appSettings = appSettings.Value;
        }
        // POST: api/Authenticate
        [HttpPost]
        //[EnableCors("AllowOrigin")]
        public IActionResult Post([FromBody] LoginRequestViewModel value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginstatus = _users.AuthenticateUsers(value.UserName, EncryptionLibrary.EncryptText(value.Password));

                    if (loginstatus)
                    {
                        var userdetails = _users.GetUserDetailsbyCredentials(value.UserName);

                        if (userdetails != null)
                        {
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                        new Claim(ClaimTypes.Name, userdetails.IDUser.ToString())
                                }),
                                Expires = DateTime.UtcNow.AddDays(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            value.Token = tokenHandler.WriteToken(token);

                            // remove password before returning
                            value.IDUser = userdetails.IDUser;
                            value.Password = null;
                            value.Usertype = userdetails.IDRole;

                            return Ok(value);
                        }
                        else
                        {
                            value.IDUser = 0;
                            value.Password = null;
                            value.Usertype = 0;

                            return Ok(value);
                        }
                    }

                    value.IDUser = 0;
                    value.Password = null;
                    value.Usertype = 0;

                    return Ok(value);
                }

                value.IDUser = 0;
                value.Password = null;
                value.Usertype = 0;

                return Ok(value);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
