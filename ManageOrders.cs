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
    public partial class ManageOrders : Form
    {
        public ManageOrders()
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
        void populateproducts()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        void fillcategory()
        {
            string query = "select * from CategoryTb1";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }
        int num = 0;
        int uprice, totprice, qty;
        string product;
        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateproducts();
            fillcategory();
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();

        }
         int flag = 0;
        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
           // qty = Convert.ToInt32(QtyTb.Text);
            uprice = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
            //totprice = qty * uprice;
            flag = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
                MessageBox.Show("Enter The Quantity of Products");
            else if (flag == 0) ;
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * uprice;
                flag = 0;
            }
        }

        private void SearchCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1 where ProdCat='" + SearchCombo.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
