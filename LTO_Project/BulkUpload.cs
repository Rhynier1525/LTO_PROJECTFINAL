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
    public partial class BulkUpload : Form
    {
        public BulkUpload()
        {
            InitializeComponent();
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
        private void BulkUpload_Load(object sender, EventArgs e)
        {

        }
        private void customerIDbtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void vehicleIDbtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void plateBtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void arrivedDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void statusCombox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uploadDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            // Check if any required field is empty
            if (string.IsNullOrWhiteSpace(customerIDbtn.Text) ||
                string.IsNullOrWhiteSpace(vehicleIDbtn.Text) ||
                string.IsNullOrWhiteSpace(plateBtn.Text) ||
                statusCombox.SelectedIndex == -1)
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string customerID = customerIDbtn.Text;
            string vehicleID = vehicleIDbtn.Text;

            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();

                    // Check for duplicate customer_id
                    string checkCustomerQuery = "SELECT COUNT(*) FROM bulk_upload WHERE customer_id = @customer_id";
                    using (MySqlCommand checkCustomerCmd = new MySqlCommand(checkCustomerQuery, mySqlConnection))
                    {
                        checkCustomerCmd.Parameters.AddWithValue("@customer_id", customerID);
                        int customerCount = Convert.ToInt32(checkCustomerCmd.ExecuteScalar());

                        if (customerCount > 0)
                        {
                            MessageBox.Show("Duplicate entry! This Customer ID already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Check for duplicate vehicle_id
                    string checkVehicleQuery = "SELECT COUNT(*) FROM bulk_upload WHERE vehicle_id = @vehicle_id";
                    using (MySqlCommand checkVehicleCmd = new MySqlCommand(checkVehicleQuery, mySqlConnection))
                    {
                        checkVehicleCmd.Parameters.AddWithValue("@vehicle_id", vehicleID);
                        int vehicleCount = Convert.ToInt32(checkVehicleCmd.ExecuteScalar());

                        if (vehicleCount > 0)
                        {
                            MessageBox.Show("Duplicate entry! This Vehicle ID already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert data into the database
                    string insertQuery = "INSERT INTO bulk_upload (customer_id, vehicle_id, license_plate, date_arrived, status, upload_date) VALUES (@customer_id, @vehicle_id, @license_plate, @date_arrived, @status, NOW())";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, mySqlConnection))
                    {
                        insertCmd.Parameters.AddWithValue("@customer_id", customerID);
                        insertCmd.Parameters.AddWithValue("@vehicle_id", vehicleID);
                        insertCmd.Parameters.AddWithValue("@license_plate", plateBtn.Text);
                        insertCmd.Parameters.AddWithValue("@date_arrived", arrivedDatePicker.Value.ToString("yyyy-MM-dd"));
                        insertCmd.Parameters.AddWithValue("@status", statusCombox.SelectedItem.ToString());

                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Data successfully added to the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void undobtn_Click(object sender, EventArgs e)
        {
            customerIDbtn.Clear();
            vehicleIDbtn.Clear();
            plateBtn.Clear();
            arrivedDatePicker.Value = DateTime.Now;
            statusCombox.SelectedIndex = -1;
            uploadDatePicker.Value = DateTime.Now;
        }
    }
}
