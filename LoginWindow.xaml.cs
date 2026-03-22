using GreenCityManagement.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;

namespace GreenCityManagement
{
    public partial class LoginWindow : Window
    {
        public LoginWindow() => InitializeComponent();

        private void Login_Click(object sender, RoutedEventArgs e) => TryLogin();

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TryLogin();
        }

        private void TryLogin()
        {
            var login    = LoginBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ShowError("Введите логин и пароль.");
                return;
            }

            using var db = new GreenCityContext();
            var user = db.AppUser
                         .Include(u => u.Role)
                         .FirstOrDefault(u => u.Login == login);

            if (user == null || !PasswordHelper.Verify(password, user.PasswordHash))
            {
                ShowError("Неверный логин или пароль.");
                PasswordBox.Clear();
                return;
            }

            SessionManager.Login(user);
            var main = new MainWindow();
            main.Show();
            Close();
        }

        private void ShowError(string msg)
        {
            ErrorText.Text       = msg;
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}
