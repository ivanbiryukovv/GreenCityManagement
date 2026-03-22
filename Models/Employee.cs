using System.ComponentModel.DataAnnotations;

namespace GreenCityManagement.Models
{
    public class Employee
    {
        [Key] public int ID_employee { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public string Surname   { get; set; } = string.Empty;
        public string Position  { get; set; } = string.Empty;
        public string Phone     { get; set; } = string.Empty;

        public List<PlantEmployee> PlantEmployees { get; set; } = new();
        public List<Maintenance>   Maintenances   { get; set; } = new();

        public string FullName => $"{LastName} {FirstName} {Surname}";
    }
}
