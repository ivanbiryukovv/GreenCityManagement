using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCityManagement.Models
{
    public class Plant
    {
        [Key] public int ID_plant { get; set; }
        public string Name { get; set; }

        public int ID_plant_type { get; set; }
        public int ID_district { get; set; }

        public DateTime Planting_date { get; set; }
        public string Health_status { get; set; }
    }
}