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
    public partial class PlateLookup : Form
    {
        public PlateLookup()
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

        private void searchtextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchValue = searchtextbox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();

                    // Detect if the search term is numeric (for exact match on IDs)
                    bool isNumeric = long.TryParse(searchValue, out _);

                    string query = "SELECT customer_id AS 'Customer ID', vehicle_id AS 'Vehicle ID', " +
                                   "license_plate AS 'License Plate', date_arrived AS 'Date Arrived', " +
                                   "status AS 'Status', upload_date AS 'Upload Date' " +
                                   "FROM bulk_upload " +
                                   "WHERE " +
                                   (isNumeric ? "customer_id = @search OR vehicle_id = @search " : "license_plate LIKE @search");

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        if (isNumeric)
                        {
                            cmd.Parameters.AddWithValue("@search", searchValue); // Exact match for IDs
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%"); // Partial match for license plate
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        searchDataGrid.DataSource = dt;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
