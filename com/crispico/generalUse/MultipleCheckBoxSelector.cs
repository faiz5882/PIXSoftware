using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    public partial class MultipleCheckBoxSelector : Form
    {
        List<string> entitiesCodes = new List<string>();
        List<string> receivedSelectedEntityCodes = new List<string>();

        public List<string> selectedEntityCodes = new List<string>();

        public MultipleCheckBoxSelector(string entityType, List<string> _entitiesCodes,
            List<string> _receivedSelectedEntityCodes)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            this.Text = "Please select the " + entityType + "(s)";
            allCheckBox.Text = "All " + entityType + "s";

            entitiesCodes = _entitiesCodes;
            receivedSelectedEntityCodes = _receivedSelectedEntityCodes;

            fillEntitiesPanel(entitiesCodes);
        }

        private void fillEntitiesPanel(List<string> entitiesCodes)
        {
            GroupBox gb = new GroupBox();
            gb.Text = "";
            
            int checkBoxHeight = 17;
            int gbTopAndBottomPaddingSum = 50;
            int gbHeight = entitiesCodes.Count * checkBoxHeight + gbTopAndBottomPaddingSum;            
            Size gbSize = new Size(entitiesPanel.Size.Width - 18, gbHeight);
            gb.Size = gbSize;

            entitiesPanel.Controls.Add(gb);

            int cbLocationX = 6;
            int cbLocationY = 19;
            int cbLocationYOffset = 23;
            for (int i = 0; i <= entitiesCodes.Count -1; i++)
            {
                string code = entitiesCodes[i];
                CheckBox cb = new CheckBox();
                cb.Text = code;
                cb.Checked = receivedSelectedEntityCodes.Contains(code);
                cb.CheckedChanged += entityCheckBox_CheckedChanged;
                gb.Controls.Add(cb);

                Point cbLocation = new Point(cbLocationX, cbLocationY);
                cb.Location = cbLocation;

                cbLocationY = cbLocationY + cbLocationYOffset;
            }
            allCheckBox.Checked = receivedSelectedEntityCodes.Count == entitiesCodes.Count;
        }

        private void allCheckBox_Click(object sender, EventArgs e)
        {
            setEntitiesCheckBoxState(allCheckBox.Checked);
        }

        private void setEntitiesCheckBoxState(bool isChecked)
        {
            foreach (Control control in entitiesPanel.Controls)
            {
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control gbControl in gb.Controls)
                    {
                        if (gbControl is CheckBox)
                            ((CheckBox)gbControl).Checked = isChecked;
                    }
                }
            }
        }

        private void entityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            allCheckBox.Checked = areAllEntitiesCheckBoxesChecked();
        }

        private bool areAllEntitiesCheckBoxesChecked()
        {
            foreach (Control control in entitiesPanel.Controls)
            {
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control gbControl in gb.Controls)
                    {
                        if (gbControl is CheckBox
                            && !((CheckBox)gbControl).Checked)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            selectedEntityCodes = getSelectedEntityCodes();
        }

        private List<string> getSelectedEntityCodes()
        {
            List<string> selectedEntityCodes = new List<string>();
            foreach (Control control in entitiesPanel.Controls)
            {
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control gbControl in gb.Controls)
                    {
                        if (gbControl is CheckBox
                            && ((CheckBox)gbControl).Checked)
                        {
                            selectedEntityCodes.Add(((CheckBox)gbControl).Text);
                        }
                    }
                }
            }
            return selectedEntityCodes;
        }


    }
}
