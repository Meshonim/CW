using System;
using System.Collections.Generic;
using System.Linq;
using DalToWeb.Interfacies;

namespace DalToWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context = new UserContext();

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool CreateUser(User user)
        {
            if (user.Id != 0) return false;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User GetUserByEmail(string email)
        {
            var user = (from u in _context.Users
                        where u.Email == email
                        select u).FirstOrDefault();
            return user;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}