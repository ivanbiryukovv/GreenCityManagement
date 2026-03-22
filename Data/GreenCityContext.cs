using GreenCityManagement.Models;
using Microsoft.EntityFrameworkCore;

public class GreenCityContext : DbContext
{
    public DbSet<District>    District    { get; set; }
    public DbSet<PlantType>   PlantType   { get; set; }
    public DbSet<Plant>       Plant       { get; set; }
    public DbSet<PlantPassport> PlantPassport { get; set; }
    public DbSet<Employee>    Employee    { get; set; }
    public DbSet<WorkType>    WorkType    { get; set; }
    public DbSet<Maintenance> Maintenance { get; set; }
    public DbSet<PlantEmployee> PlantEmployee { get; set; }
    public DbSet<Role>        Role        { get; set; }
    public DbSet<AppUser>     AppUser     { get; set; }

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
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<AppUser>().ToTable("AppUser");

        // Plant -> PlantType (N:1)
        modelBuilder.Entity<Plant>()
            .HasOne(p => p.PlantType)
            .WithMany(pt => pt.Plants)
            .HasForeignKey(p => p.ID_plant_type);

        // Plant -> District (N:1)
        modelBuilder.Entity<Plant>()
            .HasOne(p => p.District)
            .WithMany(d => d.Plants)
            .HasForeignKey(p => p.ID_district);

        // PlantPassport -> Plant (1:1)
        modelBuilder.Entity<PlantPassport>()
            .HasOne(pp => pp.Plant)
            .WithOne(p => p.PlantPassport)
            .HasForeignKey<PlantPassport>(pp => pp.ID_plant);

        // Maintenance -> Plant (N:1)
        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Plant)
            .WithMany(p => p.Maintenances)
            .HasForeignKey(m => m.ID_plant);

        // Maintenance -> WorkType (N:1)
        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.WorkType)
            .WithMany(wt => wt.Maintenances)
            .HasForeignKey(m => m.ID_work_type);

        // Maintenance -> Employee (N:1)
        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Employee)
            .WithMany(e => e.Maintenances)
            .HasForeignKey(m => m.ID_employee);

        // PlantEmployee -> Plant (N:1)
        modelBuilder.Entity<PlantEmployee>()
            .HasOne(pe => pe.Plant)
            .WithMany(p => p.PlantEmployees)
            .HasForeignKey(pe => pe.ID_plant);

        // PlantEmployee -> Employee (N:1)
        modelBuilder.Entity<PlantEmployee>()
            .HasOne(pe => pe.Employee)
            .WithMany(e => e.PlantEmployees)
            .HasForeignKey(pe => pe.ID_employee);

        // AppUser -> Role (N:1)
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.ID_role);
    }
}
