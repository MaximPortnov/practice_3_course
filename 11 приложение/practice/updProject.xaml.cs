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
    /// Логика взаимодействия для updProject.xaml
    /// </summary>
    public partial class updProject : Window
    {
        public int project_id = -1;
        public string cost;
        public DateTime startDate;
        public DateTime endDate;
        public string status;
        public int managerID;
        public int clientID;

        public updProject()
        {
            InitializeComponent();
        }
        public void init()
        {
            tb_cost.Text = cost;
            dp_startDate.SelectedDate = startDate;
            dp_endDate.SelectedDate = endDate;
            tb_status.Text = status;
            loadData();
        }

        private void loadData()
        {

            string managerQuery = "SELECT pm.manager_id, u.first_name, u.last_name FROM users u INNER JOIN project_managers pm ON u.user_id = pm.user_id";
            string clientQuery = "SELECT c.client_id, u.first_name, u.last_name FROM users u INNER JOIN clients c ON u.user_id = c.user_id";

            var managerDataReader = db.query(managerQuery);
            while (managerDataReader.Read())
            {
                int managerId = managerDataReader.GetInt32(0);
                string managerName = $"{managerDataReader.GetString(1)} {managerDataReader.GetString(2)}";

                cb_manager.Items.Add(new ComboBoxItem { Content = managerName, Tag = managerId });
                if (managerId == managerID)
                {
                    cb_manager.SelectedIndex = cb_manager.Items.Count - 1;
                }
            }
            managerDataReader.Close();

            var clientDataReader = db.query(clientQuery);
            while (clientDataReader.Read())
            {
                int clientId = clientDataReader.GetInt32(0);
                string clientName = $"{clientDataReader.GetString(1)} {clientDataReader.GetString(2)}";
                cb_client.Items.Add(new ComboBoxItem { Content = clientName, Tag = clientId });
                if (clientId == clientID)
                {
                    cb_client.SelectedIndex = cb_client.Items.Count - 1;
                }
            }
            clientDataReader.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cost = tb_cost.Text;
            DateTime startDate = dp_startDate.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = dp_endDate.SelectedDate ?? DateTime.MinValue;
            string status = tb_status.Text;
            int manager_id = (int)((ComboBoxItem)cb_manager.SelectedItem).Tag ;
            string manager = cb_manager.SelectedItem?.ToString();
            int client_id = (int)((ComboBoxItem)cb_client.SelectedItem).Tag ;
            string client = cb_client.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(cost) || startDate == DateTime.MinValue || endDate == DateTime.MinValue || string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(manager) || string.IsNullOrWhiteSpace(client))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
            float temp = 0;
            if (!float.TryParse(cost, out temp))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (startDate >= endDate)
            {
                MessageBox.Show("Дата начала проекта должна быть раньше даты окончания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
            string updateQuery = $"UPDATE projects SET cost = {cost}, start_date = '{startDate.ToString("yyyy-MM-dd")}', end_date = '{endDate.ToString("yyyy-MM-dd")}', status = '{status}', manager_id = {manager_id}, client_id = {client_id} WHERE project_id = {project_id}";
            Console.WriteLine(updateQuery );
            try
            {
                db.query(updateQuery).Close();
                MessageBox.Show("Запись успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
