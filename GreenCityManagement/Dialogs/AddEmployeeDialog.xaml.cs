using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class AddEmployeeDialog : Window
    {
        public Employee Result { get; private set; }

        public AddEmployeeDialog() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameBox.Text) || string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(SurnameBox.Text) || string.IsNullOrWhiteSpace(PositionBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                MessageBox.Show("Заполните все поля."); return;
            }
            Result = new Employee
            {
                LastName = LastNameBox.Text, FirstName = FirstNameBox.Text,
                Surname = SurnameBox.Text, Position = PositionBox.Text, Phone = PhoneBox.Text
            };
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
