using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class Stock : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {

        }
        void StockDisplay()
        {
            try
            {
                con.Open();
                string Query = "select * from Product";
                SqlDataAdapter adp = new SqlDataAdapter(Query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);
            }

        }


        private void OutOfStock_Click(object sender, EventArgs e)
        {


            try
            {
                con.Open();
                string Query = "select * from Product where ProductQty < 10 ";
                SqlDataAdapter adp = new SqlDataAdapter(Query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StockDisplay();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    }

    