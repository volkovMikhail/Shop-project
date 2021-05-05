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
using System.IO;

namespace Shop_project.Forms
{
    public partial class Admin : Form
    {
        SqlConnection conn;

        struct validData
        {
            public bool nameIsOk;
            public bool priceIsOk;
            public bool categoryIsOk;
            public bool discriptionIsOk;
            public bool imgIsOk;
            public bool countIsok;
        }
        validData valid;
        public Admin(SqlConnection connection)
        {
            conn = connection;
            InitializeComponent();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            updateOrders();
            updateProducts();
            valid.nameIsOk = false;
            valid.priceIsOk = false;
            valid.categoryIsOk = false;
            valid.discriptionIsOk = false;
            valid.imgIsOk = false;
            valid.countIsok = false;
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
                    viewItem.Tag = dataReader[0];
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

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty)
            {
                valid.imgIsOk = true;
                labelFile.Text = openFileDialog1.SafeFileName;
                try
                {
                    File.Copy(openFileDialog1.FileName, @"Resources\images\" + openFileDialog1.SafeFileName);
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != string.Empty)
            {
                valid.nameIsOk = true;
            }
            if (textBoxCategory.Text != string.Empty)
            {
                valid.categoryIsOk = true;
            }
            if (textBoxDiscription.Text != string.Empty)
            {
                valid.discriptionIsOk = true;
            }
            if (textBoxCount.Text != string.Empty)
            {
                valid.countIsok = true;
            }
            if (textBoxPrice.Text != string.Empty)
            {
                valid.priceIsOk = true;
            }

            if (valid.categoryIsOk && valid.countIsok && valid.discriptionIsOk && valid.imgIsOk && valid.nameIsOk && valid.priceIsOk)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Products VALUES(@name,@price,@discr,@image,@category,@popularity,@quantity)", conn);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = textBoxPrice.Text;
                    cmd.Parameters.Add("@discr", SqlDbType.NVarChar).Value = textBoxDiscription.Text;
                    cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = labelFile.Text;
                    cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = textBoxCategory.Text;
                    cmd.Parameters.Add("@popularity", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = textBoxCount.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    updateProducts();
                    textBoxName.Text = "";
                    textBoxPrice.Text = "";
                    textBoxCategory.Text = "";
                    labelFile.Text = "";
                    textBoxCount.Text = "";
                    textBoxDiscription.Text = "";
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                }   
            }
            else
            {
                MessageBox.Show("Неверные данные","Внимание!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBoxPrice.Text.IndexOf(",") == -1) && (textBoxPrice.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void textBoxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && !Char.IsControl(number))
            {
                e.Handled = true;
            }
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in listViewProducts.SelectedItems)
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM Products WHERE Id = {Convert.ToInt32(item.Tag)}", conn);
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
                updateProducts();
            }
            listViewProducts.SelectedItems.Clear();
            GC.Collect();
        }
    }
}
