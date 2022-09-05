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

namespace ShopriteManagementSystem
{
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\frankomarii\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomerTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTb1 values('" + Customerid.Text + "','" + CustomernameTb.Text + "','" + CustomerPhoneTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customers Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Customerid.Text == "")
            {
                MessageBox.Show("Enter The Customers Id");
            }
            else
            {
                Con.Open();
                string myquery = "delete from CustomerTb1 where CustId ='" + Customerid.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Customerid.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustomernameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
            CustomerPhoneTb.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTb1 set Custname='" + CustomernameTb.Text + "',CustPhone='" + CustomerPhoneTb.Text + "',where CustId='" + Customerid.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {

        }
    }
}
