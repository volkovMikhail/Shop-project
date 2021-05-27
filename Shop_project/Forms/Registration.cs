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
    public partial class Registration : Form
    {
        SqlConnection conn;
        public Registration(SqlConnection connection)
        {
            conn = connection;
            InitializeComponent();
        }

        bool nameIsOk;
        bool lastnameIsok;
        bool phoneNumIsOk;
        bool emailIsOk;
        bool passwordIsOk;

        private void Registration_Load(object sender, EventArgs e)
        {
            nameIsOk = false;
            phoneNumIsOk = false;
            emailIsOk = false;
            passwordIsOk = false;
            lastnameIsok = false;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length < 2 || textBoxName.Text.Length > 50)
            {
                nameIsOk = false;
                labelErrorReg.Text = "Не допустимое значение имени";
                labelErrorReg.Visible = true;
            }
            else
            {
                nameIsOk = true;
                labelErrorReg.Text = "";
                labelErrorReg.Visible = false;
            }
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLastName.Text.Length < 2 || textBoxLastName.Text.Length > 50)
            {
                lastnameIsok = false;
                labelErrorReg.Text = "Не допустимое значение фамилии";
                labelErrorReg.Visible = true;
            }
            else
            {
                lastnameIsok = true;
                labelErrorReg.Text = "";
                labelErrorReg.Visible = false;
            }
        }

        private void maskedTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            char[] phone = new char[17];
            bool isFull = true;
            for (int i = 5; i < 17; i++)
            {
                try
                {
                    phone[i] = maskedTextBoxPhone.Text[i];
                }
                catch (Exception)
                {
                    phone[i] = ' ';
                }
                if (phone[i] == ' ')
                {
                    isFull = false;
                }
            }
            if (isFull)
            {
                phoneNumIsOk = true;
                labelErrorReg.Visible = false;
            }
            else
            {
                phoneNumIsOk = false;
                labelErrorReg.Text = "Неверный номер телефона";
                labelErrorReg.Visible = true;
            }
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            bool emailCheck = false;
            for (int i = 0; i < textBoxEmail.Text.Length; i++)
            {
                if (textBoxEmail.Text[i] == '@')
                {
                    emailCheck = true;
                }
            }
            if (emailCheck)
            {
                emailIsOk = true;
                labelErrorReg.Text = "";
                labelErrorReg.Visible = false;
            }
            else
            {
                emailIsOk = false;
                labelErrorReg.Text = "Неверный email";
                labelErrorReg.Visible = true;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length < 8)
            {
                passwordIsOk = false;
                labelErrorReg.Text = "Пароль слишком маленький";
                labelErrorReg.Visible = true;
            }
            else if (textBoxPassword.Text.Length > 50)
            {
                passwordIsOk = false;
                labelErrorReg.Text = "Пароль слишком большой";
                labelErrorReg.Visible = true;
            }
            else if (textBoxPassword.Text != textBoxConfirm.Text)
            {
                passwordIsOk = false;
                labelErrorReg.Text = "Подтвердите пароль";
                labelErrorReg.Visible = true;
            }
            else
            {
                passwordIsOk = true;
                labelErrorReg.Text = "";
                labelErrorReg.Visible = false;
            }
        }

        private void textBoxConfirm_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length < 8)
            {
                passwordIsOk = false;
                labelErrorReg.Text = "Пароль слишком маленький";
                labelErrorReg.Visible = true;
            }
            else if (textBoxPassword.Text.Length > 50)
            {
                passwordIsOk = false;
                labelErrorReg.Text = "Пароль слишком большой";
                labelErrorReg.Visible = true;
            }
            else if (textBoxPassword.Text != textBoxConfirm.Text)
            {
                passwordIsOk = false;
                labelErrorReg.Text = "Вы не потдвердили пароль";
                labelErrorReg.Visible = true;
            }
            else
            {
                passwordIsOk = true;
                labelErrorReg.Text = "";
                labelErrorReg.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameIsOk && phoneNumIsOk && passwordIsOk && emailIsOk && lastnameIsok)
            {
                labelErrorReg.Text = "";
                labelErrorReg.Visible = false;
                int res = 0;
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@name,@lastname,@phoneNumber,@password,@email)", conn);
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = textBoxName.Text;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = textBoxPassword.Text;
                cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = maskedTextBoxPhone.Text;
                cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = textBoxLastName.Text;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = textBoxEmail.Text;
                try
                {
                    res = cmd.ExecuteNonQuery();
                    phoneNumIsOk = true;
                }
                catch (Exception)
                {
                    if (res != 1)
                    {
                        labelErrorReg.Text = "Этот номер уже занят";
                        labelErrorReg.Visible = true;
                        phoneNumIsOk = false;
                    }
                }
                conn.Close();
                if (res == 1)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                labelErrorReg.Text = "Проверьте введённые данные";
                labelErrorReg.Visible = true;
            }
        }

        private void maskedTextBoxPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

