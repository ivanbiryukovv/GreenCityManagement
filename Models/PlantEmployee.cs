using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class PlantEmployee
    {
        [Key] public int ID_plant_employee { get; set; }
        public int ID_plant { get; set; }
        public int ID_employee { get; set; }

        public Plant Plant { get; set; }
        public Employee Employee { get; set; }
    }
}
