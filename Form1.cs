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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection _sqlconn = new SqlConnection(@"Data Source=LAB5-PC4\SQL2016ENT;Initial Catalog=apnistore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private SqlCommand _cmd;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txt_userame.Text;
            string password = txt_password.Text;
            
            if(username != string.Empty && password != string.Empty)
            {
                try
                {
                    _sqlconn.Open();
                    _cmd = new SqlCommand("SELECT * FROM USERS WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD", _sqlconn);
                    _cmd.Parameters.AddWithValue("@USERNAME", username);
                    _cmd.Parameters.AddWithValue("@PASSWORD", password);
                    SqlDataReader _reader = _cmd.ExecuteReader();
                    if (_reader.Read())
                    {
                        this.Hide();
                        new AdminDashboard().Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials");
                    }
                }catch (Exception err){

                }finally{
                    _sqlconn.Close();
                }
            }
            else
            {
                MessageBox.Show("Don't leave any fields empty");
            }

        }
    }
}
