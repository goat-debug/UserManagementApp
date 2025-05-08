using System;
using System.Linq;
using UserManagementApp.Services;
using UserManagementApp.Core;

namespace UserManagementApp.Core
{
    public class CommandHandler
    {
        private readonly UserService _userService;

        public CommandHandler(UserService userService)
        {
            _userService = userService;
        }

        public void Handle(string input)
        {
            var parts = input.Split("--", StringSplitOptions.RemoveEmptyEntries)
                             .Select(p => p.Trim())
                             .ToArray();

            if (parts.Length == 0)
            {
                Console.WriteLine("invalid command.");
                return;
            }

            var command = parts[0].ToLower();

            var args = parts.Skip(1)
                            .Select(p => p.Split(' ', 2))
                            .Where(p => p.Length == 2)
                            .ToDictionary(p => p[0].ToLower(), p => p[1]);

            switch (command)
            {
                case "register":
                    HandleRegister(args);
                    break;

                case "login":
                    HandleLogin(args);
                    break;

                case "logout":
                    HandleLogout();
                    break;

                case "change":
                    HandleChange(args);
                    break;

                case "changepassword":
                    HandleChangePassword(args);
                    break;

                case "search":
                    HandleSearch(args);
                    break;

                default:
                    Console.WriteLine("unknown command.");
                    break;
            }
        }

        private void HandleRegister(Dictionary<string, string> args)
        {
            if (!args.ContainsKey("username") || !args.ContainsKey("password"))
            {
                Console.WriteLine("register failed! missing parameters.");
                return;
            }

            var result = _userService.Register(args["username"], args["password"]);
            Console.WriteLine(result);
        }

        private void HandleLogin(Dictionary<string, string> args)
        {
            if (!args.ContainsKey("username") || !args.ContainsKey("password"))
            {
                Console.WriteLine("login failed! missing parameters.");
                return;
            }

            var result = _userService.Login(args["username"], args["password"]);
            if (result.StartsWith("login successful"))
                SessionManager.Login(_userService.LoggedInUser);

            Console.WriteLine(result);
        }

        private void HandleLogout()
        {
            if (!SessionManager.IsLoggedIn)
            {
                Console.WriteLine("no user is currently logged in.");
                return;
            }

            var result = _userService.Logout();
            SessionManager.Logout();
            Console.WriteLine(result);
        }

        private void HandleChange(Dictionary<string, string> args)
        {
            if (!SessionManager.IsLoggedIn)
            {
                Console.WriteLine("access denied. please login first.");
                return;
            }

            if (!args.ContainsKey("status"))
            {
                Console.WriteLine("change status failed! missing parameters.");
                return;
            }

            var result = _userService.ChangeStatus(args["status"]);
            Console.WriteLine(result);
        }

        private void HandleChangePassword(Dictionary<string, string> args)
        {
            if (!SessionManager.IsLoggedIn)
            {
                Console.WriteLine("access denied. please login first.");
                return;
            }

            if (!args.ContainsKey("old") || !args.ContainsKey("new"))
            {
                Console.WriteLine("change password failed! missing parameters.");
                return;
            }

            var result = _userService.ChangePassword(args["old"], args["new"]);
            Console.WriteLine(result);
        }

        private void HandleSearch(Dictionary<string, string> args)
        {
            if (!SessionManager.IsLoggedIn)
            {
                Console.WriteLine("access denied. please login first.");
                return;
            }

            if (!args.ContainsKey("username"))
            {
                Console.WriteLine("search failed! missing parameters.");
                return;
            }

            var results = _userService.SearchUsers(args["username"]);
            if (results.Any())
                results.ForEach(Console.WriteLine);
            else
                Console.WriteLine("no users found.");
        }
    }
}
