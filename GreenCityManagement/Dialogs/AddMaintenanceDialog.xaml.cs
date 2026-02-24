using GreenCityManagement.Models;
using System;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddMaintenanceDialog : Window
    {
        public Maintenance Result { get; private set; }

        public AddMaintenanceDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PlantIdBox.Text, out int plantId) ||
                !int.TryParse(WorkTypeIdBox.Text, out int workTypeId) ||
                !DateTime.TryParse(DateBox.Text, out DateTime date) ||
                string.IsNullOrWhiteSpace(ResultBox.Text))
            {
                MessageBox.Show("Заполните все поля корректно."); return;
            }
            Result = new Maintenance { ID_plant = plantId, ID_work_type = workTypeId, Work_date = date, Result = ResultBox.Text };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
