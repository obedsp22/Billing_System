using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing_System
{
    class Queries
    {
        static readonly string connString = "SERVER = localhost; USER = root; PASSWORD = root; DATABASE = billing_system";

        public static DataTable ExecuteQuery(string query)
        {
            DataTable table;
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand(query, conn);
                    var reader = cmd.ExecuteReader();
                    table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExecuteNonQuery(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
