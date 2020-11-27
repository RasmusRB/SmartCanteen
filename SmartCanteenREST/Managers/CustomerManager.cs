using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib.Model;

namespace SmartCanteenREST.Managers
{
    public class CustomerManager
    {
        private const string connString = "Data Source=smartcanteen-db-erver.database.windows.net;Initial Catalog=SmartCanteen-DB;User ID=smadmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GET_ALL = "Select * from Customer";
        private const string CREATE = "Insert into Customer ( Counter, Customer_date ) values ( @Counter, @Customer_date )";


        // GETS all customer Data stored in DB
        public IList<Customers> GetCustomerData()
        {
            List<Customers> customerData = new List<Customers>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customerData.Add(ReadNextCustomer(reader));
                    }
                }
            }

            return customerData;
        }

        public bool CreateCustomerData(Customers customer)
        {
            bool created = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.Parameters.AddWithValue("@Counter", customer.Counter);
                    cmd.Parameters.AddWithValue("@Customer_date", customer.DateTime);

                    int rows = cmd.ExecuteNonQuery();
                    created = rows == 1;
                }
            }

            return created;
        }

        private Customers ReadNextCustomer(SqlDataReader reader)
        {
            Customers customer = new Customers();

            customer.Counter = reader.GetInt32(0);
            customer.DateTime = reader.GetDateTime(1);

            return customer;
        }
    }
}
