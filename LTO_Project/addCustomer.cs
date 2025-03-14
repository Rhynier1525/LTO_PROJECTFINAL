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
    public partial class addCustomer : Form
    {
        public addCustomer()
        {
            InitializeComponent();
            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            try
            {
                mySqlConnection.Open();
                MessageBox.Show("Loading...");
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

        private void customerIDbtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void contactBtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstname_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastname_TextChanged(object sender, EventArgs e)
        {

        }

        private void birtdateDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void addresstxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void gendercomBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            // Check if any textbox is empty
            if (string.IsNullOrWhiteSpace(customerIDbtn.Text) ||
                string.IsNullOrWhiteSpace(firstname.Text) ||
                string.IsNullOrWhiteSpace(lastname.Text) ||
                string.IsNullOrWhiteSpace(contactBtn.Text) ||
                string.IsNullOrWhiteSpace(addresstxtbox.Text) ||
                gendercomBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill up all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mysqlCon = "server=127.0.0.1; user=root; database=lto_plate_mgmt_v2; password=";
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
            {
                try
                {
                    mySqlConnection.Open();

                    string query = "INSERT INTO customer (customer_id, first_name, last_name, birthday, gender, address, contact_number) " +
                                   "VALUES (@customer_id, @first_name, @last_name, @birthday, @gender, @address, @contact_number)";

                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@customer_id", customerIDbtn.Text);
                        cmd.Parameters.AddWithValue("@first_name", firstname.Text);
                        cmd.Parameters.AddWithValue("@last_name", lastname.Text);
                        cmd.Parameters.AddWithValue("@birthday", birtdateDatePicker.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", gendercomBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@address", addresstxtbox.Text);
                        cmd.Parameters.AddWithValue("@contact_number", contactBtn.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear text fields after adding
                        ClearFields();
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
            // Clear text fields when Cancel button is clicked
            ClearFields();
        }

        private void ClearFields()
        {
            customerIDbtn.Clear();
            firstname.Clear();
            lastname.Clear();
            contactBtn.Clear();
            addresstxtbox.Clear();
            gendercomBox.SelectedIndex = -1;
            birtdateDatePicker.Value = DateTime.Now;
        }
    }
}
