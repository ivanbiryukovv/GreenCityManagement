using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class EmployeeDialog : Window
    {
        public Employee Result { get; private set; } = new();
        private readonly Employee? _edit;

        public EmployeeDialog(Employee? edit = null)
        {
            InitializeComponent();
            _edit = edit;
            if (edit != null)
            {
                Title = "Редактировать сотрудника";
                LastNameBox.Text  = edit.LastName;
                FirstNameBox.Text = edit.FirstName;
                SurnameBox.Text   = edit.Surname;
                PositionBox.Text  = edit.Position;
                PhoneBox.Text     = edit.Phone;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameBox.Text) || string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(SurnameBox.Text)  || string.IsNullOrWhiteSpace(PositionBox.Text)  ||
                string.IsNullOrWhiteSpace(PhoneBox.Text))
            { MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            Result = new Employee
            {
                ID_employee = _edit?.ID_employee ?? 0,
                LastName    = LastNameBox.Text.Trim(),
                FirstName   = FirstNameBox.Text.Trim(),
                Surname     = SurnameBox.Text.Trim(),
                Position    = PositionBox.Text.Trim(),
                Phone       = PhoneBox.Text.Trim()
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
