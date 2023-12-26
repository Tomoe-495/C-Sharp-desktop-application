using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GrandMasala
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private SqlConnection _sqlconn = new SqlConnection(@"Data Source=LAB5-PC4\SQL2016ENT;Initial Catalog=apnistore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private SqlCommand _cmd;


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminDashboard().Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string productName = txt_productname.Text;
            string productPrice = txt_price.Text;
            string productquantity = txt_quantity.Text;
            int cat = comboBox1.SelectedIndex;

            if(productName != string.Empty && productPrice != string.Empty && productquantity != string.Empty)
            {
                try
                {
                    _sqlconn.Open();
                    _cmd = new SqlCommand("INSERT INTO PRODUCT (NAME, PRICE, QUANTITY, CATID) VALUES(@NAME, @PRICE, @QUANTITY, @CATID)", _sqlconn);
                    _cmd.Parameters.AddWithValue("@NAME", productName);
                    _cmd.Parameters.AddWithValue("@PRICE", productPrice);
                    _cmd.Parameters.AddWithValue("@QUANTITY", productquantity);
                    _cmd.Parameters.AddWithValue("@CATID", cat+1);
                    _cmd.ExecuteNonQuery();

                    MessageBox.Show("Product has been added");
                    txt_productname.Text = "";
                    txt_price.Text = "";
                    txt_quantity.Text = "";

                }catch(Exception err)
                {
                    MessageBox.Show("A product with this name already exists");
                }
                finally
                {
                    _sqlconn.Close();
                }
            }
            else
            {
                MessageBox.Show("Don't leave any fields empty");
            }

        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            try
            {
                _sqlconn.Open();
                _cmd = new SqlCommand("SELECT * FROM CATEGORY", _sqlconn);
                SqlDataReader _reader = _cmd.ExecuteReader();
                while (_reader.Read())
                {
                    comboBox1.Items.Add(_reader.GetString(1));
                }
                comboBox1.SelectedIndex = 0;
            }catch(Exception err)
            {

            }
            finally
            {
                _sqlconn.Close();
            }
        }
    }
}
