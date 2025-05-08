namespace UserManagementApp.Interfaces
{
    using UserManagementApp.Models;
    using System.Collections.Generic;

    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void SaveAllUsers(List<User> users);
        void AddUser(User user);
        User GetUserByUsername(string username);
    }
}
