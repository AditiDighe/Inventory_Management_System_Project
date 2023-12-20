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
using OfficeOpenXml;
using System.IO;



namespace InventorySystem
{
    public partial class Order : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=Aditi-PC\MSSQLSERVER01;Initial Catalog=InventorySystem;Integrated Security=True");

        public Order()
        {
            InitializeComponent();
        }

        int no = 0;
        int price, totalprice, quantity;
        string product;


        private void Order_Load(object sender, EventArgs e)
        {
            OrderIdtxt.Text= getOrderId().ToString();
            DisplayProduct();
            DisplayOrder();
            fillcategory();
            DiaplayCustomer();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(con.State!=ConnectionState.Open)
            con.Open();

            string Query = "select * from product where ProductCategory='"+CatCombo.Text+"'";
            SqlDataAdapter adp = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView2.DataSource = dt;

            con.Close();
        }



        public int getOrderId()
        {
            int id = 0;

            string Query = "select max(OrderId)+1 from [InventorySystem].[dbo].[Order]";
            var dt = new db().GetData(Query);
            if (dt != null && dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            return id;
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OrderIdtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            

        }
        void DisplayProduct()
        {
            try
            {
                con.Open();
                string Query = "select * from product";
                SqlDataAdapter adp = new SqlDataAdapter(Query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);
            }

        }
        void DisplayOrder()
        {
            try
            {
                con.Open();
                string Query = "select * from Order";
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




        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void UpdateProduct()
        {
            
           // int id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            int newQty = stock - Convert.ToInt32(Qtytxt.Text);
            if (newQty < 0)
                MessageBox.Show("Operation Failed");
            else
            {
                con.Open();
                string query = "update Product set ProductQty="+newQty+" where ProductId ="+product+"";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayProduct();
            }
        }








        int flag = 0;
        int stock;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            

             product =  dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString(); //dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            quantity = Convert.ToInt32(Qtytxt.Text);
            stock = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
            price = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString());
            totalprice = quantity * price;
            flag = 1;
        }
        int sum;
        private void button1_Click_1(object sender, EventArgs e)
        {

            if (Qtytxt.Text =="")
                MessageBox.Show("Enter The Quantity of Products");
            else if (flag == 0)
                MessageBox.Show("Select The Product");
            else if (Convert.ToInt32(Qtytxt.Text) > stock)
                MessageBox.Show("No Enough Stock Available");

            else
            {
                no = no +1;
                quantity = Convert.ToInt32(Qtytxt.Text);
                totalprice = quantity * price;
                DataTable dt = new DataTable();
                dt.Columns.Add("no");
                dt.Columns.Add("product");
                dt.Columns.Add("quantity");
                dt.Columns.Add("price");
                dt.Columns.Add("totalprice");
                //dt.Columns.Add("ID");
                dt.Rows.Add(no, product, quantity, price, totalprice);
                dataGridView1.DataSource = dt;
                flag = 0;
            }
            sum = sum + totalprice;
                TotalAmount.Text = "Rs"+ sum.ToString();
            UpdateProduct();

        }

        private void InsertOrder_Click(object sender, EventArgs e)
        {
            if(OrderIdtxt.Text == "" || TotalAmount.Text =="" )
            {
                MessageBox.Show("Fill The Data Correctly..");
            }
            else
            {
                if(con.State!=ConnectionState.Open)
                con.Open();
                string Query = @"insert into [InventorySystem].[dbo].[Order] values('"+OrderIdtxt.Text+"','"+OrderDatetxt.Text+"','"+TotalAmount.Text.Replace("Rs", "").Trim()+"','"+CustomerId.Text+"','"+CustomerName.Text+"')";
                SqlCommand cmd = new SqlCommand(Query);
                cmd.Connection=con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully..!");
                con.Close();
                DisplayOrder();




            }

        }

        private void ViewOrder_Click(object sender, EventArgs e)
        {
            ViewOrder View = new ViewOrder();
            View.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        void fillcategory()
        {

            string query = "select CatName from Categories";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                if(con.State==ConnectionState.Closed)
                con.Open();

                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.Items.Clear();
                CatCombo.Items.Add("Select Category");
           //     CatCombo.ValueMember = "CatName";
             //   CatCombo.DataSource = dt;
               // CatCombo.DisplayMember ="CatName";
               foreach(DataRow v in dt.Rows)
                {
                    CatCombo.Items.Add(Convert.ToString(v["CatName"]));
                }
                CatCombo.SelectedIndex=0;
                //SearchCombo.ValueMember= "CatName";
              //  SearchCombo.DataSource= dt;
                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);

            }
        }
        int rowIndex = 0;
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerId.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            CustomerName.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {


                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                if (File.Exists(@"C:\New.xlsx"))
                {
                    File.Delete(@"C:\New.xlsx");
                }


                var ExcelPkg = new ExcelPackage(new FileInfo(@"C:\New.xlsx"));
                ExcelWorksheet wsSheet1 = null;
                //ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");
                //ExcelWorksheet wssheet2 = ExcelPkg.Workbook.Worksheets.Add("Sheet 2");


                string Query = @"select * from  [InventorySystem].[dbo].[Order]";
            string OrderDate = DateTime.Now.ToString("yyyy-MM-dd")+".OrderDatetxt";
           
            DataSet ds = new DataSet();
                // Database db = new Database();
                var dt = getData(Query);
                dt.TableName = "Order";
                ds.Tables.Add(dt);



                foreach (DataTable table in ds.Tables)
                {
                    wsSheet1 = ExcelPkg.Workbook.Worksheets.Add(table.TableName);



                    int rownumber = 1;
                    if (table != null && table.Rows.Count > 0)
                    {
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            wsSheet1.Cells[rownumber, k + 1].Value = table.Columns[k].ColumnName;


                        }

                        rownumber++;

                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            for (int k = 0; k < table.Columns.Count; k++)
                            {
                                wsSheet1.Cells[rownumber, k + 1].Value = table.Rows[i][k];

                            }
                            rownumber++;
                        }

                    }

                    wsSheet1.Protection.IsProtected = false;
                    wsSheet1.Protection.AllowSelectLockedCells = false;
                    ExcelPkg.Save();
                }
            }
        

        public DataTable getData(string Query)
        {
            DataTable dt = new DataTable();

            {
                //con.Open();
                var cmd = new SqlCommand(Query, con);
                var adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Close();
               
            }
            return dt;
        }

        public int EecuteQuery(string Query)
        {

            {
                con.Open();
                var cmd = new SqlCommand(Query, con);
                return cmd.ExecuteNonQuery();
            }
        }

        private void OrderDatetxt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CatCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Query = "select * from Categories where CatName='"+CatCombo.Text.ToString()+"'";
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
        void DiaplayCustomer()
        {
            try
            {
                con.Open();
                string Query = "select * from Customer";
                SqlDataAdapter adp = new SqlDataAdapter(Query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView3.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("db>> ExecuteNonQuery " + ex.Message);
            }

        }
    }
}
