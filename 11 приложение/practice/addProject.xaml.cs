using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace practice
{
    /// <summary>
    /// Логика взаимодействия для addProject.xaml
    /// </summary>
    public partial class addProject : Window
    {
        public int clientID = -1;
        public addProject()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_name.Text;
            DateTime startDate = dp_startDate.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = dp_endDate.SelectedDate ?? DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(name) || startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string query = $"INSERT INTO projects (name, start_date, end_date, cost, status, manager_id, client_id) VALUES ('{name}', '{startDate.ToString("yyyy-MM-dd")}', '{endDate.ToString("yyyy-MM-dd")}', 0, 'for approval', NULL, {clientID})";
            Console.WriteLine(query);
            try
            {
                db.query(query).Close();
                MessageBox.Show("Проект успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_name.Text = "";
                dp_startDate.SelectedDate = null;
                dp_endDate.SelectedDate = null;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении проекта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
