using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
