﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class UserInfo
    {
        //The database domain class will not model the admin and instructor role
        //information since AspNetUsers and the respective security tables
        //are in-charge of helping the security module in the Web App to 
        //decide authorization.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<InstructorAccount> InstructorAccounts { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }

    }
}
