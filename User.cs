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
    public partial class User : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
       

        public User()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            

        }

        private void User_Load(object sender, EventArgs e)
        {
           
            Display();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        void Display()
        {
            try
            {
                con.Open();
                string Query = "select * from [InventorySystem].[dbo].[User]";
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

        private void button1_Click(object sender, EventArgs e)
        {
            try

            {
                if (UserName.Text == "")
                {
                    MessageBox.Show("Please Enter User Name");
         
                }
                else
                {
                    con.Open();
                    string query = @"insert into [InventorySystem].[dbo].[User] values('"+UserName.Text+"','"+Password.Text+"','"+PhoneNotxt.Text+"','"+Email.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added Successfully!");
                    con.Close();
                    Display();
                    UserName.Clear();
                    Password.Clear();
                    PhoneNotxt.Clear();
                    Email.Clear();
                   

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UserName.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Password.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            PhoneNotxt.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Email.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UserName.Text == "")
            {
                MessageBox.Show("Please Enter UserName");
            }
            else
            {
                con.Open();
                string query = @"delete [InventorySystem].[dbo].[User] where UserName=('"+ UserName.Text +"')";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User Deleted Successfully!");
                con.Close();
                Display();
                UserName.Clear();
                Password.Clear();
                PhoneNotxt.Clear();
                Email.Clear();


            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserName.Text == "")
                {
                    MessageBox.Show("Please Enter Values");
                }
                else
                {
                    con.Open();
                    string query = @"update [InventorySystem].[dbo].[User] set Password='"+Password.Text+"',PhoneNo='"+PhoneNotxt.Text+"',Email='"+Email.Text+"' where UserName='"+UserName.Text+"' ";

                    //int PhoneNo = Convert.ToInt32(PhoneNo.Text);
                    //Int64 PhoneNo = Int64.Parse(PhoneNo.Text);

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully");
                    con.Close();
                    Display();
                    UserName.Clear();
                    Password.Clear();
                    PhoneNotxt.Clear();
                    Email.Clear();

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
