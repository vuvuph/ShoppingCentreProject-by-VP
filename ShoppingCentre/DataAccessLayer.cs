using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingCentre
{
    public class DataAccessLayer
    {
        DataAdapterHandler dah = new();
        DataSet dataSet = new("ShoppingCentre");

        // Customer

        // Add Customer

        public void AddCustomer(string customerID, string customerName, string customerAddress)
        {       
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter adapter = DataAdapterHandler.CustomerAdapter(connection))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    adapter.Fill(dataSet, "Customer");

                    DataTable customerDataTable = dataSet.Tables["Customer"];

                    DataRow CustomerRow = customerDataTable.NewRow();

                    CustomerRow["CustomerID"] = customerID;
                    CustomerRow["CustomerName"] = customerName;
                    CustomerRow["CustomerAddress"] = customerAddress;

                    customerDataTable.Rows.Add(CustomerRow);
                    adapter.Update(dataSet, "Customer");                
                }
            }
        }

        // Get Customers

        public DataSet GetCustomers()
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Customer");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Customer");

                return dataSet;
            }
        }

        // Update Customer

        public void UpdateCustomer(string customerName, string customerAddress, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter adapter = DataAdapterHandler.CustomerAdapter(connection))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    adapter.Fill(dataSet, "Customer");

                    DataTable customerDataTable = dataSet.Tables["Customer"];

                    DataRow CustomerRow = customerDataTable.Rows[indexRow];

                    CustomerRow["CustomerName"] = customerName;
                    CustomerRow["CustomerAddress"] = customerAddress;

                    adapter.Update(dataSet, "Customer");
                }
            }
        }

        // Delete Customer

        public void DeleteCustomer(string customerID, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter adapter = DataAdapterHandler.CustomerAdapter(connection))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    adapter.Fill(dataSet, "Customer");

                    DataTable customerDataTable = dataSet.Tables["Customer"];

                    DataRow CustomerRow = customerDataTable.Rows[indexRow];

                    CustomerRow.Delete();
                    adapter.Update(dataSet, "Customer");
                }
            }
        }

        // Visits

        //Get Visits

        public DataSet GetVisits()
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Visits");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new();

                adapter.Fill(dataSet, "Visits");

                return dataSet;
            }
        }

        // Add Visits

        public void AddVisits(string customerID, string storeName)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter visitsAdapter = DataAdapterHandler.VisitsAdapter(connection))
                {
                    visitsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    visitsAdapter.Fill(dataSet, "Visits");

                    DataTable visitsDataTable = dataSet.Tables["Visits"];

                    DataRow visitsRow = visitsDataTable.NewRow();

                    visitsRow["CustomerCustomerID"] = customerID;
                    visitsRow["StoreStoreName"] = storeName;

                    visitsDataTable.Rows.Add(visitsRow);
                    visitsAdapter.Update(dataSet, "Visits");
                }
            }
        }

        // Delete Visits

        public void DeleteVisits(string customerID, string storeName, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter adapter = DataAdapterHandler.VisitsAdapter(connection))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    adapter.Fill(dataSet, "Visits");

                    DataTable visitsDataTable = dataSet.Tables["Visits"];

                    DataRow visitsRow = visitsDataTable.Rows[indexRow];

                    visitsRow.Delete();
                    adapter.Update(dataSet, "Visits");
                }
            }
        }

        // Employees

        // Get Employees

        public DataSet GetEmployees()
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Employee");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Employee");

                return dataSet;
            }
        }

        // Add Employee

        public void AddEmployee(string employeeID, string employeeName, string employeeAddress, int employeeSalary, string storeName)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter employeeDataAdapter = DataAdapterHandler.EmployeeAdapter(connection))
                {
                    using (SqlDataAdapter storeDataAdapter = DataAdapterHandler.StoreAdapter(connection))
                    {
                        {
                            employeeDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                            employeeDataAdapter.Fill(dataSet, "Employee");
                            storeDataAdapter.Fill(dataSet, "Store");

                            DataTable employeeDataTable = dataSet.Tables["Employee"];
                            DataTable storeDataTable = dataSet.Tables["Store"];
                            DataColumn parentColumn = storeDataTable.Columns["StoreName"];
                            DataColumn childColumn = employeeDataTable.Columns["StoreStoreName"];
                            DataRelation dataRelation = new("work", parentColumn, childColumn);

                            if (!dataSet.Relations.Contains("work"))
                            {
                                dataSet.Relations.Add(dataRelation);
                            }

                            ForeignKeyConstraint foreignKey = employeeDataTable.Constraints[1] as ForeignKeyConstraint;

                            DataRow employeeRow = employeeDataTable.NewRow();

                            employeeRow["EmployeeID"] = employeeID;
                            employeeRow["EmployeeName"] = employeeName;
                            employeeRow["EmployeeAddress"] = employeeAddress;
                            employeeRow["EmployeeSalary"] = employeeSalary;
                            employeeRow["StoreStoreName"] = storeName;

                            employeeDataTable.Rows.Add(employeeRow);
                            employeeDataAdapter.Update(dataSet, "Employee");
                        }
                    }
                }
            }
        }

        //Update Employee

        public void UpdateEmployee(string employeeName, string employeeAddress, int employeeSalary, string storeName, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter employeeAdapter = DataAdapterHandler.EmployeeAdapter(connection))
                {
                    employeeAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    employeeAdapter.Fill(dataSet, "Employee");
                
                    DataTable employeeDataTable = dataSet.Tables["Employee"];

                    DataRow employeeRow = employeeDataTable.Rows[indexRow];

                    employeeRow["EmployeeName"] = employeeName;
                    employeeRow["EmployeeAddress"] = employeeAddress;
                    employeeRow["EmployeeSalary"] = employeeSalary;
                    employeeRow["StoreStoreName"] = storeName;

                    employeeAdapter.Update(dataSet, "Employee");
                }
            }
        }

        // Delete Employee

        public void DeleteEmployee(string employeeID, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter employeeAdapter = DataAdapterHandler.EmployeeAdapter(connection))
                {
                    employeeAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    employeeAdapter.Fill(dataSet, "Employee");
                    
                    DataTable employeeDataTable = dataSet.Tables["Employee"];

                    DataRow employeeRow = employeeDataTable.Rows[indexRow];

                    employeeRow.Delete();

                    employeeAdapter.Update(dataSet, "Employee");
                }
            }
        }


        // Product

        // Get Products

        public DataSet GetProducts()
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Product");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Product");

                return dataSet;
            }
        }

        // Add Product

        public void AddProduct(string productID, string productName, int productPrice)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter productAdapter = DataAdapterHandler.ProductAdapter(connection))
                {
                    productAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    productAdapter.Fill(dataSet, "Product");

                    DataTable productDataTable = dataSet.Tables["Product"];

                    DataRow ProductRow = productDataTable.NewRow();

                    ProductRow["ProductID"] = productID;
                    ProductRow["ProductName"] = productName;
                    ProductRow["ProductPrice"] = productPrice;

                    productDataTable.Rows.Add(ProductRow);
                    productAdapter.Update(dataSet, "Product");
                }
           
            }
      
        }

        // Update Product

        public void UpdateProduct(string productName, int productPrice, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter productAdapter = DataAdapterHandler.ProductAdapter(connection))
                {
                    productAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    productAdapter.Fill(dataSet, "Product");

                    DataTable productDataTable = dataSet.Tables["Product"];

                    DataRow productRow = productDataTable.Rows[indexRow];

                    productRow["ProductName"] = productName;
                    productRow["ProductPrice"] = productPrice;

                    productAdapter.Update(dataSet, "Product");
                }

            }

        }

        // Delete Product

        public void DeleteProduct(string productID, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter productAdapter = DataAdapterHandler.ProductAdapter(connection))
                {
                    productAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    productAdapter.Fill(dataSet, "Product");

                    DataTable productDataTable = dataSet.Tables["Product"];

                    DataRow productRow = productDataTable.Rows[indexRow];

                    productRow.Delete();

                    productAdapter.Update(dataSet, "Product");
                }
            }
        }

        // Sells

        //Get Sells

        public DataSet GetSells()
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Sells");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new();

                adapter.Fill(dataSet, "Sells");

                return dataSet;
            }
        }

        // Add Sells

        public void AddSells(string productID, string storeName)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter sellsAdapter = DataAdapterHandler.SellsAdapter(connection))
                {
                    sellsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    sellsAdapter.Fill(dataSet, "Sells");

                    DataTable sellsDataTable = dataSet.Tables["Sells"];

                    DataRow sellsRow = sellsDataTable.NewRow();

                    sellsRow["ProductProductID"] = productID;
                    sellsRow["StoreStoreName"] = storeName;

                    sellsDataTable.Rows.Add(sellsRow);
                    sellsAdapter.Update(dataSet, "Sells");
                }
            }
        }

        // Delete Sells

        public void DeleteSells(string productID, string storeName, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter sellsAdapter = DataAdapterHandler.SellsAdapter(connection))
                {
                    sellsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    sellsAdapter.Fill(dataSet, "Sells");

                    DataTable sellsDataTable = dataSet.Tables["Sells"];

                    DataRow sellsRow = sellsDataTable.Rows[indexRow];

                    sellsRow.Delete();
                    sellsAdapter.Update(dataSet, "Sells");
                }
            }
        }

        // Store

        // Get Store

        public DataSet GetStore()
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Store");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Store");

                return dataSet;
            }
        }

        // Add Store

        public void AddStore(string storeName, int storeRent, string shoppingCentreScName)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter storeDataAdapter = DataAdapterHandler.StoreAdapter(connection))
                {
                    using (SqlDataAdapter shoppingCentreAdapter = DataAdapterHandler.ShoppingCentreAdapter(connection))
                    {
                        storeDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        storeDataAdapter.Fill(dataSet, "Store");
                        shoppingCentreAdapter.Fill(dataSet, "ShoppingCentre");

                        DataTable storeDataTable = dataSet.Tables["Store"];
                        DataTable shoppingCentreDataTable = dataSet.Tables["ShoppingCentre"];
                        DataColumn parentColumn = shoppingCentreDataTable.Columns["ScName"];
                        DataColumn childColumn = storeDataTable.Columns["ShoppingCentreScName"];
                        DataRelation dataRelation = new("has", parentColumn, childColumn);
                        
                        if (!dataSet.Relations.Contains("has"))
                        {
                            dataSet.Relations.Add(dataRelation);
                        }

                        ForeignKeyConstraint foreignKeyConstraint = storeDataTable.Constraints[1] as ForeignKeyConstraint;

                        DataRow storeRow = storeDataTable.NewRow();

                        storeRow["StoreName"] = storeName;
                        storeRow["StoreRent"] = storeRent;
                        storeRow["ShoppingCentreScName"] = shoppingCentreScName;

                        storeDataTable.Rows.Add(storeRow);
                        storeDataAdapter.Update(dataSet, "Store");
                    }
                }
            }
        }

        // Update Store

        public void UpdateStore(int storeRent, string shoppingCentreScName, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter storeDataAdapter = DataAdapterHandler.StoreAdapter(connection))
                {              
                    storeDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    storeDataAdapter.Fill(dataSet, "Store");

                    DataTable storeDataTable = dataSet.Tables["Store"];
                    DataRow storeRow = storeDataTable.Rows[indexRow];
  
                    storeRow["StoreRent"] = storeRent;
                    storeRow["ShoppingCentreScName"] = shoppingCentreScName;

                    storeDataAdapter.Update(dataSet, "Store");             
                }
            }
        }

        // Delete Store

        public void DeleteStore(string storeName, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter storeAdapter = DataAdapterHandler.StoreAdapter(connection))
                {
                    storeAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    storeAdapter.Fill(dataSet, "Store");

                    DataTable storeDataTable = dataSet.Tables["Store"];

                    DataRow storeRow = storeDataTable.Rows[indexRow];

                    storeRow.Delete();

                    storeAdapter.Update(dataSet, "Store");
                }
            }
        }

        // Shopping Centre

        // Get Shopping Centre

        public DataSet GetShoppingCentre()
        {           
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM ShoppingCentre");

                command.Connection = connection;
                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Shopping Centre");

                return dataSet;
            }         
        }

        public void AddShoppingCentre(string scName, string scAddress, string scCity)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter shoppingCentreAdapter = DataAdapterHandler.ShoppingCentreAdapter(connection))
                {
                    shoppingCentreAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    shoppingCentreAdapter.Fill(dataSet, "ShoppingCentre");

                    DataTable ShoppingCentreDataTable = dataSet.Tables["ShoppingCentre"];

                    DataRow ShoppingCentreRow = ShoppingCentreDataTable.NewRow();

                    ShoppingCentreRow["SCName"] = scName;
                    ShoppingCentreRow["SCAddress"] = scAddress;
                    ShoppingCentreRow["SCCity"] = scCity;

                    ShoppingCentreDataTable.Rows.Add(ShoppingCentreRow);
                    shoppingCentreAdapter.Update(dataSet, "ShoppingCentre");
                }
            }
        }

        // Update ShoppingCentre

        public void UpdateShoppingCentre(string scAddress, string scCity, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter shoppingCentreAdapter = DataAdapterHandler.ShoppingCentreAdapter(connection))
                {
                    shoppingCentreAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    shoppingCentreAdapter.Fill(dataSet, "ShoppingCentre");

                    DataTable ShoppingCentreDataTable = dataSet.Tables["ShoppingCentre"];

                    DataRow ShoppingCentreRow = ShoppingCentreDataTable.Rows[indexRow];

                    ShoppingCentreRow["SCAddress"] = scAddress;
                    ShoppingCentreRow["SCCity"] = scCity;

                    shoppingCentreAdapter.Update(dataSet, "ShoppingCentre"); 
                }
            }
        }

        // Delete Shoppingcentre 

        public void DeleteShoppingCentre(string scName, int indexRow)
        {
            using (SqlConnection connection = dah.GetDatabaseConnection())
            {
                using (SqlDataAdapter shoppingCentreAdapter = DataAdapterHandler.ShoppingCentreAdapter(connection))
                {
                    shoppingCentreAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    shoppingCentreAdapter.Fill(dataSet, "ShoppingCentre");

                    DataTable shoppingcentreDataTable = dataSet.Tables["ShoppingCentre"];

                    DataRow ShoppingCentreRow = shoppingcentreDataTable.Rows[indexRow];

                    ShoppingCentreRow.Delete();
                    shoppingCentreAdapter.Update(dataSet, "ShoppingCentre");
                }
            }
        }


    }
}

