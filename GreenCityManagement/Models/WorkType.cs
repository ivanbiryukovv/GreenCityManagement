using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class WorkType
    {
        [Key] public int ID_work_type { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Maintenance> Maintenances { get; set; }
    }
}