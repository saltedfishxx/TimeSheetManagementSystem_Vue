using AspNetCore.Data;
using AspNetCore.Helpers;
using AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Services
{
    public interface IUserService
    {
        UserInfo Authenticate(string inUserId, string inPassword);
        IEnumerable<UserInfo> GetAll();
        UserInfo GetById(int id);
        UserInfo Create(UserInfo user, string password);
        void Update(UserInfo user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserInfo Authenticate(string inUserName, string inPassword)
        {
            if (string.IsNullOrEmpty(inUserName) || string.IsNullOrEmpty(inPassword))
                return null;
            //Check whether there is a matching user name information first.
            //Then, the subsequent code will verify the password by calling
            //the VefiryPasswordHash method
            var user = _context.UserInfo.SingleOrDefault(x => x.UserName == inUserName);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(inPassword, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<UserInfo> GetAll()
        {
            return _context.UserInfo;
        }

        public UserInfo GetById(int id)
        {
            return _context.UserInfo.Find(id);
        }

        public UserInfo Create(UserInfo user, string password)
        {

            // validation to check if the password is empty or spaces only.
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required, password given is " + password + ", name is " + user.FirstName);
            // If the user name (email) already exists, raise an exception
            // so that the Web API controller class code can capture the error and
            // send back a JSON response to the client side.
            if (_context.UserInfo.Any(x => x.UserName == user.UserName))
                throw new AppException("Username " + user.UserName + " is already taken");

            byte[] passwordHash, passwordSalt;  
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.UserInfo.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(UserInfo userParam, string inPassword = null)
        {
            var user = _context.UserInfo.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.UserName != user.UserName)
            {
                // username has changed so check if the new username is already taken
                if (_context.UserInfo.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("User name " + userParam.UserName + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.UserName = userParam.UserName;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(inPassword))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(inPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.UserInfo.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.UserInfo.Find(id);
            if (user != null)
            {
                _context.UserInfo.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string inPassword, out byte[] inPasswordHash, out byte[] inPasswordSalt)
        {
            if (inPassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(inPassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                inPasswordSalt = hmac.Key;
                inPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inPassword));
            }
        }

        private static bool VerifyPasswordHash(string inPassword, byte[] inStoredHash, byte[] inStoredSalt)
        {
            if (inPassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(inPassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (inStoredHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (inStoredSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(inStoredSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inPassword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != inStoredHash[i]) return false;
                }
            }

            return true;
        }
    }
}
