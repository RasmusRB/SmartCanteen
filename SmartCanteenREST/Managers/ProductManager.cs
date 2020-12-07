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
        private const string GET_ALL_BY_FK = "Select* from Product Where FK_Category_Id = @id";
        private const string GET_BY_ID = "Select * from Product Where Product_Id = @id";
        private const string CREATE_PROD =
            "Insert into Product (FK_Category_Id, Name, Price, Protein, IsHot) VALUES (@CategoryID, @Name, @Price, @Protein, @IsHot)";

        /*
         * Only GET implemented as we do not wish to
         * POST, DELETE or UPDATE product data
         */

        // GETS all product info
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

        // GETS product based on category FK (Fx. Soup, snack etc.)
        public IList<Products> GetProductByCategory(int id)
        {
            List<Products> fkList = new List<Products>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL_BY_FK, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        fkList.Add(ReadNextProduct(reader));
                    }
                }
            }

            return fkList;
        }

        public Products GetById(int id)
        {
            Products product = new Products();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_BY_ID, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        product = ReadNextProduct(reader);
                    }
                }
            }

            return product;

        }

        // GETS product based on bool
        public IList<Products> GetProductsFromBool(bool isHot)
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

        // CREATES a new product
        public bool CreateProduct(Products product)
        {
            bool created = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(CREATE_PROD, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", product.FkId);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Protein", product.Protein);
                    cmd.Parameters.AddWithValue("@IsHot", product.IsHot);

                    int rows = cmd.ExecuteNonQuery();
                    created = rows == 1;
                }
            }

            return created;
        }

        public Products DeleteProduct(int id)
        {
            Products product = GetById(id);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_BY_ID, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return product;
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
