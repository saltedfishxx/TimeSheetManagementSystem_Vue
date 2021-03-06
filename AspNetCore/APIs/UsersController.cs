﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using AspNetCore.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AspNetCore.Services;
using AspNetCore.Models;
using AspNetCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public IActionResult SignIn([FromForm]IFormCollection inFormData)
        {
            //to get data from IForm --> inFormData["dataname"]
            var user = _userService.Authenticate(inFormData["username"], inFormData["password"]);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                                new Claim(ClaimTypes.Name, user.Id.ToString() ),
                                new Claim("username", user.UserName.ToString()),
                                new Claim("userid", user.Id.ToString()),
                                new Claim("role", user.Roles.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                user = new
                {
                    userId = user.Id,
                    userName = user.UserName,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    roles = user.Roles
                },
                token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromForm]IFormCollection inFormData)
        {
            UserInfo newUser = new UserInfo()
            {
                FirstName = inFormData["firstname"],
                LastName = inFormData["lastname"],
                UserName = inFormData["username"],
                Roles = inFormData["roles"]
            };
            string password = inFormData["password"];
            try
            {
                // save 
                _userService.Create(newUser, password);
                return Ok(new
                {
                    signUpStatus = true,
                    message = "Completed user registration"
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            List<object> userList = new List<object>();
            foreach (UserInfo user in users)
            {
                userList.Add(new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    username = user.UserName,
                    roles = user.Roles,
                    userId = user.Id
                });
            }
            return Ok(userList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            return Ok(new
            {
                firstname = user.FirstName,
                lastname = user.LastName,
                username = user.UserName,
                roles = user.Roles,
                userId = user.Id
            });
        }

        [HttpPut("{id}")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult Update(int id, IFormCollection inFormData)
        {
            //Developer note:
            //In most user update use case studies, changes on user name
            //is not encouraged. It is very rare for us to see a 
            //system (e.g. Facebook, Twitter etc) that lets you change the
            //user name. This is just a project example which focuses on JWT
            //token security concept.
            UserInfo user = new UserInfo()
            {
                Id = id,
                FirstName = inFormData["firstname"],
                LastName = inFormData["lastname"],
                Roles = inFormData["roles"]

            };

            try
            {
                // save 
                _userService.Update(user);
                return Ok(new { message = "Completed user profile update." });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "Completed user deletion" });
        }
    }
}
