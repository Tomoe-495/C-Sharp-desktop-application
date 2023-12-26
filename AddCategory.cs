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
    public partial class AddCategory : Form
    {
        public AddCategory()
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

        private void button2_Click(object sender, EventArgs e)
        {
            string catname = txt_catname.Text;
            if(catname != string.Empty)
            {
                try
                {
                    _sqlconn.Open();
                    _cmd = new SqlCommand("INSERT INTO CATEGORY(NAME) VALUES(@NAME)", _sqlconn);
                    _cmd.Parameters.AddWithValue("@NAME", catname);
                    _cmd.ExecuteNonQuery();

                    MessageBox.Show("Category has been added");
                    txt_catname.Text = "";

                }catch(Exception err)
                {
                    MessageBox.Show("This category already exists");
                }
                finally
                {
                    _sqlconn.Close();
                }
            }
            else
            {
                MessageBox.Show("Enter a Category name");
            }
        }

        private void AddCategory_Load(object sender, EventArgs e)
        {

        }
    }
}
