using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementApp.Interfaces;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public User LoggedInUser { get; private set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Register(string username, string password)
        {
            var existingUser = _userRepository.GetUserByUsername(username);
            if (existingUser != null)
                return "register failed! username already exists.";

            var newUser = new User
            {
                Username = username,
                Password = password,
                Status = "available"
            };

            _userRepository.AddUser(newUser);
            return "register successful.";
        }

        public string Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password)
                return "login failed! invalid username or password.";

            LoggedInUser = user;
            return $"login successful. welcome {username}.";
        }

        public string Logout()
        {
            if (LoggedInUser == null)
                return "logout failed! no user is logged in.";

            LoggedInUser = null;
            return "logout successful.";
        }

        public string ChangeStatus(string newStatus)
        {
            if (LoggedInUser == null)
                return "access denied. please login first.";

            var users = _userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Username == LoggedInUser.Username);
            if (user != null)
            {
                user.Status = newStatus;
                _userRepository.SaveAllUsers(users);
                LoggedInUser.Status = newStatus;
                return $"status changed to {newStatus}.";
            }

            return "error: user not found.";
        }

        public string ChangePassword(string oldPassword, string newPassword)
        {
            if (LoggedInUser == null)
                return "access denied. please login first.";

            if (LoggedInUser.Password != oldPassword)
                return "change password failed! old password incorrect.";

            var users = _userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Username == LoggedInUser.Username);
            if (user != null)
            {
                user.Password = newPassword;
                _userRepository.SaveAllUsers(users);
                LoggedInUser.Password = newPassword;
                return "password changed successfully.";
            }

            return "error: user not found.";
        }

        public List<string> SearchUsers(string startsWith)
        {
            var allUsers = _userRepository.GetAllUsers();
            return allUsers
                .Where(u => u.Username.StartsWith(startsWith, StringComparison.OrdinalIgnoreCase))
                .Select(u => $"{u.Username} | status: {u.Status}")
                .ToList();
        }
    }
}
