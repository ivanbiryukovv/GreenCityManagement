using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddPlantEmployeeDialog : Window
    {
        public PlantEmployee Result { get; private set; }

        public AddPlantEmployeeDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PlantIdBox.Text, out int plantId) ||
                !int.TryParse(EmployeeIdBox.Text, out int employeeId))
            {
                MessageBox.Show("Введите корректные ID."); return;
            }
            Result = new PlantEmployee { ID_plant = plantId, ID_employee = employeeId };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
