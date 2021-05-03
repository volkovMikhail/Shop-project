using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Shop_project.Utils;
using Shop_project.Forms;

namespace Shop_project
{
    public partial class Main : Form
    {
        User user;
        SqlConnection conn;
        public Main()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            user = new User();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            updateCategory();
            outputFromProducts();
        }

        private void updateCategory()
        {
            SqlDataReader dataReader = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT category FROM Products", conn);
                dataReader = cmd.ExecuteReader();
                comboBoxCategory.Items.Add("Все категории");
                while (dataReader.Read())
                {
                    comboBoxCategory.Items.Add(dataReader[0]);
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
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
            comboBoxCategory.SelectedIndex = 0;
        }

        private void outputFromProducts()
        {
            ListViewItem viewItem;
            listViewCatalog.Items.Clear();
            listViewCatalog.Clear();
            imageList.Images.Clear();
            SqlDataReader dataReader = null;
            try
            {

                queryParams queryParams;
                queryParams.searchText = textBoxNameSearch.Text;
                queryParams.popularity = radioButtonPopularity.Checked;
                queryParams.pure = radioButtonPure.Checked;
                queryParams.expansive = radioButtonExpansive.Checked;
                queryParams.bottomPrice = textBoxPriceBottom.Text;
                queryParams.topPrice = textBoxPriceTop.Text;
                queryParams.category = comboBoxCategory.SelectedItem.ToString();

                string commandText = queryBuilder.query(queryParams);
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText, conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    imageList.Images.Add(ImageSizeUtil.stratchImage(Image.FromFile("Resources\\images\\" + Convert.ToString(dataReader[3]))));
                    viewItem = new ListViewItem();
                    viewItem.Text = $"{Convert.ToString(dataReader[1])} - Цена: [{dataReader[2]}]";
                    viewItem.ImageIndex = imageList.Images.Count - 1;
                    viewItem.Tag = dataReader[0];
                    listViewCatalog.Items.Add(viewItem);
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
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

        private void textBoxPriceBottom_TextChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void textBoxPriceBottom_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && !Char.IsControl(number))
            {
                e.Handled = true;
            }
        }

        private void textBoxPriceTop_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && !Char.IsControl(number))
            {
                e.Handled = true;
            }
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void textBoxNameSearch_TextChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void radioButtonPopularity_CheckedChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void radioButtonPure_CheckedChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void radioButtonExpansive_CheckedChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void textBoxPriceTop_TextChanged(object sender, EventArgs e)
        {
            outputFromProducts();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration(conn);
            if (registration.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Регистрация завершена!","Успех!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void updateUserData()
        {
            if (user.id != -1)
            {
                textBoxName.Text = user.name;
                textBoxLastName.Text = user.lastName;
                textBoxEmail.Text = user.email;
                maskedTextBoxPhone.Text = user.phoneNum;
            }
            else
            {
                textBoxName.Text = string.Empty;
                textBoxLastName.Text = string.Empty;
                textBoxEmail.Text = string.Empty;
                maskedTextBoxPhone.Text = string.Empty;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (buttonLogin.Text == "Войти" )//login
            {
                Login login = new Login(conn);
                if (login.ShowDialog() == DialogResult.OK)
                {
                    user = login.user;
                    if (user.name == "admin")
                    {
                        Admin admin = new Admin(conn);
                        this.Hide();
                        admin.ShowDialog();
                        this.Show();
                        user = new User();
                    }
                    else
                    {
                        buttonLogin.Text = "Выйти";
                        buttonReg.Enabled = false;
                        buttonEdit.Enabled = true;
                        updateUserData();
                    }
                }
            }
            else//logout
            {
                user = new User();
                buttonLogin.Text = "Войти";
                buttonReg.Enabled = true;
                buttonEdit.Enabled = false;
                updateUserData();
            }
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "Редактировать")
            {
                buttonEdit.Text = "Сохранить";
                textBoxName.Enabled = true;
                textBoxLastName.Enabled = true;
                textBoxEmail.Enabled = true;
                maskedTextBoxPhone.Enabled = true;
            }
            else
            {
                if (user.id != -1)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET name = @name, lastName = @lastname, phoneNum = @phone, email = @email WHERE Id = @id", conn);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = textBoxName.Text;
                    cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = textBoxLastName.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = maskedTextBoxPhone.Text;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = textBoxEmail.Text;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.id;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        user.name = textBoxName.Text;
                        user.lastName = textBoxLastName.Text;
                        user.phoneNum = maskedTextBoxPhone.Text;
                        user.email = textBoxEmail.Text;
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                updateUserData();
                buttonEdit.Text = "Редактировать";
                textBoxName.Enabled = false;
                textBoxLastName.Enabled = false;
                textBoxEmail.Enabled = false;
                maskedTextBoxPhone.Enabled = false;
            }
        }
    }
}
