using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShoppingCentre
{
    public partial class ShoppingCentreForm : Form
    {
        DataAccessLayer dah = new();
        int indexRow;

        public ShoppingCentreForm()
        {
            InitializeComponent();
        }

        private void ShoppingCentreForm_Load(object sender, EventArgs e)
        {
            PopulateData();
            dgvCustomer.CurrentCell = null;
        }

        // Populate Data

        public void PopulateData()
        {
            dgvCustomer.DataSource = dah.GetCustomers().Tables["Customer"];
            dgvCustomer.Columns[0].HeaderText = "ID";
            dgvCustomer.Columns[1].HeaderText = "Name";
            dgvCustomer.Columns[2].HeaderText = "Address";

            dgvVisits.DataSource = dah.GetVisits().Tables["Visits"];
            cmbVisitsId.DataSource = dah.GetCustomers().Tables["Customer"];
            cmbVisitsName.DataSource = dah.GetStore().Tables["Store"];
            cmbVisitsId.ValueMember = "CustomerID";
            cmbVisitsName.ValueMember = "StoreName";
            dgvVisits.Columns[0].HeaderText = "Customer ID";
            dgvVisits.Columns[1].HeaderText = "Store Name";

            dgvEmployee.DataSource = dah.GetEmployees().Tables[0];
            dgvEmployee.Columns[0].HeaderText = "ID";
            dgvEmployee.Columns[1].HeaderText = "Name";
            dgvEmployee.Columns[2].HeaderText = "Address";
            dgvEmployee.Columns[3].HeaderText = "Salary";
            dgvEmployee.Columns[4].HeaderText = "StoreName";

            dgvProducts.DataSource = dah.GetProducts().Tables[0];
            dgvProducts.Columns[0].HeaderText = "ID";
            dgvProducts.Columns[1].HeaderText = "Name";
            dgvProducts.Columns[2].HeaderText = "Price";

            dgvSells.DataSource = dah.GetSells().Tables["Sells"];
            cmbSellsId.DataSource = dah.GetProducts().Tables["Product"];
            cmbSellsName.DataSource = dah.GetStore().Tables["Store"];
            cmbSellsId.ValueMember = "ProductID";
            cmbSellsName.ValueMember = "StoreName";
            dgvSells.Columns[0].HeaderText = "Product ID";
            dgvSells.Columns[1].HeaderText = "Store Name";

            dgvStore.DataSource = dah.GetStore().Tables[0];
            cmbEmployeeStore.DataSource = dah.GetStore().Tables[0];
            cmbEmployeeStore.ValueMember = "StoreName";
            dgvStore.DataSource = dah.GetStore().Tables[0];
            dgvStore.Columns[0].HeaderText = "Name";
            dgvStore.Columns[1].HeaderText = "Rent";
            dgvStore.Columns[2].HeaderText = "Shopping Centre";

            dgvShoppingCentre.DataSource = dah.GetShoppingCentre().Tables[0];
            cmbStoreSC.DataSource = dah.GetShoppingCentre().Tables[0];
            cmbStoreSC.ValueMember = "ScName";
            txtStoreName.Clear();
            txtStoreRent.Clear();
            dgvShoppingCentre.DataSource = dah.GetShoppingCentre().Tables[0];
            dgvShoppingCentre.Columns[0].HeaderText = "Name";
            dgvShoppingCentre.Columns[1].HeaderText = "Address";
            dgvShoppingCentre.Columns[2].HeaderText = "City";
        }

        // Tab Control Switch

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl.SelectedTab == tabCustomer)
            {
                dgvCustomer.CurrentCell = null;
            }
            else if(tabControl.SelectedTab == tabVisits)
            {
                dgvVisits.CurrentCell = null;
            }
            else if(tabControl.SelectedTab == tabStore)
            {
                dgvStore.CurrentCell = null;
            }
            else if (tabControl.SelectedTab == tabSells)
            {
                dgvSells.CurrentCell = null;
            }
            else if (tabControl.SelectedTab == tabProducts)
            {
                dgvProducts.CurrentCell = null;
            }
            else if(tabControl.SelectedTab == tabShoppingCentre)
            {
                dgvShoppingCentre.CurrentCell = null;
            }
            else if(tabControl.SelectedTab == tabEmployee)
            {
                dgvEmployee.CurrentCell = null;
            }
        }

        //DGV Cell Click:

        //Customer


        private void CustomerDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];

                txtCustomerId.Text = row.Cells[0].Value.ToString();
                txtCustomerName.Text = row.Cells[1].Value.ToString();
                txtCustomerAddress.Text = row.Cells[2].Value.ToString();
            }
        }

        // Visits

        private void DgvVisits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvVisits.Rows[indexRow];

                cmbVisitsId.SelectedValue = row.Cells[0].Value.ToString();
                cmbVisitsName.SelectedValue = row.Cells[1].Value.ToString();
            }
        }

        // Sells

        private void DgvSells_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvSells.Rows[indexRow];

                cmbSellsId.SelectedValue = row.Cells[0].Value.ToString();
                cmbSellsName.SelectedValue = row.Cells[1].Value.ToString();
            }
        }


        // Store

        private void StoreDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;

            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvStore.Rows[indexRow];

                txtStoreName.Text = row.Cells[0].Value.ToString();
                txtStoreRent.Text = row.Cells[1].Value.ToString();
            }
        }

        // Employee

        private void EmployeeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;

            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];

                txtEmployeeId.Text = row.Cells[0].Value.ToString();
                txtEmployeeName.Text = row.Cells[1].Value.ToString();
                txtEmployeeAddress.Text = row.Cells[2].Value.ToString();
                txtEmployeeSalary.Text = row.Cells[3].Value.ToString();
                cmbEmployeeStore.SelectedValue = row.Cells[4].Value.ToString();
            }
        }

        // Product

        private void ProductsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvProducts.Rows[indexRow];

                txtProductId.Text = row.Cells[0].Value.ToString();
                txtProductName.Text = row.Cells[1].Value.ToString();
                txtProductPrice.Text = row.Cells[2].Value.ToString();
            }
        }

        // Sells

        private void SellsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvSells.Rows[indexRow];

                cmbSellsId.SelectedValue = row.Cells[0].Value.ToString();
                cmbSellsName.SelectedValue = row.Cells[1].Value.ToString();
            }
        }

        // Shopping Centre

        private void ShoppingCentreDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = dgvShoppingCentre.Rows[e.RowIndex];

                txtSCName.Text = row.Cells[0].Value.ToString();
                txtSCAddress.Text = row.Cells[1].Value.ToString();
                txtSCCity.Text = row.Cells[2].Value.ToString();
            }
        }

        // Buttons

        // Customer Buttons

        private void BtnAddCustomerButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            string randomCustomerID = "C" + rand.Next(1000, 9999);
            string customerName = txtCustomerName.Text;
            string customerAddress = txtCustomerAddress.Text;

            try
            {
                if (customerName == "" && customerAddress == "")
                {
                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.Maroon;
                    txtResponseCustomer.Text = "Please fill in all the fields.";
                }
                else
                {
                    dah.AddCustomer(randomCustomerID, customerName, customerAddress);

                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.MediumSeaGreen;
                    txtResponseCustomer.Text = $"Customer with the ID {randomCustomerID} was successfully added!";

                    dgvCustomer.DataSource = dah.GetCustomers().Tables["Customer"];
                    cmbVisitsId.DataSource = dah.GetCustomers().Tables["Customer"];

                    txtCustomerId.Text = null;
                    txtCustomerName.Text = null;
                    txtCustomerAddress.Text = null;
                    dgvCustomer.CurrentCell = null;
                }
            }
            catch (ConstraintException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Customer ID already exists.";
            }
        }

        private void BtnUpdateCustomerButton_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerId.Text;
            string customerName = txtCustomerName.Text;
            string customerAddress = txtCustomerAddress.Text;

            try
            {
                if (dgvCustomer.CurrentRow == null)
                {
                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.Maroon;
                    txtResponseCustomer.Text = "Please select a row in the gridview.";
                }
                else if(txtCustomerName.TextLength == 0 && txtCustomerAddress.TextLength == 0)
                {
                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.Maroon;
                    txtResponseCustomer.Text = "Please fill in all the fields.";
                }
                else
                {
                    dah.UpdateCustomer(customerName, customerAddress, indexRow);
                    dgvCustomer.DataSource = dah.GetCustomers().Tables["Customer"];

                    txtCustomerId.Text = null;
                    txtCustomerName.Text = null;
                    txtCustomerAddress.Text = null;
                    dgvCustomer.CurrentCell = null;

                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.MediumSeaGreen;
                    txtResponseCustomer.Text = $"Customer {customerID} has been updated!";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseCustomer.Visible = true;
                txtResponseCustomer.ForeColor = Color.Maroon;
                txtResponseCustomer.Text = "Please select a row in the gridview.";
            }
        }

        private void BtnDeleteCustomer_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerId.Text;

            try
            {
                if (dgvCustomer.CurrentRow == null)
                {
                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.Maroon;
                    txtResponseCustomer.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteCustomer(customerID, indexRow);
                    dgvCustomer.DataSource = dah.GetCustomers().Tables["Customer"];
                    cmbVisitsId.DataSource = dah.GetCustomers().Tables["Customer"];

                    txtCustomerId.Text = null;
                    txtCustomerName.Text = null;
                    txtCustomerAddress.Text = null;
                    dgvCustomer.CurrentCell = null;

                    txtResponseCustomer.Visible = true;
                    txtResponseCustomer.ForeColor = Color.MediumSeaGreen;
                    txtResponseCustomer.Text = $"Customer {customerID} and all their references have been been deleted!";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseCustomer.Visible = true;
                txtResponseCustomer.ForeColor = Color.Maroon;
                txtResponseCustomer.Text = "Please select a row in the gridview.";
            }
        }

        //Visits Buttons

        private void BtnVisitsAdd_Click(object sender, EventArgs e)
        {
            string visitsID = cmbVisitsId.SelectedValue.ToString();
            string visitsName = cmbVisitsName.SelectedValue.ToString();

            try
            {
                dah.AddVisits(visitsID, visitsName);
                PopulateData();

                dgvVisits.CurrentCell = null;

                txtResponseVisits.Visible = true;
                txtResponseVisits.ForeColor = Color.MediumSeaGreen;
                txtResponseVisits.Text = $"Success!\r\nCustomer with the ID: {visitsID} \r\nwas successfully registered for: \r\n{visitsName}.";
            }
            catch (ConstraintException ex)
            {
                txtResponseVisits.Visible = true;
                txtResponseVisits.ForeColor = Color.Maroon;
                txtResponseVisits.Text = $"Error:\r\nA user with the ID: {visitsID} \r\nvisiting: {visitsName} \r\nis already registered in the database.";
            }
            catch (SqlException ex)
            {
                txtResponseVisits.Visible = true;
                txtResponseVisits.ForeColor = Color.Maroon;
                txtResponseVisits.Text = $"Error:\r\nA user with the ID: {visitsID} \r\nvisiting: {visitsName} \r\nis already registered in the database.";
            }
        }

        private void BtnVisitsDelete_Click(object sender, EventArgs e)
        {
            string visitsID = cmbVisitsId.SelectedValue.ToString();
            string visitsName = cmbVisitsName.SelectedValue.ToString();

            try
            {
                if (dgvVisits.CurrentRow == null)
                {
                    txtResponseVisits.Visible = true;
                    txtResponseVisits.ForeColor = Color.Maroon;
                    txtResponseVisits.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteVisits(visitsID, visitsName, indexRow);
                    PopulateData();

                    dgvVisits.CurrentCell = null;

                    txtResponseVisits.Visible = true;
                    txtResponseVisits.ForeColor = Color.MediumSeaGreen;
                    txtResponseVisits.Text = $"Success!\r\nCustomer {visitsID} visiting store {visitsName} has been deleted.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Sells Buttons

        private void BtnSellsAdd_Click(object sender, EventArgs e)
        {
            string sellsID = cmbSellsId.SelectedValue.ToString();
            string sellsName = cmbSellsName.SelectedValue.ToString();

            try
            {
                dah.AddSells(sellsID, sellsName);
                PopulateData();

                dgvSells.CurrentCell = null;

                txtResponseSells.Visible = true;
                txtResponseSells.ForeColor = Color.MediumSeaGreen;
                txtResponseSells.Text = $"Success!\r\nProduct with the ID: {sellsID} \r\nwas successfully registered for: \r\n{sellsName}.";
            }
            catch (ConstraintException ex)
            {
                txtResponseSells.Visible = true;
                txtResponseSells.ForeColor = Color.Maroon;
                txtResponseSells.Text = $"Error:\r\nA product with the ID: {sellsID} \r\nsold at: {sellsName} \r\nis already registered in the database.";
            }
            catch (SqlException ex)
            {
                txtResponseSells.Visible = true;
                txtResponseSells.ForeColor = Color.Maroon;
                txtResponseSells.Text = $"Error:\r\nA product with the ID: {sellsID} \r\nsold at: {sellsName} \r\nis already registered in the database.";
            }
        }

        private void BtnSellsDelete_Click(object sender, EventArgs e)
        {
            string sellsID = cmbSellsId.SelectedValue.ToString();
            string sellsName = cmbSellsName.SelectedValue.ToString();

            try
            {
                if (dgvSells.CurrentRow == null)
                {
                    txtResponseSells.Visible = true;
                    txtResponseSells.ForeColor = Color.Maroon;
                    txtResponseSells.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteSells(sellsID, sellsName, indexRow);
                    PopulateData();

                    dgvSells.CurrentCell = null;

                    txtResponseSells.Visible = true;
                    txtResponseSells.ForeColor = Color.MediumSeaGreen;
                    txtResponseSells.Text = $"Success!\r\nProduct {sellsID} sold in store {sellsName} has been deleted.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Employee Buttons

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            string randomEmployeeID = "E" + rand.Next(1000, 9999);
            string employeeName = txtEmployeeName.Text;
            string employeeAddress = txtEmployeeAddress.Text;
            string stringEmployeeSalary = txtEmployeeSalary.Text;
            string storeName = cmbEmployeeStore.SelectedValue.ToString();

            try
            {
                if (employeeName == "" && employeeAddress == "" && stringEmployeeSalary == "")
                {
                    txtResponseEmployee.Visible = true;
                    txtResponseEmployee.ForeColor = Color.Maroon;
                    txtResponseEmployee.Text = "Please fill in all the fields.";
                }
                else
                {
                    int newEmployeeSalary = int.Parse(stringEmployeeSalary);
                    dah.AddEmployee(randomEmployeeID, employeeName, employeeAddress, newEmployeeSalary, storeName);
                    txtResponseEmployee.Visible = true;
                    txtResponseEmployee.ForeColor = Color.MediumSeaGreen;
                    txtResponseEmployee.Text = $"Employee with the ID {randomEmployeeID} was successfully added!";
                    PopulateData();

                    txtEmployeeId.Text = null;
                    txtEmployeeName.Text = null;
                    txtEmployeeAddress.Text = null;
                    txtEmployeeSalary.Text = null;
                    dgvEmployee.CurrentCell = null;
                }
            }

            catch (FormatException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Make sure the salary is a number.";
            }
            catch (OverflowException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Salary number is too big, please try a smaller number.";
            }
            catch (ConstraintException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Employee ID already exists.";
            }
            catch (NullReferenceException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Make sure that a Store is chosen";
            }
        }

        private void BtnUpdateEmployee_Click(object sender, EventArgs e)
        {
            string employeeID = txtEmployeeId.Text;
            string employeeName = txtEmployeeName.Text;
            string employeeAddress = txtEmployeeAddress.Text;
            string employeeSalaryString = txtEmployeeSalary.Text;
            string storeName = cmbEmployeeStore.SelectedValue.ToString();

            try
            {
                if (dgvEmployee.CurrentRow == null)
                {
                    txtResponseEmployee.Visible = true;
                    txtResponseEmployee.ForeColor = Color.Maroon;
                    txtResponseEmployee.Text = "Please select a row in the gridview.";
                }

                else
                {
                    int employeeSalary = int.Parse(employeeSalaryString);
                    dah.UpdateEmployee(employeeName, employeeAddress, employeeSalary, storeName, indexRow);
                    PopulateData();
                    txtResponseEmployee.Visible = true;
                    txtResponseEmployee.ForeColor = Color.MediumSeaGreen;
                    txtResponseEmployee.Text = $"Employee {employeeID} has been updated!";

                    txtEmployeeId.Text = null;
                    txtEmployeeName.Text = null;
                    txtEmployeeAddress.Text = null;
                    txtEmployeeSalary.Text = null;
                    dgvEmployee.CurrentCell = null;
                }
            }

            catch (IndexOutOfRangeException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Please select a row in the gridview.";
            }

            catch (FormatException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Make sure the salary is a number.";
            }
            catch (OverflowException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Salary number is too big, please try a smaller number.";
            }
        }

        private void BtnDeleteEmployee_Click(object sender, EventArgs e)
        {
            string employeeID = txtEmployeeId.Text;
            try
            {
                if (dgvEmployee.CurrentRow == null)
                {
                    txtResponseEmployee.Visible = true;
                    txtResponseEmployee.ForeColor = Color.Maroon;
                    txtResponseEmployee.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteEmployee(employeeID, indexRow);
                    PopulateData();

                    txtEmployeeId.Text = null;
                    txtEmployeeName.Text = null;
                    txtEmployeeAddress.Text = null;
                    txtEmployeeSalary.Text = null;
                    dgvEmployee.CurrentCell = null;

                    txtResponseEmployee.Visible = true;
                    txtResponseEmployee.ForeColor = Color.MediumSeaGreen;
                    txtResponseEmployee.Text = $"Employee {employeeID} and all their references has been deleted!";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseEmployee.Visible = true;
                txtResponseEmployee.ForeColor = Color.Maroon;
                txtResponseEmployee.Text = "Please select a row in the gridview.";
            }
        }

        // Product Buttons

        private void BtnAddProducts_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            string randomProductID = "P" + rand.Next(1, 9999);
            string productName = txtProductName.Text;
            string stringProductPrice = txtProductPrice.Text;

            try
            {
                if (productName == "" && stringProductPrice == "")
                {
                    txtResponseProducts.Visible = true;
                    txtResponseProducts.ForeColor = Color.Maroon;
                    txtResponseProducts.Text = "Please fill in all the fields.";
                }
                else
                {
                    int productPrice = int.Parse(stringProductPrice);
                    dah.AddProduct(randomProductID, productName, productPrice);
                    txtResponseProducts.Visible = true;
                    txtResponseProducts.ForeColor = Color.MediumSeaGreen;
                    txtResponseProducts.Text = $"Product with the ID {randomProductID} was successfully added!";
                    PopulateData();

                    txtProductId.Text = null;
                    txtProductName.Text = null;
                    txtProductPrice.Text = null;
                    dgvProducts.CurrentCell = null;
                }
            }

            catch (FormatException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Make sure the price is a number.";
            }
            catch (OverflowException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Price number is too big, please try a smaller number.";
            }
            catch (ConstraintException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Product ID already exists.";
            }
        }

        private void BtnUpdateProducts_Click(object sender, EventArgs e)
        {
            string productID = txtProductId.Text;
            string productName = txtProductName.Text;
            string productPriceString = txtProductPrice.Text;

            try
            {
                if (dgvProducts.CurrentRow == null)
                {
                    txtResponseProducts.Visible = true;
                    txtResponseProducts.ForeColor = Color.Maroon;
                    txtResponseProducts.Text = "Please select a row in the gridview.";
                }
                else
                {
                    int productPrice = int.Parse(productPriceString);

                    dah.UpdateProduct(productName, productPrice, indexRow);
                    PopulateData();
                    txtResponseProducts.Visible = true;
                    txtResponseProducts.ForeColor = Color.MediumSeaGreen;
                    txtResponseProducts.Text = $"Product {productID} has been updated!";

                    txtProductId.Text = null;
                    txtProductName.Text = null;
                    txtProductPrice.Text = null;
                    dgvProducts.CurrentCell = null;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Please select a row in the gridview.";
            }
            catch (FormatException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Make sure the price is a number.";
            }
            catch (OverflowException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Price number is too big,please try a smaller number.";

            }
        }

        private void BtnDeleteProducts_Click(object sender, EventArgs e)
        {
            string productID = txtProductId.Text;
            try
            {
                if (dgvProducts.CurrentRow == null)
                {
                    txtResponseProducts.Visible = true;
                    txtResponseProducts.ForeColor = Color.Maroon;
                    txtResponseProducts.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteProduct(productID, indexRow);
                    PopulateData();

                    txtProductId.Text = null;
                    txtProductName.Text = null;
                    txtProductPrice.Text = null;
                    dgvProducts.CurrentCell = null;

                    txtResponseProducts.Visible = true;
                    txtResponseProducts.ForeColor = Color.MediumSeaGreen;
                    txtResponseProducts.Text = $"Product {productID} and all their references have been deleted!";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseProducts.Visible = true;
                txtResponseProducts.ForeColor = Color.Maroon;
                txtResponseProducts.Text = "Please select a row in the gridview.";
            }
        }

        // ShoppingCentre Buttons

        private void BtnAddShoppingCentre_Click(object sender, EventArgs e)
        {
            string scName = txtSCName.Text;
            string scAddress = txtSCAddress.Text;
            string scCity = txtSCCity.Text;

            try
            {
                if (scName == "" || scAddress == "" || scCity == "")
                {
                    txtResponseShoppingCentre.Visible = true;
                    txtResponseShoppingCentre.ForeColor = Color.Maroon;
                    txtResponseShoppingCentre.Text = "Please fill in all the fields.";
                }
                else
                {
                    dah.AddShoppingCentre(scName, scAddress, scCity);
                    txtResponseShoppingCentre.Visible = true;
                    txtResponseShoppingCentre.ForeColor = Color.MediumSeaGreen;
                    txtResponseShoppingCentre.Text = $"Shopping Centre with the name {scName} was successfully added!";
                    PopulateData();

                    txtSCName.Text = null;
                    txtSCAddress.Text = null;
                    txtSCCity.Text = null;
                    dgvShoppingCentre.CurrentCell = null;
                }
            }
            catch (ConstraintException ex)
            {
                txtResponseShoppingCentre.Visible = true;
                txtResponseShoppingCentre.ForeColor = Color.Maroon;
                txtResponseShoppingCentre.Text = "Shopping Centre Name already exists.";
            }
        }

        private void BtnUpdateShoppingCentre_Click(object sender, EventArgs e)
        {
            string scName = txtSCName.Text;
            string scAddress = txtSCAddress.Text;
            string scCity = txtSCCity.Text;

            try
            {
                if (dgvShoppingCentre.CurrentRow == null)
                {
                    txtResponseShoppingCentre.Visible = true;
                    txtResponseShoppingCentre.ForeColor = Color.Maroon;
                    txtResponseShoppingCentre.Text = "Please select a row in the gridview.";
                }

                else
                {
                    dah.UpdateShoppingCentre(scAddress, scCity, indexRow);
                    PopulateData();
                    txtResponseShoppingCentre.Visible = true;
                    txtResponseShoppingCentre.ForeColor = Color.MediumSeaGreen;
                    txtResponseShoppingCentre.Text = $"Shopping Centre {scName} has been updated!";

                    txtSCName.Text = null;
                    txtSCAddress.Text = null;
                    txtSCCity.Text = null;
                    dgvShoppingCentre.CurrentCell = null;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseShoppingCentre.Visible = true;
                txtResponseShoppingCentre.ForeColor = Color.Maroon;
                txtResponseShoppingCentre.Text = "Please select a row in the gridview.";
            }
        }

        private void BtnDeleteShoppingCentre_Click(object sender, EventArgs e)
        {
            string scName = txtSCName.Text;

            try
            {
                if (dgvShoppingCentre.CurrentRow == null)
                {
                    txtResponseShoppingCentre.Visible = true;
                    txtResponseShoppingCentre.ForeColor = Color.Maroon;
                    txtResponseShoppingCentre.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteShoppingCentre(scName, indexRow);
                    PopulateData();

                    txtSCName.Text = null;
                    txtSCAddress.Text = null;
                    txtSCCity.Text = null;
                    dgvShoppingCentre.CurrentCell = null;

                    txtResponseShoppingCentre.Visible = true;
                    txtResponseShoppingCentre.ForeColor = Color.MediumSeaGreen;
                    txtResponseShoppingCentre.Text = $"Shopping Centre {scName} and all its references have been deleted!";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseShoppingCentre.Visible = true;
                txtResponseShoppingCentre.ForeColor = Color.Maroon;
                txtResponseShoppingCentre.Text = "Please select a row in the gridview.";
            }
        }

        // Store

        private void BtnAddStore_Click(object sender, EventArgs e)
        {
            string storeName = txtStoreName.Text;
            string storeRentString = txtStoreRent.Text;
            string shoppingCentre = cmbStoreSC.SelectedValue.ToString();

            try
            {
                if (storeName == "" || storeRentString == "")
                {
                    txtResponseStore.Visible = true;
                    txtResponseStore.ForeColor = Color.Maroon;
                    txtResponseStore.Text = "Please fill in all the fields.";
                }
                else
                {
                    int storeRent = int.Parse(storeRentString);
                    dah.AddStore(storeName, storeRent, shoppingCentre);
                    txtResponseStore.Visible = true;
                    txtResponseStore.ForeColor = Color.MediumSeaGreen;
                    txtResponseStore.Text = $"Store with the name {storeName} was successfully added!";
                    PopulateData();

                    txtStoreName.Text = null;
                    txtStoreRent.Text = null;
                    dgvStore.CurrentCell = null;
                }
            }
            catch (FormatException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Make sure the rent is a number.";
            }
            catch (OverflowException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Rent number is too big, please try a smaller number";
            }
            catch (ConstraintException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Store Name already exists.";
            }
            catch (SqlException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Store Name already exists.";
            }
        }

        private void BtnUpdateStore_Click(object sender, EventArgs e)
        {
            string storeName = txtStoreName.Text;
            string storeRentString = txtStoreRent.Text;
            string shoppingCentre = cmbStoreSC.SelectedValue.ToString();

            try
            {
                if (dgvStore.CurrentRow == null)
                {
                    txtResponseStore.Visible = true;
                    txtResponseStore.ForeColor = Color.Maroon;
                    txtResponseStore.Text = "Please select a row in the gridview.";
                }
                else if (txtStoreName.TextLength > 0)
                {
                    foreach (DataGridViewRow row in dgvStore.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == storeName)
                        {
                            int storeRent = int.Parse(storeRentString);
                            dah.UpdateStore(storeRent, shoppingCentre, indexRow);
                            PopulateData();
                            txtResponseStore.Visible = true;
                            txtResponseStore.ForeColor = Color.MediumSeaGreen;
                            txtResponseStore.Text = $"Store {storeName} has been updated!";

                            txtStoreName.Text = null;
                            txtStoreRent.Text = null;
                            dgvStore.CurrentCell = null;
                            break;
                        }
                        else
                        {
                            txtResponseStore.Visible = true;
                            txtResponseStore.ForeColor = Color.Maroon;
                            txtResponseStore.Text = "Store Name cannot be updated. if you wish to change it, add a new store.";
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Please select a row in the gridview.";
            }

            catch (FormatException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Make sure the rent is a number.";
            }
            catch (OverflowException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Rent number is too big, please try a smaller number.";
            }
        }

        // Delete Store

        private void BtnDeleteStore_Click(object sender, EventArgs e)
        {
            string storeName = txtStoreName.Text;
            try
            {
                if (dgvStore.CurrentRow == null)
                {
                    txtResponseStore.Visible = true;
                    txtResponseStore.ForeColor = Color.Maroon;
                    txtResponseStore.Text = "Please select a row in the gridview.";
                }
                else
                {
                    dah.DeleteStore(storeName, indexRow);
                    PopulateData();

                    txtStoreName.Text = null;
                    txtStoreRent.Text = null;
                    dgvStore.CurrentCell = null;

                    txtResponseStore.Visible = true;
                    txtResponseStore.ForeColor = Color.MediumSeaGreen;
                    txtResponseStore.Text = $"Store {storeName} and all its references have been deleted!";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                txtResponseStore.Visible = true;
                txtResponseStore.ForeColor = Color.Maroon;
                txtResponseStore.Text = "Please select a row in the gridview.";
            }
        }


    }
}