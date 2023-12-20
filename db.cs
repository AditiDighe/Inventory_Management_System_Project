using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    class db
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");

        public db()
        {
            con.Open();
        }
        public int ExecuteQuery(string Query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public DataTable GetData(string Query)
        {
            DataTable table = new DataTable();
            try
            {

                SqlDataAdapter adp = new SqlDataAdapter(Query, con);
                adp.Fill(table);
            }
            catch (Exception ex)
            {

            }
            return table;

        }

    }
}
