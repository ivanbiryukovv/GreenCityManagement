using GreenCityManagement.Models;
using System;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddPlantPassportDialog : Window
    {
        public PlantPassport Result { get; private set; }

        public AddPlantPassportDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PlantIdBox.Text, out int plantId) ||
                !decimal.TryParse(HeightBox.Text, out decimal height) ||
                !int.TryParse(AgeBox.Text, out int age) ||
                !DateTime.TryParse(DateBox.Text, out DateTime date))
            {
                MessageBox.Show("Заполните все поля корректно."); return;
            }
            Result = new PlantPassport { ID_plant = plantId, Height = height, Age = age, Last_inspection_date = date };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
