using Microsoft.EntityFrameworkCore;
using GreenCityManagement.Models;


public class GreenCityContext : DbContext
{
    public DbSet<District> Districts { get; set; }
    public DbSet<PlantType> PlantTypes { get; set; }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<PlantPassport> PlantPassports { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<WorkType> WorkTypes { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<PlantEmployee> PlantEmployees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-5VRI3V4\\SQLEXPRESS;Database=GreenCityManagement;Trusted_Connection=True;TrustServerCertificate=True");
    }
}
