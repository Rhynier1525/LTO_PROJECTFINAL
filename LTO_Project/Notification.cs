using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTO_Project
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
            LoadCustomerData(); // Automatically load data when form opens
            LoadAvailableData(); // Load only available plates
        }
        private void LoadAvailableData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = "SELECT customer_id AS 'Customer ID', vehicle_id AS 'Vehicle ID', " +
                                   "status AS 'Status', date_arrived AS 'Date Arrived' " +
                                   "FROM bulk_upload WHERE status = 'Available'";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        availableDatagrid.DataSource = dt; // Assign data to Available DataGridView
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadCustomerData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = "SELECT customer_id AS 'Customer ID', contact_number AS 'Contact Number', " +
                                   "first_name AS 'First Name', last_name AS 'Last Name', " +
                                   "birthday AS 'Birthday', gender AS 'Gender', address AS 'Address' " +
                                   "FROM customer";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        customerData.DataSource = dt; // Assign data to DataGridView
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void customerDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure this function is used only when needed
        }

        private void addcustomerBtn_Click(object sender, EventArgs e)
        {
            addCustomer customer = new addCustomer();
            customer.ShowDialog();

            // Reload customer data after adding
            LoadCustomerData();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            LoadCustomerData(); // Refresh DataGridView
        }

        private void availableDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void donebtn_Click(object sender, EventArgs e)
        {
            if (availableDatagrid.SelectedRows.Count > 0)
            {
                string customerID = availableDatagrid.SelectedRows[0].Cells["Customer ID"].Value.ToString();
                string vehicleID = availableDatagrid.SelectedRows[0].Cells["Vehicle ID"].Value.ToString();

                string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
                using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
                {
                    try
                    {
                        mySqlConnection.Open();
                        string query = "UPDATE bulk_upload SET status = 'Claimed' WHERE customer_id = @customerID AND vehicle_id = @vehicleID";

                        using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@customerID", customerID);
                            cmd.Parameters.AddWithValue("@vehicleID", vehicleID);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Status updated to Claimed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadAvailableData(); // Refresh Available Plates DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}