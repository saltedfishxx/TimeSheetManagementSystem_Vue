using AspNetCore.Data;
using AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Services
{
    public interface IUserService
    {
        User Authenticate(string inUserId, string inPassword);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        public User Authenticate(string inUserId, string inPassword)
        {
            throw new NotImplementedException();
        }

        public User Create(User user, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
