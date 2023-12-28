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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private SqlConnection _sqlconn = new SqlConnection(@"Data Source=LAB5-PC4\SQL2016ENT;Initial Catalog=apnistore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private SqlCommand _cmd;

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        public void LoadProducts()
        {
            try
            {
                _sqlconn.Open();
                _cmd = new SqlCommand("SELECT P.ID, P.NAME, P.PRICE, P.QUANTITY, C.NAME FROM PRODUCT p  INNER JOIN CATEGORY c ON P.CATID = C.ID", _sqlconn);
                SqlDataReader _reader = _cmd.ExecuteReader();

                // 0 p.id
                // 1 p.name
                // 2 p.price
                // 3 p.quantity
                // 4 c.name
                while (_reader.Read())
                {
                    ProductView.Rows.Add(new object[] { 
                        _reader.GetInt32(0), 
                        _reader.GetString(1), 
                        _reader.GetDouble(2), 
                        _reader.GetInt32(3), 
                        _reader.GetString(4) 
                    });
                }

            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                _sqlconn.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddCategory().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddProduct().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ProductView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _sqlconn.Open();
                _cmd = new SqlCommand("UPDATE PRODUCT SET NAME = @NAME, PRICE = @PRICE, QUANTITY = @QUANTITY WHERE ID = @ID", _sqlconn);
                _cmd.Parameters.AddWithValue("@ID", GetCell(e.RowIndex, 0));
                _cmd.Parameters.AddWithValue("@NAME", GetCell(e.RowIndex, 1));
                _cmd.Parameters.AddWithValue("@PRICE", GetCell(e.RowIndex, 2));
                _cmd.Parameters.AddWithValue("@QUANTITY", GetCell(e.RowIndex, 3));
                _cmd.ExecuteNonQuery();

                MessageBox.Show("Data Been updated");

            }
            catch(Exception err) 
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                _sqlconn.Close();
            }
        }
        public string GetCell(int row, int column)
        {
            return ProductView.Rows[row].Cells[column].Value.ToString();
        }

    }
}
