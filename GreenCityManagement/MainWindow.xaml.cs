using GreenCityManagement.Dialogs;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace GreenCityManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadAll();
        }

        private void LoadAll()
        {
            using var db = new GreenCityContext();
            PlantsGrid.ItemsSource        = db.Plant.ToList();
            DistrictsGrid.ItemsSource     = db.District.ToList();
            PlantTypesGrid.ItemsSource    = db.PlantType.ToList();
            PassportsGrid.ItemsSource     = db.PlantPassport.ToList();
            EmployeesGrid.ItemsSource     = db.Employee.ToList();
            WorkTypesGrid.ItemsSource     = db.WorkType.ToList();
            MaintenanceGrid.ItemsSource   = db.Maintenance.ToList();
            PlantEmployeeGrid.ItemsSource = db.PlantEmployee.ToList();
        }

        private void AddPlant_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddPlantDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.Plant.Add(dlg.Result);
                db.SaveChanges();
                PlantsGrid.ItemsSource = db.Plant.ToList();
            }
        }

        private void AddDistrict_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddDistrictDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.District.Add(dlg.Result);
                db.SaveChanges();
                DistrictsGrid.ItemsSource = db.District.ToList();
            }
        }

        private void AddPlantType_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddPlantTypeDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.PlantType.Add(dlg.Result);
                db.SaveChanges();
                PlantTypesGrid.ItemsSource = db.PlantType.ToList();
            }
        }

        private void AddPassport_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddPlantPassportDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.PlantPassport.Add(dlg.Result);
                db.SaveChanges();
                PassportsGrid.ItemsSource = db.PlantPassport.ToList();
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddEmployeeDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.Employee.Add(dlg.Result);
                db.SaveChanges();
                EmployeesGrid.ItemsSource = db.Employee.ToList();
            }
        }

        private void AddWorkType_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddWorkTypeDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.WorkType.Add(dlg.Result);
                db.SaveChanges();
                WorkTypesGrid.ItemsSource = db.WorkType.ToList();
            }
        }

        private void AddMaintenance_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddMaintenanceDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.Maintenance.Add(dlg.Result);
                db.SaveChanges();
                MaintenanceGrid.ItemsSource = db.Maintenance.ToList();
            }
        }

        private void AddPlantEmployee_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddPlantEmployeeDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.PlantEmployee.Add(dlg.Result);
                db.SaveChanges();
                PlantEmployeeGrid.ItemsSource = db.PlantEmployee.ToList();
            }
        }
    }
}
