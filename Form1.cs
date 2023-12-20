using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace InventorySystem
{
    
    public partial class Form1 : Form
    {


        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
                Passwordtxt.PasswordChar = '*';
            
            else
                Passwordtxt.PasswordChar = '\0';
            

            /*if (!string.IsNullOrWhiteSpace(Password))
            {
                return;
            }
            throw new Exception("Password should not be empty");*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserNametxt.Text = "";
            Passwordtxt.Text ="";
            UserNametxt.Clear();
            Passwordtxt.Clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String UserName, Password;

            UserName = UserNametxt.Text;
            Password = Passwordtxt.Text;

            try
            {
                if (UserNametxt.Text == "" || Passwordtxt.Text == "")
                {
                    MessageBox.Show("Please Enter The Details");
                    return;
                }

                String query = "select * from Login where UserName='"+UserNametxt.Text+"' AND Password ='"+Passwordtxt.Text+"'";
                SqlDataAdapter adp = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    UserName = UserNametxt.Text;
                    Password = Passwordtxt.Text;
                    MessageBox.Show("Login Successfully...!");
                    User obj = new User();
                    obj.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Login Details","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    UserNametxt.Clear();
                    Passwordtxt.Clear();
                }

             
               
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
            }
            UserNametxt.Clear();
            Passwordtxt.Clear();
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void UserNametxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

