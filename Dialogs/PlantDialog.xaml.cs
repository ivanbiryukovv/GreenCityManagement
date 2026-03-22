using GreenCityManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class PlantDialog : Window
    {
        public Plant Result { get; private set; } = new();
        private readonly Plant? _edit;

        public PlantDialog(Plant? edit = null)
        {
            InitializeComponent();
            _edit = edit;
            using var db = new GreenCityContext();
            PlantTypeCombo.ItemsSource = db.PlantType.ToList();
            DistrictCombo.ItemsSource  = db.District.ToList();

            if (edit != null)
            {
                Title = "Редактировать растение";
                NameBox.Text = edit.Name;
                PlantTypeCombo.SelectedValue = edit.ID_plant_type;
                DistrictCombo.SelectedValue  = edit.ID_district;
                DatePicker.SelectedDate      = edit.Planting_date;
                // Статус
                foreach (System.Windows.Controls.ComboBoxItem item in StatusCombo.Items)
                    if (item.Content?.ToString() == edit.Health_status)
                    { StatusCombo.SelectedItem = item; break; }
            }
            else { DatePicker.SelectedDate = DateTime.Today; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                PlantTypeCombo.SelectedValue == null ||
                DistrictCombo.SelectedValue  == null ||
                DatePicker.SelectedDate      == null ||
                StatusCombo.SelectedItem     == null)
            { MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            Result = new Plant
            {
                ID_plant      = _edit?.ID_plant ?? 0,
                Name          = NameBox.Text.Trim(),
                ID_plant_type = (int)PlantTypeCombo.SelectedValue,
                ID_district   = (int)DistrictCombo.SelectedValue,
                Planting_date = DatePicker.SelectedDate!.Value,
                Health_status = ((System.Windows.Controls.ComboBoxItem)StatusCombo.SelectedItem).Content!.ToString()!
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
