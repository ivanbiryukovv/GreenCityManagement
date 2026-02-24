using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddDistrictDialog : Window
    {
        public District Result { get; private set; }

        public AddDistrictDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text)) { MessageBox.Show("Введите название."); return; }
            Result = new District { Name = NameBox.Text, Description = DescriptionBox.Text };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
