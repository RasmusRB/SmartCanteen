﻿using System;
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
        private const string GET_ALL = "SELECT * FROM Customer";
        private const string GET_TODAY = "SELECT * FROM Customer WHERE Customer_date=@Customer_date";
        private const string CREATE = "IF EXISTS(SELECT * FROM Customer WHERE Customer_date=@Customer_date)\nBEGIN\nUPDATE Customer SET Counter=@Counter WHERE Customer_date=@Customer_date\nEND\nELSE\nBEGIN\nINSERT INTO Customer VALUES(@Counter, @Customer_date)  \nEND";


        //Old create query
        //private const string CREATE = "Insert into Customer ( Counter, Customer_date ) values ( @Counter, @Customer_date )";

        /*
         * Only GET & POST implemented as we do not wish to
         * DELETE or UPDATE customer data received from sensor
         */

        // GETS all customer data
        public List<Customers> GetCustomerData()
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

        //GET todays customer data
        public Customers GetCustomerDataForDay(DateTime dateTime)
        {
            using SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            using SqlCommand cmd = new SqlCommand(GET_TODAY, conn);
            cmd.Parameters.AddWithValue("@Customer_date", dateTime);
            
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadNextCustomer(reader);
            return null;
        }

        // Creates customer data
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

        // Reads customer
        private Customers ReadNextCustomer(SqlDataReader reader)
        {
            Customers customer = new Customers();

            customer.Counter = reader.GetInt32(0);
            customer.DateTime = reader.GetDateTime(1);

            return customer;
        }
    }
}
