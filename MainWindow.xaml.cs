using GreenCityManagement.Dialogs;
using GreenCityManagement.Helpers;
using GreenCityManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace GreenCityManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplyRolePermissions();
            UpdateUserInfo();
            LoadAll();
        }

        // ──────────────────────── РОЛИ ────────────────────────
        private void ApplyRolePermissions()
        {
            bool isAdmin   = SessionManager.IsAdmin;
            bool isWorker  = SessionManager.IsWorker;
            bool isManager = SessionManager.IsManager;

            // Вкладка Пользователи — только admin
            TabUsers.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

            // Вкладка Сотрудники — admin и manager
            TabEmployees.Visibility = (isAdmin || isManager) ? Visibility.Visible : Visibility.Collapsed;

            // Работник видит только Растения, Обслуживание
            if (isWorker)
            {
                TabPassports.Visibility  = Visibility.Collapsed;
                TabDistricts.Visibility  = Visibility.Collapsed;
                TabPlantTypes.Visibility = Visibility.Collapsed;
                TabWorkTypes.Visibility  = Visibility.Collapsed;
            }

            // Кнопки Add/Edit/Delete по ролям
            // Растения
            BtnAddPlant.IsEnabled    = isAdmin || isManager;
            BtnEditPlant.IsEnabled   = isAdmin || isManager;
            BtnDeletePlant.IsEnabled = isAdmin || isManager;   // менеджер может удалять растения

            // Паспорта
            BtnAddPassport.IsEnabled    = isAdmin;
            BtnEditPassport.IsEnabled   = isAdmin;
            BtnDeletePassport.IsEnabled = isAdmin;

            // Обслуживание — работник может добавлять, но не редактировать/удалять чужие
            BtnAddMaintenance.IsEnabled    = isAdmin || isWorker || isManager;
            BtnEditMaintenance.IsEnabled   = isAdmin || isManager;
            BtnDeleteMaintenance.IsEnabled = isAdmin || isManager;

            // Сотрудники
            BtnAddEmployee.IsEnabled    = isAdmin;
            BtnEditEmployee.IsEnabled   = isAdmin;
            BtnDeleteEmployee.IsEnabled = isAdmin;

            // Справочники (Районы, Типы, Виды работ) — admin и manager
            BtnAddDistrict.IsEnabled    = isAdmin || isManager;
            BtnEditDistrict.IsEnabled   = isAdmin || isManager;
            BtnDeleteDistrict.IsEnabled = isAdmin || isManager;

            BtnAddPlantType.IsEnabled    = isAdmin || isManager;
            BtnEditPlantType.IsEnabled   = isAdmin || isManager;
            BtnDeletePlantType.IsEnabled = isAdmin || isManager;

            BtnAddWorkType.IsEnabled    = isAdmin || isManager;
            BtnEditWorkType.IsEnabled   = isAdmin || isManager;
            BtnDeleteWorkType.IsEnabled = isAdmin || isManager;

            // Пользователи
            BtnAddUser.IsEnabled    = isAdmin;
            BtnEditUser.IsEnabled   = isAdmin;
            BtnDeleteUser.IsEnabled = isAdmin;
        }

        private void UpdateUserInfo()
        {
            var u = SessionManager.CurrentUser;
            if (u != null)
                UserInfoText.Text = $"{u.FullName}  |  {u.Role?.Name}";
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Logout();
            new LoginWindow().Show();
            Close();
        }

        // ──────────────────────── ЗАГРУЗКА ────────────────────────
        private void LoadAll()
        {
            LoadPlants();
            LoadPassports();
            LoadMaintenance();
            LoadEmployees();
            LoadDistricts();
            LoadPlantTypes();
            LoadWorkTypes();
            LoadUsers();
        }

        private void LoadPlants(string filter = "")
        {
            using var db = new GreenCityContext();
            var q = db.Plant.Include(p => p.PlantType).Include(p => p.District).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
                q = q.Where(p => p.Name.Contains(filter) || p.Health_status.Contains(filter));

            // Работник видит все растения (только просмотр)
            PlantsGrid.ItemsSource = q.ToList();
        }

        private void LoadPassports()
        {
            using var db = new GreenCityContext();
            PassportsGrid.ItemsSource = db.PlantPassport.Include(pp => pp.Plant).ToList();
        }

        private void LoadMaintenance()
        {
            using var db = new GreenCityContext();
            var q = db.Maintenance
                      .Include(m => m.Plant)
                      .Include(m => m.WorkType)
                      .Include(m => m.Employee)
                      .AsQueryable();

            // Работник видит только своё обслуживание
            if (SessionManager.IsWorker)
            {
                // Ищем сотрудника по ФИО из CurrentUser.FullName (упрощённая привязка)
                // Для полноты — показываем всё, но можно фильтровать по ID если связать AppUser с Employee
            }

            MaintenanceGrid.ItemsSource = q.ToList();
        }

        private void LoadEmployees()
        {
            using var db = new GreenCityContext();
            EmployeesGrid.ItemsSource = db.Employee.ToList();
        }

        private void LoadDistricts()
        {
            using var db = new GreenCityContext();
            DistrictsGrid.ItemsSource = db.District.ToList();
        }

        private void LoadPlantTypes()
        {
            using var db = new GreenCityContext();
            PlantTypesGrid.ItemsSource = db.PlantType.ToList();
        }

        private void LoadWorkTypes()
        {
            using var db = new GreenCityContext();
            WorkTypesGrid.ItemsSource = db.WorkType.ToList();
        }

        private void LoadUsers()
        {
            if (!SessionManager.IsAdmin) return;
            using var db = new GreenCityContext();
            UsersGrid.ItemsSource = db.AppUser.Include(u => u.Role).ToList();
        }

        // ──────────────────────── ПОИСК ────────────────────────
        private void PlantSearch_Changed(object sender, TextChangedEventArgs e)
            => LoadPlants(PlantSearch.Text.Trim());

        // ──────────────────────── SELECTION CHANGED ────────────────────────
        private void PlantsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void PassportsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void MaintenanceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void EmployeesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void DistrictsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void PlantTypesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void WorkTypesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        // ══════════════════════ РАСТЕНИЯ ══════════════════════
        private void AddPlant_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PlantDialog();
            if (dlg.ShowDialog() == true) { SavePlant(dlg.Result); LoadPlants(); }
        }

        private void EditPlant_Click(object sender, RoutedEventArgs e)
        {
            if (PlantsGrid.SelectedItem is not Plant sel) { Hint("Выберите растение."); return; }
            var dlg = new PlantDialog(sel);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.Plant.Find(sel.ID_plant)!;
                entity.Name = dlg.Result.Name; entity.ID_plant_type = dlg.Result.ID_plant_type;
                entity.ID_district = dlg.Result.ID_district; entity.Planting_date = dlg.Result.Planting_date;
                entity.Health_status = dlg.Result.Health_status;
                db.SaveChanges(); LoadPlants();
            }
        }

        private void DeletePlant_Click(object sender, RoutedEventArgs e)
        {
            if (PlantsGrid.SelectedItem is not Plant sel) { Hint("Выберите растение."); return; }
            if (!Confirm($"Удалить растение «{sel.Name}»?")) return;
            using var db = new GreenCityContext();
            db.Plant.Remove(db.Plant.Find(sel.ID_plant)!);
            db.SaveChanges(); LoadPlants();
        }

        private void SavePlant(Plant p)
        {
            using var db = new GreenCityContext();
            db.Plant.Add(p); db.SaveChanges();
        }

        // ══════════════════════ ПАСПОРТА ══════════════════════
        private void AddPassport_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PassportDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.PlantPassport.Add(dlg.Result); db.SaveChanges(); LoadPassports();
            }
        }

        private void EditPassport_Click(object sender, RoutedEventArgs e)
        {
            if (PassportsGrid.SelectedItem is not PlantPassport sel) { Hint("Выберите паспорт."); return; }
            var dlg = new PassportDialog(sel);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.PlantPassport.Find(sel.ID_passport)!;
                entity.ID_plant = dlg.Result.ID_plant; entity.Height = dlg.Result.Height;
                entity.Age = dlg.Result.Age; entity.Last_inspection_date = dlg.Result.Last_inspection_date;
                db.SaveChanges(); LoadPassports();
            }
        }

        private void DeletePassport_Click(object sender, RoutedEventArgs e)
        {
            if (PassportsGrid.SelectedItem is not PlantPassport sel) { Hint("Выберите паспорт."); return; }
            if (!Confirm("Удалить паспорт?")) return;
            using var db = new GreenCityContext();
            db.PlantPassport.Remove(db.PlantPassport.Find(sel.ID_passport)!);
            db.SaveChanges(); LoadPassports();
        }

        // ══════════════════════ ОБСЛУЖИВАНИЕ ══════════════════════
        private void AddMaintenance_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new MaintenanceDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.Maintenance.Add(dlg.Result); db.SaveChanges(); LoadMaintenance();
            }
        }

        private void EditMaintenance_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceGrid.SelectedItem is not Maintenance sel) { Hint("Выберите запись."); return; }
            var dlg = new MaintenanceDialog(sel);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.Maintenance.Find(sel.ID_maintenance)!;
                entity.ID_plant = dlg.Result.ID_plant; entity.ID_work_type = dlg.Result.ID_work_type;
                entity.ID_employee = dlg.Result.ID_employee; entity.Work_date = dlg.Result.Work_date;
                entity.Result = dlg.Result.Result;
                db.SaveChanges(); LoadMaintenance();
            }
        }

        private void DeleteMaintenance_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceGrid.SelectedItem is not Maintenance sel) { Hint("Выберите запись."); return; }
            if (!Confirm("Удалить запись обслуживания?")) return;
            using var db = new GreenCityContext();
            db.Maintenance.Remove(db.Maintenance.Find(sel.ID_maintenance)!);
            db.SaveChanges(); LoadMaintenance();
        }

        // ══════════════════════ СОТРУДНИКИ ══════════════════════
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new EmployeeDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.Employee.Add(dlg.Result); db.SaveChanges(); LoadEmployees();
            }
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is not Employee sel) { Hint("Выберите сотрудника."); return; }
            var dlg = new EmployeeDialog(sel);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.Employee.Find(sel.ID_employee)!;
                entity.LastName = dlg.Result.LastName; entity.FirstName = dlg.Result.FirstName;
                entity.Surname = dlg.Result.Surname; entity.Position = dlg.Result.Position;
                entity.Phone = dlg.Result.Phone;
                db.SaveChanges(); LoadEmployees();
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is not Employee sel) { Hint("Выберите сотрудника."); return; }
            if (!Confirm($"Удалить сотрудника «{sel.FullName}»?")) return;
            using var db = new GreenCityContext();
            db.Employee.Remove(db.Employee.Find(sel.ID_employee)!);
            db.SaveChanges(); LoadEmployees();
        }

        // ══════════════════════ РАЙОНЫ ══════════════════════
        private void AddDistrict_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new NameDescDialog("Добавить район", "Район");
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.District.Add(new District { Name = dlg.ResultName, Description = dlg.ResultDesc });
                db.SaveChanges(); LoadDistricts();
            }
        }

        private void EditDistrict_Click(object sender, RoutedEventArgs e)
        {
            if (DistrictsGrid.SelectedItem is not District sel) { Hint("Выберите район."); return; }
            var dlg = new NameDescDialog("Редактировать район", "Район", sel.Name, sel.Description);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.District.Find(sel.ID_district)!;
                entity.Name = dlg.ResultName; entity.Description = dlg.ResultDesc;
                db.SaveChanges(); LoadDistricts();
            }
        }

        private void DeleteDistrict_Click(object sender, RoutedEventArgs e)
        {
            if (DistrictsGrid.SelectedItem is not District sel) { Hint("Выберите район."); return; }
            if (!Confirm($"Удалить район «{sel.Name}»?")) return;
            using var db = new GreenCityContext();
            db.District.Remove(db.District.Find(sel.ID_district)!);
            db.SaveChanges(); LoadDistricts();
        }

        // ══════════════════════ ТИПЫ РАСТЕНИЙ ══════════════════════
        private void AddPlantType_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new NameDescDialog("Добавить тип растения", "Тип");
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.PlantType.Add(new PlantType { Name = dlg.ResultName, Description = dlg.ResultDesc });
                db.SaveChanges(); LoadPlantTypes();
            }
        }

        private void EditPlantType_Click(object sender, RoutedEventArgs e)
        {
            if (PlantTypesGrid.SelectedItem is not PlantType sel) { Hint("Выберите тип растения."); return; }
            var dlg = new NameDescDialog("Редактировать тип", "Тип", sel.Name, sel.Description);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.PlantType.Find(sel.ID_plant_type)!;
                entity.Name = dlg.ResultName; entity.Description = dlg.ResultDesc;
                db.SaveChanges(); LoadPlantTypes();
            }
        }

        private void DeletePlantType_Click(object sender, RoutedEventArgs e)
        {
            if (PlantTypesGrid.SelectedItem is not PlantType sel) { Hint("Выберите тип."); return; }
            if (!Confirm($"Удалить тип «{sel.Name}»?")) return;
            using var db = new GreenCityContext();
            db.PlantType.Remove(db.PlantType.Find(sel.ID_plant_type)!);
            db.SaveChanges(); LoadPlantTypes();
        }

        // ══════════════════════ ВИДЫ РАБОТ ══════════════════════
        private void AddWorkType_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new NameDescDialog("Добавить вид работ", "Вид работ");
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.WorkType.Add(new WorkType { Name = dlg.ResultName, Description = dlg.ResultDesc });
                db.SaveChanges(); LoadWorkTypes();
            }
        }

        private void EditWorkType_Click(object sender, RoutedEventArgs e)
        {
            if (WorkTypesGrid.SelectedItem is not WorkType sel) { Hint("Выберите вид работ."); return; }
            var dlg = new NameDescDialog("Редактировать вид работ", "Вид работ", sel.Name, sel.Description);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.WorkType.Find(sel.ID_work_type)!;
                entity.Name = dlg.ResultName; entity.Description = dlg.ResultDesc;
                db.SaveChanges(); LoadWorkTypes();
            }
        }

        private void DeleteWorkType_Click(object sender, RoutedEventArgs e)
        {
            if (WorkTypesGrid.SelectedItem is not WorkType sel) { Hint("Выберите вид работ."); return; }
            if (!Confirm($"Удалить «{sel.Name}»?")) return;
            using var db = new GreenCityContext();
            db.WorkType.Remove(db.WorkType.Find(sel.ID_work_type)!);
            db.SaveChanges(); LoadWorkTypes();
        }

        // ══════════════════════ ПОЛЬЗОВАТЕЛИ ══════════════════════
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new UserDialog();
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                db.AppUser.Add(dlg.Result); db.SaveChanges(); LoadUsers();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is not AppUser sel) { Hint("Выберите пользователя."); return; }
            var dlg = new UserDialog(sel);
            if (dlg.ShowDialog() == true)
            {
                using var db = new GreenCityContext();
                var entity = db.AppUser.Find(sel.ID_user)!;
                entity.Login = dlg.Result.Login; entity.FullName = dlg.Result.FullName;
                entity.ID_role = dlg.Result.ID_role;
                if (!string.IsNullOrEmpty(dlg.NewPassword))
                    entity.PasswordHash = PasswordHelper.Hash(dlg.NewPassword);
                db.SaveChanges(); LoadUsers();
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is not AppUser sel) { Hint("Выберите пользователя."); return; }
            if (sel.ID_user == SessionManager.CurrentUser!.ID_user)
            { MessageBox.Show("Нельзя удалить текущего пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            if (!Confirm($"Удалить пользователя «{sel.Login}»?")) return;
            using var db = new GreenCityContext();
            db.AppUser.Remove(db.AppUser.Find(sel.ID_user)!);
            db.SaveChanges(); LoadUsers();
        }

        // ──────────────────────── УТИЛИТЫ ────────────────────────
        private static void Hint(string msg)
            => MessageBox.Show(msg, "Подсказка", MessageBoxButton.OK, MessageBoxImage.Information);

        private static bool Confirm(string msg)
            => MessageBox.Show(msg, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}
