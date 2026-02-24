using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddPlantTypeDialog : Window
    {
        public PlantType Result { get; private set; }

        public AddPlantTypeDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text)) { MessageBox.Show("Введите название."); return; }
            Result = new PlantType { Name = NameBox.Text, Description = DescriptionBox.Text };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
