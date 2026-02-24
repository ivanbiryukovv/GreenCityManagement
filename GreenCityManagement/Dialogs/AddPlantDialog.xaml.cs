using GreenCityManagement.Models;
using System;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddPlantDialog : Window
    {
        public Plant Result { get; private set; }

        public AddPlantDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                !int.TryParse(PlantTypeBox.Text, out int typeId) ||
                !int.TryParse(DistrictBox.Text, out int districtId) ||
                !DateTime.TryParse(DateBox.Text, out DateTime date) ||
                string.IsNullOrWhiteSpace(StatusBox.Text))
            {
                MessageBox.Show("Заполните все поля корректно."); return;
            }
            Result = new Plant
            {
                Name = NameBox.Text,
                ID_plant_type = typeId,
                ID_district = districtId,
                Planting_date = date,
                Health_status = StatusBox.Text
            };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
