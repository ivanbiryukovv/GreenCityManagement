using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class PlantType
    {
        [Key] public int ID_plant_type { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Plant> Plants { get; set; }
    }
}