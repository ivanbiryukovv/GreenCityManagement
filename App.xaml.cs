using GreenCityManagement.Helpers;
using GreenCityManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace GreenCityManagement
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using var db = new GreenCityContext();
            db.Database.EnsureCreated();
            SeedData(db);

            var login = new LoginWindow();
            login.Show();
        }

        private static void SeedData(GreenCityContext db)
        {
            // Роли
            if (!db.Role.Any())
            {
                db.Role.AddRange(
                    new Role { Name = "Администратор",     Description = "Полный доступ ко всем функциям" },
                    new Role { Name = "Работник",          Description = "Просмотр и добавление обслуживания" },
                    new Role { Name = "Менеджер по учёту", Description = "Справочники, растения и отчёты" }
                );
                db.SaveChanges();
            }

            // Пользователь admin
            if (!db.AppUser.Any())
            {
                var adminRole = db.Role.First(r => r.Name == "Администратор");
                db.AppUser.Add(new AppUser
                {
                    Login        = "admin",
                    PasswordHash = PasswordHelper.Hash("admin123"),
                    FullName     = "Администратор системы",
                    ID_role      = adminRole.ID_role
                });
                db.SaveChanges();
            }
        }
    }
}
