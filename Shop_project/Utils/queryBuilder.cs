using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_project.Utils;

namespace Shop_project.Utils
{
    class queryBuilder
    {
        public static string query(queryParams queryParams)
        {
            string cmd = "";
            cmd += $"SELECT id,name,price,image FROM Products WHERE name LIKE N'%{queryParams.searchText}%' ";
            if (queryParams.category != "Все категории")
            {
                cmd += $"AND category = N'{queryParams.category}' ";
            }
            if (queryParams.topPrice != string.Empty)
            {
                cmd += $"AND price < {queryParams.topPrice} ";
            }
            if (queryParams.bottomPrice != string.Empty)
            {
                cmd += $"AND price > {queryParams.bottomPrice} ";
            }
            if (queryParams.popularity)
            {
                cmd += "ORDER BY popularity DESC";
            }
            if (queryParams.pure)
            {
                cmd += "ORDER BY price ASC";
            }
            if (queryParams.expansive)
            {
                cmd += "ORDER BY price DESC";
            }

            return cmd;
        }
    }
}
