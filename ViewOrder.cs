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
    public partial class ViewOrder : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
        public ViewOrder()
        {
            InitializeComponent();
        }

        int rowIndex = 0;
        void DisplayOrder()
        {
            try
            {
                con.Open();
                string Query = "select * from [InventorySystem].[dbo].[Order]";
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

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {
            DisplayOrder();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Order Summary", new Font("Century", 25 , FontStyle.Bold) , Brushes.Red,new Point(230) ) ;
            e.Graphics.DrawString("Order Id:"+dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230, 100));
           e.Graphics.DrawString("Order Date:"+dataGridView1.Rows[rowIndex].Cells[1].Value.ToString(), new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230, 130));
            e.Graphics.DrawString("Total Amount:"+dataGridView1.Rows[rowIndex].Cells[2].Value.ToString(), new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230, 160));
            e.Graphics.DrawString("CustomerId:"+dataGridView1.Rows[rowIndex].Cells[3].Value.ToString(), new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230, 200));
            e.Graphics.DrawString("CustomerName:"+dataGridView1.Rows[rowIndex].Cells[4].Value.ToString(), new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230, 230));
            e.Graphics.DrawString("---------!---------", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(260,260));
        }
    }
}
