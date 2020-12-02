using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib.Model;

namespace SmartCanteenREST.Managers
{
    public class OrderManager
    {
        private const string connString = "Data Source=smartcanteen-db-erver.database.windows.net;Initial Catalog=SmartCanteen-DB;User ID=smadmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GET_ALL = "Select * from Orders";
        private const string GET_BY_DATE = "Select * from Orders WHERE Order_date = @Date";

        /*
         * Only GETS implemented as we do not wish to
         * POST, DELETE or UPDATE product data
         */

        // GETS all orders
        public IList<Orders> GetAllOrders()
        {
            List<Orders> allOrders = new List<Orders>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            allOrders.Add(ReadNextOrder(reader));
                        }
                    }
                }
            }

            return allOrders;
        }

        // GETS Order by specific date
        public Orders GetOrderByDate(DateTime date)
        {
            Orders order = new Orders();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_BY_DATE, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        order = ReadNextOrder(reader);
                    }
                }
            }

            return order;
        }

        // Reads the columns in the table
        private Orders ReadNextOrder(SqlDataReader reader)
        {
            Orders order = new Orders();

            order.OrderId = reader.GetInt32(0);
            order.ProductCount = reader.GetInt32(1);
            order.OrderDate = reader.GetDateTime(2);

            return order;
        }
    }
}
