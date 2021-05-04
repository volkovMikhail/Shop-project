using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shop_project.Utils;
using System.Data.SqlClient;

namespace Shop_project.Forms
{
    public partial class ConfirmOrder : Form
    {
        User user;
        List<Product> cart;
        SqlConnection conn;

        public ConfirmOrder(User user, List<Product> cart,SqlConnection connection)
        {
            this.conn = connection;
            this.user = user;
            this.cart = cart;
            InitializeComponent();
        }

        private void confirmOrder_Load(object sender, EventArgs e)
        {
            textBoxEmail.Text = user.email;
        }

        private void buttonAddOrder_Click(object sender, EventArgs e)
        {
            if (textBoxAddress.Text != string.Empty && textBoxEmail.Text != string.Empty)
            {
                try
                {
                    conn.Open();
                    foreach (Product product in cart)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Orders VALUES (@userId,@productId,@productName,@comment,@address,@price,@date,@state)", conn);
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = user.id;
                        cmd.Parameters.Add("@productId", SqlDbType.Int).Value = product.id;
                        cmd.Parameters.Add("@productName", SqlDbType.NVarChar).Value = product.name;
                        cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = textBoxComment.Text;
                        cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = textBoxAddress.Text;
                        cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = product.price;
                        cmd.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Now.ToShortDateString();
                        cmd.Parameters.Add("@state", SqlDbType.Bit).Value = true;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    //TODO show buttons to output
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Abort;
                }
            }
        }

        private void buttonSendMail_Click(object sender, EventArgs e)
        {

        }

        private void buttonToTXT_Click(object sender, EventArgs e)
        {

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
