using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCityManagement.Models
{
    public class PlantPassport
    {
        [Key] public int ID_passport { get; set; }
        public int ID_plant { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Height { get; set; }

        public int Age { get; set; }
        public DateTime Last_inspection_date { get; set; }

        public Plant Plant { get; set; }
    }
}