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
    public class CustomerAccountsController : Controller
    {


        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }
        private IUserService _userService;


        public CustomerAccountsController(ApplicationDbContext database, IUserService userService)
        {
            Database = database; //Initialize the Database property
            _userService = userService;
        }

        //GET 
        [HttpGet]
        public IActionResult Get()
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

                List<object> customerList = new List<object>();
                var customers = Database.CustomerAccounts
                    .Include(input => input.CreatedBy)
                    .Include(input => input.UpdatedBy)
                    .Include(input => input.AccountRates)
                    .Include(input => input.AccountDetails);


                foreach (var oneCustomer in customers)
                {
                    if (roles.Equals("Admin"))
                    {
                        customerList.Add(new
                        {
                            customerAccountId = oneCustomer.CustomerAccountId,
                            accountName = oneCustomer.AccountName,
                            comments = oneCustomer.Comments,
                            visibility = oneCustomer.IsVisible,
                            numAccRates = oneCustomer.AccountRates.Count,
                            numAccDetails = oneCustomer.AccountDetails.Count,
                            updatedAt = oneCustomer.UpdatedAt.ToString("dd/MM/yyyy"),
                            updatedBy = oneCustomer.UpdatedBy.UserName
                        });
                    }
                    else
                    {
                        if (oneCustomer.IsVisible == true)
                        {
                            customerList.Add(new
                            {
                                customerAccountId = oneCustomer.CustomerAccountId,
                                accountName = oneCustomer.AccountName,
                                comments = oneCustomer.Comments,
                                visibility = oneCustomer.IsVisible,
                                numAccRates = oneCustomer.AccountRates.Count,
                                numAccDetails = oneCustomer.AccountDetails.Count,
                                updatedAt = oneCustomer.UpdatedAt.ToString("dd/MM/yyyy"),
                                updatedBy = oneCustomer.UpdatedBy.UserName
                            });
                        }
                    }
                }//foreach

                return new JsonResult(customerList);
            }
            else
            {
                return BadRequest("User not authorized");
            }
        }

        //GET 
        [HttpGet("CustomerList")]
        public IActionResult GetCustomerList()
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

                List<object> customerList = new List<object>();
                var customers = Database.CustomerAccounts
                    .Include(input => input.CreatedBy)
                    .Include(input => input.UpdatedBy)
                    .Include(input => input.AccountRates)
                    .Include(input => input.AccountDetails);


                foreach (var oneCustomer in customers)
                {
                    if (roles.Equals("Admin"))
                    {
                        customerList.Add(new
                        {
                            customerAccountId = oneCustomer.CustomerAccountId,
                            accountName = oneCustomer.AccountName,
                            comments = oneCustomer.Comments,
                            visibility = oneCustomer.IsVisible,
                            numAccRates = oneCustomer.AccountRates.Count,
                            numAccDetails = oneCustomer.AccountDetails.Count,
                            updatedAt = oneCustomer.UpdatedAt,
                            updatedBy = oneCustomer.UpdatedBy.UserName
                        });
                    }
                    else
                    {
                        if (oneCustomer.IsVisible == true)
                        {
                            customerList.Add(new
                            {
                                customerAccountId = oneCustomer.CustomerAccountId,
                                accountName = oneCustomer.AccountName,
                                comments = oneCustomer.Comments,
                                visibility = oneCustomer.IsVisible,
                                numAccRates = oneCustomer.AccountRates.Count,
                                numAccDetails = oneCustomer.AccountDetails.Count,
                                updatedAt = oneCustomer.UpdatedAt,
                                updatedBy = oneCustomer.UpdatedBy.UserName
                            });
                        }
                    }
                }//foreach

                return new JsonResult(customerList);
            }
            else
            {
                return BadRequest("User not authorized");
            }
        }
        //GET api/CustomerAccounts/UpdateGeneralInfo/5
        [HttpGet("UpdateGeneralInfo/{custID}")]
        public IActionResult GetCustInfo(int custID)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                List<object> customerList = new List<object>();
                var selectedCustomer = Database.CustomerAccounts
                    .Where(eachCust => eachCust.CustomerAccountId == custID)
                    .Include(cust => cust.UpdatedBy).Single();
                var response = new
                {
                    accountId = selectedCustomer.CustomerAccountId,
                    accountName = selectedCustomer.AccountName,
                    comments = selectedCustomer.Comments,
                    visibility = selectedCustomer.IsVisible,
                    updatedAt = selectedCustomer.UpdatedAt.ToString("dd/MM/yyyy"),
                    updatedBy = selectedCustomer.UpdatedBy.FirstName
                };

                return new JsonResult(response);
            }
            else
            {
                return BadRequest("User not authorised");
            }
        }

        //GET api/CustomerAccounts/GetAllRates
        [HttpGet("GetAllRates")]
        public IActionResult GetAllRates()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                List<object> ratesList = new List<object>();
                var accountRates = Database.AccountRates
                .Include(input => input.CustomerAccount);

                foreach (var oneRate in accountRates)
                {
                    ratesList.Add(new
                    {
                        customerAccountId = oneRate.CustomerAccountId,
                        rateId = oneRate.AccountRateId,
                        rateHour = oneRate.RatePerHour,
                        startDate = oneRate.EffectiveStartDate,
                        endDate = oneRate.EffectiveEndDate,
                        updatedAt = oneRate.CustomerAccount.UpdatedAt,
                        updatedBy = oneRate.CustomerAccount.UpdatedBy.UserName,
                        accountName = oneRate.CustomerAccount.AccountName

                    });
                }//foreach

                return new JsonResult(ratesList);
            }
            else
            {
                return BadRequest("User unauthorised");
            }
        }

        //GET api/CustomerAccounts/ManageAccountRates/5
        [HttpGet("ManageAccountRates/{custID}")]
        public IActionResult GetAccountRates(int custID)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                List<object> ratesList = new List<object>();
                var accountRates = Database.AccountRates
                    .Where(eachRate => eachRate.CustomerAccountId == custID);

                foreach (var oneRate in accountRates)
                {
                    ratesList.Add(new
                    {
                        customerAccountId = oneRate.CustomerAccountId,
                        rateId = oneRate.AccountRateId,
                        rateHour = oneRate.RatePerHour,
                        startDate = oneRate.EffectiveStartDate.ToString("dd/MM/yyyy"),
                        endDate = oneRate.EffectiveEndDate?.ToString("dd/MM/yyyy")

                    });
                }//foreach

                return new JsonResult(ratesList);
            }
            else
            {
                return BadRequest("User unauthorised");
            }
        }

        //GET api/CustomerAccounts/UpdateAccountRates/2
        [HttpGet("UpdateAccountRates/{rateId}")]
        public IActionResult GetSpecificAccountRates(int rateID)
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
                var accountRates = Database.AccountRates
                    .Where(eachRate => eachRate.AccountRateId == rateID).Single();

                var response = new
                {
                    customerAccountId = accountRates.CustomerAccountId,
                    rateId = accountRates.AccountRateId,
                    rateHour = accountRates.RatePerHour,
                    startDate = accountRates.EffectiveStartDate,
                    endDate = accountRates.EffectiveEndDate

                };

                return new JsonResult(response);
            }
            else
            {
                return BadRequest("User unauthorised");
            }
        }

        //PUT api/CustomerAccounts/UpdateAccountRates/2
        [HttpPut("UpdateAccountRates/{id}")]
        public IActionResult PutAccountRate(int id, [FromForm] IFormCollection webFormData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);


                var oneAcc = Database.AccountRates
                    .Where(item => item.AccountRateId == id).Single();

                oneAcc.RatePerHour = Convert.ToDecimal(webFormData["rateHour"]);
                oneAcc.EffectiveStartDate = Convert.ToDateTime(webFormData["startDate"]);

                if (Convert.ToDecimal(webFormData["rateHour"]) == null || Convert.ToDateTime(webFormData["startDate"]) == null)
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
                var oneCust = Database.CustomerAccounts
                    .Where(item => item.CustomerAccountId == oneAcc.CustomerAccountId).Single();
                oneCust.UpdatedAt = DateTime.Now;
                oneCust.UpdatedById = userId;

                try
                {
                    Database.Update(oneAcc);
                    Database.Update(oneCust);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                var successRequestResultMessage = new
                {
                    message = "Saved Account record"
                };

                OkObjectResult httpOkResult =
                             new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to create record" });
        }

        //PUT api/CustomerAccounts/UpdateGeneralInfo/5
        [HttpPut("UpdateGeneralInfo/{id}")]
        public IActionResult Put(int id, [FromForm] IFormCollection webFormData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                var oneCust = Database.CustomerAccounts
                    .Where(item => item.CustomerAccountId == id).Single();

                if (webFormData["accountName"].ToString() == null || Convert.ToBoolean(webFormData["visibility"]) == null)
                {
                    object httpFailRequestResultMessage = new { message = "Could not update. Please fill in all fields and try again." };
                    return BadRequest(httpFailRequestResultMessage);

                }
                oneCust.AccountName = webFormData["accountName"];
                oneCust.Comments = webFormData["comments"];
                oneCust.IsVisible = Convert.ToBoolean(webFormData["visibility"]);
                oneCust.UpdatedById = userId;
                oneCust.UpdatedAt = DateTime.Now;


                try
                {
                    Database.Update(oneCust);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message
                                              .Contains("CustomerAccounts_CustomerAccountId_UniqueConstraint") == true)
                        {

                            return BadRequest(ex);
                        }
                    }
                    else
                    {
                        return BadRequest(ex);
                    }
                }
                var successRequestResultMessage = new
                {
                    message = "Saved Customer record"
                };


                OkObjectResult httpOkResult =
                             new OkObjectResult(successRequestResultMessage);

                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to update record" });
        }


        //POST api/CustomerAccounts
        [HttpPost]
        public IActionResult Post([FromForm] IFormCollection webFormData)
        {
            string customMessage = "Unable to save record. Please fill in all fields and try again.";
            CustomerAccount newCustomer = new CustomerAccount();
            AccountRate newRate = new AccountRate();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);


                try
                {
                    if (webFormData["accountName"].ToString() == null || Convert.ToBoolean(webFormData["visibility"]) == null)
                    {
                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);
                    }
                    newCustomer.AccountName = webFormData["accountName"];
                    newCustomer.IsVisible = Convert.ToBoolean(webFormData["visibility"]);
                    newCustomer.CreatedById = userId;
                    newCustomer.UpdatedById = userId;
                    newCustomer.Comments = webFormData["comments"];
                    newCustomer.CreatedAt = DateTime.Now;
                    newCustomer.UpdatedAt = DateTime.Now;

                    Console.Write(newCustomer);
                    Database.CustomerAccounts.Add(newCustomer);

                    Database.SaveChanges();

                    //get cust id to save account rate
                    var createdCustomer = Database.CustomerAccounts
                  .Where(eachCustomer => eachCustomer.AccountName == newCustomer.AccountName).Single();
                    int AccountId = createdCustomer.CustomerAccountId;

                    if (createdCustomer == null || AccountId == null || newRate.RatePerHour == null || Convert.ToDecimal(webFormData["rateHour"]) == null || Convert.ToDateTime(webFormData["startDate"]) == null)
                    {
                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);
                    }

                    newRate.CustomerAccount = createdCustomer;
                    newRate.CustomerAccountId = AccountId;
                    newRate.RatePerHour = Convert.ToDecimal(webFormData["rateHour"]);
                    newRate.EffectiveStartDate = Convert.ToDateTime(webFormData["startDate"]);

                    DateTime dateTime;
                    if (DateTime.TryParse(webFormData["endDate"], out dateTime) == false)
                    {
                        newRate.EffectiveEndDate = null;
                    }
                    else
                    {
                        newRate.EffectiveEndDate = Convert.ToDateTime(webFormData["endDate"]);

                    }

                    Console.Write(newRate);
                    Database.AccountRates.Add(newRate);

                    Database.SaveChanges();

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("CustomerAccount_AccountName_UniqueConstraint") == true)
                        {
                            customMessage = "Unable to save record due to another customer having the same name: " + webFormData["accountName"];

                            object httpFailRequestResultMessage = new { message = customMessage };
                            return BadRequest(httpFailRequestResultMessage);
                        }
                    }
                    return BadRequest(ex);
                }//end try catch

                var successRequestResultMessage = new
                {
                    message = "saved customer record"
                };

                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to create new record" });

        }




        //POST api/CustomerAccounts
        [HttpPost("CreateAccountRate/{id}")]
        public IActionResult PostAccountRate(int id, [FromForm] IFormCollection webFormData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                string customMessage = "failed to save";
                int custId = id;
                AccountRate newRate = new AccountRate();

                try
                {
                    var createdCustomer = Database.CustomerAccounts
                  .Where(eachCustomer => eachCustomer.CustomerAccountId == custId).Single();
                    int AccountId = createdCustomer.CustomerAccountId;

                    if (Convert.ToDecimal(webFormData["rateHour"]) == null || Convert.ToDateTime(webFormData["startDate"]) == null)
                    {
                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);
                    }

                    newRate.CustomerAccount = createdCustomer;
                    newRate.CustomerAccountId = AccountId;
                    newRate.RatePerHour = Convert.ToDecimal(webFormData["rateHour"]);
                    newRate.EffectiveStartDate = Convert.ToDateTime(webFormData["startDate"]);
                    DateTime dateTime;
                    if (DateTime.TryParse(webFormData["endDate"], out dateTime) == false)
                    {
                        newRate.EffectiveEndDate = null;
                    }
                    else
                    {
                        newRate.EffectiveEndDate = Convert.ToDateTime(webFormData["endDate"]);

                    }
                    Console.Write(newRate);
                    Database.AccountRates.Add(newRate);

                    Database.SaveChanges();



                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("CustomerAccount_AccountName_UniqueConstraint") == true)
                        {


                            object httpFailRequestResultMessage = new { message = customMessage };
                            return BadRequest(httpFailRequestResultMessage);
                        }
                    }

                    return BadRequest(ex);
                }//end try catch

                var successRequestResultMessage = new
                {
                    message = "saved account rate record"
                };

                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                return httpOkResult;
            }
            else
            {
                return BadRequest("User unauthorised");
            }
        }

        // DELETE api/CustomerAccounts/UpdateAccountRates/5
        [HttpDelete("UpdateAccountRates/{id}")]
        public IActionResult DeleteAccount(int id)
        {
            string customMessage = "Deleted Account Record";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                try
                {
                    var delAccount = Database.AccountRates
                    .SingleOrDefault(eachAcc => eachAcc.AccountRateId == id);

                    if (delAccount != null)
                    {
                        Database.AccountRates.Remove(delAccount);
                        //Tell the db model to commit/persist the changes to the database, 
                        //I use the following command.
                        Database.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    customMessage = "Unable to delete account record.";
                    object httpFailRequestResultMessage = new { message = customMessage };

                    return BadRequest(httpFailRequestResultMessage);

                }//End of try .. catch block on manage data


                var successRequestResultMessage = new
                {
                    message = "Deleted account record"
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

        //DELETE api/CustomerAccounts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCust(int id)
        {
            string customMessage = "Deleted Customer Record";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                userId = Int32.Parse(identity.FindFirst("userid").Value);

                try
                {
                    var delCust = Database.CustomerAccounts
                    .Single(eachAcc => eachAcc.CustomerAccountId == id);

                    Database.CustomerAccounts.Remove(delCust);

                    var delAccount = Database.AccountRates
                    .Where(eachAcc => eachAcc.CustomerAccountId == id);

                    foreach (var rates in delAccount)
                    {
                        Database.AccountRates.Remove(rates);
                    }
                    Database.SaveChanges();

                    try
                    {
                        var delRate = Database.AccountRates
                        .Where(eachAcc => eachAcc.CustomerAccountId == id);

                        foreach (var rates in delRate)
                        {
                            Database.AccountRates.Remove(rates);
                        }
                        Database.SaveChanges();

                        try
                        {
                            var delDetails = Database.AccountDetails
                            .Where(eachDetail => eachDetail.CustomerAccountId == id);

                            foreach (var details in delDetails)
                            {
                                Database.AccountDetails.Remove(details);
                            }
                            Database.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            customMessage = "Unable to delete account record.";
                            object httpFailRequestResultMessage = new { message = customMessage };

                            return BadRequest(httpFailRequestResultMessage);
                        }//End of try .. catch block on manage data
                    }
                    catch (Exception ex)
                    {
                        customMessage = "Unable to delete account record.";
                        object httpFailRequestResultMessage = new { message = customMessage };

                        return BadRequest(httpFailRequestResultMessage);
                    }//End of try .. catch block on manage data

                    var successRequestResultMessage = new
                    {
                        message = "Deleted account record"
                    };
                    OkObjectResult httpOkResult =
                                new OkObjectResult(successRequestResultMessage);

                    return httpOkResult;


                }
                catch (Exception ex)
                {
                    customMessage = "Unable to delete account record.";
                    object httpFailRequestResultMessage = new { message = customMessage };

                    return BadRequest(httpFailRequestResultMessage);
                }//End of try .. catch block on manage data

            }
            else
            {
                return BadRequest("User unauthorised");
            }

        }//end of Delete() Web API method


    }
}