using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class MaintenanceDialog : Window
    {
        public Maintenance Result { get; private set; } = new();
        private readonly Maintenance? _edit;

        public MaintenanceDialog(Maintenance? edit = null)
        {
            InitializeComponent();
            _edit = edit;
            using var db = new GreenCityContext();
            PlantCombo.ItemsSource    = db.Plant.ToList();
            WorkTypeCombo.ItemsSource = db.WorkType.ToList();
            EmployeeCombo.ItemsSource = db.Employee.ToList();

            if (edit != null)
            {
                Title = "Редактировать обслуживание";
                PlantCombo.SelectedValue    = edit.ID_plant;
                WorkTypeCombo.SelectedValue = edit.ID_work_type;
                EmployeeCombo.SelectedValue = edit.ID_employee;
                DatePicker.SelectedDate     = edit.Work_date;
                ResultBox.Text              = edit.Result;
            }
            else { DatePicker.SelectedDate = DateTime.Today; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (PlantCombo.SelectedValue    == null ||
                WorkTypeCombo.SelectedValue == null ||
                EmployeeCombo.SelectedValue == null ||
                DatePicker.SelectedDate     == null ||
                string.IsNullOrWhiteSpace(ResultBox.Text))
            { MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            Result = new Maintenance
            {
                ID_maintenance = _edit?.ID_maintenance ?? 0,
                ID_plant       = (int)PlantCombo.SelectedValue,
                ID_work_type   = (int)WorkTypeCombo.SelectedValue,
                ID_employee    = (int)EmployeeCombo.SelectedValue,
                Work_date      = DatePicker.SelectedDate!.Value,
                Result         = ResultBox.Text.Trim()
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
