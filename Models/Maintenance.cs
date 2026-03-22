using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class Maintenance
    {
        [Key] public int ID_maintenance { get; set; }
        public int ID_plant { get; set; }
        public int ID_work_type { get; set; }
        public int ID_employee { get; set; }
        public DateTime Work_date { get; set; }
        public string Result { get; set; } = string.Empty;

        public Plant Plant { get; set; } = null!;
        public WorkType WorkType { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}