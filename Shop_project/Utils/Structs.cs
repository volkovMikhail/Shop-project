using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Shop_project.Utils
{
    public struct queryParams
    {
        public string searchText;
        public bool popularity;
        public bool pure;
        public bool expansive;
        public string bottomPrice;
        public string topPrice;
        public string category;
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string phoneNum { get; set; }
        public string email { get; set; }

        public User()
        {
            id = -1;
            name = string.Empty;
            lastName = string.Empty;
            phoneNum = string.Empty;
            email = string.Empty;
        }
    }

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string discription { get; set; }
        public string image { get; set; }
        public string category { get; set; }
        public int popularity { get; set; }
        public int quantity { get; set; }
        public Product()
        {
            id = -1;
            name = string.Empty;
            price = 0;
            discription = string.Empty;
            image = string.Empty;
            category = string.Empty;
            popularity = 0;
            quantity = 0;
        }

        public Product(int id)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Products WHERE Id = {id}",connection);
            SqlDataReader dataReader = null;
            connection.Open();
            try
            {
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                this.id = id;
                name = Convert.ToString(dataReader[1]);
                price = Convert.ToSingle(dataReader[2]);
                discription = Convert.ToString(dataReader[3]);
                image = Convert.ToString(dataReader[4]);
                category = Convert.ToString(dataReader[5]);
                popularity = Convert.ToInt32(dataReader[6]);
                quantity = Convert.ToInt32(dataReader[7]);
            }
            catch (Exception ex)
            {
                id = -1;
                name = string.Empty;
                price = 0;
                discription = string.Empty;
                image = string.Empty;
                category = string.Empty;
                popularity = 0;
                quantity = 0;
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                connection.Close();
            }
        }
    }
}
