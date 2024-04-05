using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для authentication.xaml
    /// </summary>
    public partial class authentication : Page
    {
        public authentication()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text;
            string password = tb_password.Text;
            if (string.IsNullOrEmpty(login) )
            {
                MessageBox.Show("");
                return;
            }
            if (string.IsNullOrEmpty(password) )
            {
                MessageBox.Show("");
                return;
            }
            string sql = " SELECT" +
                "     u.user_id," +
                "     u.first_name," +
                "     u.last_name," +
                "     u.login," +
                "     u.password," +
                "     c.client_id," +
                "     pm.manager_id," +
                "     s.specialty_id" +
                " FROM users u" +
                " LEFT JOIN clients c ON c.user_id = u.user_id" +
                " LEFT JOIN project_managers pm ON pm.user_id = u.user_id" +
                " LEFT JOIN specialists s ON s.user_id = u.user_id" +
                $" WHERE u.login = '{login}' AND u.password = '{password}'" +
                " ORDER BY u.user_id;";
            Console.WriteLine(sql);
            var r = db.query(sql);
            if (r.Read())
            {
                if (!r.IsDBNull(5))
                {
                    int id = r.GetInt32(5);
                    r.Close();
                    var t = new client(); 
                    t.clientId = id;
                    NavigationService.Navigate(t);

                    Console.WriteLine(id);
                }
                else if (!r.IsDBNull(6))
                {
                    int id = r.GetInt32(6);
                    r.Close();
                    NavigationService.Navigate(new manager());
                }
                else if (!r.IsDBNull(7))
                {
                    int id = r.GetInt32(7);
                    r.Close();
                    Console.WriteLine(id);
                }
                else
                    r.Close();
            } else
            {
                MessageBox.Show("неправильный пароль или логин");
                r.Close();
            }
        }
    }
}
