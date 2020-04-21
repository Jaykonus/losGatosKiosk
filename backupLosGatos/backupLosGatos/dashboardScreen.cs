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
    public partial class dashboardScreen : Form
    {
        public dashboardScreen()
        {
            InitializeComponent();
        }

        SqlConnection conn = null;
        SqlDataReader reader = null;

        private void dashboardScreen_Load(object sender, EventArgs e)
        {
            //populates database information upon loading dashboard
            //working rj 04/15 11:03am
            this.equipmentTableAdapter.Fill(this.gROUP6DataSet.Equipment);
            this.usersTableAdapter.Fill(this.gROUP6DataSet.Users);
            this.ticketsTableAdapter.Fill(this.gROUP6DataSet.Tickets);
        }

        private void ticketPage_Click(object sender, EventArgs e)
        {
            //opens ticketDetails page when ribbon link is clicked
            //working rj 04/15 11:03am
            ticketDetails newTicket = new ticketDetails();

            if (labelRole.Text == "coordinator")
            {
                //disables "update" and "save changes" button for coordinator
                //working rj 04/15 11:03am
                newTicket.updateButton.Enabled = false;
                newTicket.saveButton.Enabled = false;
            }

            this.Hide();
            newTicket.Show();
        }

        private void dashboardGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //generates ticketDetails window populated with database information relevant to datagridview cell selected
            //working rj 04/15 11:03am
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

        private void statusOption_MouseClick(object sender, MouseEventArgs e)
        {
            //displays different statuses from which to filter by when dropdown is clicked
            //working rj 04/15 11:03am
            conn = new
            SqlConnection(@"Data Source = 10.135.85.184; Initial Catalog = GROUP6; Persist Security Info = True; User ID = Group6; Password = Grp6s2117; MultipleActiveResultSets=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT DISTINCT status FROM dbo.Tickets", conn);
            reader = cmd.ExecuteReader();

            DataSet dsstatus = new DataSet();
            DataTable dtstatus = new DataTable("Table1");
            dsstatus.Tables.Add(dtstatus);
            dsstatus.Load(reader, LoadOption.PreserveChanges, dsstatus.Tables[0]);

            statusOption.ValueMember = "status";
            statusOption.DisplayMember = "status";
            statusOption.DataSource = dsstatus.Tables[0];
            statusOption.SelectedIndex = 0;
            statusOption.SelectedValue = 0;
        }

        private void statusOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (statusOption.SelectedValue == null)
            {
            }
            else
            {
                //when a status is selected, datagridview filters information appropriately
                //working rj 04/15 11:03am
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tickets WHERE status = @status", conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@status";
                param.Value = statusOption.SelectedValue.ToString();
                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable("Table1");
                ds.Tables.Add(dt);
                ds.Load(reader, LoadOption.PreserveChanges, ds.Tables[0]);

                dashboardGrid.DataSource = ds.Tables[0];
            }
        }

        private void technicianOption_MouseClick(object sender, MouseEventArgs e)
        {
            //displays different technicians from which to filter by when dropdown is clicked
            //not working - displays shayne's username twice
            conn = new
            SqlConnection(@"Data Source = 10.135.85.184; Initial Catalog = GROUP6; Persist Security Info = True; User ID = Group6; Password = Grp6s2117; MultipleActiveResultSets=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Users.username, Tickets.ticketID FROM Assignments INNER JOIN Tickets ON Assignments.ticketID = Tickets.ticketID INNER JOIN Users ON Assignments.associateID = Users.associateID", conn);
            reader = cmd.ExecuteReader();

            DataSet dstechnician = new DataSet();
            DataTable dttechnician = new DataTable("Table1");
            dstechnician.Tables.Add(dttechnician);
            dstechnician.Load(reader, LoadOption.PreserveChanges, dstechnician.Tables[0]);

            technicianOption.ValueMember = "associateID";
            technicianOption.DisplayMember = "username";
            technicianOption.DataSource = dstechnician.Tables[0];
            technicianOption.SelectedIndex = 0;
            technicianOption.SelectedValue = 0;
        }

        private void technicianOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (technicianOption.SelectedValue == null)
            {
            }
            else
            {
                //when a technician is selected, datagridview filters information appropriately
                //not working - original datagridview is cleared but no information populates.  information could be missing from the database or the select statement might not be gathering the correct data.  the logic of the query: select username from the users table --> traces associateID assigned to username to assignments table --> selects ticketID matched to associateID.  maybe querying the tickets table is unnecessary?  look at database
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username = @username", conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@username";
                param.Value = technicianOption.SelectedValue.ToString();
                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable("Table1");
                ds.Tables.Add(dt);
                ds.Load(reader, LoadOption.PreserveChanges, ds.Tables[0]);

                dashboardGrid.DataSource = ds.Tables[0];
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dashboardGrid.Refresh();
        }
    }
}
