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
            updateProducts();
        }

        private void updateProducts()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);
            SqlDataReader dataReader = null;
            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                listViewProducts.Items.Clear();
                while (dataReader.Read())
                {
                    ListViewItem viewItem = new ListViewItem(new string[]
                    {
                        Convert.ToString(dataReader[0]),
                        Convert.ToString(dataReader[1]),
                        Convert.ToString(dataReader[2]),
                        Convert.ToString(dataReader[3]),
                        Convert.ToString(dataReader[4]),
                        Convert.ToString(dataReader[5]),
                        Convert.ToString(dataReader[6]),
                        Convert.ToString(dataReader[7])
                    }) ;
                    listViewProducts.Items.Add(viewItem);
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
            }
            GC.Collect();
        }

        private void активаироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in listViewOrders.SelectedItems)
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE Orders SET state = 1 WHERE Id = {Convert.ToInt32(item.Tag)}", conn);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
                updateOrders();
            }
            listViewOrders.SelectedItems.Clear();
            GC.Collect();
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
            bool firstWhere = true;
            string command = "SELECT * FROM Orders INNER JOIN Users ON Orders.UserID = Users.Id ";
            if (textBoxID.Text != string.Empty)
            {
                if (firstWhere)
                {
                    firstWhere = false;
                    command += "WHERE ";
                    command += $"Orders.UserID = {textBoxID.Text} ";
                }
                else
                {
                    command += $"AND Orders.UserID = {textBoxID.Text} ";
                }
               
            }
            bool isPhoneHaveEmpty = false;
            for (int i = 0; i < maskedTextBoxPhone.Text.Length; i++)
            {
                if (maskedTextBoxPhone.Text[i] == ' ')
                {
                    isPhoneHaveEmpty = true;
                }
            }
            if (!isPhoneHaveEmpty)
            {
                if (firstWhere)
                {
                    command += $"WHERE Users.phoneNum = N'{maskedTextBoxPhone.Text}'";
                }
                else
                {
                    command += $"AND Users.phoneNum = N'{maskedTextBoxPhone.Text}'";
                }
            }
            command += " ORDER BY Orders.orderDate ASC";
            SqlCommand cmd = new SqlCommand(command,conn);
            SqlDataReader dataReader = null;

            try
            {
                conn.Open();
                listViewOrders.Items.Clear();
                dataReader = cmd.ExecuteReader();
                ListViewItem viewItem;
                float sum = 0;
                
                while (dataReader.Read())
                {
                    sum += Convert.ToSingle(dataReader[6]);
                    string state = "";
                    if (Convert.ToBoolean(dataReader[8]))
                    {
                        state = "Активный";
                    }else
                    {
                        state = "Неактивный"; 
                    }
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
                       Convert.ToDateTime(dataReader[7]).ToShortDateString(),
                       state
                    });
                    viewItem.Tag = dataReader[0];
                    listViewOrders.Items.Add(viewItem);
                }
                labelSum.Text = sum.ToString();
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

        private void maskedTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            updateOrders();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in listViewOrders.SelectedItems)
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM Orders WHERE Id = {Convert.ToInt32(item.Tag)}", conn);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
                updateOrders();
            }
            listViewOrders.SelectedItems.Clear();
            GC.Collect();
        }

        private void деактивироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listViewOrders.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in listViewOrders.SelectedItems)
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE Orders SET state = 0 WHERE Id = {Convert.ToInt32(item.Tag)}", conn);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
                updateOrders();
            }
            listViewOrders.SelectedItems.Clear();
            GC.Collect();
        }
    }
}
