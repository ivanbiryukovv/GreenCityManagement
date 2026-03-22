using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class District
    {
        [Key] public int ID_district { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Plant> Plants { get; set; }
    }
}