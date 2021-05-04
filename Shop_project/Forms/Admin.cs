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

namespace Shop_project.Forms
{
    public partial class Admin : Form
    {
        SqlConnection conn;
        public Admin(SqlConnection connection)
        {
            conn = connection;
            InitializeComponent();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            updateOrders();
        }

        private void активаироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && !Char.IsControl(number))
            {
                e.Handled = true;
            }
        }

        private void updateOrders()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Orders INNER JOIN Users ON Orders.UserID = Users.Id",conn);
            SqlDataReader dataReader = null;

            try
            {
                conn.Open();
                listViewOrders.Items.Clear();
                dataReader = cmd.ExecuteReader();
                ListViewItem viewItem;
                while (dataReader.Read())
                {
                    viewItem = new ListViewItem(new string[]
                    {
                       Convert.ToString(dataReader[0]),
                        Convert.ToString(dataReader[9]),
                        Convert.ToString(dataReader[10]),
                        Convert.ToString(dataReader[11]),
                        Convert.ToString(dataReader[12]),
                        Convert.ToString(dataReader[14]),
                       Convert.ToString(dataReader[2]),
                       Convert.ToString(dataReader[3]),
                       Convert.ToString(dataReader[4]),
                       Convert.ToString(dataReader[5]),
                       Convert.ToString(dataReader[6]),
                       Convert.ToString(dataReader[7]),
                       Convert.ToString(dataReader[8])
                      
                    });
                    viewItem.Tag = dataReader[0];
                    listViewOrders.Items.Add(viewItem);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                conn.Close();
            }
        }

        private void maskedTextBoxPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            updateOrders();
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            updateOrders();
        }

        
    }
}
