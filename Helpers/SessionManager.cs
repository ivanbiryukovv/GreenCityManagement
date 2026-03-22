using GreenCityManagement.Models;

namespace GreenCityManagement.Helpers
{
    public static class SessionManager
    {
        public static AppUser? CurrentUser { get; private set; }
        public static string   RoleName    => CurrentUser?.Role?.Name ?? "";

        public static bool IsAdmin   => RoleName == "Администратор";
        public static bool IsWorker  => RoleName == "Работник";
        public static bool IsManager => RoleName == "Менеджер по учёту";

        public static void Login(AppUser user)  => CurrentUser = user;
        public static void Logout()             => CurrentUser = null;
    }
}
