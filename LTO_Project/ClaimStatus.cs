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
    public partial class ClaimStatus : Form
    {
        public ClaimStatus()
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pendingRefresh_Click(object sender, EventArgs e)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = "SELECT customer_id AS 'Customer ID', vehicle_id AS 'Vehicle ID', date_arrived AS 'Date Arrived', status AS 'Status' " +
                                   "FROM bulk_upload WHERE status = 'Pending'";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        pendingDatagrid.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void completedRefresh_Click(object sender, EventArgs e)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = "SELECT customer_id AS 'Customer ID', vehicle_id AS 'Vehicle ID', date_arrived AS 'Date Arrived', status AS 'Status' " +
                                   "FROM bulk_upload WHERE status = 'Claimed'";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        completedDataGrid.DataSource = dt; // Ensure this DataGridView exists in the form
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pendingDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void completedDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
