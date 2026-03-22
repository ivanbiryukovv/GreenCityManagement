using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class AppUser
    {
        [Key] public int ID_user { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int ID_role { get; set; }

        public Role Role { get; set; } = null!;
    }
}
