using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib.Model;

namespace SmartCanteenREST.Managers
{
    public class ProductManager
    {
        private const string connString = "Data Source=smartcanteen-db-erver.database.windows.net;Initial Catalog=SmartCanteen-DB;User ID=smadmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GET_ALL = "Select * from Product";
        private const string GET_IS_HOT = "Select * from Product WHERE isHot = @isHot";

        /*
         * Only GET implemented as we do not wish to
         * POST, DELETE or UPDATE product data
         */

        // GET all product info
        public IList<Products> GetProductInfo()
        {
            List<Products> productInfo = new List<Products>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        productInfo.Add(ReadNextProduct(reader));
                    }
                }
            }

            return productInfo;
        }

        //GETS product based on bool
        public IList<Products> GetProductsFromIsHot(bool isHot)
        {
            List<Products> isHotList = new List<Products>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_IS_HOT, conn))
                {
                    cmd.Parameters.AddWithValue("@isHot", isHot);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        isHotList.Add(ReadNextProduct(reader));
                    }
                }
            }

            return isHotList;
        }

        // Reads the columns in the table
        private Products ReadNextProduct(SqlDataReader reader)
        {
            Products product = new Products();

            product.ProdId = reader.GetInt32(0);
            product.FkId = reader.GetInt32(1);
            product.Name = reader.GetString(2);
            product.Price = reader.GetInt32(3);
            product.Protein = reader.GetInt32(4);
            product.IsHot = reader.GetBoolean(5);

            return product;
        }
    }
}
