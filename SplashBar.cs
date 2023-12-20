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
    public partial class SplashBar : Form
    {
        public SplashBar()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint +=1;
            progressBar.Value = startpoint;
            if(progressBar.Value == 100)
            {
                progressBar.Value = 0;
                timer1.Stop();
                Home obj = new Home();
                this.Hide();
                obj.Show();
            }
        }

        private void SplashBar_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
