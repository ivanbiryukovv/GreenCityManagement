using ClosedXML.Excel;
using GreenCityManagement.Models;
using Microsoft.Win32;

namespace GreenCityManagement.Helpers
{
    public static class ExcelExporter
    {
        public static void ExportPlants(IEnumerable<Plant> plants)
        {
            string? path = PickSavePath("Растения");
            if (path == null) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Растения");

            string[] headers = { "ID", "Название", "Тип растения", "Район", "Дата посадки", "Состояние" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(1, i + 1);
                cell.Value = headers[i];
                StyleHeader(cell);
            }

            int row = 2;
            foreach (var p in plants)
            {
                ws.Cell(row, 1).Value = p.ID_plant;
                ws.Cell(row, 2).Value = p.Name;
                ws.Cell(row, 3).Value = p.PlantType?.Name ?? "";
                ws.Cell(row, 4).Value = p.District?.Name ?? "";
                ws.Cell(row, 5).Value = p.Planting_date.ToString("dd.MM.yyyy");
                ws.Cell(row, 6).Value = p.Health_status;
                row++;
            }

            ws.Columns().AdjustToContents();
            wb.SaveAs(path);
            OpenFile(path);
        }

        public static void ExportEmployees(IEnumerable<Employee> employees)
        {
            string? path = PickSavePath("Сотрудники");
            if (path == null) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Сотрудники");

            string[] headers = { "ID", "Фамилия", "Имя", "Отчество", "Должность", "Телефон" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(1, i + 1);
                cell.Value = headers[i];
                StyleHeader(cell);
            }

            int row = 2;
            foreach (var e in employees)
            {
                ws.Cell(row, 1).Value = e.ID_employee;
                ws.Cell(row, 2).Value = e.LastName;
                ws.Cell(row, 3).Value = e.FirstName;
                ws.Cell(row, 4).Value = e.Surname;
                ws.Cell(row, 5).Value = e.Position;
                ws.Cell(row, 6).Value = e.Phone;
                row++;
            }

            ws.Columns().AdjustToContents();
            wb.SaveAs(path);
            OpenFile(path);
        }

        public static void ExportMaintenance(IEnumerable<Maintenance> records)
        {
            string? path = PickSavePath("Обслуживание");
            if (path == null) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Обслуживание");

            string[] headers = { "ID", "Растение", "Вид работы", "Сотрудник", "Дата", "Результат" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(1, i + 1);
                cell.Value = headers[i];
                StyleHeader(cell);
            }

            int row = 2;
            foreach (var m in records)
            {
                ws.Cell(row, 1).Value = m.ID_maintenance;
                ws.Cell(row, 2).Value = m.Plant?.Name ?? "";
                ws.Cell(row, 3).Value = m.WorkType?.Name ?? "";
                ws.Cell(row, 4).Value = m.Employee?.FullName ?? "";
                ws.Cell(row, 5).Value = m.Work_date.ToString("dd.MM.yyyy");
                ws.Cell(row, 6).Value = m.Result;
                row++;
            }

            ws.Columns().AdjustToContents();
            wb.SaveAs(path);
            OpenFile(path);
        }

        private static void StyleHeader(IXLCell cell)
        {
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#4CAF50");
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        }

        private static string? PickSavePath(string defaultName)
        {
            var dlg = new SaveFileDialog
            {
                Title = $"Сохранить «{defaultName}» в Excel",
                FileName = $"{defaultName}_{DateTime.Now:yyyy-MM-dd}",
                DefaultExt = ".xlsx",
                Filter = "Excel файл (*.xlsx)|*.xlsx"
            };
            return dlg.ShowDialog() == true ? dlg.FileName : null;
        }

        private static void OpenFile(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path)
                {
                    UseShellExecute = true
                });
            }
            catch { }
        }
    }
}
