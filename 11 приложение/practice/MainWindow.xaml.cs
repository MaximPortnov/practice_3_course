using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Npgsql;

namespace practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new authentication());


            db.connection();

            var sql = "SELECT * FROM \"users\"";

            using (var reader = db.query(sql))
            {
                while (reader.Read())
                {
                    Console.WriteLine($"User ID: {reader.GetInt32(0)}, First Name: {reader.GetString(1)}, Last Name: {reader.GetString(2)}, Login: {reader.GetString(3)}, Password: {reader.GetString(4)}");
                }
                reader.Close();
            }
            
        }
    }
}
