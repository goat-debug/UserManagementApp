using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UserManagementApp.Interfaces;
using UserManagementApp.Models;

namespace UserManagementApp.Repositories
{
    public class FileUserRepository : IUserRepository
    {
        private readonly string _filePath = "Data/users.txt";

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            var lines = File.ReadAllLines(_filePath);
            return lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseLineToUser)
                .ToList();
        }

        public void SaveAllUsers(List<User> users)
        {
            var lines = users.Select(ConvertUserToLine);
            File.WriteAllLines(_filePath, lines);
        }

        public void AddUser(User user)
        {
            var line = ConvertUserToLine(user);
            File.AppendAllLines(_filePath, new[] { line });
        }

        public User GetUserByUsername(string username)
        {
            return GetAllUsers().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private User ParseLineToUser(string line)
        {
            var parts = line.Split('|');
            return new User
            {
                Username = parts[0],
                Password = parts[1],
                Status = parts.Length > 2 ? parts[2] : "available"
            };
        }

        private string ConvertUserToLine(User user)
        {
            return $"{user.Username}|{user.Password}|{user.Status}";
        }
    }
}