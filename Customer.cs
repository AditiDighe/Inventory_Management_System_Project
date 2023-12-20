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
    public partial class Customer : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
        public Customer()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        void Diaplay()
        {
            try
            {
                con.Open();
                string Query = "select * from Customer";
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
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            CustomerId.Text = getCustomerId().ToString();
            Diaplay();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }
        public int getCustomerId()
        {
            int id = 0;

            string Query = "select max(CustomerId)+1 from [InventorySystem].[dbo].[Customer]";
            var dt = new db().GetData(Query);
            if (dt != null && dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            return id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try

            {
                if (CustomerId.Text == "")
                {
                    MessageBox.Show("Please Enter Customer id");

                }
                else
                {
                    con.Open();
                    string query = @"insert into [InventorySystem].[dbo].[Customer] values('"+CustomerId.Text+"','"+CustomerName.Text+"','"+CustomerPhoneNo.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully!");
                    con.Close();
                    Diaplay();
                    CustomerId.Clear();
                    CustomerName.Clear();
                    CustomerPhoneNo.Clear();



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (CustomerId.Text == "")
            {
                MessageBox.Show("Please Enter Customer Id");
            }
            else
            {

                if (con.State==ConnectionState.Closed)
                    con.Open();
                string query = @"delete from [InventorySystem].[dbo].[Customer] where CustomerId=('"+ CustomerId.Text +"')";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Deleted Successfully!");
                con.Close();
                Diaplay();
                CustomerId.Clear();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (CustomerId.Text == "" || CustomerName.Text == ""  || CustomerPhoneNo.Text == "")
                {
                    MessageBox.Show("Please Enter The Values");
                }
                else
                {
                    con.Open();
                    string query = @"update [InventorySystem].[dbo].[Customer] set CustomerName='"+CustomerName.Text+"',CustomerPhoneNo='"+CustomerPhoneNo.Text+"' where CustomerId='"+CustomerId.Text+"' ";

                    //int PhoneNo = Convert.ToInt32(PhoneNo.Text);
                    //Int64 PhoneNo = Int64.Parse(PhoneNo.Text);

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully");
                    con.Close();
                    Diaplay();
                    CustomerId.Clear();
                    CustomerName.Clear();
                    CustomerPhoneNo.Clear();
                    

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }

        private void CustomerId_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }


