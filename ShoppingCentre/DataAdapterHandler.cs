using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCentre
{
    public class DataAdapterHandler
    {
        // Database Connection

        public SqlConnection GetDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShoppingCentreConnectionString"].ConnectionString;

            SqlConnectionStringBuilder builder = new(connectionString);

            SqlConnection connection = new(builder.ConnectionString);

            return connection;
        }

        // Data Adapters

        // Customer

        public static SqlDataAdapter CustomerAdapter(SqlConnection connection)
        {
            string customerQuery = "SELECT * FROM Customer";

            SqlCommand customerSelectCommand = connection.CreateCommand();
            customerSelectCommand.CommandText = customerQuery;

            SqlDataAdapter customerDataAdapter = new();
            customerDataAdapter.SelectCommand = customerSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = customerDataAdapter;

            return customerDataAdapter;
        }

        // Visits

        public static SqlDataAdapter VisitsAdapter(SqlConnection connection)
        {
            string visitsQuery = "SELECT * FROM Visits";

            SqlCommand visitsSelectCommand = connection.CreateCommand();
            visitsSelectCommand.CommandText = visitsQuery;

            SqlDataAdapter visitsDataAdapter = new();
            visitsDataAdapter.SelectCommand = visitsSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = visitsDataAdapter;

            return visitsDataAdapter;
        }


        // Employee

        public static SqlDataAdapter EmployeeAdapter(SqlConnection connection)
        {
            string employeeQuery = "SELECT * FROM Employee";

            SqlCommand employeeSelectCommand = connection.CreateCommand();
            employeeSelectCommand.CommandText = employeeQuery;

            SqlDataAdapter employeeDataAdapter = new();
            employeeDataAdapter.SelectCommand = employeeSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = employeeDataAdapter;

            return employeeDataAdapter;
        }

        // Product

        public static SqlDataAdapter ProductAdapter(SqlConnection connection)
        {
            string productQuery = "SELECT * FROM Product";

            SqlCommand productSelectCommand = connection.CreateCommand();
            productSelectCommand.CommandText = productQuery;

            SqlDataAdapter productDataAdapter = new();
            productDataAdapter.SelectCommand = productSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = productDataAdapter;

            return productDataAdapter;
        }

        // Sells

        public static SqlDataAdapter SellsAdapter(SqlConnection connection)
        {
            string sellsQuery = "SELECT * FROM Sells";

            SqlCommand sellsSelectCommand = connection.CreateCommand();
            sellsSelectCommand.CommandText = sellsQuery;

            SqlDataAdapter sellsDataAdapter = new();
            sellsDataAdapter.SelectCommand = sellsSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = sellsDataAdapter;

            return sellsDataAdapter;
        }

        //Store 

        public static SqlDataAdapter StoreAdapter(SqlConnection connection)
        {
            string storeQuery = "SELECT * FROM Store";

            SqlCommand storeSelectCommand = connection.CreateCommand();
            storeSelectCommand.CommandText = storeQuery;

            SqlDataAdapter storeDataAdapter = new();
            storeDataAdapter.SelectCommand = storeSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = storeDataAdapter;

            return storeDataAdapter;
        }

        // Shopping Centre

        public static SqlDataAdapter ShoppingCentreAdapter(SqlConnection connection)
        {
            string shoppingCentreQuery = "SELECT * FROM ShoppingCentre";

            SqlCommand shoppingCentreSelectCommand = connection.CreateCommand();
            shoppingCentreSelectCommand.CommandText = shoppingCentreQuery;

            SqlDataAdapter shoppingCentreDataAdapter = new();
            shoppingCentreDataAdapter.SelectCommand = shoppingCentreSelectCommand;

            SqlCommandBuilder commandBuilder = new();
            commandBuilder.DataAdapter = shoppingCentreDataAdapter;

            return shoppingCentreDataAdapter;
        }


    }
}
