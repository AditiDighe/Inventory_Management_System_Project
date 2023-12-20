using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            AboutUs obj = new AboutUs();
            obj.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Order obj = new Order();
            obj.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
             Form1 obj = new Form1();
            obj.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            User obj = new User();
            obj.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Categories obj = new Categories();
            obj.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Product obj = new Product();
            obj.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure ? This will Delete Your UNSAVED Data", "Cnfirmation Daialog!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
