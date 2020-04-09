using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace losGatosKiosk
{
    public partial class frmKiosk : Form
    {
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
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            cboUnit.SelectedIndex = -1;
            //cboEquipment.Items[cboEquipment.SelectedIndex] = "";
            cboPriority.SelectedIndex = -1;
            cboPriority.Enabled = false;
            cboEquipment.Enabled = false;
            txtInfo.Text = "";
            txtEmployee.Text = "";
           
            
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
    }
}
