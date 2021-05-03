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
using Shop_project.Utils;

namespace Shop_project.Forms
{
    public partial class Login : Form
    {
        SqlConnection conn;
        public User user { get; private set; }
        public Login(SqlConnection connection)
        {
            user = new User();
            conn = connection;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader dataReader = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE phoneNum = @num", conn);
                cmd.Parameters.Add("@num", SqlDbType.NVarChar).Value = maskedTextBoxNum.Text;
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                user.id = Convert.ToInt32(dataReader[0]);
                user.name = Convert.ToString(dataReader[1]);
                user.lastName = Convert.ToString(dataReader[2]);
                user.phoneNum = Convert.ToString(dataReader[3]);
                user.email = Convert.ToString(dataReader[5]);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                conn.Close();
                if (DialogResult == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }
    }
}
