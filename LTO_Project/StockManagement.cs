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
    public partial class StockManagement : Form
    {
        public StockManagement()
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            BulkUpload bulk = new BulkUpload();
            bulk.ShowDialog();
            LoadStockData();
        }

        private void stocksDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadStockData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = "SELECT customer_id AS 'Customer ID', vehicle_id AS 'Vehicle ID', license_plate AS 'License Plate', date_arrived AS 'Date Arrived', status AS 'Status', upload_date AS 'Upload Date' FROM bulk_upload";
                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        stocksDataGrid.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            LoadStockData();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (stocksDataGrid.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    string customerID = stocksDataGrid.SelectedRows[0].Cells["Customer ID"].Value.ToString();
                    string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
                    using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
                    {
                        try
                        {
                            mySqlConnection.Open();
                            string query = "DELETE FROM bulk_upload WHERE customer_id = @customer_id";
                            using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                            {
                                cmd.Parameters.AddWithValue("@customer_id", customerID);
                                cmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("Record deleted successfully.");
                            LoadStockData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
