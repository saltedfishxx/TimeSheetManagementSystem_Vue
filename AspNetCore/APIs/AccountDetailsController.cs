using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AspNetCore.Data;
using AspNetCore.Models;
using AspNetCore.Services;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace TimeSheetManagementSystem.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    public class AccountDetailsController : Controller
    {


        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }
        private IUserService _userService;


        public AccountDetailsController(ApplicationDbContext database, IUserService userService)
        {
            Database = database; //Initialize the Database property
            _userService = userService;
        }

        //GET AccountDetails/GetFiltered/accname
        [HttpGet("GetFiltered/{custID}")]
        public IActionResult GetFilteredDetails(int custID)
        {
            //TODO: Fix code here
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var user = _userService.GetById(userId);
                string roles = user.Roles;

                List<object> accountDetailList = new List<object>();
                var accountDetails = Database.AccountDetails
                    .Include(input => input.CustomerAccount)
                    .Where(input => input.CustomerAccountId == custID);

                if (accountDetails != null)
                {

                    foreach (var oneDetail in accountDetails)
                    {
                        if (roles.Equals("Admin"))
                        {
                            accountDetailList.Add(new
                            {
                                accountDetailId = oneDetail.AccountDetailId,
                                dayOfWeek = oneDetail.DayOfWeekNumber,
                                startTimeMin = oneDetail.StartTimeInMinutes,
                                visibility = oneDetail.IsVisible,
                                endTimeMin = oneDetail.EndTimeInMinutes,
                                startDate = oneDetail.EffectiveStartDate,
                                endDate = oneDetail.EffectiveEndDate,
                                updatedAt = oneDetail.CustomerAccount.UpdatedAt,
                                updatedBy = oneDetail.CustomerAccount.UpdatedBy.UserName,
                                accountName = oneDetail.CustomerAccount.AccountName
                            });
                        }
                        else if (roles.Equals("Instructor"))
                        {
                            if (oneDetail.IsVisible == true)
                            {
                                accountDetailList.Add(new
                                {
                                    accountDetailId = oneDetail.AccountDetailId,
                                    dayOfWeek = oneDetail.DayOfWeekNumber,
                                    startTimeMin = oneDetail.StartTimeInMinutes,
                                    visibility = oneDetail.IsVisible,
                                    endTimeMin = oneDetail.EndTimeInMinutes,
                                    startDate = oneDetail.EffectiveStartDate,
                                    endDate = oneDetail.EffectiveEndDate,
                                    updatedAt = oneDetail.CustomerAccount.UpdatedAt,
                                    accountName = oneDetail.CustomerAccount.AccountName
                                    //updatedBy = oneDetail.CustomerAccount.UpdatedBy.UserName
                                });
                            }
                        }
                    }//foreach
                }
                return new JsonResult(accountDetailList);
            }
            else
            {
                return BadRequest("User not authorized");
            }
        }

        //GET AccountDetails/GetAllDetails
        [HttpGet("GetAllDetails")]
        public IActionResult GetAllDetails()
        {
            //TODO: Fix code here
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var user = _userService.GetById(userId);
                string roles = user.Roles;

                List<object> accountDetailList = new List<object>();
                var accountDetails = Database.AccountDetails
                    .Include(input => input.CustomerAccount);

                if (accountDetails != null)
                {

                    foreach (var oneDetail in accountDetails)
                    {
                        if (roles.Equals("Admin"))
                        {
                            accountDetailList.Add(new
                            {
                                accountDetailId = oneDetail.AccountDetailId,
                                dayOfWeek = oneDetail.DayOfWeekNumber,
                                startTimeMin = oneDetail.StartTimeInMinutes,
                                visibility = oneDetail.IsVisible,
                                endTimeMin = oneDetail.EndTimeInMinutes,
                                startDate = oneDetail.EffectiveStartDate,
                                endDate = oneDetail.EffectiveEndDate,
                                updatedAt = oneDetail.CustomerAccount.UpdatedAt,
                                custID = oneDetail.CustomerAccountId,
                                updatedBy = oneDetail.CustomerAccount.UpdatedBy.UserName,
                                accountName = oneDetail.CustomerAccount.AccountName
                            });
                        }
                        else if (roles.Equals("Instructor"))
                        {
                            if (oneDetail.IsVisible == true)
                            {
                                accountDetailList.Add(new
                                {
                                    accountDetailId = oneDetail.AccountDetailId,
                                    dayOfWeek = oneDetail.DayOfWeekNumber,
                                    startTimeMin = oneDetail.StartTimeInMinutes,
                                    visibility = oneDetail.IsVisible,
                                    endTimeMin = oneDetail.EndTimeInMinutes,
                                    startDate = oneDetail.EffectiveStartDate,
                                    endDate = oneDetail.EffectiveEndDate,
                                    updatedAt = oneDetail.CustomerAccount.UpdatedAt,
                                    custID = oneDetail.CustomerAccountId,
                                    accountName = oneDetail.CustomerAccount.AccountName
                                    //updatedBy = oneDetail.CustomerAccount.UpdatedBy.UserName
                                });
                            }
                        }
                    }//foreach
                }
                return new JsonResult(accountDetailList);
            }
            else
            {
                return BadRequest("User not authorized");
            }
        }

        //GET 
        [HttpGet("{custID}")]
        public IActionResult Get(int custID)
        {
            //TODO: Fix code here
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);
                var user = _userService.GetById(userId);
                string roles = user.Roles;

                List<object> accountDetailList = new List<object>();
                var accountDetails = Database.AccountDetails
                    .Include(input => input.CustomerAccount)
                    .Where(input => input.CustomerAccountId == custID);

                if (accountDetails != null)
                {

                    foreach (var oneDetail in accountDetails)
                    {
                        if (roles.Equals("Admin"))
                        {
                            accountDetailList.Add(new
                            {
                                accountDetailId = oneDetail.AccountDetailId,
                                dayOfWeek = oneDetail.DayOfWeekNumber,
                                startTimeMin = oneDetail.StartTimeInMinutes,
                                visibility = oneDetail.IsVisible,
                                endTimeMin = oneDetail.EndTimeInMinutes,
                                startDate = oneDetail.EffectiveStartDate.ToString("dd/MM/yyyy"),
                                endDate = oneDetail.EffectiveEndDate?.ToString("dd/MM/yyyy"),
                                updatedAt = oneDetail.CustomerAccount.UpdatedAt.ToString("dd/MM/yyyy"),
                                //updatedBy = oneDetail.CustomerAccount.UpdatedBy.UserName
                            });
                        }
                        else if (roles.Equals("Instructor"))
                        {
                            if (oneDetail.IsVisible == true)
                            {
                                accountDetailList.Add(new
                                {
                                    accountDetailId = oneDetail.AccountDetailId,
                                    dayOfWeek = oneDetail.DayOfWeekNumber,
                                    startTimeMin = oneDetail.StartTimeInMinutes,
                                    visibility = oneDetail.IsVisible,
                                    endTimeMin = oneDetail.EndTimeInMinutes,
                                    startDate = oneDetail.EffectiveStartDate.ToString("dd/MM/yyyy"),
                                    endDate = oneDetail.EffectiveEndDate?.ToString("dd/MM/yyyy"),
                                    updatedAt = oneDetail.CustomerAccount.UpdatedAt.ToString("dd/MM/yyyy"),
                                    //updatedBy = oneDetail.CustomerAccount.UpdatedBy.UserName
                                });
                            }
                        }
                    }//foreach
                }
                return new JsonResult(accountDetailList);
            }
            else
            {
                return BadRequest("User not authorized");
            }
        }


        //GET api/CustomerAccounts/UpdateAccountDetail/2
        [HttpGet("UpdateAccountDetail/{accId}")]
        public IActionResult GetSpecificAccountDetail(int accId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
                userId = Int32.Parse(identity.FindFirst("userid").Value);

                List<object> ratesList = new List<object>();
                var accountDetail = Database.AccountDetails
                    .Where(eachRate => eachRate.AccountDetailId == accId).Single();

                var response = new
                {
                    accountDetailId = accountDetail.AccountDetailId,
                    dayOfWeek = accountDetail.DayOfWeekNumber,
                    startTimeMin = accountDetail.StartTimeInMinutes,
                    visibility = accountDetail.IsVisible,
                    endTimeMin = accountDetail.EndTimeInMinutes,
                    startDate = accountDetail.EffectiveStartDate,
                    endDate = accountDetail.EffectiveEndDate,
                    //updatedAt = accountDetail.CustomerAccount.UpdatedAt.ToString("dd/MM/yyyy"),
                    customerAccountId = accountDetail.CustomerAccountId
                    //updatedBy = accountDetail.CustomerAccount.UpdatedBy.UserName

                };

                return new JsonResult(response);
            }
            else
            {
                return BadRequest("User unauthorised");
            }
        }

        //PUT api/AccountDetails/UpdateAccountDetail/2
        [HttpPut("UpdateAccountDetail/{id}")]
        public IActionResult PutAccountDetail(int id, [FromForm] IFormCollection webFormData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);


                var oneAcc = Database.AccountDetails
                    .Include(item => item.CustomerAccount)
                    .Where(item => item.AccountDetailId == id).Single();

                oneAcc.EffectiveStartDate = Convert.ToDateTime(webFormData["startDate"]);
                oneAcc.StartTimeInMinutes = Convert.ToInt32(webFormData["startTimeinMin"]);
                oneAcc.EndTimeInMinutes = Convert.ToInt32(webFormData["endTimeinMin"]);
                oneAcc.DayOfWeekNumber = Convert.ToInt32(webFormData["dayOfWeek"]);
                oneAcc.IsVisible = Convert.ToBoolean(webFormData["visibility"]);
                oneAcc.CustomerAccount.UpdatedAt = DateTime.Now;
                oneAcc.CustomerAccount.UpdatedById = userId;

                if (Convert.ToDateTime(webFormData["startTime"]) == null || Convert.ToDateTime(webFormData["endTime"]) == null || Convert.ToInt32(webFormData["dayOfWeek"]) == null || Convert.ToDateTime(webFormData["startDate"]) == null)
                {
                    object httpFailRequestResultMessage = new { message = "Could not update. Please fill in all fields and try again." };
                    return BadRequest(httpFailRequestResultMessage);
                }

                DateTime dateTime;
                if (DateTime.TryParse(webFormData["endDate"], out dateTime) == false)
                {
                    oneAcc.EffectiveEndDate = null;
                }
                else
                {
                    oneAcc.EffectiveEndDate = Convert.ToDateTime(webFormData["endDate"]);

                }

                try
                {
                    Database.Update(oneAcc);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                var successRequestResultMessage = new
                {
                    message = "Saved Account Detail record"
                };

                OkObjectResult httpOkResult =
                             new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to update Account Detail record" });
        }


        //POST api/AccountDetails
        [HttpPost("CreateAccountDetail/{custId}")]
        public IActionResult Post([FromForm] IFormCollection webFormData, int custId)
        {
            string customMessage = "Unable to save record. Please fill in all fields and try again.";
            AccountDetail newDetail = new AccountDetail();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);


                try
                {
                    if (Convert.ToDateTime(webFormData["startTime"]) == null || Convert.ToDateTime(webFormData["endTime"]) == null || Convert.ToInt32(webFormData["dayOfWeek"]) == null || Convert.ToDateTime(webFormData["startDate"]) == null)
                    {
                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);
                    }
                    newDetail.EffectiveStartDate = Convert.ToDateTime(webFormData["startDate"]);
                    newDetail.StartTimeInMinutes = Convert.ToInt32(webFormData["startTimeinMin"]);
                    newDetail.EndTimeInMinutes = Convert.ToInt32(webFormData["endTimeinMin"]);
                    newDetail.DayOfWeekNumber = Convert.ToInt32(webFormData["dayOfWeek"]);
                    newDetail.IsVisible = Convert.ToBoolean(webFormData["visibility"]);
                    newDetail.CustomerAccountId = custId;

                    var cust = Database.CustomerAccounts
                    .SingleOrDefault(eachAcc => eachAcc.CustomerAccountId == custId);

                    if (cust != null)
                    {
                        cust.UpdatedById = userId;
                        cust.UpdatedAt = DateTime.Now;
                    }

                    Console.Write(newDetail);


                    DateTime dateTime;
                    if (DateTime.TryParse(webFormData["endDate"], out dateTime) == false)
                    {
                        newDetail.EffectiveEndDate = null;
                    }
                    else
                    {
                        newDetail.EffectiveEndDate = Convert.ToDateTime(webFormData["endDate"]);

                    }

                    Console.Write(newDetail);
                    Database.AccountDetails.Add(newDetail);
                    Database.CustomerAccounts.Update(cust);

                    Database.SaveChanges();

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {

                        customMessage = "Unable to save record due" + ex;

                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);

                    }
                    return BadRequest(ex);
                }//end try catch

                var successRequestResultMessage = new
                {
                    message = "saved account detail record"
                };

                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to create new record" });

        }



        // DELETE api/AccountDetail/5
        [HttpDelete("{detailId}")]
        public IActionResult DeleteAccount(int detailId)
        {
            string customMessage = "Deleted Account detail Record";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                try
                {
                    var delAccount = Database.AccountDetails
                    .SingleOrDefault(eachAcc => eachAcc.AccountDetailId == detailId);

                    if (delAccount != null)
                    {
                        Database.AccountDetails.Remove(delAccount);
                        //Tell the db model to commit/persist the changes to the database, 
                        //I use the following command.
                        Database.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    customMessage = "Unable to delete account detail record.";
                    object httpFailRequestResultMessage = new { message = customMessage };

                    return BadRequest(httpFailRequestResultMessage);

                }//End of try .. catch block on manage data


                var successRequestResultMessage = new
                {
                    message = "Deleted account detail record"
                };

                OkObjectResult httpOkResult =
                            new OkObjectResult(successRequestResultMessage);
                //Send the OkObjectResult class object back to the client.
                return httpOkResult;
            }
            else
            {
                return BadRequest("User unauthorised");
            }
        }//end of Delete() Web API method


    }
}