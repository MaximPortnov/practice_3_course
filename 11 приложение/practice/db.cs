using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    public static class db
    {
        static string connectionString = "Host=localhost;Port=5432;Database=practice;Username=postgres;Password=jeka";
        static public NpgsqlConnection conn = null;
        static public void connection()
        {
            conn = new NpgsqlConnection(connectionString);
            conn.Open();
        }
        static public NpgsqlDataReader query(string sql)
        {
            NpgsqlDataReader reader;

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                reader = cmd.ExecuteReader();
            }
            return reader;
        }
    }
}
