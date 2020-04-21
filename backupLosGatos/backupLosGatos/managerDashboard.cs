using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace backupLosGatos
{
    public partial class managerDashboard : Form
    {
        public managerDashboard()
        {
            InitializeComponent();
        }


        SqlConnection conn = null;
        SqlDataReader reader = null;

        private void managerDashboard_Load(object sender, EventArgs e)
        {
            //this.usersTableAdapter.Fill(this.gROUP6DataSet.Users);
            //this.ticketsTableAdapter.Fill(this.gROUP6DataSet.Tickets);

            conn = new
            SqlConnection(@"Data Source = 10.135.85.184; Initial Catalog = GROUP6; Persist Security Info = True; User ID = Group6; Password = Grp6s2117; MultipleActiveResultSets=true");

            conn.Open();

            // 1. declare a SQL Command. Select a Unique Set of Statuses from the Tickets Table. 
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT status FROM dbo.Tickets", conn);

            // 2. get a data stream set up
            reader = cmd.ExecuteReader();

            // 3. Get a Data Set called Status
            DataSet dsstatus = new DataSet();

            // 4. Create Data Table from the Data Set to hold query results
            DataTable dtstatus = new DataTable("Table1");
            dsstatus.Tables.Add(dtstatus);

            // 5. Load up the Query result
            dsstatus.Load(reader, LoadOption.PreserveChanges, dsstatus.Tables[0]);

            // 6. Put the retrieved values into the ComboBox
            statusOption.ValueMember = "status";
            statusOption.DisplayMember = "status";
            statusOption.DataSource = dsstatus.Tables[0];
            statusOption.SelectedIndex = 0;
            statusOption.SelectedValue = 0;

            // Lab 7 screen load code ends here

            this.ActiveControl = technicianOption;

        }

        private void ticketPage_Click(object sender, EventArgs e)
        {
            //disables buttons for managers - is this necessary?
            ticketDetails newTicket = new ticketDetails();

            //if (labelRole.Text == "manager")
            //{
            //    newTicket.updateButton.Enabled = false;
            //    newTicket.saveButton.Enabled = false;
            //}

            newTicket.Show();
            this.Close();
        }

        private void dashboardGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //generates ticketDetails window populated with database information relevant to datagridview cell selected
            ticketDetails viewTicket = new ticketDetails();
            viewTicket.ticketIDTextBox.Text = this.dashboardGrid.CurrentRow.Cells[0].Value.ToString();
            viewTicket.unitIDTextBox.Text = this.dashboardGrid.CurrentRow.Cells[1].Value.ToString();
            viewTicket.equipmentIDTextBox.Text = this.dashboardGrid.CurrentRow.Cells[2].Value.ToString();
            viewTicket.dateSubmittedDateTimePicker.Text = this.dashboardGrid.CurrentRow.Cells[3].Value.ToString();
            viewTicket.priorityComboBox.Text = this.dashboardGrid.CurrentRow.Cells[4].Value.ToString();
            viewTicket.statusComboBox.Text = this.dashboardGrid.CurrentRow.Cells[5].Value.ToString();
            viewTicket.welderSignatureTextBox.Text = this.dashboardGrid.CurrentRow.Cells[6].Value.ToString();
            viewTicket.inspectorSignatureTextBox.Text = this.dashboardGrid.CurrentRow.Cells[7].Value.ToString();
            viewTicket.additionalInformationTextBox.Text = this.dashboardGrid.CurrentRow.Cells[8].Value.ToString();

            viewTicket.Show();
            this.Close();
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // start code from lab 7 regarding drop down

            // 7. First time this runs, it will be null so do not send a parameter query
            if (statusOption.SelectedValue == null)
            {
            }

            else
            {
                // 9. declare command object with parameter
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tickets WHERE status = @status", conn);

                // 10. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@status";

                // 11. Get value to populate parameter from combo box selection
                param.Value = statusOption.SelectedValue.ToString();

                // 12. add new parameter to command object
                cmd.Parameters.Add(param);

                // 13. Execute the command
                reader = cmd.ExecuteReader();

                // 14. Get the new data set and load it as a table
                DataSet ds = new DataSet();
                DataTable dt = new DataTable("Table1");
                ds.Tables.Add(dt);
                ds.Load(reader, LoadOption.PreserveChanges, ds.Tables[0]);

                dashboardGrid.DataSource = ds.Tables[0];

            }
        }

        private void ticketPage_Click_1(object sender, EventArgs e)
        {
            ticketDetails newTicket = new ticketDetails();
            newTicket.Show();
            this.Close();
        }

       

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            dashboardGrid.Refresh();
        }
    }
}
