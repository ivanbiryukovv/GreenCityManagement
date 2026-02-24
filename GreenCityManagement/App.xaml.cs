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
            db.Database.Migrate();
        }
    }
}