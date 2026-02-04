using GreenCityManagement.Models;
using Microsoft.EntityFrameworkCore;

public class GreenCityContext : DbContext
{
    public DbSet<District> District { get; set; }
    public DbSet<PlantType> PlantType { get; set; }
    public DbSet<Plant> Plant { get; set; }
    public DbSet<PlantPassport> PlantPassport { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<WorkType> WorkType { get; set; }
    public DbSet<Maintenance> Maintenance { get; set; }
    public DbSet<PlantEmployee> PlantEmployee { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;Database=GreenCityManagement;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<District>().ToTable("District");
        modelBuilder.Entity<PlantType>().ToTable("PlantType");
        modelBuilder.Entity<Plant>().ToTable("Plant");
        modelBuilder.Entity<PlantPassport>().ToTable("PlantPassport");
        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<WorkType>().ToTable("WorkType");
        modelBuilder.Entity<Maintenance>().ToTable("Maintenance");
        modelBuilder.Entity<PlantEmployee>().ToTable("Plant_Employee");

        modelBuilder.Entity<Plant>()
            .HasOne(p => p.PlantType)
            .WithMany(pt => pt.Plants)
            .HasForeignKey(p => p.ID_plant_type);

        modelBuilder.Entity<Plant>()
            .HasOne(p => p.District)
            .WithMany(d => d.Plants)
            .HasForeignKey(p => p.ID_district);

        modelBuilder.Entity<PlantPassport>()
            .HasOne(pp => pp.Plant)
            .WithOne(p => p.PlantPassport)
            .HasForeignKey<PlantPassport>(pp => pp.ID_plant);

        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Plant)
            .WithMany(p => p.Maintenances)
            .HasForeignKey(m => m.ID_plant);

        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.WorkType)
            .WithMany(wt => wt.Maintenances)
            .HasForeignKey(m => m.ID_work_type);

        modelBuilder.Entity<PlantEmployee>()
            .HasOne(pe => pe.Plant)
            .WithMany(p => p.PlantEmployees)
            .HasForeignKey(pe => pe.ID_plant);

        modelBuilder.Entity<PlantEmployee>()
            .HasOne(pe => pe.Employee)
            .WithMany(e => e.PlantEmployees)
            .HasForeignKey(pe => pe.ID_employee);
    }
}

//EntityFrameworkCore\Remove-Migration
//EntityFrameworkCore\Add-Migration InitialCreate
//EntityFrameworkCore\Update-Database