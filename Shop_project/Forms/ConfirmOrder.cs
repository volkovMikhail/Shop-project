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
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Drawing.Printing;

namespace Shop_project.Forms
{
    public partial class ConfirmOrder : Form
    {
        User user;
        List<Product> cart;
        SqlConnection conn;
        bool isOrdered;
        string address;
        public ConfirmOrder(User user, List<Product> cart,SqlConnection connection)
        {
            this.conn = connection;
            this.user = user;
            this.cart = cart;
            InitializeComponent();
        }

        private void confirmOrder_Load(object sender, EventArgs e)
        {
            isOrdered = false;
            textBoxEmail.Text = user.email;
        }

        private void buttonAddOrder_Click(object sender, EventArgs e)
        {
            if (textBoxAddress.Text != string.Empty && textBoxEmail.Text != string.Empty)
            {
                try
                {
                    if (textBoxComment.Text == string.Empty)
                    {
                        textBoxComment.Text = "-";
                    }
                    SqlCommand cmd;
                    SqlCommand updateCmd;
                    conn.Open();
                    foreach (Product product in cart)
                    {
                        Product p = new Product(product.id);
                        
                        cmd = new SqlCommand("INSERT INTO Orders VALUES (@userId,@productId,@productName,@comment,@address,@price,@date,@state)", conn);
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = user.id;
                        cmd.Parameters.Add("@productId", SqlDbType.Int).Value = product.id;
                        cmd.Parameters.Add("@productName", SqlDbType.NVarChar).Value = product.name;
                        cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = textBoxComment.Text;
                        cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = textBoxAddress.Text;
                        cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = product.price;
                        cmd.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Now.ToShortDateString();
                        cmd.Parameters.Add("@state", SqlDbType.Bit).Value = true;
                        cmd.ExecuteNonQuery();
                        if (p.quantity > 0)
                        {
                            updateCmd = new SqlCommand($"UPDATE Products SET popularity = popularity + 10, quantity = quantity - 1 WHERE Id = {product.id}", conn);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            updateCmd = new SqlCommand($"UPDATE Products SET popularity = popularity + 10 WHERE Id = {product.id}", conn);
                            updateCmd.ExecuteNonQuery();
                        }
                        address = textBoxAddress.Text;
                    }
                    GC.Collect();
                    conn.Close();
                    isOrdered = true;
                    buttonAddOrder.Enabled = false;
                    labelMessage.Visible = true;
                    buttonSendMail.Enabled = true;
                    buttonToTXT.Enabled = true;
                    buttonPrint.Enabled = true;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Abort;
                }
            }
            else
            {
                MessageBox.Show("Дополните данные формы","Info",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            //send order to admin
            try 
            {
                string body = "";
                body += $"<b>Пользователь: {user.name} {user.lastName}</b><br/>";
                body += $"<b>Номер телефона: {user.phoneNum}</b><br/>";
                body += $"<b>Адрес: {address}</b><br/>";
                body += $"<b>Дата: {DateTime.Now.ToShortDateString()}</b><br/><br/>";
                body += $"<h1>\tТовары:</h1></br><ol>";
                float sum = 0;
                foreach (Product item in cart)
                {
                    sum += item.price;
                    body += $"<li>{item.name} (id: {item.id}) - Цена: {item.price}</li>";
                }
                body += "</ol><br/><hr/>";
                body += $"<b><i>Сумма заказа = {sum}</b></i>";
                MailAddress from = new MailAddress("horoshihblag@gmail.com", "admin");
                MailAddress to = new MailAddress("horoshihblag@gmail.com");
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = $"Чек заказа {user.name}";
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("horoshihblag@gmail.com", "horoshihblag123");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception)
            {
                //nothing
            }
        }

        private void buttonSendMail_Click(object sender, EventArgs e)
        {
            string body = "";
            body += $"<b>Пользователь: {user.name} {user.lastName}</b><br/>";
            body += $"<b>Номер телефона: {user.phoneNum}</b><br/>";
            body += $"<b>Адрес: {address}</b><br/>";
            body += $"<b>Дата: {DateTime.Now.ToShortDateString()}</b><br/><br/>";
            body += $"<h1>\tТовары:</h1></br><ol>";
            float sum = 0;
            foreach (Product item in cart)
            {
                sum += item.price;
                body += $"<li>{item.name} (id: {item.id}) - Цена: {item.price}</li>";
            }
            body += "</ol><br/><hr/>";
            body += $"<b><i>Сумма заказа = {sum}</b></i>";
            try
            {
                MailAddress from = new MailAddress("horoshihblag@gmail.com","Менеджер \"ХорошихБлаг\"");
                MailAddress to = new MailAddress(textBoxEmail.Text);
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Чек заказа \"ХорошихБлаг\"";
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("horoshihblag@gmail.com", "horoshihblag123");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show($"Чек успешно оправлен на почту {textBoxEmail.Text}", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToTXT_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != string.Empty)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                writer.WriteLine($"Пользователь: {user.name} {user.lastName}");
                writer.WriteLine($"Номер телефона: {user.phoneNum}");
                writer.WriteLine($"Адрес: {address}");
                writer.WriteLine($"Эл. почта: {textBoxEmail.Text}");
                writer.WriteLine($"Дата: {DateTime.Now.ToShortDateString()}");
                writer.WriteLine();
                writer.WriteLine($"\tТовары:");
                int counter = 1;
                float sum = 0;
                foreach (Product item in cart)
                {
                    sum += item.price;
                    writer.WriteLine($"{counter}. {item.name} (id: {item.id}) - Цена: {item.price}");
                    counter++;
                }
                writer.WriteLine("==============================");
                writer.WriteLine($"Сумма заказа = {sum}");
                writer.Close();
                MessageBox.Show("Файл сохранён", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Вы не выбрали файл", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string pageBody = "";// var for print function
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            pageBody += $"Пользователь: {user.name} {user.lastName}\n";
            pageBody += $"Номер телефона: {user.phoneNum}\n";
            pageBody += $"Адрес: {address}\n";
            pageBody += $"Эл. почта: {textBoxEmail.Text}\n";
            pageBody += $"Дата: {DateTime.Now.ToShortDateString()}\n\n";
            pageBody += $"\tТовары:\n";
            double sum = 0;
            int counter = 1;
            foreach (Product item in cart)
            {
                sum += item.price;
                pageBody += $"{counter}. {item.name} (id: {item.id}) - Цена: {item.price}\n";
                counter++;
            }
            pageBody += "==============================\n";
            pageBody += $"Сумма заказа = {sum}";

            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += PrintPageHandler;
                printPreviewDialog1.Document = doc;
                printPreviewDialog1.ShowDialog();
                printDialog1.Document = doc;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.Document.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            pageBody = string.Empty;
        }
        void PrintPageHandler(object sender, PrintPageEventArgs e) // for printing handler
        {
            e.Graphics.DrawString(pageBody, new Font("Arial", 14), Brushes.Black, 0, 0);
        }

        private void ConfirmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOrdered)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
