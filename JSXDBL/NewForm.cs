using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSXDBL
{
    public partial class NewForm : Form
    {
        public NewForm()
        {
            InitializeComponent();

            comboBoxMake.Items.Add("Audi");
            comboBoxMake.Items.Add("BMW");
            comboBoxMake.Items.Add("Mercedes-Benz");
            comboBoxMake.SelectedIndex = 0;

            comboBoxFuel.Items.Add("Gasoline");
            comboBoxFuel.Items.Add("Diesel");
            comboBoxFuel.Items.Add("Electric");
            comboBoxFuel.SelectedIndex = 0;

            comboBoxGear.Items.Add("Automatic");
            comboBoxGear.Items.Add("Manual");
            comboBoxGear.SelectedIndex = 0;

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren() == true)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to leave this form?", "Form Leaving Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }          
        }

        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxName.Text.Length < 3)
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxName, "First name your car\nAt least 3 characters");
                buttonOk.Enabled = false;
            }
        }

        private void textBoxName_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxName, string.Empty);
            buttonOk.Enabled = true;

        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void textBoxConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxConfirm.Text.ToLower() != "yes")
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxConfirm, "Text doesnt match");
            }
        }

        private void textBoxConfirm_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxConfirm, string.Empty);
        }
    }
}
