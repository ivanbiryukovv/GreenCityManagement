using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class Employee
    {
        [Key] public int ID_employee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }

        public List<PlantEmployee> PlantEmployees { get; set; }
    }
}