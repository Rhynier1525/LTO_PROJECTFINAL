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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            UpdateButtonCounts();
            LoadRecentRecords();
        }

        private int GetRecordCount(string tableName)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            int count = 0;

            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = $"SELECT COUNT(*) FROM {tableName}";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return count;
        }

        private void UpdateButtonCounts()
        {
            addedcustomer.Text = $"{GetRecordCount("customer")} Customers";
            addedplates.Text = $"{GetRecordCount("bulk_upload")} Added Plates";
            admins.Text = $"{GetRecordCount("admin")} Admins";
        }

        private void LoadRecentRecords()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();

                    string query = @"
                        SELECT vehicle_id, status, date_arrived, upload_date
                        FROM bulk_upload
                        WHERE DATE(upload_date) = CURDATE()
                        ORDER BY upload_date DESC
                        LIMIT 10";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        overviewDatagrid.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading recent records: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            UpdateButtonCounts();
            LoadRecentRecords();
        }

        private void claimstatBtn_Click(object sender, EventArgs e)
        {
            ClaimStatus claim = new ClaimStatus();
            claim.ShowDialog();
        }

        private void platelookBtn_Click(object sender, EventArgs e)
        {
            PlateLookup plate = new PlateLookup();
            plate.ShowDialog();
        }

        private void stockmngmntBtn_Click(object sender, EventArgs e)
        {
            StockManagement stock = new StockManagement();
            stock.ShowDialog();
        }

        private void bulkUploadBtn_Click(object sender, EventArgs e)
        {
            BulkUpload bulk = new BulkUpload();
            bulk.ShowDialog();
        }

        private void notificationBtn_Click(object sender, EventArgs e)
        {
            Notification notif = new Notification();
            notif.ShowDialog();
        }

        private void addedcustomer_Click(object sender, EventArgs e) => UpdateButtonCounts();

        private void addedplates_Click(object sender, EventArgs e) => UpdateButtonCounts();

        private void admins_Click(object sender, EventArgs e) => UpdateButtonCounts();

        private void overviewDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}