using GreenCityManagement.Helpers;
using GreenCityManagement.Models;
using System.Windows;

namespace GreenCityManagement.Dialogs
{
    public partial class UserDialog : Window
    {
        public AppUser Result    { get; private set; } = new();
        public string  NewPassword { get; private set; } = string.Empty;
        private readonly AppUser? _edit;

        public UserDialog(AppUser? edit = null)
        {
            InitializeComponent();
            _edit = edit;
            using var db = new GreenCityContext();
            RoleCombo.ItemsSource = db.Role.ToList();

            if (edit != null)
            {
                Title              = "Редактировать пользователя";
                LoginBox.Text      = edit.Login;
                FullNameBox.Text   = edit.FullName;
                RoleCombo.SelectedValue = edit.ID_role;
                PasswordLabel.Text = "Новый пароль (оставьте пустым, чтобы не менять)";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text) ||
                string.IsNullOrWhiteSpace(FullNameBox.Text) ||
                RoleCombo.SelectedValue == null)
            { MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            if (_edit == null && string.IsNullOrEmpty(PasswordBox.Password))
            { MessageBox.Show("Введите пароль для нового пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            NewPassword = PasswordBox.Password;
            Result = new AppUser
            {
                ID_user      = _edit?.ID_user ?? 0,
                Login        = LoginBox.Text.Trim(),
                FullName     = FullNameBox.Text.Trim(),
                ID_role      = (int)RoleCombo.SelectedValue,
                PasswordHash = string.IsNullOrEmpty(PasswordBox.Password)
                                  ? (_edit?.PasswordHash ?? string.Empty)
                                  : PasswordHelper.Hash(PasswordBox.Password)
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
