using GreenCityManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class PassportDialog : Window
    {
        public PlantPassport Result { get; private set; } = new();
        private readonly PlantPassport? _edit;

        public PassportDialog(PlantPassport? edit = null)
        {
            InitializeComponent();
            _edit = edit;
            using var db = new GreenCityContext();
            PlantCombo.ItemsSource = db.Plant.ToList();

            if (edit != null)
            {
                Title = "Редактировать паспорт";
                PlantCombo.SelectedValue = edit.ID_plant;
                HeightBox.Text = edit.Height.ToString("F2");
                AgeBox.Text = edit.Age.ToString();
                DatePicker.SelectedDate = edit.Last_inspection_date;
            }
            else { DatePicker.SelectedDate = DateTime.Today; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (PlantCombo.SelectedValue == null ||
                !decimal.TryParse(HeightBox.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal h) ||
                !int.TryParse(AgeBox.Text, out int age) ||
                DatePicker.SelectedDate == null)
            { MessageBox.Show("Заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            Result = new PlantPassport
            {
                ID_passport = _edit?.ID_passport ?? 0,
                ID_plant = (int)PlantCombo.SelectedValue,
                Height = h, Age = age,
                Last_inspection_date = DatePicker.SelectedDate!.Value
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
