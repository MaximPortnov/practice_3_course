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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace practice
{

    /// <summary>
    /// Логика взаимодействия для client.xaml
    /// </summary>
    public partial class client : Page
    {
        public int clientId = -1;
        public List<Project> projects = new List<Project>();
        public client()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            projects.Clear();
            string sql =
                "SELECT " +
                "    p.project_id,  " +
                "    p.name,  " +
                "    p.cost,  " +
                "    p.start_date,  " +
                "    p.end_date,  " +
                "    p.status,  " +
                "    pm.manager_id,  " +
                "    u1.first_name, " +
                "    u1.last_name, " +
                "    c.client_id, " +
                "    u2.first_name, " +
                "    u2.last_name " +
                "FROM projects p " +
                "JOIN project_managers pm ON p.manager_id = pm.manager_id " +
                "JOIN users u1 ON u1.user_id = pm.user_id " +
                "JOIN clients c ON p.client_id = c.client_id " +
                "JOIN users u2 ON u2.user_id = c.user_id " +
                $"WHERE c.client_id = {clientId}";
            var reader = db.query(sql);
            while (reader.Read())
            {
                projects.Add(new Project
                {
                    ProjectId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Cost = reader.GetDecimal(2),
                    StartDate = reader.GetDateTime(3),
                    EndDate = reader.GetDateTime(4),
                    Status = reader.GetString(5),
                    ManagerID = reader.GetInt32(6),
                    ManagerFirstName = reader.GetString(7),
                    ManagerLastName = reader.GetString(8),
                    CliientID = reader.GetInt32(9),
                    ClientFirstName = reader.GetString(10),
                    ClientLastName = reader.GetString(11),
                });
            }
            dataGtid.ItemsSource = null;
            dataGtid.ItemsSource = projects;
            reader.Close();
        }


        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var t = new addProject();
            t.clientID = clientId;
            t.ShowDialog();
            loadData();
        }

        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            int selected = dataGtid.SelectedIndex;
            if (selected == -1)
            {
                MessageBox.Show("выберете проект");
                return;
            }
            var t = new updProject();
            t.project_id = projects[selected].ProjectId;
            t.cost = projects[selected].Cost.ToString();
            t.startDate = projects[selected].StartDate;
            t.endDate = projects[selected].EndDate;
            t.status = projects[selected].Status;
            t.managerID = projects[selected].ManagerID;
            t.clientID = projects[selected].CliientID;
            t.init();
            t.ShowDialog();
            loadData();
        }
    }
}
