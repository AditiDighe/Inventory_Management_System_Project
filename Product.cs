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
    public partial class Product : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
        

       

        public Product()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        
        void fillcategory()
        {

            string query = "select * from Categories";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName",typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                Catcombo.ValueMember = "CatName";
                Catcombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource =dt;

                
                con.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }

        void fillSearchCombo()
        {

            string query = "select * from Categories where CatName= '"+SearchCombo.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                Catcombo.ValueMember = "CatName";
                Catcombo.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }

        private void Product_Load(object sender, EventArgs e)
        {
           
            fillcategory();
            Display();

         




        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductIdtxt.Text == "")
                {
                    MessageBox.Show("Please Enter Product Id");
                }
                else 
                {
                    if (con.State==ConnectionState.Closed)
                        con.Open();
                    SqlCommand commToCheckProductId = new SqlCommand("select ProductId from Product where ProductId='"+int.Parse(ProductIdtxt.Text)+"'", con);
                    SqlDataAdapter sd = new SqlDataAdapter(commToCheckProductId);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Already Exist ProductId..!");

                    }
                   

                    else
                    {
                        con.Open();
                        string query = @"insert into [InventorySystem].[dbo].[Product] values('"+ProductIdtxt.Text+"','"+ProductNametxt.Text+"','"+ProductQtytxt.Text+"','"+ProductPricetxt.Text+"','"+Descriptiontxt.Text+"','"+Catcombo.Text+"') ";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product Added Successfully!");
                        con.Close();
                        Display();
                        FilterByCategory();
                        clearall();
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }


        }

        public void clearall()
        {
            ProductIdtxt.Clear();
            ProductNametxt.Clear();
            ProductQtytxt.Clear();
            ProductPricetxt.Clear();
            Descriptiontxt.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (ProductIdtxt.Text == "")
            {
                MessageBox.Show("Please Enter Product Id");
            }
            else
            {
                if (con.State==ConnectionState.Closed)
                    con.Open();
                string query = @"delete from [InventorySystem].[dbo].[Product] where ProductId='"+ ProductIdtxt.Text +"' ";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Deleted Successfully!");
                con.Close();
                Display();
                FilterByCategory();
                ProductIdtxt.Clear();

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductIdtxt.Text == "")
                {
                    MessageBox.Show("Please Enter Product Id");
                }
                else
                    con.Open();
                string query = @"update [InventorySystem].[dbo].[Product] set ProductName='"+ProductNametxt.Text+"',ProductQty='"+ProductQtytxt.Text+"',ProductPrice='"+ProductPricetxt.Text+"',[Description]='"+Descriptiontxt.Text+"',[ProductCategory]='"+Catcombo.Text+"' where ProductId='"+ProductIdtxt.Text+"' ";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Updated Successfully");
                con.Close();
                Display();
              //FilterByCategory();
                ProductIdtxt.Clear();
                ProductNametxt.Clear();
                ProductQtytxt.Clear();
                ProductPricetxt.Clear();
                Descriptiontxt.Clear();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }
        void FilterByCategory()
        {
            try
            {
                con.Open();
                string Query = "select * from Product where Catcombo='"+SearchCombo.SelectedValue.ToString()+"' ";
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

        private void Search_Click(object sender, EventArgs e)
        {
            FilterByCategory();
            Display();



        }
       


        private void Catcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State!=ConnectionState.Open)
                con.Open();

            string Query = "select * from product where ProductCategory='"+SearchCombo.Text+"'";
            SqlDataAdapter adp = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FilterByCategory();
        }
        void Display()
        {
            try
            {
                con.Open();
                string Query = "select * from Product order by ProductId";
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

        private void ProductIdtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void Stock_Click(object sender, EventArgs e)
        {
            Stock obj = new Stock();
            obj.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
