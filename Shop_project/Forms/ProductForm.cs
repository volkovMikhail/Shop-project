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

namespace Shop_project.Forms
{
    public partial class ProductForm : Form
    {
        Product product;
        bool toCart;
        public ProductForm(int id)
        {
            product = new Product(id);
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            toCart = false;
            this.Text = product.name;
            labelName.Text = product.name;
            labelDiscription.Text = product.discription;
            labelPrice.Text = Convert.ToString(product.price);
            labelQuantity.Text = Convert.ToString(product.quantity);
            labelCategory.Text = product.category;
            pictureBox1.Image = ImageSizeUtil.stratchImage(Image.FromFile("Resources\\images\\" + product.image));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toCart = true;
            this.Close();
        }

        private void ProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (toCart)
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
