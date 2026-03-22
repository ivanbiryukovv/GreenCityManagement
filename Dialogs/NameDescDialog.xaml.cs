using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class NameDescDialog : Window
    {
        public string  ResultName { get; private set; } = string.Empty;
        public string? ResultDesc { get; private set; }

        public NameDescDialog(string title, string label, string? name = null, string? desc = null)
        {
            InitializeComponent();
            Title          = title;
            LabelName.Text = $"{label} (название) *";
            NameBox.Text   = name ?? string.Empty;
            DescBox.Text   = desc ?? string.Empty;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            { MessageBox.Show("Введите название.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            ResultName = NameBox.Text.Trim();
            ResultDesc = string.IsNullOrWhiteSpace(DescBox.Text) ? null : DescBox.Text.Trim();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
