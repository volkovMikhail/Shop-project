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

namespace Shop_project
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
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
                updateCategory();

                queryParams queryParams;
                queryParams.searchText = textBoxNameSearch.Text;
                queryParams.popularity = radioButtonPopularity.Checked;
                queryParams.pure = radioButtonPure.Checked;
                queryParams.expansive = radioButtonExpansive.Checked;
                queryParams.bottomPrice = textBoxPriceBottom.Text;
                queryParams.topPrice = textBoxPriceTop.Text;
                queryParams.category = comboBoxCategory.SelectedItem.ToString();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id,name,price,image FROM Products", conn);
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
    }
}
