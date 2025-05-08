using UserManagementApp.Models;

namespace UserManagementApp.Core
{
    public static class SessionManager
    {
        public static User CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
