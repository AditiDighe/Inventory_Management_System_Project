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
    public partial class Categories : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
        public Categories()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            //if (CatName.Text == "")
            //{
            //    MessageBox.Show("Please Enter Category Name");
            //}
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatName.Text == "")
                {
                    MessageBox.Show("Please Enter Category Name");
                }
                else
                {
                    con.Open();
                    string query = @"update [InventorySystem].[dbo].[Categories] set CatName='"+CatName.Text+"' where CatId='"+CatId.Text+"' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Categories Updated Successfully");
                    con.Close();
                    Diaplay();
                    
                    //CatName.Clear();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatId.Text == "")
                {
                    MessageBox.Show("Please Enter Category ID");
                }
                else
                con.Open();
                string query = @"delete from [InventorySystem].[dbo].[Categories] where CatId=('" + CatId.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Deleted Successfully!");
                con.Close();
                Diaplay();

            }
            catch(Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void CatId_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void Categories_Load(object sender, EventArgs e)
        {
            CatId.Text= getCatId().ToString();
        }

        public int getCatId()
        {
            int id = 0;

            string Query = "select max(catid)+1 from Categories";
            var dt = new db().GetData(Query);
            if (dt!=null&&dt.Rows.Count>0)
            {
                id = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            return id;
        }
        void Diaplay()
        {
            try
            {
                con.Open();
                string Query = "select * from Categories";
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

        private void label5_Click(object sender, EventArgs e)
        {

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (CatName.Text == "")
                {
                    MessageBox.Show("Please Enter Category Name");
                }
                else
                {
                    con.Open();
                    string query = @"insert into [InventorySystem].[dbo].[Categories] values('"+CatId.Text+"','"+CatName.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.ExecuteNonQuery();
                    CatId.Text= getCatId().ToString();
                    MessageBox.Show("Category Added Successfully!");
                    con.Close();
                    Diaplay();

                    CatName.Clear();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

