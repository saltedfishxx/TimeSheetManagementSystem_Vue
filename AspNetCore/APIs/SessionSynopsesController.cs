using AspNetCore.Models;
using AspNetCore.Data;
using AspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using AspNetCore.Helpers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    public class SessionSynopsesController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private ApplicationDbContext Database;

        public SessionSynopsesController(
                IUserService userService,
                IMapper mapper,
                IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            Database = context;
        }

        //GET 
        [HttpGet]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            int userId = 0;
            if (identity != null)
			{
                
                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var user = _userService.GetById(userId);
                //get user role 
                string roles = user.Roles;

                List<object> sessionList = new List<object>();
   

                var sessions = Database.SessionSynopses
                    .Include(input => input.CreatedBy)
                    .Include(input => input.UpdatedBy);

                foreach (var oneSession in sessions)
                {
                    if (roles.Equals("Admin"))
                    {
                        sessionList.Add(new
                        {
                            sessionId = oneSession.SessionSynopsisId,
                            sessionName = oneSession.SessionSynopsisName,
                            visibility = oneSession.IsVisible,
                            createdBy = oneSession.CreatedBy.UserName,
                            updatedBy = oneSession.UpdatedBy.UserName
                        });
                    }
                    else
                    {
                        if (oneSession.IsVisible == true)
                        {
                            sessionList.Add(new
                            {
                                sessionId = oneSession.SessionSynopsisId,
                                sessionName = oneSession.SessionSynopsisName,
                                visibility = oneSession.IsVisible,
                                createdBy = oneSession.CreatedBy.UserName,
                                updatedBy = oneSession.UpdatedBy.UserName
                            });
                        }
                    }
                }//foreach
                return new JsonResult(sessionList);

            }
            return BadRequest(new { message = "Unable to retrieve records" });
        }

        //GET api/SessionSynopses/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
			if (identity != null)
			{
                List<object> sessionList = new List<object>();
                var selectedSession = Database.SessionSynopses
                    .Where(eachSession => eachSession.SessionSynopsisId == id)
                    .Include(user => user.CreatedBy)
                    .Include(user => user.UpdatedBy).Single();
                var response = new
                {
                    sessionId = selectedSession.SessionSynopsisId,
                    sessionName = selectedSession.SessionSynopsisName,
                    visibility = selectedSession.IsVisible,
                    createdBy = selectedSession.CreatedBy.UserName,
                    updatedBy = selectedSession.UpdatedBy.UserName
                };

                return new JsonResult(response);
            }
            return BadRequest(new { message = "Unable to retrieve the record" });
        }

        //PUT api/SessionSynopses/5


        //POST api/SessionSynopses
        [EnableCors("VueCorsPolicy")]
        [HttpPost]
        public IActionResult Post([FromForm] IFormCollection webFormData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
			int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                SessionSynopsis newSession = new SessionSynopsis();

                if(webFormData["sessionName"].ToString() == null || Convert.ToBoolean(webFormData["visibility"]) == null)
                {
                    string customMessage = "Unable to save record. Please try again";

                    object httpFailRequestResultMessage = new { message = customMessage };
                    return BadRequest(httpFailRequestResultMessage);
                }

                newSession.SessionSynopsisName = webFormData["sessionName"];
                newSession.IsVisible = Convert.ToBoolean(webFormData["visibility"]);
                newSession.CreatedById = userId;
                newSession.UpdatedById = userId;

                try
                {
                    Console.Write(newSession);
                    Database.SessionSynopses.Add(newSession);

                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("SessionSynopses_SessionSynopsisName_UniqueConstraint") == true)
                        {
                            string customMessage = "Unable to save record due to another session having the same name: " + webFormData["sessionName"];

                            object httpFailRequestResultMessage = new { message = customMessage };
                            return BadRequest(httpFailRequestResultMessage);
                        }
                    }
                    return BadRequest(ex);
                }//end try catch
                return Ok(new
                {
                    message = "Create Web API is called. The extracted Id is " + userId.ToString() +
                             ". Created a record " + newSession.SessionSynopsisName
                });
            }
            else
            {
                 return BadRequest(new { message = "Unable to create record" });
            }
        }

        //PUT api/SessionSynopses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] IFormCollection webFormData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
			int userId = 0;
			if (identity != null)
			{
					IEnumerable<Claim> claims = identity.Claims;

					userId = Int32.Parse(identity.FindFirst("userid").Value);
                    
                var oneSession = Database.SessionSynopses
                    .Where(item => item.SessionSynopsisId == id).Single();

                if (webFormData["sessionName"].ToString() == null || Convert.ToBoolean(webFormData["visibility"]) == null)
                {
                    string customMessage = "Unable to update record. Please try again";

                    object httpFailRequestResultMessage = new { message = customMessage };
                    return BadRequest(httpFailRequestResultMessage);

                }
                oneSession.SessionSynopsisName = webFormData["sessionName"];
                oneSession.IsVisible = Convert.ToBoolean(webFormData["visibility"]);
                oneSession.UpdatedById = userId;

                try
                {
                    Database.Update(oneSession);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("SessionSynopses_SessionSynopsisName_UniqueConstraint") == true)
                        {
                            string customMessage = "Unable to save record due to another session having the same name: " + webFormData["sessionName"];

                            object httpFailRequestResultMessage = new { message = customMessage };
                            return BadRequest(httpFailRequestResultMessage);
                        }
                    }
                }//end try catch
                return Ok(new
                {
                    message = "Create Web API is called. The extracted Id is " + userId.ToString() +
                            ". Updated a record " + oneSession.SessionSynopsisName
                });
            }else
            return BadRequest(new { message = "Unable to create record" });



        }

        // DELETE api/SessionSynopses/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

                       var identity = HttpContext.User.Identity as ClaimsIdentity;
			int userId = 0;
			if (identity != null)
			{
				IEnumerable<Claim> claims = identity.Claims;

				userId = Int32.Parse(identity.FindFirst("userid").Value);
               
                 var delSession = Database.SessionSynopses
                .Single(eachSession => eachSession.SessionSynopsisId == id);

                try
                {

                    Database.SessionSynopses.Remove(delSession);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                   
                            return BadRequest("Unable to delete record");
                        
                    
                }//end try catch

                return Ok(new
                {
                    message = "Delete Web API is called. The extracted Id is " + userId.ToString() +
                                            ". Deleted a record " + delSession.SessionSynopsisName
                });
            }else
            return BadRequest(new { message = "Unable to retrieve the record" });
        }//end of Delete() Web API method


    }
}
