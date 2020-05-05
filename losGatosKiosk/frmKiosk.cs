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
using System.Data.OleDb;


namespace losGatosKiosk
{
    public partial class frmKiosk : Form
    {
        //Committed 3:30p 4/14

        public frmKiosk()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEquipment.Enabled = true;
            cboEquipment.Items.Clear();
            cboEquipment.DropDownStyle = ComboBoxStyle.DropDownList;

            if (cboUnit.SelectedItem == "Division 1")
            {
                cboEquipment.Items.Add(1);
                cboEquipment.Items.Add(2);
                cboEquipment.Items.Add(3);
                cboEquipment.Items.Add(4);
                cboEquipment.Items.Add(5);
            }
            else if (cboUnit.SelectedItem == "Division 2")
            {
                cboEquipment.Items.Add(6);
                cboEquipment.Items.Add(7);
                cboEquipment.Items.Add(8);
                cboEquipment.Items.Add(9);
                cboEquipment.Items.Add(10);
            }
            else if (cboUnit.SelectedItem == "Division 3")
            {
                cboEquipment.Items.Add(11);
                cboEquipment.Items.Add(12);
                cboEquipment.Items.Add(13);
                cboEquipment.Items.Add(14);
                cboEquipment.Items.Add(15);
            }
            else if (cboUnit.SelectedItem == "Maintenance")
            {
                cboEquipment.Items.Add(16);
                cboEquipment.Items.Add(17);
                cboEquipment.Items.Add(18);
            }
        }

        private void cboEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPriority.Enabled = true;
        }

        private void cboPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEmployee.Enabled = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit ticket?", "Ticket Submission", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string connectionString = @"Data Source = 10.135.85.184; Initial Catalog = GROUP6; Persist Security Info = True; User ID = Group6; Password = Grp6s2117; MultipleActiveResultSets=true";
                SqlConnection con = new SqlConnection(connectionString);
                DateTime thisDay = DateTime.Today;
                int numID = 0;

                //pull number of rows from database in order to create ticketID


                /* string stmt = "SELECT COUNT(*) FROM dbo.tablename";
                 using (SqlConnection (con))
                 {
                     using (SqlCommand cmdCount = new SqlCommand(stmt, thisConnection))
                     {
                         thisConnection.Open();
                         numID = (int)cmdCount.ExecuteScalar();
                     }
                 }
                 return numID;
                 //thisConnection.Close();*/


                //create strings for all variables
                numID = numID + 1;
                DateTime date = DateTime.Today;
                string unitID = cboUnit.Text.ToString();
                string equipmentID = cboEquipment.Text.ToString();
                string priorityLevel = cboPriority.Text.ToString();
                string employeeName = txtEmployee.Text.ToString();
                string addInfo = txtInfo.Text.ToString();


                
                    using (SqlConnection openCon = new SqlConnection(connectionString))
                    {
                        string injection = "INSERT into Kiosk (ticketID, unitID, equipmentID, priorityLevel, employeeName, addInfo, date) VALUES (@ticketID,@unitID,@equipmentID,@priorityLevel, @employeeName, @addInfo, @date)";

                        using (SqlCommand command = new SqlCommand(injection))
                        {
                            command.Connection = openCon;
                            command.Parameters.Add("@ticketID", SqlDbType.NVarChar, 50).Value = numID;
                            command.Parameters.Add("@unitID", SqlDbType.NChar, 10).Value = unitID;
                            command.Parameters.Add("@equipmentID", SqlDbType.NChar, 10).Value = equipmentID;
                            command.Parameters.Add("@priorityLevel", SqlDbType.NChar, 10).Value = priorityLevel;
                            command.Parameters.Add("@employeeName", SqlDbType.NVarChar, 50).Value = employeeName;
                            command.Parameters.Add("@addInfo", SqlDbType.NVarChar, 1000).Value = addInfo;
                            command.Parameters.Add("@date", SqlDbType.Date, 30).Value = date;

                            openCon.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Entry Saved");
                



             
            }
            //reset app
            else if (dialogResult == DialogResult.No)
            {
                cboUnit.SelectedIndex = -1;
                cboPriority.SelectedIndex = -1;
                cboPriority.Enabled = false;
                cboEquipment.Enabled = false;
                txtInfo.Text = "";
                txtEmployee.Text = "";
                cboUnit.Select();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            cboUnit.SelectedIndex = -1;
            cboPriority.SelectedIndex = -1;
            cboPriority.Enabled = false;
            cboEquipment.Enabled = false;
            txtInfo.Text = "";
            txtEmployee.Text = "";
            cboUnit.Select();

        }

        private void txtEmployee_TextChanged(object sender, EventArgs e)
        {
            if(txtEmployee.Text.Trim().Length > 0)
            {
                btnSubmit.Enabled = true;
            }
            else if(txtEmployee.Text.Trim().Length == 0)
            {
                btnSubmit.Enabled = false;
            }
        }

        private void frmKiosk_Load(object sender, EventArgs e)
        {
            cboUnit.Select();
        }
    }
}
