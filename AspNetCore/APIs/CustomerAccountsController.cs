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

namespace TimeSheetManagementSystem.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    public class CustomerAccountsController : Controller
    {
        //The following five properties are required for every web api controller
        //class

        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }

        //Create a Constructor, so that the .NET engine can pass in the ApplicationDbContext object
        //which represents the database session.
        public CustomerAccountsController(ApplicationDbContext database)
        {
            Database = database; //Initialize the Database property

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
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
                userId = Int32.Parse(identity.FindFirst("userid").Value);

                List<object> customerList = new List<object>();
                var customers = Database.CustomerAccounts
                    .Include(input => input.CreatedBy)
                    .Include(input => input.UpdatedBy)
                .Where(eachCustomer => eachCustomer.IsVisible == true);

                foreach (var oneCustomer in customers)
                {
                    customerList.Add(new
                    {
                        customerAccountId = oneCustomer.CustomerAccountId,
                        accountName = oneCustomer.AccountName,
                        comments = oneCustomer.Comments,
                        visibility = oneCustomer.IsVisible,
                        updatedAt = oneCustomer.UpdatedAt.ToString("yyyy-MM-dd"),
                        updatedBy = oneCustomer.UpdatedBy.UserName
                    });
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
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
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
                    updatedAt = selectedCustomer.UpdatedAt.ToString("yyyy-MM-dd"),
                    updatedBy = selectedCustomer.UpdatedBy.FirstName
                };

                return new JsonResult(response);
            }
            else
            {
                return BadRequest("User not authorised");
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
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
                userId = Int32.Parse(identity.FindFirst("userid").Value);

                List<object> ratesList = new List<object>();
                var accountRates = Database.AccountRates
                    .Where(eachRate => eachRate.CustomerAccountId == custID);
                //.Where(eachSession => eachSession.DeletedAt == null)

                foreach (var oneRate in accountRates)
                {
                    ratesList.Add(new
                    {
                        customerAccountId = oneRate.CustomerAccountId,
                        rateId = oneRate.AccountRateId,
                        rateHour = oneRate.RatePerHour,
                        startDate = oneRate.EffectiveStartDate.ToString("yyyy-MM-dd"),
                        endDate = oneRate.EffectiveEndDate.Value.ToString("yyyy-MM-dd")

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
                //.Where(eachSession => eachSession.DeletedAt == null)

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
        public IActionResult PutAccountRate(int id, [FromBody]string value)
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

                var accountChangeInput = JsonConvert.DeserializeObject<dynamic>(value);

                var oneAcc = Database.AccountRates
                    .Where(item => item.AccountRateId == id).Single();
                oneAcc.RatePerHour = decimal.Parse(accountChangeInput.rateHour.Value);
                oneAcc.EffectiveStartDate = Convert.ToDateTime(accountChangeInput.startDate.Value);
                oneAcc.EffectiveEndDate = Convert.ToDateTime(accountChangeInput.endDate.Value);
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


                }//End of try .. catch block on saving data
                 //Construct a custom message for the client
                 //Create a success message anonymous object which has a 
                 //message member variable (property)
                var successRequestResultMessage = new
                {
                    message = "Saved Account record"
                };

                //Create a OkObjectResult class instance, httpOkResult.
                //When creating the object, provide the previous message object into it.
                OkObjectResult httpOkResult =
                             new OkObjectResult(successRequestResultMessage);
                //Send the OkObjectResult class object back to the client.
                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to create record" });
        }

        //PUT api/CustomerAccounts/UpdateGeneralInfo/5
        [HttpPut("UpdateGeneralInfo/{id}")]
        public IActionResult Put(int id, [FromBody]string value)
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

                string customMessage = "";
                var customerChangeInput = JsonConvert.DeserializeObject<dynamic>(value);
                //To obtain the full name information, use studentChangeInput.FullName.value
                //To obtain the email information, use studentChangeInput.Email.value
                var oneCust = Database.CustomerAccounts
                    .Where(item => item.CustomerAccountId == id).Single();
                oneCust.AccountName = customerChangeInput.accountName.Value;
                oneCust.Comments = customerChangeInput.comments.Value;
                oneCust.IsVisible = customerChangeInput.visibility.Value;
                oneCust.UpdatedById = userId;
                oneCust.UpdatedAt = DateTime.Now;

                try
                {
                    Database.Update(oneCust);
                    Database.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message
                                          .Contains("CustomerAccounts_CustomerAccountId_UniqueConstraint") == true)
                    {
                        customMessage = "Unable to save student record due " +
                                          "to another record having the same admin id : " +
                        customerChangeInput.admissionId.Value;
                        //Create a fail fail message anonymous object that has one property, message.
                        //This anonymous object's Message property contains a simple string message
                        object httpFailRequestResultMessage = new { message = customMessage };
                        //Return a bad http request message to the client
                        return BadRequest(httpFailRequestResultMessage);
                    }
                }//End of try .. catch block on saving data
                 //Construct a custom message for the client
                 //Create a success message anonymous object which has a 
                 //message member variable (property)
                var successRequestResultMessage = new
                {
                    message = "Saved Customer record"
                };

                //Create a OkObjectResult class instance, httpOkResult.
                //When creating the object, provide the previous message object into it.
                OkObjectResult httpOkResult =
                             new OkObjectResult(successRequestResultMessage);
                //Send the OkObjectResult class object back to the client.
                return httpOkResult;
            }
            return BadRequest(new { message = "Unable to update record" });
        }


        //POST api/CustomerAccounts
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            string customMessage = "";
            CustomerAccount newCustomer = new CustomerAccount();
            AccountRate newRate = new AccountRate();
            var newInput = JsonConvert.DeserializeObject<dynamic>(value);
            Console.Write(newInput);

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
                userId = Int32.Parse(identity.FindFirst("userid").Value);


                try
                {
                    newCustomer.AccountName = newInput.accountName.Value;
                    newCustomer.IsVisible = newInput.visibility.Value;
                    newCustomer.CreatedById = userId;
                    newCustomer.UpdatedById = userId;
                    newCustomer.Comments = newInput.comments.Value;
                    newCustomer.CreatedAt = DateTime.Now;
                    newCustomer.UpdatedAt = DateTime.Now;

                    if (newCustomer.AccountName == null)
                    {
                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);
                    }
                    else
                    {
                        Console.Write(newCustomer);
                        Database.CustomerAccounts.Add(newCustomer);

                        Database.SaveChanges();

                        var createdCustomer = Database.CustomerAccounts
                      .Where(eachCustomer => eachCustomer.AccountName == newCustomer.AccountName).Single();
                        int AccountId = createdCustomer.CustomerAccountId;

                        newRate.CustomerAccount = createdCustomer;
                        newRate.CustomerAccountId = AccountId;
                        newRate.RatePerHour = decimal.Parse(newInput.rateHour.Value);
                        newRate.EffectiveStartDate = Convert.ToDateTime(newInput.startDate.Value);
                        newRate.EffectiveEndDate = Convert.ToDateTime(newInput.endDate.Value);

                        Console.Write(newRate);
                        Database.AccountRates.Add(newRate);

                        Database.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("CustomerAccount_AccountName_UniqueConstraint") == true)
                        {
                            customMessage = "Unable to save record due to another customer having the same name: " + newInput.sessionName.Value;

                            object httpFailRequestResultMessage = new { message = customMessage };
                            return BadRequest(httpFailRequestResultMessage);
                        }
                    }
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
        public IActionResult PostAccountRate(int id, [FromBody] string value)
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

                string customMessage = "failed to save";
                int custId = id;
                AccountRate newRate = new AccountRate();
                var newInput = JsonConvert.DeserializeObject<dynamic>(value);
                Console.Write(newInput);
                try
                {
                    var createdCustomer = Database.CustomerAccounts
                  .Where(eachCustomer => eachCustomer.CustomerAccountId == custId).Single();
                    int AccountId = createdCustomer.CustomerAccountId;

                    newRate.CustomerAccount = createdCustomer;
                    newRate.CustomerAccountId = AccountId;
                    newRate.RatePerHour = decimal.Parse(newInput.rateHour.Value);
                    newRate.EffectiveStartDate = Convert.ToDateTime(newInput.startDate.Value);
                    newRate.EffectiveEndDate = Convert.ToDateTime(newInput.endDate.Value);

                    if (newRate.RatePerHour == null || newRate.EffectiveStartDate == null || newRate.EffectiveEndDate == null)
                    {
                        object httpFailRequestResultMessage = new { message = customMessage };
                        return BadRequest(httpFailRequestResultMessage);
                    }
                    else
                    {
                        Console.Write(newRate);
                        Database.AccountRates.Add(newRate);

                        Database.SaveChanges();
                    }


                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        //if (ex.InnerException.Message.Contains("CustomerAccount_AccountName_UniqueConstraint") == true)
                        //{


                        //    object httpFailRequestResultMessage = new { message = customMessage };
                        //    return BadRequest(httpFailRequestResultMessage);
                        //}
                    }
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
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
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
                    //Return a bad http request message to the client
                    return BadRequest(httpFailRequestResultMessage);
                }//End of try .. catch block on manage data

                //Build a custom message for the client
                //Create a success message anonymous type object which has a 
                //message member variable (property)
                var successRequestResultMessage = new
                {
                    message = "Deleted account record"
                };

                //Create a OkObjectResult class instance, httpOkResult.
                //When creating the object, provide the previous message object into it.
                OkObjectResult httpOkResult =
                            new OkObjectResult(successRequestResultMessage);
                //Send the OkObjectResult class object back to the client.
                return httpOkResult;
            }
            else
            {
                return BadRequest("user unauthorised");
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
                //claims.Where(i=>i.Type=="username").First().Value
                //claims.Where(i=>i.Type=="userid").First().Value

                // or
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
                    //string customMessage = "Deleted Account Record";

                    try
                    {
                        var delRate = Database.AccountRates
                        .Where(eachAcc => eachAcc.CustomerAccountId == id);

                        foreach (var rates in delRate)
                        {
                            Database.AccountRates.Remove(rates);
                            //Tell the db model to commit/persist the changes to the database, 
                            //I use the following command.
                        }
                        Database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        customMessage = "Unable to delete account record.";
                        object httpFailRequestResultMessage = new { message = customMessage };
                        //Return a bad http request message to the client
                        return BadRequest(httpFailRequestResultMessage);
                    }//End of try .. catch block on manage data

                    //Build a custom message for the client
                    //Create a success message anonymous type object which has a 
                    //message member variable (property)
                    var successRequestResultMessage = new
                    {
                        message = "Deleted account record"
                    };
                    OkObjectResult httpOkResult =
                                new OkObjectResult(successRequestResultMessage);
                    //Send the OkObjectResult class object back to the client.
                    return httpOkResult;
                    //Send the OkObjectResult class object back to the client.

                }
                catch (Exception ex)
                {
                    customMessage = "Unable to delete account record.";
                    object httpFailRequestResultMessage = new { message = customMessage };
                    //Return a bad http request message to the client
                    return BadRequest(httpFailRequestResultMessage);
                }//End of try .. catch block on manage data

            }
            else
            {
                return BadRequest("User unauthorised");
            }

        }//end of Delete() Web API method

        //helper method to get user info id

    }
}