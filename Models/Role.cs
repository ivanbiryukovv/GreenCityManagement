using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class Role
    {
        [Key] public int ID_role { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public List<AppUser> Users { get; set; } = new();
    }
}
