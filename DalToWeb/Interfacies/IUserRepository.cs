using System.Collections.Generic;
using DalToWeb.Repositories;

namespace DalToWeb.Interfacies
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        bool CreateUser(User user);
        User GetUserByEmail(string email);
        bool UpdateUser(User user);
        bool RemoveUser(int id);
    }
}